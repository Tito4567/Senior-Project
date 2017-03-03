using System;

namespace LacesRepo.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PrimaryKeyName : Attribute
    {
        private string _primaryKey = string.Empty;

        public PrimaryKeyName(string pkeyName)
        {
            _primaryKey = pkeyName;
        }

        public string Name
        {
            get
            {
                return _primaryKey;
            }
        }
    }
}
