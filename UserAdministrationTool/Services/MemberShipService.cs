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
        private readonly MembershipSection _membershipSection;
        private readonly ProviderSettings _memberShipSettingsProvider;
        private readonly RoleManagerSection _roleManagerSection;
        private readonly ProviderSettings _roleProviderSettings;

        private readonly NameValueCollection _membershipParameters;
        private readonly NameValueCollection _rolemanagerParameters;

        public MemberShipService()
        {
            _membershipSection = (MembershipSection)ConfigurationManager.GetSection("system.web/membership");

            _memberShipSettingsProvider = _membershipSection.Providers[_membershipSection.DefaultProvider];
            _roleManagerSection = (RoleManagerSection)ConfigurationManager.GetSection("system.web/roleManager");
            _roleProviderSettings = _roleManagerSection.Providers[_roleManagerSection.DefaultProvider];

            _membershipParameters = _memberShipSettingsProvider.Parameters;
            _rolemanagerParameters = _roleProviderSettings.Parameters;

        }

      

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

            var membershipParameters = CopyMembershipParameters();

            Membership.Provider.Initialize(_membershipSection.DefaultProvider, membershipParameters);


            var roleParameters = CopyRoleParameters();

            Roles.Provider.Initialize(_roleManagerSection.DefaultProvider, roleParameters);

        }

        private NameValueCollection CopyMembershipParameters()
        {
            var membershipParameters = new NameValueCollection();

            foreach (var parameter in _membershipParameters.AllKeys)
            {
                membershipParameters.Add(parameter, _membershipParameters[parameter]);
            }
            return membershipParameters;
        }

        private NameValueCollection CopyRoleParameters()
        {
            var roleParameters = new NameValueCollection();

            foreach (var parameter in _rolemanagerParameters.AllKeys)
            {
                roleParameters.Add(parameter, _rolemanagerParameters[parameter]);
            }
            return roleParameters;
        }
    }
}
