namespace UangKuAPI.BusinessObjects.Filter
{
    public class LocationFillter : Base
    {
        public int ProvID { get; set; }
        public int CityID { get; set; }
        public int DistrictID { get; set; }
        public int SubdisID { get; set; }
        public LocationFillter() : base()
        {
            ProvID = 0;
            CityID = 0;
            DistrictID = 0;
            SubdisID = 0;
        }

        public LocationFillter(int pageNumber, int pageSize, int provID, int cityID, int districtID, int subdisID, int disID) : base(pageNumber, pageSize)
        {
            ProvID = provID == 0 ? 0 : provID;
            CityID = cityID == 0 ? 0 : cityID;
            DistrictID = districtID == 0 ? 0 : districtID;
            SubdisID = subdisID == 0 ? 0 : subdisID;
        }
    }
}
