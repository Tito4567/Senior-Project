using System;

namespace LacesDataModel.Image
{
    public enum ImageEntityTypeOptions
    {
        Unknown = 0,
        User = 1,
        Product = 2
    }

    public class ImageEntityType : DataObject
    {
        public int ImageEntityTypeId { get; set; }
        public string Name { get; set; }

        public ImageEntityType() { }
        public ImageEntityType(int id) : base(id) { }

        public override void Load(int id)
        {
            ImageEntityType temp = GetByValue<ImageEntityType>("ImageEntityTypeId", Convert.ToString(id), Constants.TABLE_IMAGE_ENTITY_TYPES, Constants.SCHEMA_DEFAULT);

            if (temp != null)
            {
                ImageEntityTypeId = temp.ImageEntityTypeId;
                Name = temp.Name;
            }
            else
            {
                throw new Exception("Could not find image entity type with Id " + id);
            }
        }
    }
}
