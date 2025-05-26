using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisoiuComputers.Database.Context;
using VisoiuComputers.Database.Entities;

namespace VisoiuComputers.Database.Repositories
{
    public class CategoryRepository : BaseRepository<Category>
    {
        public CategoryRepository(VisoiuComputersDatabaseContext dbContext) : base(dbContext)
        {

        }
        public async Task<Category?> GetCategoriesAsync(int id)
        {
            return await GetRecords().FirstOrDefaultAsync(nt => nt.Id == id);
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await GetRecords().ToListAsync();
        }

        public async Task<(IEnumerable<Category> categories, int totalCount)> GetCategoriesAsync(int? pageNumber = null, int? pageSize = null)
        {
            IQueryable<Category> query = GetRecords();

            var totalCount = await query.CountAsync();

            if (pageNumber.HasValue && pageSize.HasValue && pageNumber > 0 && pageSize > 0)
            {
                query = query
                    .Skip((pageNumber.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);
            }

            var categories = await query.ToListAsync();

            return (categories, totalCount);
        }
    }
}