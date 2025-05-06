using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisoiuComputers.Core.Dtos.Common
{
    public class CategoryDto : BaseDto
    {
        public string Name { get; set; }
        public List<ComponentDto> Components { get; set; }
    }
}
