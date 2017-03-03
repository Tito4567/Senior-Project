using System;

namespace LacesRepo.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ConnectionString : Attribute
    {
        private string _connectionName = string.Empty;

        public ConnectionString(string connection)
        {
            _connectionName = connection;
        }

        public string Name
        {
            get
            {
                return _connectionName;
            }
        }
    }
}
