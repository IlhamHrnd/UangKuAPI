using UangKuAPI.BusinessObjects.Entity.Generated;

namespace UangKuAPI.BusinessObjects.Entity.Custom
{
    public class User
    {
        public static bool IsUserAdmin(string? userID)
        {
            var result = false;

            if (string.IsNullOrEmpty(userID))
                result = false;
            
            var u = new Generated.User();
            if (!u.LoadByPrimaryKey(userID))
                result = false;

            if (string.IsNullOrEmpty(u.SRAccess))
                result = false;

            var asri = new Appstandardreferenceitem();
            if (!asri.LoadByPrimaryKey("Access", u.SRAccess))
                result = false;

            if (u.SRAccess == "Access-01" && asri.ItemName == "Admin")
                result = true;

            return result;
        }
    }
}