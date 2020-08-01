using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payment.Models
{
    public class PaymentDetails
    {
        public int CurrentBalance { get; set; }

        public string Message { get; set; }

    }
}
