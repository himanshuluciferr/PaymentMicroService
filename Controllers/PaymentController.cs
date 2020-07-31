using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Payment.Model;

namespace Payment.Controllers

{    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        [HttpGet]
        public dynamic GetProcessPayment(int card_no, int card_limit, int processing_charge)
        {
            PaymentDetails payment = new PaymentDetails();

            int bal_amount = card_limit;

            bal_amount = bal_amount - processing_charge;

            return payment;


        }
    }
}
