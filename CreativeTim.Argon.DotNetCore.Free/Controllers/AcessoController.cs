using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WebApp.Identity;

namespace JuntoSeguros.Controllers
{
    [Authorize]
    [Route("Acesso")]
    public class AcessoController : Controller
    {

        private readonly ILogger<AcessoController> _logger;
        private readonly UserManager<MyUser> _userManager;
        private readonly SignInManager<MyUser> _signInManager;
        private readonly Context _context;

        public AcessoController(
         ILogger<AcessoController> logger,
         UserManager<MyUser> userManager,
         SignInManager<MyUser> signInManage,
         IConfiguration _config,
         Context context)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManage;
            _context = context;
        }

        [HttpGet]
        [Route("Index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
