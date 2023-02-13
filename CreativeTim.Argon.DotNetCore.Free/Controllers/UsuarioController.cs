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
using JuntoSeguros.Utils;
using System.Collections.Generic;
using JuntoSeguros.Business;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using System.Globalization;

namespace JuntoSeguros.Controllers
{
    [Authorize]

    [Route("Usuario")]
    public class UsuarioController : BaseController
    {
        private readonly ILogger<UsuarioController> _logger;
        private readonly UserManager<MyUser> _userManager;
        private readonly SignInManager<MyUser> _signInManager;
        private readonly Context _context;
        private readonly UsuarioBusiness UsuarioBusiness;


        public UsuarioController(
         ILogger<UsuarioController> logger,
         UserManager<MyUser> userManager,
         SignInManager<MyUser> signInManage,
         Context context)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManage;
            _context = context;
            UsuarioBusiness = new UsuarioBusiness(_context);
        }

        [HttpGet]
        [Route("Index")]

        public async Task<IActionResult> Index(string situacao = "A")
        {
            try
            {
                int IdUsuario = int.Parse(User.Claims.FirstOrDefault(w => w.Type == "Id_Usuario")?.Value);

                var Lista = UsuarioBusiness.ListaUsuarios();

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
                Usuario_ViewModel viewModel = new Usuario_ViewModel();
                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro");
            }
            return View();
        }

        [HttpGet]
        [Route("Usuarioedit")]
        public IActionResult Usuarioedit(int? IdUsuario)
        {
            try
            {
                var Usuario = _context.Tusuario.Include(x => x.Tpessoa).Where(x => x.ID_USUARIO == IdUsuario).FirstOrDefault();
                Usuario_ViewModel_Editar viewModel = new Usuario_ViewModel_Editar();
                
                viewModel.Id = IdUsuario;
                viewModel.Nome = Usuario.Tpessoa.NO_PESSOA;

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro");
            }
            return View();
        }

        [HttpPost]
        [Route("Cadastrar")]
        public async Task<IActionResult> Cadastrar(Usuario_ViewModel Usuario)
        {
            int Id_Pessoa = int.Parse(User.Claims.FirstOrDefault(w => w.Type == "Id_Pessoa")?.Value);
            Usuario.Id_Pessoa_Cadastro = Id_Pessoa;
            try
            {
                if (ModelState.IsValid)
                {
                    #region Inserir o Usuario para Autenticacao nas tabelas do IDENTY

                    var user = new MyUser
                    {
                        UserName = Usuario.Email,
                        Email = Usuario.Email,
                        //// Remove the code below to require the user to confirm their e-mail
                        //EmailConfirmed = true,
                        TwoFactorEnabled = true,
                        NomeCompleto = Usuario.Nome,
                    };
                    var SenhaRandon = Usuario.GeraSenhaAleatoria();
                    var result = await _userManager.CreateAsync(user, SenhaRandon);

                    if (result.Succeeded)
                    {
                        _logger.LogInformation("Usuário criado.");
                        // Uncomment the code below to enable sending a confirmation e-mail
                        //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        Usuario.Id_Identity_User = user.Id;
                        UsuarioBusiness.CadastrarUsuario(Usuario);
                        Usuario.SendRegisterEmail(SenhaRandon);
                    }
                    foreach (var error in result.Errors)
                    {
                        // We are setting the username to be the same as the e-mail address in the
                        // section above, so avoid complaining about the username when it is duplicated
                        if (error.Code == "DuplicateUserName") continue;
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    #endregion
                }
                else
                {
                    return View(Usuario);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro");
            }
            return View(Usuario);
        }

        [HttpPost]
        [Route("Usuarioedit")]
        public async Task<IActionResult> Usuarioedit(Usuario_ViewModel_Editar Usuario)
        {
            int Id_Pessoa = int.Parse(User.Claims.FirstOrDefault(w => w.Type == "Id_Pessoa")?.Value);
            if (ModelState.IsValid)
            {
                using (var objTrans = _context.Database.BeginTransaction())
                {
                    try
                    {
                        var UsuarioAlterar = _context.Tusuario.Where(x => x.ID_USUARIO == Usuario.Id).FirstOrDefault();

                        #region ALTERACAO EMAIL (A Alteracao de email deve ser Feita nas tabela Do IDenty)

                        var IdIdenty = UsuarioAlterar.ID_IDENTITY_USER;
                        var userIdenty = await _userManager.FindByIdAsync(IdIdenty);

                        var Email = new Email();
                        if (userIdenty.Email != Usuario.Email)
                        {
                            if (Email.IsValidEmail(Usuario.Email))
                            {
                                var buscaExiste = await _userManager.FindByEmailAsync(Usuario.Email);
                                if (buscaExiste == null)
                                {
                                    var result = await _userManager.SetEmailAsync(userIdenty, Usuario.Email);
                                    if (result.Succeeded)
                                    {
                                        Email _emailSender = new Email
                                        {
                                            EmailFrom = "borinha52@hotmail.com",
                                            Subject = "E-mail Alterado",
                                            Body = "Seu email de acesso foi alterado, para primeiro login você deve Redefinir sua senha.",
                                            EmailTo = Usuario.Email
                                        };
                                        _emailSender.Enviar();

                                        var Pessoa = _context.Tpessoa.Where(x => x.ID_PESSOA == UsuarioAlterar.FK_PESSOA).FirstOrDefault();
                                        _context.Tpessoa.Update(Pessoa);
                                        _context.SaveChanges();
                                    }
                                }
                            }
                        }
                        #endregion

                        if (UsuarioAlterar != null)
                        {
                            UsuarioAlterar.DT_ATUALIZACAO = DateTime.Now;
                            _context.Tusuario.Update(UsuarioAlterar);
                            _context.SaveChanges();
                        }
                    }
                    catch (Exception ex)
                    {
                        objTrans.Rollback();
                        _logger.LogError(ex, "Erro");
                    }
                    objTrans.Commit();
      
                    return RedirectToAction("Usuarioedit", new { IdUsuario = Usuario.Id });
                }
            }
            Usuario.Lista_FilaUsuario.Insert(0, new SelectListItem() { Value = "", Text = "", Selected = true });
            return View(Usuario);
        }

        [HttpGet]
        [Route("AlterarSituacaoUsuario")]
        public HttpStatusCode AlterarSituacaoUsuario(string IdUsuario, string IdNovoStatus)
        {
            using (var objTrans = _context.Database.BeginTransaction())
            {
                try
                {
                    var Alterar = _context.Tusuario.Where(x => x.ID_USUARIO == Convert.ToInt32(IdUsuario)).FirstOrDefault();
                    //*IMPORTANTE ver como vai ficar a tabela ASPNETUSER definir ainda
                    if (Alterar != null)
                    {
                        _context.Tusuario.Update(Alterar);
                        _context.SaveChanges();
                        objTrans.Commit();
                    }
                }
                catch (Exception ex)
                {
                    objTrans.Rollback();
                    _logger.LogError(ex, "Erro");
                    return HttpStatusCode.BadRequest;
                }
            }
            return HttpStatusCode.OK;
        }
    }
}
