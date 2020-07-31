using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Payment.Models;
using Microsoft.Extensions.Logging;

namespace Payment.Controllers

{    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(PaymentController));

        [HttpGet]
        public dynamic GetProcessPayment(int card_no, int card_limit, int processing_charge)
        {

            _log4net.Info("Payment initiated");

            PaymentDetails payment = new PaymentDetails();
            
            int bal_amount = card_limit;

            bal_amount = bal_amount - processing_charge;

            if( bal_amount>= 0)
            {
                payment.Message = "Successful";
            }
            else
            {
                payment.Message = "Failed";
            }

            payment.CardDetails = new CardDetails() { Card_No = card_no, Card_Limit =bal_amount  };


            return payment;


        }
    }
}
