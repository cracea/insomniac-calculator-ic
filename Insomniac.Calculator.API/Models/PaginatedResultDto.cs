namespace Insomniac.Calculator.API.Models
{
    public class PaginatedResultDto<TItem>
    {
        public IList<TItem> Items { get; set; } = [];
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
