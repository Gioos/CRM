using System.Diagnostics;
using System.Threading.Tasks;
using CreativeTim.Argon.DotNetCore.Free.Infrastructure;
using CreativeTim.Argon.DotNetCore.Free.Infrastructure.ErrorHandling;
using Microsoft.AspNetCore.Mvc;
using CreativeTim.Argon.DotNetCore.Free.Models;
using CreativeTim.Argon.DotNetCore.Free.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using JuntoSeguros.Models;
using WebApp.Identity;
using System;
using System.Linq;
using JuntoSeguros.ViewModel;

namespace JuntoSeguros.Controllers
{

    [Authorize]
    [Route("Senha")]
    public class SenhaController : BaseController
    {
        private readonly ILogger<SenhaController> _logger;
        private readonly UserManager<MyUser> _userManager;
        private readonly SignInManager<MyUser> _signInManager;
        private readonly Context _context;


        public SenhaController(
         ILogger<SenhaController> logger,
         UserManager<MyUser> userManager,
         SignInManager<MyUser> signInManage,
         Context context)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManage;
            _context = context;
        }
        [HttpGet]
        [Route("Redefinir")]
        public IActionResult Redefinir()
        {
            return View();
        }
        [HttpPost]
        [Route("Redefinir")]
        public async Task<IActionResult> Redefinir(Senha_ViewModel senha)
        {
            var Email = User.Claims.FirstOrDefault(w => w.Type == "Email")?.Value.ToString();

            if (!ModelState.IsValid)
            {
                return View(senha);
            }
            var user = await _userManager.FindByEmailAsync(Email);
           
            try
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, token, senha.NovaSenha.ToString());
                if (result.Succeeded)
                {
                    return View();
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Erro ao redefinir senha!");
            }
            return View(senha);
        }
    }
}


