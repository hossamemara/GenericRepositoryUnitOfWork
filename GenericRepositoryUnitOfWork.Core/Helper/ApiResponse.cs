using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericRepositoryUnitOfWork.Core.Helper
{
    public class ApiResponse<T>
    {

        public int? StatusCode { get; set; }
        public string? HttpStatusCodes{ get; set; }
        public string? Message { get; set; }
        public dynamic Data { get; set; }
        public int? AffectedRows { get; set; }
        public dynamic Error { get; set; }

        public bool ExistanceFlag { get; set; }

    }
}
