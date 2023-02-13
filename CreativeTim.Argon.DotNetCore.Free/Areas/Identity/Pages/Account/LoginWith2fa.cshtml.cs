using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using WebApp.Identity;

namespace JuntoSeguros.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginWith2faModel : PageModel
    {
        private readonly SignInManager<MyUser> _signInManager;
        private readonly ILogger<LoginWith2faModel> _logger;
        private readonly UserManager<MyUser> _userManager;
        private readonly IUserClaimsPrincipalFactory<MyUser> _userClaimsPrincipalFactory;
        private readonly Context _context;

        public LoginWith2faModel(SignInManager<MyUser> signInManager, ILogger<LoginWith2faModel> logger, UserManager<MyUser> userManager, IUserClaimsPrincipalFactory<MyUser> userClaimsPrincipalFactory,Context context)
        {
            _signInManager = signInManager;
            _logger = logger;
            _userManager = userManager;
            _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            [StringLength(7, ErrorMessage = "O {0} deve ter no mínimo {2} e no máximo {1} caracteres.", MinimumLength = 6)]
            [DataType(DataType.Text)]
            [Display(Name = "Código do autenticador")]
            public string TwoFactorCode { get; set; }

            [Display(Name = "Lembre-se desta máquina")]
            public bool RememberMachine { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(bool rememberMe, string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            RememberMe = rememberMe;
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(bool rememberMe, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            returnUrl = returnUrl ?? Url.Content("~/");
            var result = await HttpContext.AuthenticateAsync(IdentityConstants.TwoFactorUserIdScheme);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Código expirado!");
                return Page();
            }

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(result.Principal.FindFirstValue("sub2FA"));
                if (user != null)
                {
                    var isValid = await _userManager.VerifyTwoFactorTokenAsync(
                        user,
                        result.Principal.FindFirstValue("provider2FA"), Input.TwoFactorCode);



                    if (isValid)
                    {
                        await HttpContext.SignOutAsync(IdentityConstants.TwoFactorUserIdScheme);

                        var claimsPrincipal = await _userClaimsPrincipalFactory.CreateAsync(user);
                        await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, claimsPrincipal);

                        var Usuario = _context.Tusuario.Where(x => x.ID_IDENTITY_USER == user.Id).FirstOrDefault();
                        Usuario.DT_ULTIMO_ACESSO = DateTime.Now;//Usado pra vereficar expiração autenticacao que foi estipulaod em 3 horas
                        _context.Tusuario.Update(Usuario);
                        _context.SaveChanges();

                        return LocalRedirect(returnUrl);
                    }

                    ModelState.AddModelError("", "Código inválido!");
                    return Page();
                }
                ModelState.AddModelError("", "Codigo inválido!");
 
            }
            return Page();
        }
    }
}
