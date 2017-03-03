using System;

namespace LacesRepo.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SchemaName : Attribute
    {
        private string _schemaName = string.Empty;

        public SchemaName(string schema)
        {
            _schemaName = schema;
        }

        public string Name
        {
            get
            {
                return _schemaName;
            }
        }
    }
}
