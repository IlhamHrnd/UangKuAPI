using UangKuAPI.EntitySpaces.Generated;

namespace UangKuAPI.EntitySpaces.Custom
{
    public class AppParameter
    {
        public static string GetAppParameterValue(string? parameterID)
        {
            if (string.IsNullOrEmpty(parameterID))
                return string.Empty;

            var ap = new Appparameter();
            if (!ap.LoadByPrimaryKey(parameterID))
                return string.Empty;
            else
                return ap.ParameterValue;
        }
    }
}