using AutoMapper;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.Controllers.Dtos;
using Sat.Recruitment.Api.Services;
using Xunit;
using Sat.Recruitment.Api.Repository;
using Sat.Recruitment.Api.Controllers.Mappers;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class TestUserCreation
    {
        private UserRepository _userRepository;
        private UserServices _userServices;
        private IMapper _mapper;

        public TestUserCreation()
        {
            _userRepository = new UserRepository();
            _userServices = new UserServices(_userRepository);

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new UserMapper());
            });
            _mapper = mockMapper.CreateMapper();
        }

        [Fact]
        public void TestCreateNotExistingUser()
        {
            var userController = new UsersController(_userServices, _mapper);
            var user = CreateUserDto("Mike", "mike@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", 124);

            var result = userController.CreateUser(user).Result;

            Assert.Equal(true, result.IsSuccess);
            Assert.Equal("User Created", result.Errors);
        }


        [Fact]
        public void TestCreateNewUserThenTryToDuplcateSameUserFails()
        {
            var userController = new UsersController(_userServices, _mapper);
            var user = CreateUserDto("Mike", "mike@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", 124);

            var result = userController.CreateUser(user).Result;

            Assert.Equal(true, result.IsSuccess);
            Assert.Equal("User Created", result.Errors);

            result = userController.CreateUser(user).Result;
            Assert.Equal(false, result.IsSuccess);
            Assert.Equal("User is duplicated", result.Errors);
        }

        [Fact]
        public void TestCreateUserWithExistingEmailFails()
        {
            var userController = new UsersController(_userServices, _mapper);
            var user = CreateUserDto("Agustina", "Agustina@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", 124);
            var result = userController.CreateUser(user).Result;

            Assert.Equal(false, result.IsSuccess);
            Assert.Equal("User is duplicated", result.Errors);
        }

        private UserRequest CreateUserDto(string name, string email, string address, string phone, string userType, decimal money)
        {
            return new UserRequest()
            {
                Name = name,
                Email = email,
                Address = address,
                Phone = phone,
                UserType = userType,
                Money = money
            };
        }

    }
}
