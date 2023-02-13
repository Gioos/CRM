using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentScheduler;
using JuntoSeguros.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace CreativeTim.Argon.DotNetCore.Free.Services.EmailsSchedule
{
    public class ServicosRegistry : Registry
    {
        public ServicosRegistry(IServiceScopeFactory serviceFactory)
        {
            //Schedule(() =>
            //{
            //    DayOfWeek[] dias = new DayOfWeek[] { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday, DayOfWeek.Saturday };

            //    if (dias.Contains(DateTime.Now.DayOfWeek))
            //    {
            //        new Servico(serviceFactory).Execute();
            //    }

            //}).ToRunEvery(0).Hours().At(0).Between(8, 0, 18, 0);

            //Schedule(() =>
            //{
            //    new Servico(serviceFactory).Execute();
            //}).ToRunEvery(0).Weekdays().At(08, 00);

            //Schedule(() =>
            //{
            //    new Servico(serviceFactory).Execute();
            //}).ToRunEvery(0).Weekdays().At(16, 00);

            //Implementação de teste (Usado para Debug)
            //Schedule(() => new Servico(serviceFactory)).ToRunNow().AndEvery(5).Minutes();
        }
    }
}
