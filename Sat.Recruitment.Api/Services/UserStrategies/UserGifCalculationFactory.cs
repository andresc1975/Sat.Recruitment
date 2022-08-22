using Sat.Recruitment.Api.Domain;
using System;
using System.Collections.Generic;

namespace Sat.Recruitment.Api.Services.UserStrategies
{
    public class UserGifCalculationFactory
    {
        private Dictionary<UserTypeEnum, Func<IUserGifCalculation>> _userTypeMapper;

        public UserGifCalculationFactory()
        {
            _userTypeMapper = new Dictionary<UserTypeEnum, Func<IUserGifCalculation>>();
            _userTypeMapper.Add(UserTypeEnum.Normal, () => { return new NormalUserGifCalculation(); });
            _userTypeMapper.Add(UserTypeEnum.SuperUser, () => { return new SuperUserGifCalculation(); });
            _userTypeMapper.Add(UserTypeEnum.Premium, () => { return new PremiumUserGifCalculation(); });
        }

        public IUserGifCalculation GetGifCalculationBasedOnUserType(UserTypeEnum userType)
        {
            return _userTypeMapper[userType]();
        }

    }
}
