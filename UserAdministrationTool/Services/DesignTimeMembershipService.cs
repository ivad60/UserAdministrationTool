using System;
using System.Collections.Generic;
using System.Web.Security;
using UserAdministrationTool.Interfaces;

namespace UserAdministrationTool.Services
{
    public class DesignTimeMembershipService : IMembershipService
    {
        public MembershipUserCollection GetUsers()
        {
            return new MembershipUserCollection();
        }

        public MembershipUser AddUser(string userName, string password, string email)
        {
            return null;
        }

        public void DeleteUser(string userName)
        {

        }

        public void AddUserToRole(string userName, string role)
        {

        }

        public void AddRole(string role)
        {
            throw new NotImplementedException();
        }

        public string[] GetRoles()
        {
            return new string[1];
        }

        public void DeleteRole(string role)
        {


        }

        public void ResetConnection()
        {
            throw new NotImplementedException();
        }

        public void UpdateUserRoles(List<MembershipUser> users, List<string> roles)
        {
                
        }

        public string[] GetRolesForUser(string userName)
        {
            throw new NotImplementedException();
        }
    }
}
