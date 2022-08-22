using Sat.Recruitment.Api.Domain;
using Sat.Recruitment.Api.Repository;
using Sat.Recruitment.Api.Services.UserStrategies;
using System;

namespace Sat.Recruitment.Api.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository _userRepository;

        public UserServices(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        } 

        public void CreateUser(User user)
        {
            user.Email = NormalizeEmail(user.Email);

            if (ExistsUser(user))
            {
                throw new Exception("User is duplicated");
            }
            else
            {
                user.Money += CalculateGifFor(user);
                _userRepository.AddUser(user);
            }
        }


        private bool ExistsUser(User user)
        {
            return _userRepository.GetAllUsers().Exists(x => x.Email == user.Email || x.Phone == user.Phone || (x.Name == user.Name && x.Address == user.Address));
        }

        private decimal CalculateGifFor(User newUser)
        {
            var gifCalculationFactory = new UserGifCalculationFactory();
            var gifCalculation = gifCalculationFactory.GetGifCalculationBasedOnUserType(newUser.UserType);
            return gifCalculation.CalculateGif(newUser.Money);
        }

        private string NormalizeEmail(string originalEmail)
        {
            var aux = originalEmail.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);
            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);
            aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);

            return string.Join("@", new string[] { aux[0], aux[1] });
        }

    }
}
