using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Banking.Core;
using System.Security.Claims;

namespace DotNetBanking.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<Banking.Core.User> _signInManager;
        private readonly UserManager<Banking.Core.User> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly Banking.Data.ApplicationDbContext _context;
        public RegisterModel(
            UserManager<Banking.Core.User> userManager,
            SignInManager<Banking.Core.User> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender, Banking.Data.ApplicationDbContext context)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Correo Electronico")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Contraseña *")]
            public string Password { get; set; }

            /*-------------------------------------------------------------*/
            [Required]
            [Display(Name = "Documento de Identidad *")]
            public string DNI { get; set; }

            [Required]
            [Display(Name = "Nombre *")]
            public string Name { get; set; }

            [Required]
            [Display(Name = "Apellido *")]
            public string LastName { get; set; }

            [Display(Name = "Telefono")]
            public string Phone { get; set; }

            [Display(Name = "Direceccion de Domicilio")]
            public string HomeAddress { get; set; }

        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            Guid myuuid = Guid.NewGuid();
            string myuuidAsString = myuuid.ToString();


            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                /*
                 Aqui estamos creado un object de la clase Usuario para leugo guardar esa informacion en el usuario
                 */
                var user = new User
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                    Nombres = Input.Name,
                    Apellidos = Input.LastName,
                    PhoneNumber = Input.Phone,
                    Documento_Identidad = Input.DNI,
                };
                /*
                 Aqui hacemos el proceso de espera para la creacion del usuario con la clave 
                 */
                var result = await _userManager.CreateAsync(user, Input.Password);


                /*
                 Aqui estamos creando un objecto de la clase account y con un campo UserId Para luego poder vincular la cuenta del USUARIO
                 */
                var account = new Banking.Core.Account { Amount = 1500, DateCreate = DateTime.Now, UserID = user.Id, NO_Account = myuuidAsString };
                /*
                 Aqui estamos guardando la cuenta y esperando que se guarden los cambios 
                 */
                _context.accounts.Add(account);
                await _context.SaveChangesAsync();
                if (result.Succeeded)
                {

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
