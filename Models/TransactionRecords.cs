using LINQtoCSV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payment.Models
{
    public class TransactionRecords
    {
        [CsvColumn(FieldIndex = 1)]
        public long CreditCardNumber { get; set; }

        [CsvColumn(FieldIndex = 2)]
        public int CreditLimit { get; set; }

        [CsvColumn(FieldIndex = 3)]
        public int Count { get; set; }
    }
}
