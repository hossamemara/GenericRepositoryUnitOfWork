using GenericRepositoryUnitOfWork.Core.Helper;
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
        private readonly ITokenService _tokenService;
        public SignInManager<ApplicationUser> _signInManager { get; }

        #endregion

        #region Constructor

        public MicrosoftIdentityService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ITokenService tokenService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenService = tokenService;
        }

        #endregion

        #region SignUp
        public async Task<RegisterResponse> SignUpAsync(SignUpVM model)
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
            var token = _tokenService.CreateToken(user);
            var registerInfo = await _userManager.CreateAsync(user, model.Password);
            return new RegisterResponse
            
            {
                Token = token, IdentityResult = registerInfo
            };


        }
        #endregion

        #region SignIn
        public async Task<RegisterResponse> SignInAsync(SignInVM model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            var token = _tokenService.CreateToken(user);
            
            if (user is not null)
            {
                var password = await _userManager.CheckPasswordAsync(user, model.Password);
                if (password)
                {
                    var userInfo = _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                    return new RegisterResponse

                    {
                        Token = token,
                        SignInResult = userInfo.Result
                    }; 
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
