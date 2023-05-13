using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericRepositoryUnitOfWork.Core.MicrosoftIdentity
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsAgree { get; set; }
        public string DisplayName { get; set; }
        public virtual Address Addrese { get; set; }
    }
}
