using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;

namespace UserAdministrationTool.Interfaces
{
    public interface IMembershipService
    {
        MembershipUserCollection GetUsers();
        MembershipUser AddUser(string userName, string password, string email);
        void DeleteUser(string userName);
        void AddUserToRole(string userName,string role);
        void AddRole(string role);
        string[] GetRoles();
        void DeleteRole(string role);
        void ResetConnection();
        void UpdateUserRoles(List<MembershipUser> users, List<string> roles);
        string[] GetRolesForUser(string userName);
    }
}
