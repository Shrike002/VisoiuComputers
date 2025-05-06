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
    }
}
