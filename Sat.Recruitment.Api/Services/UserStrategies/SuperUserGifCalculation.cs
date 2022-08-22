using System;

namespace Sat.Recruitment.Api.Services.UserStrategies
{
    public class SuperUserGifCalculation : IUserGifCalculation
    {
        public decimal CalculateGif(decimal money)
        {
            decimal percentage = 0;

            if (money > 100)
            {
                percentage = Convert.ToDecimal(0.20);
            }
            return money * percentage;
        }

    }
}
