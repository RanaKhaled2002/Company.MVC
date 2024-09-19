using Company.G03.BLL.Interfaces;
using Company.G03.DAL.Data.Contexts;
using Company.G03.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G03.BLL.Repostiories
{
    public class EmployeePepository : GenericRepository<Employee>, IEmployeeRepository
    {

        public EmployeePepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Employee>> GetByNameAsync(string name)
        {
            return await _context.Employees.Include(E => E.WorkFor).AsNoTracking().Where(E => E.Name.ToLower().Contains(name.ToLower())).ToListAsync();
        }
    }
}
