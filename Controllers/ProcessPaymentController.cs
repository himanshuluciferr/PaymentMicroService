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
using LINQtoCSV;
using System.Threading;

namespace Payment.Controllers

{    [Route("api/[controller]")]
    [ApiController]
    public class ProcessPaymentController : ControllerBase
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(ProcessPaymentController));

        [HttpGet]
        public dynamic ProcessPayment(long CreditCardNumber, int CreditLimit, int ProcessingCharge)
        {

            _log4net.Info("Payment initiated");
            PaymentDetails payment = new PaymentDetails();
            payment.CurrentBalance = CreditLimit;               //current balance initiated

            var CSVFile = new CsvFileDescription
            {
                SeparatorChar = ',',
                FirstLineHasColumnNames = true
            };
            int Count = 0;
            var CSV = new CsvContext();
            var Trans = from values in CSV.Read<TransactionRecords>(@"./TransactionRecord.csv", CSVFile)
                        select new
                        {
                            CreditCardNumber = values.CreditCardNumber,
                            CreditLimit = values.CreditLimit,
                            Count = values.Count
                        };

            var count = Trans.Select(count => count.Count);
            foreach (int c in count)
            {
                Count += c;
            }
            
                if (Count > 0)
                {
                    payment.Message = "one transaction for one card";
                }
                else
                {
                    if (payment.CurrentBalance >= ProcessingCharge)
                    {
                        payment.CurrentBalance -= ProcessingCharge;
                        payment.Message = "Successful";
                        Count += 1;
                    }

                    else
                    {
                        payment.Message = "Failed";      // message generated
                    }
                }
             foreach(int c in count)
            {

            }
                return payment;             // returns message & balance amount

            
            }


        }
    }
