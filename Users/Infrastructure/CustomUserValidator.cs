﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Users.Models;

namespace Users.Infrastructure
{
    public class CustomUserValidator : UserValidator<ApplicationUser>
    {
        public CustomUserValidator(AppUserManager mgr)
            : base(mgr)
        {
        }

        public override async Task<IdentityResult> ValidateAsync(ApplicationUser user)
        {
            IdentityResult result = await base.ValidateAsync(user);

            if (!user.Email.ToLower().EndsWith("@gmail.com"))
            {
                List<string> errors = result.Errors.ToList();
                errors.Add("Email 地址只支持gmail域名");
                result = new IdentityResult(errors);
            }
            return result;
        }
    }
}
