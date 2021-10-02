using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Core
{
    public class Account
    {
        public int AccountID { get; set; }
        public string NO_Account { get; set; }
        public string Type_Account { get; set; }
        public double Amount { get; set; }
        public DateTime DateCreate { get; set; }

        //TODO:CREATE RELATION WITH CUSTOMER


    }
}
