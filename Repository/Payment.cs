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


