using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisoiuComputers.Core.Dtos.Requests
{
    public class GetComponentsRequest
    {
        public string? Name { get; set; }
        public int? CategoryId { get; set; }
        public string? SortBy { get; set; }
        public string? SortOrder { get; set; } = "asc";
    }
}