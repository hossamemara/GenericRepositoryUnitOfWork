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

        [HttpPost("SignUpAsync")]
        public async Task<IActionResult> SignUpAsync(SignUpVM model)
        {
            try
            {
                if (model.IsAgree == true)
                {
                    var res = await _microsoftIdentity.SignUpAsync(model);
                    var data = _mapper.Map<SignUpDto>(model);
                    if (res.Succeeded)
                    {
                        return Ok(new ApiResponse<IEnumerable<SignUpDto>>

                        {

                            StatusCode = 200,
                            HttpStatusCodes = "Ok",
                            Message = "Data Retrived",
                            AffectedRows = 1,
                            Data = data

                        });
                    }
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
                else
                {
                    return BadRequest(new ApiResponse<string>

                    {


                        StatusCode = 400,
                        HttpStatusCodes = "BadRequest",
                        Message = "Please Agree Sign Up Terms",
                        AffectedRows = 0,
                        Error = "Please Agree Sign Up Terms"
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

        [HttpPost("SignInAsync")]
        public async Task<IActionResult> SignInAsync(SignInVM model)
        {
            try
            {
                var res = await _microsoftIdentity.SignInAsync(model);
                var data = _mapper.Map<SignInDto>(model);
                if(res is not null)
                {
                    if (res.Succeeded)
                        return Ok(new ApiResponse<IEnumerable<SignInDto>>

                        {

                            StatusCode = 200,
                            HttpStatusCodes = "Ok",
                            Message = "Data Retrived",
                            AffectedRows = 1,
                            Data = data

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
                    return Unauthorized(new ApiResponse<string>

                    {

                        StatusCode = 401,
                        HttpStatusCodes = "Unauthorized",
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


        #region Sign Out

        [HttpGet("SignOutAsync")]
        public async Task<IActionResult> SignOutAsync()
        {
            try
            {
                await _microsoftIdentity.SignOutAsync();
                return Ok(new ApiResponse<IEnumerable<SignInVM>>

                {

                    StatusCode = 200,
                    HttpStatusCodes = "Ok",
                    Message = "Data Retrived",
                    AffectedRows = 1

                });


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
