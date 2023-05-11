

using AutoMapper;
using GenericRepositoryUnitOfWork.Core.Constants;
using GenericRepositoryUnitOfWork.Core.Dto;
using GenericRepositoryUnitOfWork.Core.FilterModels;
using GenericRepositoryUnitOfWork.Core.Helper;
using GenericRepositoryUnitOfWork.Core.Models;
using GenericRepositoryUnitOfWork.Core.Repository;
using GenericRepositoryUnitOfWork.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq.Expressions;
using System.Xml.Linq;

namespace GenericRepositoryUnitOfWork.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartementsController : ControllerBase
    {
        #region Private Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public DepartementsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region Actions

        #region Get Actions

        #region Match All Action
        [HttpPost("MatchAll")]
        public async Task<IActionResult> MatchAll([FromBody] PaginationFilter filter)
        {
            try
            {
                var data = await this._unitOfWork.Departments.MatchAll(null, filter.Take, filter.Skip, A => A.Id, filter.OrderByDirection);
                var res = _mapper.Map<IEnumerable<DepartmentDto>>(data);
                if (res.Count() == 0)
                    return NotFound("No Data Found For Departements");
                else
                    return Ok(res);
            }

            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<string>

                {

                    StatusCode = 400,
                    HttpStatusCodes = "BadRequest",
                    Message = ex.Message,
                    Error = ex.Message
                });
            }
        }
        #endregion

        #region GetAllAsync Action
        [HttpGet("GetAllAsync")]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var data = await this._unitOfWork.Departments.GetAllAsync();
                var res = _mapper.Map<IEnumerable<DepartmentDto>>(data);
                if (res.Count() == 0)
                    return NotFound(new ApiResponse<string>

                    {

                        StatusCode = 404,
                        HttpStatusCodes = "NotFound",
                        Message = "No Data Found For Departements",
                        Error = "No Data Found For Departements"
                    });
                else
                    return Ok(new ApiResponse<IEnumerable<DepartmentDto>>

                    {

                        StatusCode = 200,
                        HttpStatusCodes = "Ok",
                        Message = "Data Retrived",
                        AffectedRows = res.Count(),
                        Data = res

                    });
            }

            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<string>

                {

                    StatusCode = 400,
                    HttpStatusCodes = "BadRequest",
                    Message = ex.Message,
                    Error = ex.Message
                });
            }
        }
        #endregion

        #region GetByIdAsync Action
        [HttpGet("GetByIdAsync/{id}")]

        public IActionResult GetByIdAsync(int id)
        {
            try
            {
                var data = this._unitOfWork.Departments.GetByIdAsync(id);
                var res = _mapper.Map<DepartmentDto>(data);
                if (res == null)
                    return NotFound($"No Data Found For This Id :: {id}");
                else
                    return Ok(res);
            }

            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<string>

                {

                    StatusCode = 400,
                    HttpStatusCodes = "BadRequest",
                    Message = ex.Message,
                    Error = ex.Message
                });
            }
        }


        #endregion

        #region MatchFirst Action

        [HttpGet("MatchFirst/{id}")]
        public async Task<IActionResult> MatchFirst(int id)
        {
            try
            {
                var data = await this._unitOfWork.Departments.MatchFirst(D => D.Id == id);

                var res = _mapper.Map<DepartmentDto>(data);
                List<DepartmentDto> AffectedRows = new List<DepartmentDto>() { res };

                if (res == null)
                    return NotFound(new ApiResponse<string>

                    {

                        StatusCode = 404,
                        HttpStatusCodes = "NotFound",
                        Message = $"No Data Found For This Id :: {id}",
                        Error = $"No Data Found For This Id :: {id}"
                    });
                else
                    return Ok(new ApiResponse<IEnumerable<DepartmentDto>>

                    {

                        StatusCode = 200,
                        HttpStatusCodes = "Ok",
                        Message = "Data Retrived",
                        AffectedRows = AffectedRows.Count,
                        Data = res

                    });
            }

            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<string>

                {

                    StatusCode = 400,
                    HttpStatusCodes = "BadRequest",
                    Message = ex.Message,
                    Error = ex.Message
                });
            }
        }

        #endregion

        #region Count Action

        [HttpGet("Count")]
        public IActionResult Count()
        {
            try
            {
                int count = this._unitOfWork.Departments.Count();


                if (count == 0)
                    return NotFound(new ApiResponse<string>

                    {

                        StatusCode = 404,
                        HttpStatusCodes = "NotFound",
                        Message = $"No Data Found For Departments",
                        Error = $"No Data Found For Departments"
                    });
                else
                    return Ok(new ApiResponse<IEnumerable<DepartmentDto>>

                    {

                        StatusCode = 200,
                        HttpStatusCodes = "Ok",
                        Message = "Data Retrived",
                        AffectedRows = count,
                        Data = count

                    });
            }

            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<string>

                {

                    StatusCode = 400,
                    HttpStatusCodes = "BadRequest",
                    Message = ex.Message,
                    Error = ex.Message
                });
            }
        }

        #endregion

        

        #endregion

        #region Post Actions

        #region Add Action
        [HttpPost("Add")]

        public IActionResult Add(DepartmentVM model)
        {
            try
            {
                var data = _mapper.Map<Department>(model);
                var department = this._unitOfWork.Departments.Add(data);
                var AffectedRow = this._unitOfWork.Complete();
                var res = _mapper.Map<DepartmentDto>(department);
                return Ok(new ApiResponse<DepartmentDto>

                {

                    StatusCode = 200,
                    HttpStatusCodes = "Ok",
                    Message = "Data Retrived",
                    AffectedRows = AffectedRow,
                    Data = res

                });



            }

            catch (Exception ex)
            {

                return BadRequest(new ApiResponse<string>

                {

                    StatusCode = 400,
                    HttpStatusCodes = "BadRequest",
                    Message = ex.Message,
                    Error = ex.Message
                });
            }
        }
        #endregion

        #region AddRange Action
        [HttpPost("AddRange")]

        public IActionResult AddRange(IEnumerable<DepartmentVM> model)
        {
            try
            {
                var data = _mapper.Map<IEnumerable<Department>>(model);
                var department = this._unitOfWork.Departments.AddRange(data);
                var AffectedRow = this._unitOfWork.Complete();
                var res = _mapper.Map<IEnumerable<DepartmentDto>>(department);
                return Ok(new ApiResponse<IEnumerable<DepartmentDto>>

                {

                    StatusCode = 200,
                    HttpStatusCodes = "Ok",
                    Message = "Data Retrived",
                    AffectedRows = AffectedRow,
                    Data = res

                });



            }

            catch (Exception ex)
            {

                return BadRequest(new ApiResponse<string>

                {

                    StatusCode = 400,
                    HttpStatusCodes = "BadRequest",
                    Message = ex.Message,
                    Error = ex.Message
                });
            }
        }
        #endregion

        #endregion

        #region Delete Actions

        #region Delete Action
        [HttpDelete("Delete/{id}")]

        public IActionResult Delete(int id)
        {
            try
            {
                var data = this._unitOfWork.Departments.GetByIdAsync(id);
                if (data == null)
                    return NotFound(new ApiResponse<string>

                    {

                        StatusCode = 404,
                        HttpStatusCodes = "NotFound",
                        Message = $"No Data Found For This Id :: {id}",
                        Error = $"No Data Found For This Id :: {id}"
                    });
                else
                {
                    var department = this._unitOfWork.Departments.Delete(data);
                    var AffectedRow = this._unitOfWork.Complete();
                    var res = _mapper.Map<DepartmentDto>(department);
                    return Ok(new ApiResponse<IEnumerable<DepartmentDto>>

                    {

                        StatusCode = 200,
                        HttpStatusCodes = "Ok",
                        Message = "Data Retrived",
                        AffectedRows = AffectedRow,
                        Data = res

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
                    Error = ex.Message
                });
            }
        }

        #endregion

        #region DeleteRange Action
        //[HttpDelete("DeleteRange/{ids}")]

        //public  async Task<IActionResult> DeleteRange(IEnumerable<DepartmentVM> models)
        //{
        //    try
        //    {
        //        var res = _mapper.Map<IEnumerable<Department>>(models);
        //        var data = this._unitOfWork.Departments.ContainsIds(res);
        //        if (res == null)
        //            return NotFound(new ApiResponse<string>

        //            {

        //                StatusCode = 404,
        //                HttpStatusCodes = "NotFound",
        //                Message = $"No Data Found For This Ids :: ",
        //                Error = $"No Data Found For This Ids :: "
        //            });
        //        else
        //        {
        //            var department = this._unitOfWork.Departments.DeleteRange(res);
        //            var AffectedRow = this._unitOfWork.Complete();
        //            //var res = _mapper.Map< IEnumerable<DepartmentDto>>(department);
        //            return Ok(new ApiResponse<IEnumerable<DepartmentDto>>

        //            {

        //                StatusCode = 200,
        //                HttpStatusCodes = "Ok",
        //                Message = "Data Retrived",
        //                AffectedRows = AffectedRow,
        //                Data = res

        //            });
        //        }

        //    }

        //    catch (Exception ex)
        //    {

        //        return BadRequest(new ApiResponse<string>

        //        {

        //            StatusCode = 400,
        //            HttpStatusCodes = "BadRequest",
        //            Message = ex.Message,
        //            Error = ex.Message
        //        });
        //    }
        //}
        #endregion

        #endregion

        #region Put Actions

        #region Update Action
        [HttpPut("Update/{id}")]

        public IActionResult Update(int id, DepartmentVM model)
        {
            try
            {
                var data = this._unitOfWork.Departments.GetByIdAsync(id);

                if (data == null)

                    return NotFound(new ApiResponse<string>

                    {

                        StatusCode = 404,
                        HttpStatusCodes = "NotFound",
                        Message = $"No Data Found For This Id :: {id}",
                        Error = $"No Data Found For This Id :: {id}"
                    });
                else
                {
                    data.Name = model.Name;
                    var department = this._unitOfWork.Departments.Update(data);
                    var AffectedRow = this._unitOfWork.Complete();
                    var res = _mapper.Map<DepartmentDto>(department);
                    return Ok(new ApiResponse<IEnumerable<DepartmentDto>>

                    {

                        StatusCode = 200,
                        HttpStatusCodes = "Ok",
                        Message = "Data Retrived",
                        AffectedRows = AffectedRow,
                        Data = res

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
                    Error = ex.Message
                });
            }
        }

        #endregion

        #endregion

        #endregion

    }
}
