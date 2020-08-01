using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Payment.Models;
using Microsoft.Extensions.Logging;
using System.Configuration;
using System.Net.Http.Headers;

namespace Payment.Controllers

{    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(PaymentController));

        List<CardDetails> CardDetails = new List<CardDetails>()
        {
            new CardDetails{CardNumber=1099,CardLimit=20000},
            new CardDetails{CardNumber=1089,CardLimit=30000},
            new CardDetails{CardNumber=1011,CardLimit=10000},
            new CardDetails{CardNumber=1032,CardLimit=5000}
        };

        [HttpGet]
        public dynamic GetProcessPayment(int CardNo, int ProcessingCharge)
        {

            _log4net.Info("Payment initiated");
           
            
            PaymentDetails payment = new PaymentDetails();
            int CardLimit = 0;
            var CardLim = CardDetails.Where(Card => Card.CardNumber == CardNo);
            var Limit = CardLim.Select(Limit => Limit.CardLimit);
            foreach(int limit in Limit)
            {
                CardLimit += limit;
            }
            payment.BalanceAmount = CardLimit;

            if (payment.BalanceAmount >= ProcessingCharge)
            {
                payment.BalanceAmount -= ProcessingCharge;
                payment.Message = "Successful";
                CardDetails.Select(c => 
                { 
                    c.CardLimit = payment.BalanceAmount; return c;
                });

            }
            else
            {
                payment.Message = "Failed";      // message generated

            }                                             


            payment.CardDetails = new CardDetails() { CardNumber =CardNo, CardLimit=payment.BalanceAmount};    // card details updated
            

            return payment.Message;             // returns bal amount, message with their card details
            
        }


    }
}
