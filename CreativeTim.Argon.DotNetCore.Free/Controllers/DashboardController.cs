using CreativeTim.Argon.DotNetCore.Free.Infrastructure;
using JuntoSeguros.Business;
using JuntoSeguros.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApp.Identity;

namespace JuntoSeguros.Controllers
{


    [Authorize]
    [Route("Dashboard")]
    public class DashboardController : BaseController
    {
        private readonly ILogger<DashboardController> _logger;
        private readonly UserManager<MyUser> _userManager;
        private readonly SignInManager<MyUser> _signInManager;
        private readonly Context _context;
        private readonly DashboardBusiness DashboardBusiness;


        public DashboardController(
         ILogger<DashboardController> logger,
         UserManager<MyUser> userManager,
         SignInManager<MyUser> signInManage,
         Context context)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManage;
            _context = context;
            DashboardBusiness = new DashboardBusiness(_context);
        }

        [HttpGet]
        [Route("Index")]
        public IActionResult Index(string valor)
        {

            return View();
        }
    }
}
