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
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AppDbContext _Context;

        public DepartmentRepository(AppDbContext Context)
        {
            _Context = Context; // Ask CLR Create Object From AppDbContext
        }

        public IEnumerable<Department> GetAll()
        {
            return _Context.Departments.ToList();
        }

        public Department Get(int id)
        {
            return _Context.Departments.Find(id);
        }

        public int Add(Department entity)
        {
            _Context.Departments.Add(entity);
            return _Context.SaveChanges();
        }

        public int Update(Department entity)
        {
            _Context.Departments.Update(entity);
            return _Context.SaveChanges();
        }

        public int Delete(Department entity)
        {
            _Context.Departments.Remove(entity);
            return _Context.SaveChanges();
        }
    }
}
