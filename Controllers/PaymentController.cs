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
           // card_limit = 20000;
           // processing_charge = 500;

            PaymentDetails payment = new PaymentDetails();
            
            int bal_amount = card_limit;                    

            bal_amount = bal_amount - processing_charge;    // processing_charge received from component processing microservice
            
            payment.BalAmount = bal_amount;                // balance amount updated

            if( bal_amount>= 0)
            {
                payment.Message = "Successful";
            }
            else
            {
                payment.Message = "Failed";
            }                                              // message generated

            payment.CardDetails = new CardDetails() { Card_No = card_no, Card_Limit =bal_amount  };         // card details updated


            return payment;                               // returns bal amount, message with their card details
            

        }
    }
}
