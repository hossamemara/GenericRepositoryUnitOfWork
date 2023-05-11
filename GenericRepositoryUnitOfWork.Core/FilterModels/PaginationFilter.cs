using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericRepositoryUnitOfWork.Core.FilterModels
{
    public class PaginationFilter
    {
        public int? Take { get; set; }
        public int? Skip { get; set; }

        //public string? MatchKey { get; set; }
        //public string? MatchValue { get; set; }
        //public string? OrderByKey { get; set; }
        //public string? OrderByValue { get; set; }
        public string? OrderByDirection { get; set; }

    }
}
