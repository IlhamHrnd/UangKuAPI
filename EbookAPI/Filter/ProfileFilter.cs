namespace UangKuAPI.Filter
{
    public class ProfileFilter
    {
        public string? PersonID { get; set; }

        public ProfileFilter()
        {
            PersonID = string.Empty;
        }

        public ProfileFilter(string personID)
        {
            PersonID = string.IsNullOrEmpty(personID) ? string.Empty : personID;
        }
    }
}
