using Banking.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetBanking.Pages
{
    public class IndexModel : PageModel
    {

        private readonly ILogger<IndexModel> _logger;
        private readonly UserManager<Banking.Core.User> _userManager;
        private readonly Banking.Data.ApplicationDbContext _context;

        public User _user { get; set; }

        public Account _account { get; set; }
        public IndexModel(ILogger<IndexModel> logger, UserManager<Banking.Core.User> userManager, Banking.Data.ApplicationDbContext context)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            var _UserId = _userManager.GetUserId(HttpContext.User);
            _account = _context.accounts.FirstOrDefault(account => account.UserID == _UserId);
            return Page();

        }
    }
}
