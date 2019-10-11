using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmsDAL.Repository
{
    public class EmployeeRepository : RepositoryBase<Employee>
    {
        public EmployeeRepository(EmployeeDepartment objContext)
            : base(objContext) { }

        public int AddEmployee(Employee employee)
        {
            _objDbContext.Employees.Add(employee);
            _objDbContext.SaveChanges();
            return _objDbContext.Employees.Select(x => x.EId).Max();
        }

        public void UpdateEmployee(Employee employee) {
            Employee employeeOld = _objDbContext.Employees.FirstOrDefault(x => x.EId == employee.EId);
            _objDbContext.SaveChanges();
        }
    }
}
