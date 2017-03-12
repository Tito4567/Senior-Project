using LacesRepo;
using LacesRepo.Attributes;
using System;
using System.Collections.Generic;
namespace LacesDataModel.Product
{
    [ConnectionString(Constants.CONNECTION_STRING)]
    [TableName(Constants.TABLE_TAGS)]
    [PrimaryKeyName("TagId")]
    [SchemaName(Constants.SCHEMA_DEFAULT)]
    public class Tag : DataObject
    {
        public int TagId { get; set; }
        public int ProductId { get; set; }
        public string Description { get; set; }

        public Tag() { }

        public Tag(int id) : base(id) { }

        public override void Load(int id)
        {
            Tag temp = GetByValue<Tag>("TagId", Convert.ToString(id), Constants.TABLE_TAGS, Constants.SCHEMA_DEFAULT);

            if (temp != null)
            {
                TagId = temp.TagId;
                ProductId = temp.ProductId;
                Description = temp.Description;
            }
            else
            {
                throw new Exception("Could not find tag with Id " + id);
            }
        }

        // This might be best moved into a different class later on.
        public static List<Tag> GetTagsForProduct(int productId)
        {
            List<Tag> result = new List<Tag>();

            SearchEntity search = new SearchEntity();
            
            search.ConnectionString = Constants.CONNECTION_STRING;

            LacesRepo.Condition userIdCond = new LacesRepo.Condition();
            userIdCond.Column = "ProductId";
            userIdCond.Operator = LacesRepo.Condition.Operators.EqualTo;
            userIdCond.Value = Convert.ToString(productId);

            search.Conditions.Add(userIdCond);

            search.SchemaName = Constants.SCHEMA_DEFAULT;
            search.TableName = Constants.TABLE_TAGS;

            result = new GenericRepository<Tag>().Read(search);

            return result;
        }
    }
}
