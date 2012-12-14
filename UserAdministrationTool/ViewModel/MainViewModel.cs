using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Web.Security;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using UserAdministrationTool.Interfaces;
using UserAdministrationTool.Services;

namespace UserAdministrationTool.ViewModel
{
    /// <summary>

    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private readonly IMembershipService _memberShipService;
        private ObservableCollection<MembershipUser> _users;
        private string _userName;
        private string _password;
        private string _email;
        private ObservableCollection<string> _roles;
        private string _newRoleName;
        private string _connectionString;
        private string _applicationName;
        private string _selectedRole;
        private MembershipUser _selectedUser;


        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IMembershipService memberShipService)
        {
            _memberShipService = memberShipService;
            Users = new ObservableCollection<MembershipUser>();
            Roles = new ObservableCollection<string>();
            SelectedRoles = new List<string>();
            SelectedUsers = new List<MembershipUser>();
            AddUserCommand = new RelayCommand(ExecuteAddUser, CanAddUser);
            AddRoleCommand = new RelayCommand(ExecuteAddRole, CanAddRole);
            AddUsersToRolesCommand = new RelayCommand(ExecuteAddUsersToRoles);
            ResetConnectionCommand = new RelayCommand(ExecuteResetConnection, CanResetConnection);
            DeleteRoleCommand = new RelayCommand(ExecuteDeleteRole, CanDeleteRole);
            DeleteUserCommand = new RelayCommand(ExecuteDeleteUsers, CanDeleteUser);
            ApplicationName = StaticConnectionString.Instance.ApplicationName;
            ConnectionString = StaticConnectionString.Instance.ConnectionString;



        }

        private void ExecuteAddUsersToRoles()
        {
            _memberShipService.UpdateUserRoles(SelectedUsers, SelectedRoles);
        }

        private bool CanDeleteUser()
        {
            return SelectedUsers.Count > 0;
        }

        private void ExecuteDeleteUsers()
        {
            if (MessageBox.Show("Deleting " + SelectedUsers.Count + " users:. Continue?", "Delete user", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                foreach (var membershipUser in SelectedUsers)
                {
                    _memberShipService.DeleteUser(membershipUser.UserName);
                }

                LoadUsers();
            }
        }

        private bool CanDeleteRole()
        {
            return SelectedRoles.Count > 0;
        }

        private void ExecuteDeleteRole()
        {

            if (MessageBox.Show("Deleting " + SelectedRoles.Count + " roles. Continue?", "Delete role", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                foreach (var selectedRole in SelectedRoles)
                {
                    _memberShipService.DeleteRole(selectedRole);
                }

                LoadRoles();
            }


        }




        private bool CanResetConnection()
        {
            return !string.IsNullOrWhiteSpace(ConnectionString) && !string.IsNullOrWhiteSpace(ApplicationName);
        }

        private void ExecuteResetConnection()
        {
            StaticConnectionString.Instance.ApplicationName = ApplicationName;
            StaticConnectionString.Instance.ConnectionString = ConnectionString;
            _memberShipService.ResetConnection();
            LoadRoles();
            LoadUsers();

        }

        private bool CanAddRole()
        {
            return !string.IsNullOrWhiteSpace(NewRoleName);
        }

        private void ExecuteAddRole()
        {
            try
            {
                _memberShipService.AddRole(NewRoleName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Role creation failed");
                
            }
            LoadRoles();
        }

        private void LoadRoles()
        {
            try
            {
                Roles.Clear();
                var roles = _memberShipService.GetRoles();
                foreach (var role in roles)
                {
                    Roles.Add(role);
                }
            }
            catch (SqlException ex)
            {

                MessageBox.Show("Error connecting to database:" + Environment.NewLine + ex.ToString());
            }
        }

        private void LoadUsers()
        {

            try
            {
                Users.Clear();
                var users = _memberShipService.GetUsers();

                foreach (var user in users)
                {
                    Users.Add(((MembershipUser)user));
                }
            }
            catch (SqlException ex)
            {

                MessageBox.Show("Error connecting to database:" + Environment.NewLine + ex.ToString());
            }
        }

        private bool CanAddUser()
        {
            return !string.IsNullOrWhiteSpace(UserName) && !string.IsNullOrWhiteSpace(Password) && !string.IsNullOrWhiteSpace(Email);
        }

        private void ExecuteAddUser()
        {
            try
            {
                _memberShipService.AddUser(UserName, Password, Email);
            }
            catch (MembershipCreateUserException ex)
            {
                MessageBox.Show( ex.Message,"User creation failed");
            }
            LoadUsers();
        }

        public ObservableCollection<string> Roles
        {
            get { return _roles; }
            set
            {
                _roles = value;
                RaisePropertyChanged(() => Roles);
            }
        }

        public ObservableCollection<MembershipUser> Users
        {
            get { return _users; }
            set
            {
                _users = value;
                RaisePropertyChanged(() => Users);
            }
        }

        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                RaisePropertyChanged(() => UserName);
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                RaisePropertyChanged(() => Password);
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                RaisePropertyChanged(() => Email);
            }
        }

        public RelayCommand AddUserCommand
        {
            get;
            set;
        }

        public string NewRoleName
        {
            get { return _newRoleName; }
            set
            {
                _newRoleName = value;
                RaisePropertyChanged(() => NewRoleName);
            }
        }

        public RelayCommand AddRoleCommand { get; set; }

        public string ConnectionString
        {
            get { return _connectionString; }
            set
            {
                _connectionString = value;
                RaisePropertyChanged(() => ConnectionString);
            }
        }


        public string ApplicationName
        {
            get { return _applicationName; }
            set
            {
                _applicationName = value;
                RaisePropertyChanged(() => ApplicationName);
            }
        }

        public RelayCommand ResetConnectionCommand { get; set; }

        public RelayCommand DeleteRoleCommand { get; set; }

        public RelayCommand DeleteUserCommand { get; set; }

        public List<string> SelectedRoles { get; set; }

        public List<MembershipUser> SelectedUsers { get; set; }

        public RelayCommand AddUsersToRolesCommand { get; private set; }
    }
}