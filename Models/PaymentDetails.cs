using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payment.Models
{
    public class PaymentDetails
    {
        public int BalAmount { get; set; }

        public string Message { get; set; }

        public CardDetails CardDetails { get; set;}
    }
}
