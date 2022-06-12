namespace old_stuff_exchange_v2.Model
{
    public class PagingModel
    {
        public string FilterWith { get; set; } 
        public string FilterValue { get; set; }
        public string SortBy { get; set; }
        public string SortType { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
