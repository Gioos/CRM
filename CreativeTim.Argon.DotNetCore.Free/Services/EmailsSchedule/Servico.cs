using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FluentScheduler;
using JuntoSeguros.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApp.Identity;

namespace CreativeTim.Argon.DotNetCore.Free.Services.EmailsSchedule
{
    public class Servico : IJob
    {
        private readonly IServiceScopeFactory scopeFactory;

        public Servico(IServiceScopeFactory serviceFactory)
        {
            scopeFactory = serviceFactory;
        }

        /// <summary>
        //Pode ser Configurados Serviços
        /// </summary>
        public void Execute()
        {

        }
    }
}
