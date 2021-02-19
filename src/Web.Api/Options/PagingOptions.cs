namespace CinemaService.Web.Api.Options
{
    public class PagingOptions
    {
        public int MaxPageSize { get; set; } = 50;

        public int MinPageIndex { get; set; } = 1;

        public int MinPageSize { get; set; } = 1;
    }
}
