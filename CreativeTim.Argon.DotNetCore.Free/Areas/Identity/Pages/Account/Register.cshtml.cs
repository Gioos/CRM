using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using CreativeTim.Argon.DotNetCore.Free.Models.Identity;
using JuntoSeguros.Business;
using JuntoSeguros.Models;
using JuntoSeguros.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using WebApp.Identity;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CreativeTim.Argon.DotNetCore.Free.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<MyUser> _signInManager;
        private readonly UserManager<MyUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly Context _context;
        private readonly UsuarioBusiness UsuarioBusiness;

        public RegisterModel(
            UserManager<MyUser> userManager,
            SignInManager<MyUser> signInManager,
            ILogger<RegisterModel> logger,
            Context context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _context = context;
            UsuarioBusiness = new UsuarioBusiness(_context);
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
            [Display(Name = "Nome")]
            public string FullName { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Senha")]
            public string Password { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Confirmar senha")]
            [Compare("Password", ErrorMessage = "A senha e a senha de confirmação não coincidem.")]
            public string ConfirmPassword { get; set; }

            [Required]
            public bool AcceptPrivacyPolicy { get; set; }
        }

        public async Task OnGet(string returnUrl = null)
        {
            //await CadastroUsuarios_Tabelas_AspNetUser();(NAO MECHE SE NAO SOUBER O QUE ESTA FAZENDO)
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (!Input.AcceptPrivacyPolicy)
            {
                ModelState.AddModelError(string.Empty, "Você deve aceitar a Política de Privacidade para se registrar");
            }
            else if (ModelState.IsValid)
            {
                var user = new MyUser
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                    //// Remove the code below to require the user to confirm their e-mail
                    //EmailConfirmed = true,
                    // Custom fields next
                    TwoFactorEnabled = true,
                    NomeCompleto = Input.FullName,
                };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("Usuário criado.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    var Usuario = new Usuario_ViewModel
                    {
                        Email = Input.Email,
                        Id_Identity_User = user.Id,
                        Nome = Input.FullName,
                        Login = Input.Email
                    };
                    var urlCode = $"Por favor, confirme seu email <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicando aqui</a>.";
                    Usuario.SendConfirmarEmail(Input.Email, urlCode);


                    Usuario.Id_Identity_User = user.Id;
                    UsuarioBusiness.CadastrarUsuario(Usuario);
                    Usuario.SendRegisterEmail(Input.Password);

                    //await _signInManager.SignInAsync(user, isPersistent: false);
                    //return LocalRedirect(returnUrl);

                    return RedirectToPage("./ForgotPasswordConfirmation");
                }
                foreach (var error in result.Errors)
                {
                    // We are setting the username to be the same as the e-mail address in the
                    // section above, so avoid complaining about the username when it is duplicated
                    if (error.Code == "DuplicateUserName") continue;
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
