using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDHC.Payment.Stripe
{
  public class StripeItem
  {
    public long amount { get; set; }
    public string currency { get; set; }
    public string description { get; set; }
    public string parent { get; set; }
    public long? quantity { get; set; }
    public string type { get; set; }
  }

  public class StripeChargeResponse
  {
    public string id { get; set; }
    public long? amount { get; set; }
    public long? amount_refunded { get; set; }
    public string application { get; set; }
    public long? application_fee { get; set; }
    public long? application_fee_amount { get; set; }
    public string balance_transaction { get; set; }
    public bool captured { get; set; }
    public long? charge { get; set; }
    public long? created { get; set; }
    public string currency { get; set; }
    public string email { get; set; }
    public bool livemode { get; set; }
    public string status { get; set; }
    public string description { get; set; }
    public string receipt_url { get; set; }
    public bool refunded { get; set; }
}
}

