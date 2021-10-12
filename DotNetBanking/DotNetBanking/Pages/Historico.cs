using Banking.Core;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotNetBanking.Pages
{
    public class HistoricoViewModel : PageModel
    {
        private readonly Banking.Data.ApplicationDbContext _context;

        public HistoricoViewModel(Banking.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Transaction> Transaction { get; set; }

        public async Task OnGetAsync()
        {
            Transaction = await _context.transactions.ToListAsync();
        }
    }
}
