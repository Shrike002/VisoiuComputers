using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using VisoiuComputers.Core.Dtos.Common;
using VisoiuComputers.Core.Dtos.Requests;
using VisoiuComputers.Core.Mapping;
using VisoiuComputers.Database.Entities;
using VisoiuComputers.Database.Repositories;

namespace VisoiuComputers.Core.Services
{
    public class CategoryService
    {
        private readonly CategoryRepository _repository;

        public CategoryService(CategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
        {
            var categories = await _repository.GetCategoriesAsync();

            return MapCategoryToDto(categories);
        }

        public async Task<object> GetCategoriesAsync(int? pageNumber = null, int? pageSize = null)
        {
            var (categories, totalCount) = await _repository.GetCategoriesAsync(pageNumber, pageSize);

            var categoryDtos = MapCategoryToDto(categories);

            if (pageNumber.HasValue && pageSize.HasValue && pageNumber > 0 && pageSize > 0)
            {
                var totalPages = (int)Math.Ceiling((double)totalCount / pageSize.Value);

                return new
                {
                    data = categoryDtos,
                    pageNumber = pageNumber.Value,
                    pageSize = pageSize.Value,
                    totalRecords = totalCount,
                    totalPages = totalPages,
                    hasNextPage = pageNumber.Value < totalPages,
                    hasPreviousPage = pageNumber.Value > 1
                };
            }

            return categoryDtos;
        }

        public async Task<CategoryDto?> GetCategoriesAsync(int id)
        {
            var categories = await _repository.GetCategoriesAsync(id);

            if (categories == null)
                return null;

            return MapCategoryToDto(categories);
        }

        private IEnumerable<CategoryDto> MapCategoryToDto(IEnumerable<Category> categories)
        {
            return categories.Select(category => MapCategoryToDto(category)).ToList();
        }

        private CategoryDto MapCategoryToDto(Category category)
        {
            return new CategoryDto
            {
                Id = category.Id,
                CreatedAt = category.CreatedAt,
                ModifiedAt = category.ModifiedAt,

                Name = category.Name
            };
        }
        public async Task AddCategoryAsync(AddCategoryRequest request)
        {
            Category category;

            category = request.ToEntity();

            _repository.Insert(category);
            await _repository.SaveChangesAsync();

            // return await _repository.SaveChangesAsync();
        }
    }
}