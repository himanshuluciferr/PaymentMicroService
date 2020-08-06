using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using Payment.Models;

namespace Payment.Repository
{
    public class Payment : IPayment
    {
        public dynamic ProcessPayment(CardDetails det)
        {

            PaymentDetails payment = new PaymentDetails();
            payment.CurrentBalance = det.CreditLimit;               //current balance initiated


            if (payment.CurrentBalance >= det.ProcessingCharge)
            {
                payment.CurrentBalance -= det.ProcessingCharge;
                payment.Message = "Successful";
            }

            else
            {
                payment.Message = "Failed";      // message generated
            }

            return payment;   // returns message & balance amount
        }
    }
}


