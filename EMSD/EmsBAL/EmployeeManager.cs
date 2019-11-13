using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;//Regex
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
                employeeEntity.Sex = employeeEntity.Gender == 1 ? "Male" : "Female";

                employeeEntity.JobType = employee.JobType;
                employeeEntity.JobName = employeeEntity.JobType == 1 ? "Full-Time" : employeeEntity.JobType == 2 ? "Part-Time" :
                    employeeEntity.JobType == 3 ? "Fixed" : "Trainee";
                employeeEntity.Active = employee.IsActive;
                employeeEntity.AddressType = employee.AddressType;
                employeeEntity.DOB = employee.DOB.ToString("MMM dd, yyyy");//Use for display this format [Jan 01, 2019] Date of Birth on employee Index.cshtml [format:"dd-MMMM-yyyy"]
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

            //employeeEntity.BirthDate = employee.DOB;
            employeeEntity.DOB = employee.DOB.ToString("dd-MMM-yyyy");//convert date to string from database and store in [DOB]entity
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

            //employeeAdd.DOB = employeeEntity.BirthDate;
            employeeAdd.DOB = Convert.ToDateTime(employeeEntity.DOB);//convert string to date from [DOB]entity] & store in database
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
            //employeeUpdate.DOB = employeeEntity.BirthDate;
            employeeUpdate.DOB = Convert.ToDateTime(employeeEntity.DOB);//convert string to date from [DOB]entity] & store in database to UPDATE
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

        /// <summary>
        /// This method returns the list of all employees from database
        /// </summary>
        /// <remarks>Author: Harri</remarks>
        /// <param name="searchString">string to search</param>
        /// <param name="sortField">field to sort</param>
        /// <param name="sortDirection">sort direction</param>
        /// <param name="start">start from</param>
        /// <param name="length">length of records</param>
        /// <returns>Employee List</returns>
        public List<EmployeeEntities> EmployeeList(string searchString, string sortField, string sortDirection, int start, int length)
        {
            List<Employee> employees = new List<Employee>();

            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var regex = new Regex(Regex.Escape(searchString), RegexOptions.IgnoreCase);
                employees = unitOfWork.EmployeeRepository.Get(
                    e => e.Name.Contains(searchString) || e.Department.Name.Contains(searchString) ||
                    e.Salary.ToString().Contains(searchString) || e.Gender.ToString().Contains(searchString) ||
                    e.JobType.ToString().Contains(searchString) || e.IsActive,
                    unitOfWork.EmployeeRepository.GetOrderBy(sortField, sortDirection)).ToList();
            }
            List<Employee> displayEmployees = new List<Employee>();
            displayEmployees = employees.Skip(start).Take(length).ToList();
            //var type = Enum.GetValues(typeof(Job));
            var employeeResult = from e in displayEmployees
                                 select new EmployeeEntities
                                 {
                                     Name = e.Name,
                                     DepartmentId = e.DepartmentId,
                                     DepartmentName = e.Department.Name,
                                     Salary = e.Salary,
                                     Sex = Enum.GetName(typeof(Genders), e.Gender),
                                     JobName = e.JobType == 1 ? Job.FullTime.Description() : e.JobType == 2 ? Job.PartTime.Description() :
                                     e.JobType == 3 ? Job.Permanent.Description() : Job.Temporary.Description(),
                                     Active = e.IsActive
                                 };  //JobName = Enum.GetName(typeof(Job),e.JobType),//get original name of each job enum
                                     /*JobName = e.JobType == 1 ? Job.FullTime.Description() : e.JobType == 2 ? Job.PartTime.Description() :
                                     e.JobType == 3 ? Job.Permanent.Description() : Job.Temporary.Description(),*/
            return employeeResult.ToList();//return EmployeeEntities list of displayEmployees(10) records
        }
    }
}