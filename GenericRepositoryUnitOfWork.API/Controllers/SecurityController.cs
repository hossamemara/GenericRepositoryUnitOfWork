using AutoMapper;
using GenericRepositoryUnitOfWork.Core.FilterModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GenericRepositoryUnitOfWork.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        #region Private Fields
        private readonly IMicrosoftIdentity _microsoftIdentity;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public SecurityController(IMapper mapper, IMicrosoftIdentity microsoftIdentity)
        {
            _mapper = mapper;
            _microsoftIdentity = microsoftIdentity;
        }
        #endregion

        #region Security Actions

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(SignUpVM model)
        { 
            try
            {
                var res = await _microsoftIdentity.SignUp(model);
                if(res.Succeeded)
                    return Ok(model);
                else 
                    return BadRequest(res.Errors);
            }

            catch (Exception ex) { 
             
                return BadRequest(ex.Message);
            }

            
        }

        #endregion

        }
}
