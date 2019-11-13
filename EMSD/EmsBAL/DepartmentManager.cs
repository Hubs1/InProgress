using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using EmsDAL;
using EmsEntities;
using System.Text.RegularExpressions;

namespace EmsBAL
{
    public partial class DepartmentManager
    {
        private UnitOfWork unitOfWork;
        private EmployeeDepartment employeeDepartment=new EmployeeDepartment();
        public DepartmentManager()
        {
            unitOfWork = new UnitOfWork();
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
                departmentEntity.EmployeeNames = string.Join(", ", d.Employees.Select(e => e.Name).ToArray()); //Get comma separated employee names

                //object[] array = new object[] { };//Using display comma separated those EmployeeNames which have same departments
                //array = employeeDepartment.Employees.Where(e => e.DepartmentId == d.Id).Select(e => e.Name).ToArray();
                //departmentEntity.EmployeeNames = string.Join(", ", array);

                //departmentEntity.EmployeeNames = unitOfWork.EmployeeRepository.EmployeeNames(d.Id);//Using display comma separated
                listDepartmentEntities.Add(departmentEntity);
            }
            return listDepartmentEntities;
        }
        public DepartmentEntities GetDepartment(int id)
        {
            Department department = unitOfWork.DepartmentRepository.GetById(id);
            DepartmentEntities departmentEntity = new DepartmentEntities();
            departmentEntity.Id = department.Id;
            departmentEntity.Name = department.Name;
            departmentEntity.Code = department.Code;
            departmentEntity.Active = department.IsActive;
            return departmentEntity;
            throw new NotImplementedException();
        }
        public Department AddDepartment(DepartmentEntities departmenteEntity)
        {
            Department departmentAdd = new Department();
            departmentAdd.Name = departmenteEntity.Name;
            departmentAdd.Code = departmenteEntity.Code;
            departmentAdd.IsActive = departmenteEntity.Active;
            unitOfWork.DepartmentRepository.Add(departmentAdd);
            unitOfWork.Save();
            return departmentAdd;
        }
        public Department EditDepartment(int id, DepartmentEntities departmenteEntity)
        {
            Department departmentUpdate = unitOfWork.DepartmentRepository.GetById(id);
            departmentUpdate.Name = departmenteEntity.Name;
            departmentUpdate.Code = departmenteEntity.Code;
            departmentUpdate.IsActive = departmenteEntity.Active;
            unitOfWork.Save();
            return departmentUpdate;
        }
        public Department DeleteDepartment(int id)//,DepartmentEntities departmenteEntity
        {
            try
            {
                Department departmentDelete = unitOfWork.DepartmentRepository.GetById(id);
                unitOfWork.DepartmentRepository.Delete(departmentDelete);
                unitOfWork.Save();
                return departmentDelete;
            }catch(Exception e)
            {
                throw(e);
            }
        }
        public IQueryable ActiveDepartments()
        {
            return unitOfWork.DepartmentRepository.ActiveDepartment();
        }

        /// <summary>
        /// This method returns the list of all departments
        /// </summary>
        /// <remarks>Author: Harri</remarks>
        /// <param name="searchString">string to search</param>
        /// <param name="sortField">field to sort</param>
        /// <param name="sortDirection">sort direction</param>
        /// <param name="start">start from</param>
        /// <param name="length">length of records</param>
        /// <returns>list of Departmenta</returns>
        public List<Department> DepartmentsServer(string searchString, string sortField, string sortDirection, int start, int length)
        {
            List<Department> departments = new List<Department>();
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var regex = new Regex(Regex.Escape(searchString), RegexOptions.IgnoreCase);

                departments = unitOfWork.DepartmentRepository.Get(d => d.Name.Contains(searchString) || d.Code.Contains(searchString),
                    unitOfWork.DepartmentRepository.GetOrderBy(sortField, sortDirection)).ToList();
            }

            return departments;
        }
    }
}