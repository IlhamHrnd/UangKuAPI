using UangKuAPI.BusinessObjects.Entity.Generated;

namespace UangKuAPI.BusinessObjects.Entity.Custom
{
    public class UserReport
    {
        public static bool GetPersonID(string personID)
        {
            if (string.IsNullOrEmpty(personID))
                return false;

            var urQ = new UserreportQuery("urQ");
            urQ.Select(urQ.ReportNo)
                .Where(urQ.PersonID == personID);
            var dt = urQ.LoadDataTable();
            bool result = dt.Rows.Count > 0;

            return result;
        }
    }
}