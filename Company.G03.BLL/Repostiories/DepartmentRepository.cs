using Company.G03.BLL.Interfaces;
using Company.G03.DAL.Data.Contexts;
using Company.G03.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G03.BLL.Repostiories
{
    public class DepartmentRepository : GenericRepository<Department>,IDepartmentRepository
    {

        public DepartmentRepository(AppDbContext Context) : base(Context)
        {
           // Ask CLR Create Object From AppDbContext
        }
    }
}
