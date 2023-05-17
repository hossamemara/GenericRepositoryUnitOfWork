using GenericRepositoryUnitOfWork.Core.MicrosoftIdentity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GenericRepositoryUnitOfWork.Core.UserManagerExtensions
{
    public static class UserManagerExtensions
    {
        public static async Task<ApplicationUser> GetUserByClaimPrinciple(this UserManager<ApplicationUser> userManager,ClaimsPrincipal userClaim)
        {

            var email = userClaim?.Claims?.FirstOrDefault(x=>x.Type == ClaimTypes.Email)?.Value;
            return await userManager.Users.SingleOrDefaultAsync(x=>x.Email == email);
        }
        public static async Task<Address> GetUserAddressByClaimPrinciple(this UserManager<ApplicationUser> userManager, ClaimsPrincipal userClaim)
        {

            var email = userClaim?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            var user = await userManager.Users.SingleOrDefaultAsync(x => x.Email == email);
            return user.Addrese;
        }
    }
}
