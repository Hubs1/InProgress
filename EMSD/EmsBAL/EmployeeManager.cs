using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using EmsDAL;
using EmsEntities;
using static EmsEntities.EmployeeEntities;// for access enum Job

namespace EmsBAL
{
    public partial class EmployeeManager
    {
        private UnitOfWork unitOfWork;
        public EmployeeManager()
        {
            unitOfWork = new UnitOfWork();
        }
        public IEnumerable<EmployeeEntities> GetEmployees()
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
                employeeEntity.JobType = employee.JobType;
                employeeEntity.Active = employee.IsActive;
                lstEmployeeEntities.Add(employeeEntity);
            }
            return lstEmployeeEntities;
        }
        public EmployeeEntities GetEmployee(int id)
        {
            Employee employee = unitOfWork.EmployeeRepository.GetById(id);
            EmployeeEntities employeeEntity = new EmployeeEntities();
            employeeEntity.EId = employee.EId;
            employeeEntity.Name = employee.Name;
            employeeEntity.DepartmentName = employee.Department.Name;
            employeeEntity.Salary = employee.Salary;
            employeeEntity.Gender = employee.Gender;
            employeeEntity.JobType = employee.JobType;
            employeeEntity.Active = employee.IsActive;
            return employeeEntity;
            throw new NotImplementedException();
        }
        public Employee AddEmployee(EmployeeEntities employeeEntity)
         {
            Employee employeeAdd = new Employee();
            employeeAdd.Name = employeeEntity.Name;
            employeeAdd.DepartmentId = employeeEntity.DepartmentId;
            unitOfWork.EmployeeRepository.Add(employeeAdd);
            unitOfWork.Save();
            return employeeAdd;
        }
        public Employee EditEmployee(int id,EmployeeEntities employeeEntity)
        {
            Employee employeeUpdate = unitOfWork.EmployeeRepository.GetById(id);
            employeeUpdate.Name = employeeEntity.Name;
            employeeUpdate.DepartmentId = employeeEntity.DepartmentId;
            //unitOfWork.EmployeeRepository.UpdateEmployee(employeeUpdate);            
            unitOfWork.Save();
            return employeeUpdate;
        }
        public Employee DeleteEmployee(int Eid)//,EmployeeEntities employeeEntity
        {
            Employee employeeDelete = unitOfWork.EmployeeRepository.GetById(Eid);
            if(employeeDelete!=null)
            {
                unitOfWork.EmployeeRepository.Delete(employeeDelete);
                unitOfWork.Save();
            }
            return employeeDelete;
        }
        public object enumData = from Job j in Enum.GetNames(typeof(Job))
                       select new { id = (int)j, name = j.ToString() };
    }
}
