namespace UangKuAPI.Filter
{
    public class AppParameterFilter
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public AppParameterFilter()
        {
            PageNumber = 1;
            PageSize = 25;
        }

        public AppParameterFilter(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize > 25 ? 25 : pageSize;
        }
    }
}
