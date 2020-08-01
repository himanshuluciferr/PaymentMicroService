using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payment.Models
{
    public class CardDetails
    {
        public long CreditCardNumber{ get; set; }
        public int CreditLimit { get; set; }
        public int ProcessingCharge { get; set; }

    }
}
