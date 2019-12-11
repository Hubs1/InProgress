using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Repository;//AgencyRepository
namespace DAL
{
    public class UnitOfWork : IDisposable
    {
        /// <summary>
        /// master database context
        /// </summary>
        private ZiorEntities objDb;//Dbcontext

        /// <summary>
        /// private agency repository property
        /// </summary>
        private AgencyRepository agencyRepository;

        /// <summary>
        /// private client repository property
        /// </summary>
        private ClientRepository clientRepository;

        /// <summary>
        /// private user repository property
        /// </summary>
        private UserRepository userRepository;
        public UnitOfWork()
        {
            objDb = new ZiorEntities();
        }
        public AgencyRepository AgencyRepository
        {
            get
            {
                if (this.agencyRepository == null)
                {
                    this.agencyRepository = new AgencyRepository(this.objDb);
                }
                return this.agencyRepository;
            }
        }
        public ClientRepository ClientRepository
        {
            get
            {
                if (this.clientRepository == null)
                {
                    this.clientRepository = new ClientRepository(this.objDb);
                }
                return this.clientRepository;
            }
        }
        public UserRepository UserRepository
        {
            get
            {
                if (this.userRepository == null)
                {
                    this.userRepository = new UserRepository(this.objDb);
                }
                return this.userRepository;
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
