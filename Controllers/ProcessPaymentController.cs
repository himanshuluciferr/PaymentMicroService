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
using Microsoft.AspNetCore.SignalR.Protocol;
using System.Text;
using System.IO;

namespace Payment.Controllers

{    [Route("api/[controller]")]
    [ApiController]
    public class ProcessPaymentController : ControllerBase
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(ProcessPaymentController));
       /* List<TransactionRecords> trans = new List<TransactionRecords>() {
                                             new TransactionRecords{CreditCardNumber=123, CreditLimit=20000,Count=0 },
                                             new TransactionRecords{CreditCardNumber=122, CreditLimit=30000,Count=0 }

                                             };
       */
        [HttpGet]
        public dynamic ProcessPayment(long CreditCardNumber, int CreditLimit, int ProcessingCharge)
        {

            _log4net.Info("Payment initiated");
            PaymentDetails payment = new PaymentDetails();
            
            payment.CurrentBalance = CreditLimit;               //current balance initiated

            /* var CSVFile = new CsvFileDescription
             {
                 SeparatorChar = ',',
                 FirstLineHasColumnNames = true
             };

             var CSVFile2 = new CsvFileDescription
             {
                 SeparatorChar = '\t',
                 FirstLineHasColumnNames = true, 
                 FileCultureName = "nl-NL"
             };*/
                int Count = 0;
            // var CSV = new CsvContext();
            //var Trans = File.ReadAllLines(@"Linq.csv").Select(x => x.Split(','));
            /* var Trans = from values in CSV.Read<TransactionRecords>( path,CSVFile)
                         select new
                         {
                             CreditCardNumber = values.CreditCardNumber,
                             CreditLimit = values.CreditLimit,
                             Count = values.Count
                         };

             */
            /*var count = Trans.Select(count => count.Count);
            foreach (int c in count)
            {
                Count += c;
            }
            */
            /*var val = from c in trans
                      where c.CreditCardNumber == CreditCardNumber
                      select c.Count;
            */
            /*foreach(int c in val)
            {
                Count += c; 
            }*/
           
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

                    //trans.RemoveAll(card =>card.CreditCardNumber==CreditCardNumber);
                    //TransactionRecords records = new TransactionRecords();
                    //records.CreditCardNumber = CreditCardNumber;
                    //records.CreditLimit = payment.CurrentBalance;
                    //records.Count = 1;
                    //trans.Add(records);

                     //trans.Where(w => w.CreditCardNumber == CreditCardNumber).ToList().ForEach(i => i.Count = 1);
                     //trans.Where(w => w.CreditCardNumber == CreditCardNumber).ToList().ForEach(i => i.CreditLimit = payment.CurrentBalance);
                    
                }

                else
                {
                    payment.Message = "Failed";      // message generated
                }
            }
            return payment;             // returns message & balance amount
        }
    }
    }
