using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Entities.Concrete;
using Microsoft.Extensions.Configuration;

namespace Business.Concrete
{
    public class PaymentManager : IPaymentService
    {
        private readonly IConfiguration _configuration;

        public PaymentManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IResult Pay(CreditCard creditCard)
        {
            var creditCardDetails = _configuration.GetSection("CreditCardDetails");
            var getCardNumber = creditCardDetails.GetValue<string>("CardNumber");
            var getExpirationDate = creditCardDetails.GetValue<string>("ExpirationDate");
            var getCvv = creditCardDetails.GetValue<int>("Cvv");

            HashingHelper.CreatePasswordHash(getCardNumber, out byte[] cardNumberHash, out byte[] cardNumberSalt);
            HashingHelper.CreatePasswordHash(getExpirationDate, out byte[] expirationDateHash, out byte[] expirationDateSalt);
            HashingHelper.CreatePasswordHash($"{getCvv}", out byte[] cvvHash, out byte[] cvvSalt);

            if (!HashingHelper.VerifyPasswordHash(creditCard.CardNumber, cardNumberHash, cardNumberSalt))
            {
                return new ErrorResult(Messages.PaymentFailed);
            }

            if (!HashingHelper.VerifyPasswordHash(creditCard.ExpirationDate, expirationDateHash, expirationDateSalt))
            {
                return new ErrorResult(Messages.PaymentFailed);
            }

            if (!HashingHelper.VerifyPasswordHash($"{creditCard.Cvv}", cvvHash, cvvSalt))
            {
                return new ErrorResult(Messages.PaymentFailed);
            }

            return new SuccessResult(Messages.PaymentSuccess);
        }
    }
}
