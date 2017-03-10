using System.Collections.Generic;

namespace LacesRepo
{
    public class SearchEntity
    {
        public List<string> ColumnsToReturn { get; set; }
        public List<Condition> Conditions { get; set; }
        public string ConnectionString { get; set; }
        public int PageSizeLimit { get; set; }
        public string SchemaName { get; set; }
        public string TableName { get; set; }
        public OrderBy OrderBy { get; set; }
    }
}
