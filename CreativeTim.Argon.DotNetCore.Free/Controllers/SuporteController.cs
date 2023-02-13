using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WebApp.Identity;

namespace JuntoSeguros.Controllers
{
    [Authorize]
    [Route("Suporte")]
    public class SuporteController : Controller
    {

        private readonly ILogger<SuporteController> _logger;
        private readonly UserManager<MyUser> _userManager;
        private readonly SignInManager<MyUser> _signInManager;
        private readonly Context _context;

        public SuporteController(
         ILogger<SuporteController> logger,
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
