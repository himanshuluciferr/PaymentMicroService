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
using System.Security.Permissions;
using System.Security;
using CsvHelper;
using System.Globalization;

namespace Payment.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessPaymentController : ControllerBase
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(ProcessPaymentController));
        [HttpPost]
        public dynamic ProcessPayment(CardDetails det)
        {

            _log4net.Info("Payment initiated");
            PaymentDetails payment = new PaymentDetails();
            payment.CurrentBalance = det.CreditLimit;               //current balance initiated
            int Count = 0;
            using (var reader = new StreamReader("./TransactionRecord.csv"))       //csv file read
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var count = csv.GetRecords<TransactionRecords>().Where(x => x.CreditCardNumber == det.CreditCardNumber).Select(x => x.Count);
                foreach (int counter in count)
                {
                    Count += counter;
                }
                if (Count == 1)
                {
                    var limit = csv.GetRecords<TransactionRecords>().Where(x => x.CreditCardNumber == det.CreditCardNumber).Select(x => x.CreditLimit);
                    foreach (int Limit in limit)
                    {
                        payment.CurrentBalance = Limit;
                    }
                    payment.Message = "Only one transaction per card allowed";
                    return payment;
                }
                reader.Close();
            }


            if (payment.CurrentBalance >= det.ProcessingCharge)
            {
                payment.CurrentBalance -= det.ProcessingCharge;
                payment.Message = "Successful";
                var records = new List<TransactionRecords>
                {
                    new TransactionRecords { CreditCardNumber = det.CreditCardNumber, CreditLimit = payment.CurrentBalance, Count = 1 }
                };
                using (var writer = new StreamWriter("./TransactionRecord.csv"))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))          
                {
                    csv.WriteRecords(records);                   //csv file written
                    writer.Close();
                }
            }
            else
            {
                payment.Message = "Failed";      // message generated
            }
            return payment;   // returns message & balance amount
        }

    }
}
