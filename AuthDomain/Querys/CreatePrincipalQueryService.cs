using AuthDomain.Querys.Object;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthDomain.Querys
{
    public class CreatePrincipalQueryService : IQueryService<User, ClaimsPrincipal>
    {
        public ClaimsPrincipal Execute(User obj)
        {
            List<Claim> claims = new List<Claim>();
            foreach (var name in obj.Rules)
            {
                claims.Add(new Claim("role", name));
            }
            var identity = new ClaimsIdentity(claims,
                                            "RulesClaim",
                                            ClaimTypes.Name,
                                            ClaimTypes.Role);
            return new ClaimsPrincipal(identity);
        }
    }
}
