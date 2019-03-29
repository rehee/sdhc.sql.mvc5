using SDHC.Payment.Stripe;
using ServiceStack;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
  public static class StripePaymentService
  {
    public static void CreatePayment(string stripePrivateKey, ISDHCStripe model, out ResponseStatus status, out string response)
    {
      try
      {
        StripeConfiguration.SetApiKey(stripePrivateKey);
        var token = model.stripeToken;
        var amount = Convert.ToInt64(model.Amount * 100);
        var options = new ChargeCreateOptions()
        {
          Amount = amount,
          Currency = model.Currency,
          Description = model.Describe,
          SourceId = token
        };
        var service = new ChargeService();
        Charge charge = service.Create(options);
        response = charge.StripeResponse.ResponseJson;
        status = charge.GetResponseStatus();
      }
      catch (Exception ex)
      {
        response = null;
        status = new ResponseStatus()
        {
          Message = ex.Message,
        };
        Console.WriteLine(ex.Message);
      }

    }

  }
}
