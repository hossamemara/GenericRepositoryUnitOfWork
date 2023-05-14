using AutoMapper;
using GenericRepositoryUnitOfWork.Core.Dto;
using GenericRepositoryUnitOfWork.Core.FilterModels;
using GenericRepositoryUnitOfWork.Core.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GenericRepositoryUnitOfWork.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        #region Private Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public EmployeesController(IUnitOfWork unitOfWork, IMapper mapper)
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
                var data = await this._unitOfWork.Employees.MatchAll(null, filter.Take, filter.Skip, A => A.Id, filter.OrderByDirection);
                var res = _mapper.Map<IEnumerable<EmployeeDto>>(data);
                if (res.Count() == 0)
                    return NotFound("No Data Found For Employees");
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
                var data = await this._unitOfWork.Employees.GetAllAsync();
                var res = _mapper.Map<IEnumerable<EmployeeDto>>(data);
                if (res.Count() == 0)
                    return NotFound(new ApiResponse<string>

                    {

                        StatusCode = 404,
                        HttpStatusCodes = "NotFound",
                        Message = "No Data Found For Employees",
                        Error = "No Data Found For Employees"
                    });
                else
                    return Ok(new ApiResponse<IEnumerable<EmployeeDto>>

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
                var data = this._unitOfWork.Employees.GetByIdAsync(id);
                var AffectedRow = this._unitOfWork.Complete();
                var res = _mapper.Map<EmployeeDto>(data);
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

        #region MatchFirst Action

        [HttpGet("MatchFirst/{id}")]
        public async Task<IActionResult> MatchFirst(int id)
        {
            try
            {
                var data = await this._unitOfWork.Employees.MatchFirst(E => E.Id == id);
                var res = _mapper.Map<EmployeeDto>(data);
                List<EmployeeDto> AffectedRows = new List<EmployeeDto>() { res };
                if (res == null)
                    return NotFound(new ApiResponse<string>

                    {

                        StatusCode = 404,
                        HttpStatusCodes = "NotFound",
                        Message = $"No Data Found For This Id :: {id}",
                        Error = $"No Data Found For This Id :: {id}"
                    });
                else
                    return Ok(new ApiResponse<IEnumerable<EmployeeDto>>

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
                int count = this._unitOfWork.Employees.Count();


                if (count == 0)
                    return NotFound(new ApiResponse<string>

                    {

                        StatusCode = 404,
                        HttpStatusCodes = "NotFound",
                        Message = $"No Data Found For Employee",
                        Error = $"No Data Found For Employee"
                    });
                else
                    return Ok(new ApiResponse<IEnumerable<EmployeeDto>>

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

        #region CountWithMatch Action

        [HttpGet("CountWithMatch/{departmentName}")]
        public IActionResult CountWithMatch(string departmentName)
        {
            try
            {
                int count = this._unitOfWork.Employees.CountWithMatch(E => E.Department.Name == departmentName);


                if (count == 0)
                    return NotFound(new ApiResponse<string>

                    {

                        StatusCode = 404,
                        HttpStatusCodes = "NotFound",
                        Message = $"No Data Found For Employees",
                        Error = $"No Data Found For Employees"
                    });
                else
                    return Ok(new ApiResponse<IEnumerable<EmployeeDto>>

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

        public IActionResult Add(EmployeeVM model)
        {
            try
            {
                var data = _mapper.Map<Employee>(model);
                var department = this._unitOfWork.Employees.Add(data);
                var AffectedRow = this._unitOfWork.Complete();
                var res = _mapper.Map<EmployeeDto>(department);
                return Ok(new ApiResponse<EmployeeDto>

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

        public IActionResult AddRange(IEnumerable<EmployeeVM> model)
        {
            try
            {
                var data = _mapper.Map<IEnumerable<Employee>>(model);
                var department = this._unitOfWork.Employees.AddRange(data);
                var AffectedRow = this._unitOfWork.Complete();
                var res = _mapper.Map<IEnumerable<EmployeeDto>>(department);
                return Ok(new ApiResponse<IEnumerable<EmployeeDto>>

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
                var data = this._unitOfWork.Employees.GetByIdAsync(id);
                if (data == null)
                    return NotFound($"No Data Found For This Id :: {id}");
                else
                {
                    var department = this._unitOfWork.Employees.Delete(data);
                    var res = _mapper.Map<DepartmentDto>(department);
                    return Ok(res);
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

        #region Put Action

        #region Update Action
        [HttpPut("Update/{id}")]

        public IActionResult Update(int id, EmployeeVM model)
        {
            try
            {
                var data = this._unitOfWork.Employees.GetByIdAsync(id);
                data.LastName = model.LastName;
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
                    var employee = this._unitOfWork.Employees.Update(data);
                    var AffectedRow = this._unitOfWork.Complete();
                    var res = _mapper.Map<EmployeeDto>(employee);

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
