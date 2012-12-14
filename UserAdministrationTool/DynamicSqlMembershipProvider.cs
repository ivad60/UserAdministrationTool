using System.Configuration;
using System.Reflection;
using System.Web.Security;
using UserAdministrationTool.Properties;


namespace UserAdministrationTool
{
    public class DynamicSqlMembershipProvider:SqlMembershipProvider
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
