using Sat.Recruitment.Api.Domain;
using System;

namespace Sat.Recruitment.Api.Helpers
{
    public class UserTypeConverter
    {
        public static UserTypeEnum UserTypeFromString(string userType)
        {
            return String.IsNullOrEmpty(userType) ? UserTypeEnum.Normal : (UserTypeEnum) Enum.Parse(typeof(UserTypeEnum), userType);
        }
    }
}
