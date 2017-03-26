namespace LacesAPI.Models.Request
{
    public class GetUserRequest : LacesRequest
    {
        public int UserIdToGet { get; set; }
    }
}
