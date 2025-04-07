namespace UangKuAPI.BusinessObjects.Filter
{
    public class LocationFilter : Base.Base
    {
        public int? ProvID { get; set; }
        public int? CityID { get; set; }
        public int? DistrictID { get; set; }
        public int? SubDisID { get; set; }
        public LocationFilter() : base()
        {
            ProvID = 0;
            CityID = 0;
            DistrictID = 0;
            SubDisID = 0;
        }
        public LocationFilter(int pageNumber, int pageSize, int provID, int cityID, int districtID, int subdisID)  : base(pageNumber, pageSize)
        {
            ProvID = provID;
            CityID = cityID;
            DistrictID = districtID;
            SubDisID = subdisID;
        }
    }
}