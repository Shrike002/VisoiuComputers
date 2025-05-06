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
    }
}
