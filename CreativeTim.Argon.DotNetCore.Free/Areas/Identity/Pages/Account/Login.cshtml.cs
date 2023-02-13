using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CreativeTim.Argon.DotNetCore.Free.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using WebApp.Identity;
using JuntoSeguros.ViewModel;
using System.Text.Encodings.Web;
using System.Security.Claims;
using JuntoSeguros.Models;

namespace CreativeTim.Argon.DotNetCore.Free.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly SignInManager<MyUser> _signInManager;
        private readonly UserManager<MyUser> _userManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly Context _context;

        public LoginModel(SignInManager<MyUser> signInManager, ILogger<LoginModel> logger, UserManager<MyUser> userManager, Context context)
        {
            _signInManager = signInManager;
            _logger = logger;
            _userManager = userManager;
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        [TempData]
        public string ValidaEmail_ { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Campo obrigatório!")]
            [EmailAddress(ErrorMessage = "O campo Email não é um endereço de email válido!")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Campo obrigatório!")]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Lembrar-me?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null, string ValidaEmail = null)
        {
            ValidaEmail_ = ValidaEmail;

            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            var DadosUsuario = new Tusuario();

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true

                var user = await _userManager.FindByEmailAsync(Input.Email);
                if (user != null && !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    //Caso email não esteja confirmado
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    var Usuario = new Usuario_ViewModel { };
                    var urlCode = $"Por favor, confirme seu email <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicando aqui</a>.";
                    Usuario.SendConfirmarEmail(Input.Email, urlCode);
                    return RedirectToPage("./ForgotPasswordConfirmation");
                }
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Login inválido! Contate o seu supervisor (a)");
                    return Page();
                }

                DadosUsuario = _context.Tusuario.Where(x => x.ID_IDENTITY_USER == user.Id).FirstOrDefault();


                #region DOIS FATORES (REMOVIDO BORA).
                //var UltimoAcesso = DadosUsuario.DT_ULTIMO_ACESSO;

                //if (UltimoAcesso==null)
                //{
                //    var DoisFator = await _userManager.SetTwoFactorEnabledAsync(user, true);
                //}
                //else if (DateTime.Now >= Convert.ToDateTime(DadosUsuario.DT_ULTIMO_ACESSO).AddHours(4)) //Definido expiração *Solicitacao autenticacao dois fatores em 3 horas(Hora Atualizada no momento da autenticao)
                //{
                //    var DoisFator = await _userManager.SetTwoFactorEnabledAsync(user, true);
                //}
                //else
                //{
                //    var DoisFator = await _userManager.SetTwoFactorEnabledAsync(user, false);
                //}
                #endregion

                //LOGIN
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: true);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    return LocalRedirect(returnUrl);
                }

                if (!result.RequiresTwoFactor) //Autenticação dois fatores.
                {

                    if (await _userManager.GetTwoFactorEnabledAsync(user))
                    {
                        var validator = await _userManager.GetValidTwoFactorProvidersAsync(user);

                        if (validator.Contains("Email"))
                        {
                            var token = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");
                            var Usuario = new Usuario_ViewModel { };
                            Usuario.SendAutenticacaoDoisFator(Input.Email, token);

                            await HttpContext.SignInAsync(IdentityConstants.TwoFactorUserIdScheme,
                                    Store2FA(user.Id, "Email"));

                            return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                        }
                    }
                }

                if (result.IsLockedOut)
                {
                    _logger.LogWarning("Conta bloqueada! Contate o seu supervisor (a)");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Login inválido! Contate o seu supervisor (a). Ou redefina sua senha!");
                    return Page();
                }
            }
            return Page();
        }
        public ClaimsPrincipal Store2FA(string userId, string provider)
        {
            var identity = new ClaimsIdentity(new List<Claim>
            {
                new Claim("sub2FA", userId),
                new Claim("provider2FA", provider)
            }, IdentityConstants.TwoFactorUserIdScheme);

            return new ClaimsPrincipal(identity);
        }
    }
}
