using LacesRepo.Attributes;
using System;

namespace LacesDataModel.Product
{
    [ConnectionString(Constants.CONNECTION_STRING)]
    [TableName(Constants.TABLE_CONDITIONS)]
    [PrimaryKeyName("ConditionId")]
    [SchemaName(Constants.SCHEMA_DEFAULT)]
    public class Condition : DataObject
    {
        public int ConditionId { get; set; }
        public string Name { get; set; }

        public Condition() { }

        public Condition(int id) : base(id) { }

        public override void Load(int id)
        {
            Condition temp = GetByValue<Condition>("ConditionId", Convert.ToString(id), Constants.TABLE_CONDITIONS, Constants.SCHEMA_DEFAULT);

            if (temp != null)
            {
                ConditionId = temp.ConditionId;
                Name = temp.Name;
            }
            else
            {
                throw new Exception("Could not find condition with Id " + id);
            }
        }
    }
}
