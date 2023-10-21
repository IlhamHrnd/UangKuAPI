namespace UangKuAPI.Filter
{
    public class LocationFilter
    {

    }

    public class CitiesFilter
    {
        public int ProvID { get; set; }

        public CitiesFilter()
        {
            ProvID = 0;
        }

        public CitiesFilter(int provID)
        {
            ProvID = provID == 0 ? 0 : provID;
        }
    }

    public class DistrictFilter
    {
        public int CityID { get; set;}

        public DistrictFilter()
        {
            CityID = 0;
        }

        public DistrictFilter(int cityID)
        {
            CityID = cityID == 0 ? 0 : cityID;
        }
    }

    public class SubDistrictFilter
    {
        public int DistrictID { get; set;}

        public SubDistrictFilter()
        {
            DistrictID = 0;
        }

        public SubDistrictFilter(int districtID)
        {
            DistrictID = districtID == 0 ? 0 : districtID;
        }
    }

    public class PostalCodeFilter
    {
        public int SubdisID { get; set; }
        public int DisID { get; set; }
        public int CityID { get; set; }
        public int ProvID { get; set; }

        public PostalCodeFilter()
        {
            ProvID = 0;
            CityID = 0;
            DisID = 0;
            SubdisID = 0;
        }

        public PostalCodeFilter(int provID, int cityID, int disID, int subdisID)
        {
            ProvID = provID == 0 ? 0 : provID;
            CityID = cityID == 0 ? 0 : cityID;
            DisID = disID == 0 ? 0 : disID;
            SubdisID = subdisID == 0 ? 0 : subdisID;
        }
    }
}
