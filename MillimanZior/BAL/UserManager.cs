using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;//UnitOfWork
using Entities;//UserEntities

namespace BAL
{
    public partial class UserManager
    {
        private UnitOfWork unitOfWork;
        public UserManager()
        {
            unitOfWork = new UnitOfWork();
        }
        /// <summary>
        /// this method get list of user data
        /// </summary>
        /// <returns> userentities list type</returns>
        public IEnumerable<UserEntities> GetUsers()
        {
            List<UserEntities> lstUserEntities = new List<UserEntities>();
            List<AspNetUser> lstUser = unitOfWork.UserRepository.GetAll().ToList();

            foreach (AspNetUser user in lstUser)
            {
                UserEntities userEntity = new UserEntities();
                userEntity.Id = user.UserId;
                userEntity.FirstName = user.FirstName;
                userEntity.LastName = user.LastName;
                userEntity.Email = user.Email;
                userEntity.Status = user.Status;
                lstUserEntities.Add(userEntity);
            }
            return lstUserEntities;
        }

        public UserEntities GetUser(int id)
        {
            AspNetUser user = unitOfWork.UserRepository.GetById(id);
            UserEntities userEntity = new UserEntities();
            userEntity.FirstName = user.FirstName;
            userEntity.LastName = user.LastName;
            userEntity.Email = user.Email;
            userEntity.Status = user.Status;
            return userEntity;
            throw new NotImplementedException();

        }

        public AspNetUser AddUser(UserEntities userEntity)
        {
            AspNetUser userAdd = new AspNetUser();
            userAdd.FirstName = userEntity.FirstName;
            userAdd.LastName = userEntity.LastName;
            userAdd.Email = userEntity.Email;
            userAdd.Status = userEntity.Status;
            unitOfWork.UserRepository.Add(userAdd);
            unitOfWork.Save();
            return userAdd;
        }

        public AspNetUser EditUser(int id, UserEntities userEntity)
        {
            AspNetUser userUpdate = unitOfWork.UserRepository.GetById(id);
            userUpdate.FirstName = userEntity.FirstName;
            userUpdate.LastName = userEntity.LastName;
            userUpdate.Email = userEntity.Email;
            userUpdate.Status = userEntity.Status;
            unitOfWork.UserRepository.UpdateUser(userUpdate);
            unitOfWork.Save();
            return userUpdate;
        }

        public AspNetUser DeleteUser(int id)//,UserEntities userEntity
        {
            AspNetUser userDelete = unitOfWork.UserRepository.GetById(id);
            if (userDelete != null)
            {
                unitOfWork.UserRepository.Delete(userDelete);
                unitOfWork.Save();
            }
            return userDelete;
        }
    }
}
