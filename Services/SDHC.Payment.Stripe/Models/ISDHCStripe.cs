using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDHC.Payment.Stripe
{
  public interface ISDHCStripe
  {
    string Describe { get; set; }
    string Currency { get; set; }
    decimal Amount { get; set; }

    string stripeToken { get; set; }
    string stripeTokenType { get; set; }
    string stripeEmail { get; set; }
  }
}
