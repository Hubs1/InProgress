using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmsDAL.Repository
{
    public class DepartmentRepository : RepositoryBase<Department>
    {
        public DepartmentRepository(EmployeeDepartment objContext)
            : base(objContext) { }

        public int AddDepartment(Department department)
        {
            _objDbContext.Departments.Add(department);
            _objDbContext.SaveChanges();
            return _objDbContext.Departments.Select(x => x.Id).Max();
        }

        public void UpdateDepartment(Department department)
        {
            Department departmentOld = _objDbContext.Departments.FirstOrDefault(x => x.Id == department.Id);
            _objDbContext.SaveChanges();
        }
    }
}
