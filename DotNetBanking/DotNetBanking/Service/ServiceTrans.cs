using Banking.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Banking.Core;

namespace DotNetBanking.Service
{

    public class ServiceTrans
    {
        private ApplicationDbContext _dbContext;

        public ServiceTrans(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

        }

        public async Task<User> UpdateBalanceEmisor(User user)
        {

            try
            {
                /*
                 Esta parte es para hacer un descuento del balance de emisor de la transaccion
                 */
                double totalBalance = 0;
                var transactions = _dbContext.transactions.Where(_trans => _trans.AccountID == user.AccountId).ToList();
                foreach (var transacion in transactions)
                {
                    totalBalance += transacion.Amount;
                }

                if (user.Balance < totalBalance)
                {
                    Console.WriteLine("NO TIENE DINERO");
                }
                else
                {
                    user.Balance = user.Balance - totalBalance;
                    _dbContext.users.Add(user);
                    await _dbContext.SaveChangesAsync();

                }

            }
            catch (Exception)
            {
                throw;
            }
            return user;
        }

        public async Task<User> UpdateBalanceReceptor(User user)
        {


            try
            {
                /*
                 Esta parte es para hacer un descuento del balance de Recepto de la transaccion
                 */
                double totalBalance = 0;
                var balances = _dbContext.transactions.Where(_trans => _trans.AccountID == user.AccountId).ToList();
                foreach (var balance in balances)
                {
                    totalBalance += balance.Amount;
                }

                user.Balance = user.Balance + totalBalance;
                _dbContext.users.Add(user);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return user;
        }

    }
}
