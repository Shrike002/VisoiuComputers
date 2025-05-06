using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisoiuComputers.Core.Dtos.Requests;
using VisoiuComputers.Database.Entities;

namespace VisoiuComputers.Core.Mapping
{
    public static class CategoryMappingExtension
    {
        public static Category ToEntity(this AddCategoryRequest categoryDto)
        {
            Category category = new Category();
            category.Name = categoryDto.Name;

            return category;
        }
    }
}
