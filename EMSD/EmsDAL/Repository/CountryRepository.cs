using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmsDAL.Repository
{
    public class CountryRepository:RepositoryBase<Country>
    {
        public CountryRepository(EmployeeDepartment objContext)
            : base(objContext) { }
    }
}
