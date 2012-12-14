using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UserAdministrationTool.ViewModel;

namespace UserAdministrationTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            Roles.SelectionChanged+=Roles_SelectionChanged;
            Users.SelectionChanged += Users_SelectionChanged;
        }

        void Users_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var viewModel = (MainViewModel)DataContext;

            viewModel.SelectedUsers.Clear();
            foreach (var selectedItem in Users.SelectedItems)
            {
                viewModel.SelectedUsers.Add((MembershipUser)selectedItem);
            }
           
        }

        private void Roles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var viewModel = (MainViewModel) DataContext;
            viewModel.SelectedRoles.Clear();

            foreach (var selectedItem in Roles.SelectedItems)
            {
                viewModel.SelectedRoles.Add((string)selectedItem);
            }

            
        }
    }
}
