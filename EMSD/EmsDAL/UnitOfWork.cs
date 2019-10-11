using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Data.Entity;
using System.Data;
using System.Threading.Tasks;
using EmsDAL.Repository;


namespace EmsDAL
{
    public class UnitOfWork : IDisposable
    {
        private EmployeeDepartment objDb;// = new EmployeeDepartment();//Dbcontext
        private EmployeeRepository employeeRepository;
        private DepartmentRepository departmentRepository;
        public UnitOfWork()
        {
            objDb = new EmployeeDepartment();
        }
        public EmployeeRepository EmployeeRepository
        {
            get
            {
                if (this.employeeRepository == null)
                {
                    this.employeeRepository = new EmployeeRepository(this.objDb);
                }

                return this.employeeRepository;
            }
        }
        public DepartmentRepository DepartmentRepository
        {
            get
            {
                if (this.departmentRepository == null)
                {
                    this.departmentRepository = new DepartmentRepository(this.objDb);
                }

                return this.departmentRepository;
            }
        }
        public void Save()
        {
            objDb.SaveChanges();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);    
        }

        protected virtual void Dispose(bool dis)
        {
            //if (dis)
            //    objDb.Dispose();
            //throw new NotImplementedException();
        }
    }
}
