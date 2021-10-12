//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using Microsoft.EntityFrameworkCore;
//using Banking.Core;
//using Banking.Data;

//namespace DotNetBanking.Pages
//{
//    public class TransacionViewModel : PageModel
//    {
//        private readonly Banking.Data.ApplicationDbContext _context;

//        public TransacionViewModel(Banking.Data.ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        public IList<Transaction> Transaction { get;set; }

//        public async Task OnGetAsync()
//        {
//            Transaction = await _context.transactions.ToListAsync();
//        }
//    }
//}
