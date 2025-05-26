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
    public class ComponentRepository : BaseRepository<Component>
    {
        public ComponentRepository(VisoiuComputersDatabaseContext dbContext) : base(dbContext)
        {

        }
        public async Task<Component?> GetComponentsAsync(int id)
        {
            return await GetRecords().Include(nt => nt.Category).FirstOrDefaultAsync(nt => nt.Id == id);
        }

        public async Task<IEnumerable<Component>> GetComponentsAsync()
        {
            return await GetRecords().Include(nt => nt.Category).ToListAsync();
        }

        public async Task<IEnumerable<Component>> GetComponentsAsync(string? name = null, int? categoryId = null, string? sortBy = null, string? sortOrder = "asc")
        {
            IQueryable<Component> query = GetRecords().Include(nt => nt.Category);

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(c => c.Name.ToLower().Contains(name.ToLower()));
            }

            if (categoryId.HasValue)
            {
                query = query.Where(c => c.CategoryId == categoryId.Value);
            }

            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy.ToLower())
                {
                    case "name":
                        query = sortOrder?.ToLower() == "desc"
                            ? query.OrderByDescending(c => c.Name)
                            : query.OrderBy(c => c.Name);
                        break;
                    case "price":
                        query = sortOrder?.ToLower() == "desc"
                            ? query.OrderByDescending(c => c.Price)
                            : query.OrderBy(c => c.Price);
                        break;
                    case "createdat":
                        query = sortOrder?.ToLower() == "desc"
                            ? query.OrderByDescending(c => c.CreatedAt)
                            : query.OrderBy(c => c.CreatedAt);
                        break;
                    case "categoryid":
                        query = sortOrder?.ToLower() == "desc"
                            ? query.OrderByDescending(c => c.CategoryId)
                            : query.OrderBy(c => c.CategoryId);
                        break;
                    default:
                        query = query.OrderBy(c => c.Id);
                        break;
                }
            }

            return await query.ToListAsync();
        }
    }
}