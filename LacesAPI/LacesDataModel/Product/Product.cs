using LacesRepo.Attributes;
using System;

namespace LacesDataModel.Product
{
    [ConnectionString(Constants.CONNECTION_STRING)]
    [TableName(Constants.TABLE_PRODUCTS)]
    [PrimaryKeyName("ProductId")]
    [SchemaName(Constants.SCHEMA_DEFAULT)]
    public class Product : DataObject
    {
        public int ProductId { get; set; }
	    public string Name { get; set; }
	    public string Description { get; set; }
	    public int SellerId { get; set; }
	    public double AskingPrice { get; set; }
	    public int ProductStatudId { get; set; }
	    public string Brand { get; set; }
	    public string Size { get; set; }
	    public int ProductTypeId { get; set; }
	    public int ConditionId { get; set; }
	    public DateTime CreatedDate { get; set; }
	    public DateTime UpdatedDate { get; set; }

        public Product() { }

        public Product(int id) : base(id) {  }

        public override void Load(int id)
        {
            Product temp = GetByValue<Product>("ProductId", Convert.ToString(id), Constants.TABLE_PRODUCTS, Constants.SCHEMA_DEFAULT);

            if (temp != null)
            {
                ProductId = temp.ProductId;
                Name = temp.Name;
                Description = temp.Description;
                SellerId = temp.SellerId;
                AskingPrice = temp.AskingPrice;
                ProductStatudId = temp.ProductStatudId;
                Brand = temp.Brand;
                Size = temp.Size;
                ProductTypeId = temp.ProductTypeId;
                ConditionId = temp.ConditionId;
                CreatedDate = temp.CreatedDate;
                UpdatedDate = temp.UpdatedDate;
            }
            else
            {
                throw new Exception("Could not find product with that Id");
            }
        }
    }
}
