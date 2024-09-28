namespace UangKuAPI.BusinessObjects.Filter
{
    public class LocationFilter : Base.Base
    {
        public string? ProvID { get; set; }
        public string? CityID { get; set; }
        public string? DistrictID { get; set; }
        public string? SubDisID { get; set; }
        public LocationFilter() : base()
        {
            ProvID = string.Empty;
            CityID = string.Empty;
            DistrictID = string.Empty;
            SubDisID = string.Empty;
        }
        public LocationFilter(int pageNumber, int pageSize, string provID, string cityID, string districtID, string subdisID)  : base(pageNumber, pageSize)
        {
            ProvID = !string.IsNullOrEmpty(provID) ? provID : string.Empty;
            CityID = !string.IsNullOrEmpty(cityID) ? cityID : string.Empty;
            DistrictID = !string.IsNullOrEmpty(districtID) ? districtID : string.Empty;
            SubDisID = !string.IsNullOrEmpty(subdisID) ? subdisID : string.Empty;
        }
    }
}