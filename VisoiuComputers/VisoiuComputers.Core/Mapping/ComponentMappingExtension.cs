using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisoiuComputers.Core.Dtos.Requests;
using VisoiuComputers.Database.Entities;

namespace VisoiuComputers.Core.Mapping
{
    public static class ComponentMappingExtension
    {
        public static Component ToEntity(this AddComponentRequest componentDto)
        {
            Component component = new Component();
            component.Name = componentDto.Name;
            component.Price = componentDto.Price;
            component.CategoryId = componentDto.CategoryId;


            return component;
        }
    }
}
