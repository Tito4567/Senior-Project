using LacesRepo.Attributes;
using System;

namespace LacesDataModel.Transaction
{
    [ConnectionString(Constants.CONNECTION_STRING)]
    [TableName(Constants.TABLE_TRANSACTIONS)]
    [PrimaryKeyName("TransactionId")]
    [SchemaName(Constants.SCHEMA_DEFAULT)]
    public class Transaction : DataObject
    {
        public int TransactionId { get; set; }
        public int SellerId { get; set; }
        public int BuyerId { get; set; }
        public int ProductId { get; set; }
        public decimal Amount { get; set; }
        public string ReferenceNumber { get; set; }
        public DateTime CreatedDate { get; set; }

        public Transaction() { }

        public Transaction(int id) : base(id) { }

        public override void Load(int id)
        {
            Transaction temp = GetByValue<Transaction>("TransactionId", Convert.ToString(id), Constants.TABLE_TRANSACTIONS, Constants.SCHEMA_DEFAULT);

            if (temp != null)
            {
                TransactionId = temp.TransactionId;
                SellerId = temp.SellerId;
                BuyerId = temp.BuyerId;
                ProductId = temp.ProductId;
                Amount = temp.Amount;
                ReferenceNumber = temp.ReferenceNumber;
                CreatedDate = temp.CreatedDate;
            }
            else
            {
                throw new Exception("Could not find transaction with Id " + id);
            }
        }
    }
}
