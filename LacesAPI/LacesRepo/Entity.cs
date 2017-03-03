using System.Collections.Generic;
using System.Linq;

namespace LacesRepo
{
    public class Entity
    {
        private Dictionary<string, object> _data = new Dictionary<string, object>();

        public string ConnectionString { get; set; }
        public string PrimaryKeyName { get; set; }
        public string SchemaName { get; set; }
        public string TableName { get; set; }

        public void SetValue(string key, object value)
        {
            _data[key] = value;
        }

        public object GetValue(string key)
        {
            if (_data.ContainsKey(key))
            {
                return _data[key];
            }
            else
            {
                return null;
            }
        }

        public List<string> GetKeys()
        {
            return _data.Keys.ToList();
        }
    }
}
