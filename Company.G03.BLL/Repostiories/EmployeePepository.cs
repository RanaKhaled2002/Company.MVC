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
    public class EmployeePepository : GenericRepository<Employee>,IEmployeeRepository
    {

        public EmployeePepository(AppDbContext context) : base(context)
        {
        }

    }
}
