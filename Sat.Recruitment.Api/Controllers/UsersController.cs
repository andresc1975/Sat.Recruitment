using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Api.Controllers.Dtos;
using Sat.Recruitment.Api.Domain;
using Sat.Recruitment.Api.Services;
using Sat.Recruitment.Api.Validators;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Sat.Recruitment.Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserServices _userServices;
        private readonly IMapper _mapper;

        public UsersController(IUserServices userServices, IMapper mapper)
        {
            _userServices = userServices;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("/create-user")]
        public async Task<OperationResult> CreateUser(UserRequest userRequest)
        {
            var response = new OperationResult();

            try
            {
                var newUser = _mapper.Map<User>(userRequest);

                var validator = new UserValidator();
                var validationRequest = validator.Validate(newUser);

                if (!validationRequest.IsValid)
                {
                    response.IsSuccess = false;
                    response.Errors = new StringBuilder().AppendJoin(" - ", validationRequest.Errors?.Select(e => e.ErrorMessage).ToArray()).ToString();
                }
                else
                {
                    _userServices.CreateUser(newUser);
                    response.IsSuccess = true;
                    response.Errors = "User Created";
                }
                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Errors = ex.Message;
            }

            return response;

        }

    }
}
