using System;

namespace Sat.Recruitment.Api.Services.UserStrategies
{
    public class NormalUserGifCalculation : IUserGifCalculation
    {
        public decimal CalculateGif(decimal money)
        {
            decimal percentage = 0;

            if (money > 100)
            {
                percentage = Convert.ToDecimal(0.12);
            } else if (money > 10 && money <= 100) //Assuming there was a bug in original code that didn't calculate correctly gif for money = 100
            {
                percentage = Convert.ToDecimal(0.8);
            }
            return (money * percentage);
        }

    }
}
