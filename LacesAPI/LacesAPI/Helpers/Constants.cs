namespace LacesAPI.Helpers
{
    internal class Constants
    {
        internal const string APP_SETTING_DEFAULT_PROFILE_PIC_FORMAT = "DefaultProfilePicFormat";
        internal const string APP_SETTING_DEFAULT_PROFILE_PIC_NAME = "DefaultProfilePicName";
        internal const string APP_SETTING_DEFAULT_PROFILE_PIC_PATH = "DefaultProfilePicPath";
        internal const string APP_SETTING_PRODUCT_IMAGE_DIRECTORY = "ProductImageDirectory";
        internal const string APP_SETTING_SECURITY_TOKEN = "SecurityToken";
        internal const string APP_SETTING_USER_AVATAR_DIRECTORY = "UserAvatarDirectory";

        internal const string CONNECTION_STRING = "Data Source=XPO-GD1DQ32\\SQLEXPRESS;Initial Catalog=LACES;User=LACES_USER;Password=5Quidt3rn";

        internal const string SCHEMA_DEFAULT = "[dbo]";

        internal const string STORED_PROC_GET_FOLLOWING_FEED = "[dbo].[pr_getFollowingFeed]";
        internal const string STORED_PROC_GET_INTEREST_FEED = "[dbo].[pr_getInterestFeed]";
        internal const string STORED_PROC_GET_NOTIFICATIONS = "[dbo].[pr_getNotifications]";

        internal const string TABLE_PRODUCTS = "Products";
        internal const string TABLE_TAGS = "Tags";
        internal const string TABLE_USERS = "Users";
    }
}