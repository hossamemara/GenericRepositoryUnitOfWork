using GenericRepositoryUnitOfWork.Core.Helper;
using GenericRepositoryUnitOfWork.Core.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericRepositoryUnitOfWork.Core.Interface
{
    public interface IMicrosoftIdentity
    {
        Task<RegisterResponse> SignUpAsync(SignUpVM model);
        Task<RegisterResponse> SignInAsync(SignInVM model);
        Task<dynamic> SignOutAsync();


    }
}
