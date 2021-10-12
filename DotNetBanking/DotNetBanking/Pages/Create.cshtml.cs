using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Banking.Core;
using Banking.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace DotNetBanking.Pages
{
    public class CreateModel : PageModel
    {
        private readonly Banking.Data.ApplicationDbContext _context;
        private readonly UserManager<Banking.Core.User> _userManager;
        //private readonly HttpContext _httpContext;

        //public string UserId { get; set; }

        public SelectList Staff { get; set; }
        public User _user { get; set; }

        public Account _account { get; set; }
        [BindProperty]
        public string SelectedStaffId { get; set; }

        [BindProperty]
        public Transaction Transaction { get; set; }


        [BindProperty]
        public List<User> user { get; set; }


        public CreateModel(Banking.Data.ApplicationDbContext context, UserManager<Banking.Core.User> userManager, object selectedTransc = null)
        {
            //_httpContext = httpContext;
            //this.UserId = _userManager.GetUserId(httpContext.User);
            _userManager = userManager;
            _context = context;
        }

        public IActionResult OnGet()
        {
            var UserId = _userManager.GetUserId(HttpContext.User);

            var staff = _context.accounts.Where(account => account.UserID != UserId).ToList();
            Staff = new SelectList(staff, null, nameof(_account.NO_Account));
            return Page();
        }



        public async Task<IActionResult> OnPostAsync()
        {

            var UserId = _userManager.GetUserId(HttpContext.User);

            _context.accounts.Where(account => account.UserID != UserId).ToList();
            Transaction.ReceptAccountID = SelectedStaffId.ToString();
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var account = _context.accounts.First(account => account.UserID == UserId);
            account.Amount = account.Amount - Transaction.Amount;
            _context.accounts.Update(account);
            _context.transactions.Add(Transaction);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
