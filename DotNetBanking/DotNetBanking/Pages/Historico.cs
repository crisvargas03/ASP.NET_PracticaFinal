using Banking.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetBanking.Pages
{
    public class HistoricoViewModel : PageModel
    {
        //[BindProperty(SupportsGet = true)]
        public DateTime DateSort { get; set; }
        private readonly Banking.Data.ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public HistoricoViewModel(Banking.Data.ApplicationDbContext context, UserManager<Banking.Core.User> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        public IList<Transaction> Transaction { get; set; }

        public async Task OnGetAsync(string filter)
        {
            var UserId = _userManager.GetUserId(HttpContext.User);

            DateSort = Convert.ToDateTime(filter);

            if (filter == null)
            {
                Transaction = await _context.transactions.Where(trans=>trans.AccountID == UserId).ToListAsync();

            }
            else
            {
                Transaction = await _context.transactions.Where(trans => trans.Date.Date == DateSort.Date && trans.AccountID == UserId).ToListAsync();

            }
        }
    }
}