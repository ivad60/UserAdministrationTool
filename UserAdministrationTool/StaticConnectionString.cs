namespace UserAdministrationTool
{
    public class StaticConnectionString
    {
        private static StaticConnectionString _instance;
        

        private StaticConnectionString(){}

        public static StaticConnectionString Instance {get
        {
            if(_instance == null)
                _instance = new StaticConnectionString();
            return _instance;
        }}

        public string ConnectionString { get; set; }

        public string ApplicationName { get; set; }
       
    }
}
