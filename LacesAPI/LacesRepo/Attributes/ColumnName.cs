using System;

namespace LacesRepo.Attributes
{
    [AttributeUsage(AttributeTargets.All)]
    public class ColumnName : Attribute
    {
        private string _columnName = string.Empty;

        public ColumnName(string column)
        {
            _columnName = column;
        }

        public string Name
        {
            get
            {
                return _columnName;
            }
        }
    }
}
