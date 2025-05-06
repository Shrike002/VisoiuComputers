using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisoiuComputers.Core.Dtos.Common;
using VisoiuComputers.Core.Dtos.Requests;
using VisoiuComputers.Core.Mapping;
using VisoiuComputers.Database.Entities;
using VisoiuComputers.Database.Repositories;

namespace VisoiuComputers.Core.Services
{
    public class ComponentService
    {
        private readonly ComponentRepository _repository;

        public ComponentService(ComponentRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ComponentDto>> GetComponentsAsync()
        {
            var components = await _repository.GetComponentsAsync();

            return MapComponentToDto(components);
        }

        public async Task<ComponentDto?> GetComponentsAsync(int id)
        {
            var components = await _repository.GetComponentsAsync(id);

            if (components == null)
                return null;

            return MapComponentToDto(components);
        }

        private IEnumerable<ComponentDto> MapComponentToDto(IEnumerable<Component> components)
        {
            return components.Select(component => MapComponentToDto(component)).ToList();
        }

        private ComponentDto MapComponentToDto(Component component)
        {
            return new ComponentDto
            {
                Id = component.Id,
                CreatedAt = component.CreatedAt,
                ModifiedAt = component.ModifiedAt,

                Name = component.Name,
                Price = component.Price,
                CategoryId = component.CategoryId,
                CategoryName = component.Category.Name

            };
        }
        public async Task AddComponentAsync(AddComponentRequest request)
        {
            Component component;

            component = request.ToEntity();

            _repository.Insert(component);
            await _repository.SaveChangesAsync();

           // return await _repository.SaveChangesAsync();
        }
    }
}
