using JuntoSeguros.Mapping;
using JuntoSeguros.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Identity
{
    public class Context : IdentityDbContext<MyUser>
    {
        public Context(DbContextOptions<Context> options)
            : base(options)
        {

        }
        public DbSet<Tpessoa> Tpessoa { get; set; }   
        public DbSet<Tusuario> Tusuario { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Organization>(entity =>
            {
                entity.ToTable("Organizations");
                entity.HasKey(x => x.Id);

                entity.HasMany<MyUser>()
                    .WithOne()
                    .HasForeignKey(x => x.OrgId)
                    .IsRequired(false);
            });

            new TpessoaMapping(modelBuilder.Entity<Tpessoa>());
            new TusuarioMapping(modelBuilder.Entity<Tusuario>());

        }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = errorMessages;

                // Combine the original exception message with the new one.
                var exceptionMessage = ex.Message + " => " + fullErrorMessage;

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }

    }
}
