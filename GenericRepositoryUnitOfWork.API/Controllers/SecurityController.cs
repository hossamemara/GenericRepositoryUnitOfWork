using AutoMapper;
using GenericRepositoryUnitOfWork.Core.Dto;
using GenericRepositoryUnitOfWork.Core.FilterModels;
using GenericRepositoryUnitOfWork.Core.Helper;
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


        #region SignUp

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(SignUpVM model)
        {
            try
            {
                var res = await _microsoftIdentity.SignUp(model);
                if (res.Succeeded)
                    return Ok(new ApiResponse<IEnumerable<SignUpVM>>

                    {

                        StatusCode = 200,
                        HttpStatusCodes = "Ok",
                        Message = "Data Retrived",
                        AffectedRows = 1,
                        Data = model

                    });
                else
                {
                    var errors = new List<string> { };
                    foreach (var error in res.Errors)
                    {
                        errors.Add(error.Description);
                        
                    }
                    return BadRequest(new ApiResponse<string>

                    {


                        StatusCode = 400,
                        HttpStatusCodes = "BadRequest",
                        Message = "Sign Up Failed",
                        AffectedRows = 0,
                        Error = errors
                    });
                }
                    
            }

            catch (Exception ex)
            {

                return BadRequest(new ApiResponse<string>

                {

                    StatusCode = 400,
                    HttpStatusCodes = "BadRequest",
                    Message = ex.Message,
                    AffectedRows = 0,
                    Error = ex.Message
                });
            }


        }

        #endregion

        #region SignIn

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(SignInVM model)
        {
            try
            {
                var res = await _microsoftIdentity.SignIn(model);
                if(res !=null)
                {
                    if (res.Succeeded)
                        return Ok(new ApiResponse<IEnumerable<SignInVM>>

                        {

                            StatusCode = 200,
                            HttpStatusCodes = "Ok",
                            Message = "Data Retrived",
                            AffectedRows = 1,
                            Data = res

                        });
                    else
                        return BadRequest(new ApiResponse<string>

                        {

                            StatusCode = 400,
                            HttpStatusCodes = "BadRequest",
                            Message = "Invalid Email or Password",
                            AffectedRows = 0,
                            Error = "Invalid Email or Password"
                        });
                }
                else
                {
                    return NotFound(new ApiResponse<string>

                    {

                        StatusCode = 404,
                        HttpStatusCodes = "NotFound",
                        Message = "Invalid Email or Password",
                        Error = "Invalid Email or Password",
                        AffectedRows = 0,
                    });
                }
                
                
            }

            catch (Exception ex)
            {

                return BadRequest(new ApiResponse<string>

                {

                    StatusCode = 400,
                    HttpStatusCodes = "BadRequest",
                    Message = ex.Message,
                    Error = ex.Message,
                    AffectedRows = 0
                });
            }


        }

        #endregion


        #endregion

    }
}
