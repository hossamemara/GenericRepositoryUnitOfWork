using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericRepositoryUnitOfWork.Core.Helper
{
    public class RegisterResponse
    {
        public string Token { get; set; }
        public IdentityResult IdentityResult { get; set; }

        public SignInResult SignInResult { get; set; }
    }
}
