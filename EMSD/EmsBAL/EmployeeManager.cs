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
                employeeEntity.Sex = employeeEntity.Gender == true ? "Male" : "Female";

                employeeEntity.JobType = employee.JobType;
                employeeEntity.JobName = employeeEntity.JobType == 0 ? "Full-Time" : employeeEntity.JobType == 1 ? "Part-Time" :
                    employeeEntity.JobType == 2 ? "Fixed" : "Trainee";
                employeeEntity.Active = employee.IsActive;
                employeeEntity.AddressType = employee.AddressType;
                if (employeeEntity.AddressType == 0)
                    employeeEntity.category = 0;
                else
                    employeeEntity.category=Address.Residential;

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
            employeeEntity.DepartmentId = employee.DepartmentId;
            employeeEntity.DepartmentName = employee.Department.Name;
            employeeEntity.Salary = employee.Salary;
            employeeEntity.Gender = employee.Gender;
            employeeEntity.JobType = employee.JobType;
            employeeEntity.Active = employee.IsActive;
            employeeEntity.AddressType = employee.AddressType;

            if (employee.AddressType != null){
                employeeEntity.AddressFields = new AddressEntity();
                employeeEntity.AddressFields.EmployerName = employee.EmployerName;
                employeeEntity.AddressFields.Street = employee.Street;
                employeeEntity.AddressFields.Landmark = employee.Landmark;
                employeeEntity.AddressFields.City = employee.City;
                employeeEntity.AddressFields.CountryId = employee.CountryId;
            }

            return employeeEntity;
            throw new NotImplementedException();
        }
        public Employee AddEmployee(EmployeeEntities employeeEntity)
        {
            Employee employeeAdd = new Employee();
            employeeAdd.Name = employeeEntity.Name;
            employeeAdd.DepartmentId = employeeEntity.DepartmentId;
            employeeAdd.Salary = employeeEntity.Salary;
            employeeAdd.Gender = employeeEntity.Gender;
            employeeAdd.JobType = employeeEntity.JobType;
            employeeAdd.IsActive = employeeEntity.Active;
            employeeAdd.AddressType = employeeEntity.AddressType;

            if (employeeAdd.AddressType != null)
            {
                employeeAdd.EmployerName = employeeEntity.AddressFields.EmployerName;
                employeeAdd.Street = employeeEntity.AddressFields.Street;
                employeeAdd.Landmark = employeeEntity.AddressFields.Landmark;
                employeeAdd.City = employeeEntity.AddressFields.City;
                employeeAdd.CountryId = employeeEntity.AddressFields.CountryId;
            }
            unitOfWork.EmployeeRepository.Add(employeeAdd);
            unitOfWork.Save();
            return employeeAdd;
        }
        public Employee EditEmployee(int id,EmployeeEntities employeeEntity)
        {
            Employee employeeUpdate = unitOfWork.EmployeeRepository.GetById(id);
            employeeUpdate.Name = employeeEntity.Name;
            employeeUpdate.DepartmentId = employeeEntity.DepartmentId;
            employeeUpdate.Salary = employeeEntity.Salary;
            employeeUpdate.Gender = employeeEntity.Gender;
            employeeUpdate.JobType = employeeEntity.JobType;
            employeeUpdate.IsActive = employeeEntity.Active;
            employeeUpdate.AddressType = employeeEntity.AddressType;
            if(employeeUpdate.AddressType != null)
            {
                employeeUpdate.EmployerName = employeeEntity.AddressFields.EmployerName;
                employeeUpdate.Street = employeeEntity.AddressFields.Street;
                employeeUpdate.Landmark = employeeEntity.AddressFields.Landmark;
                employeeUpdate.City = employeeEntity.AddressFields.City;
                employeeUpdate.CountryId = employeeEntity.AddressFields.CountryId;
            }
            unitOfWork.EmployeeRepository.UpdateEmployee(employeeUpdate);
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
    }
}
