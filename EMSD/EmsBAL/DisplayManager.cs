using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EmsDAL;
using EmsEntities;

namespace EmsBAL
{
    public partial class DisplayManager
    {
        private UnitOfWork unitOfWork;
        public DisplayManager()
        {
            unitOfWork = new UnitOfWork();
        }
        public IEnumerable<EmployeeEntities> AllEmployees()
        {
            List<EmployeeEntities> lstEmployeeEntities = new List<EmployeeEntities>();
            List<Employee> lstEmployee = unitOfWork.EmployeeRepository.GetAll().ToList();

            foreach (Employee employee in lstEmployee)
            {
                EmployeeEntities employeeEntity = new EmployeeEntities();
                employeeEntity.EId = employee.EId;
                employeeEntity.Name = employee.Name;
                employeeEntity.DepartmentId = employee.DepartmentId;
                employeeEntity.DepartmentName = employee.Department.Name;
                employeeEntity.Salary = employee.Salary;
                employeeEntity.Gender = employee.Gender;
                employeeEntity.Sex = employeeEntity.Gender == 1 ? "Male" : "Female";
                employeeEntity.JobType = employee.JobType;
                employeeEntity.JobName = employeeEntity.JobType == 1 ? "Full-Time" : employeeEntity.JobType == 2 ? "Part-Time" :
                    employeeEntity.JobType == 3 ? "Fixed" : "Trainee";
                employeeEntity.Active = employee.IsActive;
                employeeEntity.AddressType = employee.AddressType;
                lstEmployeeEntities.Add(employeeEntity);
            }
            return lstEmployeeEntities;
        }
        public IEnumerable<DepartmentEntities> AllDepartments()
        {
            List<DepartmentEntities> listDepartmentEntities = new List<DepartmentEntities>();
            List<Department> listDepartments = unitOfWork.DepartmentRepository.GetAll().ToList();

            foreach (Department d in listDepartments)
            {
                DepartmentEntities departmentEntity = new DepartmentEntities();
                EmployeeEntities employeeEntities = new EmployeeEntities();
                departmentEntity.Id = d.Id;
                departmentEntity.Name = d.Name;
                departmentEntity.Code = d.Code;
                departmentEntity.Active = d.IsActive;
                departmentEntity.EmployeeNames = string.Join(", ", d.Employees.Select(e => e.Name).ToArray());
                listDepartmentEntities.Add(departmentEntity);
            }
            return listDepartmentEntities;
        }
    }
}
