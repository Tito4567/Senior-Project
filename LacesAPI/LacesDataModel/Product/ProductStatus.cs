using LacesRepo.Attributes;
using System;

namespace LacesDataModel.Product
{
    public enum ProductStatusOptions
    {
        Unknown = 1,
        Open = 2,
        Sold = 3,
        Removed = 4
    }

    [ConnectionString(Constants.CONNECTION_STRING)]
    [TableName(Constants.TABLE_PRODUCT_STATUS)]
    [PrimaryKeyName("ProductStatusId")]
    [SchemaName(Constants.SCHEMA_DEFAULT)]
    public class ProductStatus : DataObject
    {
        public int ProductStatusId { get; set; }
        public string Status { get; set; }

        public ProductStatus() { }

        public ProductStatus(int id) : base(id) { }

        public override void Load(int id)
        {
            ProductStatus temp = GetByValue<ProductStatus>("ProductStatusId", Convert.ToString(id), Constants.TABLE_PRODUCT_STATUS, Constants.SCHEMA_DEFAULT);

            if (temp != null)
            {
                ProductStatusId = temp.ProductStatusId;
                Status = temp.Status;
            }
            else
            {
                throw new Exception("Could not find product status with Id " + id);
            }
        }
    }
}
