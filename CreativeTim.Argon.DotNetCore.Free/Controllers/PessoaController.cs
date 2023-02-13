using JuntoSeguros.Business;
using JuntoSeguros.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Identity;

namespace JuntoSeguros.Controllers
{
    [Authorize]
    [Route("Pessoa")]
    public class PessoaController : Controller
    {

        private readonly ILogger<PessoaController> _logger;
        private readonly UserManager<MyUser> _userManager;
        private readonly SignInManager<MyUser> _signInManager;
        private readonly Context _context;
        private readonly PessoaBusiness PessoaBusiness;

        public PessoaController(
         ILogger<PessoaController> logger,
         UserManager<MyUser> userManager,
         SignInManager<MyUser> signInManage,
         IConfiguration _config,
         Context context)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManage;
            _context = context;
            PessoaBusiness = new PessoaBusiness(_context);
        }
        [HttpGet]
        [Route("Index")]
        public async Task<IActionResult> Index(string situacao = "A")
        {
            try
            {
                int IdUsuario = int.Parse(User.Claims.FirstOrDefault(w => w.Type == "Id_Usuario")?.Value);

                var Lista = PessoaBusiness.ListaPessoas();

                ViewData["SITUACAO"] = situacao;
                return View(Lista);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString(), "Erro");
                return null;
            }
        }
        [HttpGet]
        [Route("Cadastrar")]
        public IActionResult Cadastrar()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString(), "Erro");
                return null;
            }
        }
        [HttpPost]
        [Route("Cadastrar")]
        public IActionResult Cadastrar(Pessoa_ViewModel view)
        {
            try
            {
                if (ModelState.IsValid)
                {

                }
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString(), "Erro");
                return null;
            }
        }
        [HttpGet]
        [Route("_partial_PessoaFisica")]
        public IActionResult _partial_PessoaFisica()
        {
            try
            {
                return PartialView();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString(), "Erro");
                return null;
            }
        }

        [HttpGet]
        [Route("_partial_PessoaJuridica")]
        public IActionResult _partial_PessoaJuridica()
        {
            try
            {
                return PartialView();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString(), "Erro");
                return null;
            }
        }
    }
}
