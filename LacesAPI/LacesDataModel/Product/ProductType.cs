using LacesRepo.Attributes;
using System;

namespace LacesDataModel.Product
{
    [ConnectionString(Constants.CONNECTION_STRING)]
    [TableName(Constants.TABLE_PRODUCT_TYPES)]
    [PrimaryKeyName("ProductTypeId")]
    [SchemaName(Constants.SCHEMA_DEFAULT)]
    public class ProductType : DataObject
    {
        public int ProductTypeId { get; set; }
        public string Name { get; set; }

        public ProductType() { }

        public ProductType(int id) : base(id) { }

        public override void Load(int id)
        {
            ProductType temp = GetByValue<ProductType>("ProductTypeId", Convert.ToString(id), Constants.TABLE_PRODUCT_TYPES, Constants.SCHEMA_DEFAULT);

            if (temp != null)
            {
                ProductTypeId = temp.ProductTypeId;
                Name = temp.Name;
            }
            else
            {
                throw new Exception("Could not find product type with that Id");
            }
        }
    }
}
