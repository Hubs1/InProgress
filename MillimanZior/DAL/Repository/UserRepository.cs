using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class UserRepository:Repository<AspNetUser>
    {
        public UserRepository(ZiorEntities objContext)

            : base(objContext) { }

        public void UpdateUser(AspNetUser user)
        {

            AspNetUser userOld = _objDbContext.AspNetUsers.FirstOrDefault(u => u.Id == user.Id);

            _objDbContext.SaveChanges();

        }
    }
}
