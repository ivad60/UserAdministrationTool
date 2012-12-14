using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Security;

namespace UserAdministrationTool
{
    public class DynamicSqlRoleProvider:SqlRoleProvider
    {
        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {
            FieldInfo isInitialized = GetType().BaseType.BaseType.BaseType.GetField("_Initialized", BindingFlags.Instance | BindingFlags.NonPublic);
            isInitialized.SetValue(this, false);
            base.Initialize(name, config);

            
            var connectionString = StaticConnectionString.Instance.ConnectionString;
            // Set private property of Membership provider.
            FieldInfo connectionStringField = GetType().BaseType.GetField("_sqlConnectionString", BindingFlags.Instance | BindingFlags.NonPublic);
            connectionStringField.SetValue(this, connectionString);

            

            base.ApplicationName = StaticConnectionString.Instance.ApplicationName;
        }
    }
}
