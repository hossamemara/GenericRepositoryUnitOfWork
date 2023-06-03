using AutoMapper;
using GenericRepositoryUnitOfWork.Core.Dto;
using GenericRepositoryUnitOfWork.Core.FilterModels;
using GenericRepositoryUnitOfWork.Core.Helper;
using GenericRepositoryUnitOfWork.Core.MicrosoftIdentity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GenericRepositoryUnitOfWork.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        #region Private Fields
        private readonly IMicrosoftIdentity _microsoftIdentity;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        
        #endregion

        #region Constructor
        public SecurityController(IMapper mapper, IMicrosoftIdentity microsoftIdentity, ITokenService tokenService)
        {
            _mapper = mapper;
            _microsoftIdentity = microsoftIdentity;
            _tokenService= tokenService;
        }
        #endregion

        #region Security Actions


        #region SignUp
        [AllowAnonymous]
        [HttpPost("SignUpAsync")]
        public async Task<IActionResult> SignUpAsync(SignUpVM model)
        {
            try
            {
                if (model.IsAgree == true)
                {
                    var res = await _microsoftIdentity.SignUpAsync(model);
                    var data = _mapper.Map<SignUpDto>(model);
                    
                    if (res.IdentityResult.Succeeded)
                    {
                        data.Token = res.Token;
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
                        foreach (var error in res.IdentityResult.Errors)
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
        [AllowAnonymous]
        [HttpPost("SignInAsync")]
        public async Task<IActionResult> SignInAsync(SignInVM model)
        {
            try
            {
                var res = await _microsoftIdentity.SignInAsync(model);
                var data = _mapper.Map<SignInDto>(model);
                
                if(res is not null)
                {
                    data.Token = res.Token;
                    if (res.SignInResult.Succeeded)
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
        [AllowAnonymous]
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

        #region GetCurrentUser

        [HttpGet("GetCurrentUser")]
        public async Task<IActionResult> GetCurrentUser()
        {
            try
            {
                var user = await _microsoftIdentity.GetCurrentUser(HttpContext.User);
                if (user is not null)
                {
                    var data = _mapper.Map<UserDto>(user);
                    data.Token = _tokenService.CreateToken(user);
                    

                    return Ok(new ApiResponse<IEnumerable<UserDto>>

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
                    return NotFound(new ApiResponse<string>

                    {


                        StatusCode = 404,
                        HttpStatusCodes = "Not Found",
                        Message = "Email Not Exists",
                        AffectedRows = 0,
                        Error = "Email Not Exists"
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

        #region CheckEmail Existance
        
        
        [HttpGet("CheckEmailExistance")]
        public async Task<IActionResult> CheckEmailExistance([FromQuery] string? email)
        {

            try
            {
                
                var res = await _microsoftIdentity.CheckEmailExistance(email);
                if (res == true)
                {
                    return Ok(new ApiResponse<IEnumerable<string>>

                    {

                        StatusCode = 200,
                        HttpStatusCodes = "Ok",
                        Message = "Data Retrived",
                        AffectedRows = 1,
                        ExistanceFlag = res

                    });
                }
                else
                {
                    return NotFound(new ApiResponse<string>

                    {


                        StatusCode = 404,
                        HttpStatusCodes = "Not Found",
                        Message = "Email Not Exists",
                        AffectedRows = 0,
                        Error = "Email Not Exists",
                        ExistanceFlag = res
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

        #region GetCurrentUserAddress

        [HttpGet("GetCurrentUserAddress")]
        public async Task<IActionResult> GetCurrentUserAddress()
        {
            try
            {
               
                var user = await _microsoftIdentity.GetCurrentUserAddress(HttpContext.User);
                if (user is not null)
                {
                    var data = _mapper.Map<UserAddressDto>(user);
                    
                    return Ok(new ApiResponse<IEnumerable<UserAddressDto>>

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
                    return NotFound(new ApiResponse<string>

                    {


                        StatusCode = 404,
                        HttpStatusCodes = "Not Found",
                        Message = "Email Not Exists",
                        AffectedRows = 0,
                        Error = "Email Not Exists"
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

        #region UpdateCurrentUserAddress

        [HttpPut("UpdateCurrentUserAddress")]
        public async Task<IActionResult> UpdateCurrentUserAddress(UserAddressDto model)
        {
            try
            {

                var user = await _microsoftIdentity.GetCurrentUser(HttpContext.User);

                if (user is not null)
                {
                    user.Addrese = _mapper.Map<UserAddressDto, Address>(model);
                 
                    var res =await _microsoftIdentity.UpdateCurrentUserAddress(user);
                    return Ok(new ApiResponse<IEnumerable<UserAddressDto>>

                    {

                        StatusCode = 200,
                        HttpStatusCodes = "Ok",
                        Message = "Data Retrived",
                        AffectedRows = 1,
                        Data = model

                    });
                }

                else
                {
                    return NotFound(new ApiResponse<string>

                    {


                        StatusCode = 404,
                        HttpStatusCodes = "Not Found",
                        Message = "Email Not Exists",
                        AffectedRows = 0,
                        Error = "Email Not Exists"
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
