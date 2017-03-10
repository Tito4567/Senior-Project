namespace LacesRepo
{
    public enum OrderByDirection
    {
        ASC = 1
        , DESC = 2
    }

    public class OrderBy
    {
        public string Column { get; set; }
        public OrderByDirection Direction { get; set; }
    }
}
