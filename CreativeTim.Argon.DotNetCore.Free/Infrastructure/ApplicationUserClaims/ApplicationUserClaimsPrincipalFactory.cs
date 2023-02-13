using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CreativeTim.Argon.DotNetCore.Free.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using WebApp.Identity;

namespace CreativeTim.Argon.DotNetCore.Free.Infrastructure.ApplicationUserClaims
{
    public class ApplicationUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<MyUser, IdentityRole>
    {
        private readonly Context _context;
        public ApplicationUserClaimsPrincipalFactory(
            UserManager<MyUser> userManager
            , RoleManager<IdentityRole> roleManager
            , IOptions<IdentityOptions> optionsAccessor, Context context)
            : base(userManager, roleManager, optionsAccessor)
        {
            _context = context;
        }

        public async override Task<ClaimsPrincipal> CreateAsync(MyUser user)
        {
            var principal = await base.CreateAsync(user);
            var DadosUsuario = _context.Tusuario.Where(x => x.ID_IDENTITY_USER == user.Id).FirstOrDefault();
            

            if (!string.IsNullOrWhiteSpace(user.NomeCompleto))
            {
                ((ClaimsIdentity)principal.Identity).AddClaims(new[] {
                    new Claim("FullName", user.NomeCompleto)
                });
                ((ClaimsIdentity)principal.Identity).AddClaims(new[] {
                    new Claim("Email", user.Email)
                });
                if (DadosUsuario != null)
                {
                    ((ClaimsIdentity)principal.Identity).AddClaims(new[] {
                    new Claim("Id_Usuario", DadosUsuario?.ID_USUARIO.ToString())
                });
                    ((ClaimsIdentity)principal.Identity).AddClaims(new[] {
                    new Claim("Id_Pessoa", DadosUsuario?.FK_PESSOA.ToString())
                });

                }
                else
                {
                    ((ClaimsIdentity)principal.Identity).AddClaims(new[] {
                    new Claim("Id_Usuario","3")
                });
                    ((ClaimsIdentity)principal.Identity).AddClaims(new[] {
                    new Claim("Id_Pessoa","1")
                });
                }

            }

            // You can add more properties that you want to expose on the User object below

            return principal;
        }
    }
}
