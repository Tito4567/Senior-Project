using System;

namespace LacesRepo.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TableName : Attribute
    {
        private string _tableName = string.Empty;

        public TableName(string table)
        {
            _tableName = table;
        }

        public string Name
        {
            get
            {
                return _tableName;
            }
        }
    }
}
