using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisoiuComputers.Database.Entities;

namespace VisoiuComputers.Core.Dtos.Common
{
    public class ComponentDto : BaseDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }
    }
}
