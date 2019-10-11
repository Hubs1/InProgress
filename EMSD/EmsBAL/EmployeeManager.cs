using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EmsDAL;
using EmsEntities;

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
                //employeeEntity.DepartmentName = employee.Department.Name;
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
    }
}
