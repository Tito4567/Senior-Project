namespace LacesDataModel
{
    internal class Constants
    {
        internal const string CONNECTION_STRING = "Data Source=XPO-GD1DQ32\\SQLEXPRESS;Initial Catalog=LACES;User=LACES_USER;Password=5Quidt3rn";
        internal const string SCHEMA_DEFAULT = "[dbo]";

        internal const string STORED_PROC_ADD_USER = "[dbo].[pr_addUser]";
        internal const string STORED_PROC_CHANGE_PASSWORD = "[dbo].[pr_changePassword]";
        internal const string STORED_PROC_VALIDATE_LOGIN = "[dbo].[pr_validateLogin]";

        internal const string TABLE_COMMENTS = "Comments";
        internal const string TABLE_CONDITIONS = "Conditions";
        internal const string TABLE_IMAGE_ENTITY_TYPES = "ImageEntityTypes";
        internal const string TABLE_IMAGES = "Images";
        internal const string TABLE_PRODUCT_STATUS = "ProductStatus";
        internal const string TABLE_PRODUCT_TYPES = "ProductTypes";
        internal const string TABLE_PRODUCTS = "Products";
        internal const string TABLE_USER_FOLLOWS = "UserFollows";
        internal const string TABLE_USER_LIKES = "UserLikes";
        internal const string TABLE_USERS = "Users";
    }
}
