using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericRepositoryUnitOfWork.Core.Dto
{
    public class SignInDto
    {

        public string Email { get; set; }
        public string Token { get; set; }
    }
}
