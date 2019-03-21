using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityDeepDive
{
    public class DoesNotContainPasswordValidator<TUser> : IPasswordValidator<TUser> where TUser:class
    {
        public async Task<IdentityResult> ValidateAsync(UserManager<TUser> manager, TUser user, string password)
        {
            string username = await manager.GetUserNameAsync(user);

            if(username==password)
            {
                return IdentityResult.Failed(new IdentityError { Description = "username and password cannot be same" });
            }

            if(password.Contains("password"))
            {
                return IdentityResult.Failed(new IdentityError { Description = "Password cannot contain password" });
            }

            return IdentityResult.Success;
        }
    }
}
