namespace Sat.Recruitment.Api.Services.UserStrategies
{
    public class PremiumUserGifCalculation : IUserGifCalculation
    {
        public decimal CalculateGif(decimal money)
        {
            if (money > 100)
            {
                return money * 2;
            }
            else
            {
                return 0;
            }
        }

    }
}
