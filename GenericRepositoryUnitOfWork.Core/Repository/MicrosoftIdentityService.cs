using GenericRepositoryUnitOfWork.Core.Interface;
using GenericRepositoryUnitOfWork.Core.MicrosoftIdentity;
using GenericRepositoryUnitOfWork.Core.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericRepositoryUnitOfWork.Core.Repository
{
    public class MicrosoftIdentityService : IMicrosoftIdentity
    {
        #region Private Fields

        public UserManager<ApplicationUser> _userManager { get; }
        public SignInManager<ApplicationUser> _signInManager { get; }

        #endregion

        #region Constructor

        public MicrosoftIdentityService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        #endregion

        #region SignUp
        public async Task<IdentityResult> SignUpAsync(SignUpVM model)
        {
            var user = new ApplicationUser()
            {
                UserName = model.Email.Split('@')[0],
                DisplayName = model.Email.Split('@')[0],
                Email = model.Email,
                IsAgree = model.IsAgree,
                Addrese = new Address
                {
                    FirstName = model.FirstName,
                    LastName  = model.LastName,
                    City = model.City,
                    State = model.State,
                    Street = model.Street,
                    zipCode = model.zipCode
                }
            };

            var registerInfo = await _userManager.CreateAsync(user, model.Password);
            return registerInfo;


        }
        #endregion

        #region SignIn
        public async Task<SignInResult> SignInAsync(SignInVM model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is not null)
            {
                var password = await _userManager.CheckPasswordAsync(user, model.Password);
                if (password)
                {
                    var userInfo = _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                    return userInfo.Result;
                }
                else
                    return null;
            }
            else
                return null;



        }
        #endregion

        #region SignOutAsync
        public async Task<dynamic> SignOutAsync()
        {
            await _signInManager.SignOutAsync();
            return null;
        }

        #endregion




    }
}
