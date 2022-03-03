namespace Account.DataContract.Entities
{
    public class BaseQueryModel
    {
        public string SearchText { get; set; }
        public string SortBy { get; set; }
        public int SortDir { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public string IPAddress { get; set; }
        public string UserId { get; set; }
    }
}
