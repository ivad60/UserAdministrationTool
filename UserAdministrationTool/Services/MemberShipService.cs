using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Web.Configuration;
using System.Web.Security;
using UserAdministrationTool.Interfaces;

namespace UserAdministrationTool.Services
{
    public class MemberShipService : IMembershipService
    {


        public MembershipUserCollection GetUsers()
        {

            return Membership.GetAllUsers();
        }

        public MembershipUser AddUser(string userName, string password, string email)
        {
            
                return Membership.CreateUser(userName, password, email);
            
        }

        public void UpdateUserRoles(List<MembershipUser> users, List<string> roles)
        {

            foreach (var user in users)
            {
                var userRoles = Roles.GetRolesForUser(user.UserName);
                   if(userRoles.Length > 0)
                       Roles.RemoveUserFromRoles(user.UserName, userRoles);
                }

            Roles.AddUsersToRoles(users.Select(u => u.UserName).ToArray(), roles.ToArray());
        }

        public void DeleteUser(string userName)
        {
            Membership.DeleteUser(userName, true);
        }

        public void AddUserToRole(string userName, string role)
        {
            Roles.AddUserToRole(userName, role);

        }

        public void AddRole(string role)
        {
            Roles.CreateRole(role);
        }

        public string[] GetRoles()
        {
            return Roles.GetAllRoles();
        }


        public string[] GetRolesForUser(string userName)
        {
            return Roles.GetRolesForUser(userName);
        }

        public void DeleteRole(string role)
        {
            Roles.DeleteRole(role,false);
        }

        public void ResetConnection()
        {

            var section = (MembershipSection)ConfigurationManager.GetSection("system.web/membership");

            var provider = section.Providers[section.DefaultProvider];
            Membership.Provider.Initialize(section.DefaultProvider, provider.Parameters);

            var roleSection = (RoleManagerSection)ConfigurationManager.GetSection("system.web/roleManager");

            var roleProvider = roleSection.Providers[roleSection.DefaultProvider];
            Roles.Provider.Initialize(roleSection.DefaultProvider, roleProvider.Parameters);

        }

    }
}
