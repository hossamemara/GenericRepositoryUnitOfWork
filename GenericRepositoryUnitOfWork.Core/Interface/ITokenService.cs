using GenericRepositoryUnitOfWork.Core.MicrosoftIdentity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericRepositoryUnitOfWork.Core.Interface
{
    public interface ITokenService
    {
        string CreateToken(ApplicationUser applicationUser);
    }
}
