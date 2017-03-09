using LacesRepo;
using LacesRepo.Attributes;
using System;
using System.Collections.Generic;

namespace LacesDataModel.Image
{
    [ConnectionString(Constants.CONNECTION_STRING)]
    [TableName(Constants.TABLE_IMAGES)]
    [PrimaryKeyName("ImageId")]
    [SchemaName(Constants.SCHEMA_DEFAULT)]
    public class Image : DataObject
    {
        public int ImageId { get; set; }
        public int AssociatedEntityId { get; set; }
        public int ImageEntityTypeId { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public string FileFormat { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public Image() { }

        public Image(int id) : base(id) { }

        public override void Load(int id)
        {
            Image temp = GetByValue<Image>("ImageId", Convert.ToString(id), Constants.TABLE_IMAGES, Constants.SCHEMA_DEFAULT);

            if (temp != null)
            {
                ImageId = temp.ImageId;
                AssociatedEntityId = temp.AssociatedEntityId;
                ImageEntityTypeId = temp.ImageEntityTypeId;
                FilePath = temp.FilePath;
                FileName = temp.FileName;
                FileFormat = temp.FileFormat;
            }
            else
            {
                throw new Exception("Could not find image with Id " + id);
            }
        }

        public bool LoadAvatarByUserId(int userId)
        {
            bool result = false;

            SearchEntity search = new SearchEntity();

            search.ConnectionString = Constants.CONNECTION_STRING;
            search.Conditions = new List<Condition>();

            Condition userIdCond = new Condition();
            userIdCond.Column = "AssociatedEntityId";
            userIdCond.Operator = Condition.Operators.EqualTo;
            userIdCond.Value = Convert.ToString(userId);

            Condition entityTypeCond = new Condition();
            entityTypeCond.Column = "ImageEntityTypeId";
            entityTypeCond.Operator = Condition.Operators.EqualTo;
            entityTypeCond.Value = Convert.ToString((int)ImageEntityTypeOptions.User);

            search.Conditions.Add(userIdCond);
            search.Conditions.Add(entityTypeCond);

            search.PageSizeLimit = 1;
            search.SchemaName = Constants.SCHEMA_DEFAULT;
            search.TableName = Constants.TABLE_IMAGES;

            List<Image> response = new GenericRepository<Image>().Read(search);

            if (response.Count > 0)
            {
                ImageId = response[0].ImageId;
                AssociatedEntityId = response[0].AssociatedEntityId;
                ImageEntityTypeId = response[0].ImageEntityTypeId;
                FilePath = response[0].FilePath;
                FileName = response[0].FileName;
                FileFormat = response[0].FileFormat;

                result = true;
            }

            return result;
        }

        public static List<Image> GetImagesForProduct(int productId)
        {
            List<Image> result = new List<Image>();

            SearchEntity search = new SearchEntity();

            search.ConnectionString = Constants.CONNECTION_STRING;
            search.Conditions = new List<LacesRepo.Condition>();

            Condition productIdCond = new Condition();
            productIdCond.Column = "AssociatedEntityId";
            productIdCond.Operator = Condition.Operators.EqualTo;
            productIdCond.Value = Convert.ToString(productId);

            Condition entityTypeCond = new Condition();
            entityTypeCond.Column = "ImageEntityTypeId";
            entityTypeCond.Operator = Condition.Operators.EqualTo;
            entityTypeCond.Value = Convert.ToString((int)ImageEntityTypeOptions.Product);

            search.Conditions.Add(productIdCond);
            search.Conditions.Add(entityTypeCond);

            search.SchemaName = Constants.SCHEMA_DEFAULT;
            search.TableName = Constants.TABLE_IMAGES;

            result = new GenericRepository<Image>().Read(search);

            return result;
        }
    }
}
