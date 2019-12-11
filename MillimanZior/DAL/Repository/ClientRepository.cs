using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class ClientRepository : Repository<Client>
    {
        public ClientRepository(ZiorEntities objContext)

            : base(objContext) { }
    }
}
