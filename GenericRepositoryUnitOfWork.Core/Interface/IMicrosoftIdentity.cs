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
        Task<IdentityResult> SignUp(SignUpVM model);

        Task<SignInResult> SignIn(SignInVM model);

        Task<dynamic> SignOut();
    }
}
