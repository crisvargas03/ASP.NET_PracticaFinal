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

        /*
         Este es el campo que se utilizara para la cuenta del usuario que envia 
         */
        public string  AccountID { get; set; }

        /*
         Este es el campo relacional de la cuentas de los usuarios
         */
        public string  ReceptAccountID { get; set; }

        /*
         Esta la fecha que se creo la transaccion
         */

        public DateTime Date { get; set; }
        /*
         Esta es la cantidad que se transfiere al usuario receptor
         */
        public double Amount { get; set; }
    }
}
