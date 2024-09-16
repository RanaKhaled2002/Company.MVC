using Company.G03.BLL.Interfaces;
using Company.G03.BLL.Repostiories;
using Company.G03.DAL.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G03.BLL
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly AppDbContext _context;
        private IEmployeeRepository _employeeRepository;
        private IDepartmentRepository _departmentRepository;


        public UnitOfWork(AppDbContext context) 
        {
            _context = context;
            _employeeRepository = new EmployeePepository(context);
            _departmentRepository = new DepartmentRepository(context);
        }

        public IEmployeeRepository EmployeeRepository => _employeeRepository;

        public IDepartmentRepository DepartmentRepository => _departmentRepository;

    }
}
