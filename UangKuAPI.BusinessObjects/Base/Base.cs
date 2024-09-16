namespace UangKuAPI.BusinessObjects.Base
{
    public class Base
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public Base()
        {
            PageNumber = 1;
            PageSize = 25;
        }

        public Base(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize > 25 ? 25 : pageSize;
        }
    }

    public class Parameter
    {
        public string? Key01 { get; set; }
        public string? Key02 { get; set; }
        public string? Key03 { get; set; }
    }
}
