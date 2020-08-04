using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Payment.Models;


namespace Payment.Repository
{
    public interface IPayment
    {
        public dynamic ProcessPayment(CardDetails det);
    }
}
