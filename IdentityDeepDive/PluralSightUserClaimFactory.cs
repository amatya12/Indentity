using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityDeepDive
{
    public class PluralSightUserClaimFactory : UserClaimsPrincipalFactory<PluralSightUser>
    {
        public PluralSightUserClaimFactory(UserManager<PluralSightUser> userManager, IOptions<IdentityOptions> optionsAccessor) : base(userManager, optionsAccessor)
        {

        }
        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(PluralSightUser user)
        {
           var identity =  await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim("Locale", user.Locale));
            return identity;


        }
    }
}
