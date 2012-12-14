using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace UserAdministrationTool
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {

            //TODO: Set connectionstring at login from user input, or something
            StaticConnectionString.Instance.ConnectionString =
                ConfigurationManager.ConnectionStrings["membershipConnectionString"].ConnectionString;
            StaticConnectionString.Instance.ApplicationName = "controlPanel";

        }
    }
}
