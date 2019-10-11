using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using EmsDAL;
using EmsEntities;

namespace EmsBAL
{
    public partial class DepartmentManager
    {
        private UnitOfWork unitOfWork;
        public DepartmentManager()
        {
            unitOfWork = new UnitOfWork();
        }
        public IEnumerable<Department> AllDepartments()
        {
            return unitOfWork.DepartmentRepository.GetAll();
        }
        public DepartmentEntities GetDepartment(int id)
        {
            Department department = unitOfWork.DepartmentRepository.GetById(id);
            DepartmentEntities departmentEntity = new DepartmentEntities();
            departmentEntity.Id = department.Id;
            departmentEntity.Name = department.Name;
            departmentEntity.Code = department.Code;
            return departmentEntity;
            throw new NotImplementedException();
        }
        public Department AddDepartment(DepartmentEntities departmenteEntity)
        {
            Department departmentAdd = new Department();
            departmentAdd.Name = departmenteEntity.Name;
            departmentAdd.Code = departmenteEntity.Code;
            unitOfWork.DepartmentRepository.Add(departmentAdd);
            unitOfWork.Save();
            return departmentAdd;
        }
        public Department EditDepartment(int id, DepartmentEntities departmenteEntity)
        {
            Department departmentUpdate = unitOfWork.DepartmentRepository.GetById(id);
            departmentUpdate.Name = departmenteEntity.Name;
            departmentUpdate.Code = departmenteEntity.Code;
            unitOfWork.Save();
            return departmentUpdate;
        }
        public Department DeleteDepartment(int id)//,DepartmentEntities departmenteEntity
        {
            Department departmentDelete = unitOfWork.DepartmentRepository.GetById(id);
            unitOfWork.DepartmentRepository.Delete(departmentDelete);
            unitOfWork.Save();
            return departmentDelete;
        }
    }
}
