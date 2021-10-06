using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Core
{
    public class Transaction
    {
        public int TransactionID { get; set; }
        public int AccountID { get; set; }
        public virtual Account Account { get; set; }

        public DateTime Date { get; set; }
        public double Amount { get; set; }
    }
}
