using GenericRepositoryUnitOfWork.Core.Dto;
using GenericRepositoryUnitOfWork.Core.Helper;
using GenericRepositoryUnitOfWork.Core.MicrosoftIdentity;
using GenericRepositoryUnitOfWork.Core.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GenericRepositoryUnitOfWork.Core.Interface
{
    public interface IMicrosoftIdentity
    {
        Task<RegisterResponse> SignUpAsync(SignUpVM model);
        Task<RegisterResponse> SignInAsync(SignInVM model);
        Task<dynamic> SignOutAsync();
        Task<ApplicationUser> GetCurrentUser(ClaimsPrincipal user);
        Task<bool> CheckEmailExistences(string? email);
        Task<Address> GetCurrentUserAddress(ClaimsPrincipal user);
        Task<IdentityResult> UpdateCurrentUserAddress(ApplicationUser address);
        // FindByIdAsync   string Id 
        // Edit User   string id , user to edit 
        // Delete User   string id 
        // Need To Create User Controller & Roles Controller
        // List All Users With Assigned Roles



    }
}

