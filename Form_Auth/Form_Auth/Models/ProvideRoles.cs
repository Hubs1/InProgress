using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;// access RoleProvider

namespace Form_Auth.Models
{
    public class ProvideRoles : RoleProvider //implementation all abstract members from RoleProvider abstract class to remove compile-time error
    {
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            using (var userRoles = new OfficeContext())
            {
                var roles = (from User in userRoles.Users
                             join UserRole in userRoles.UserRoles on User.Id equals UserRole.UserId
                             where User.UserName==username // execute successfully with where clause [get only login role]
                             select UserRole.Role).ToArray();
                return roles;
            }
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}