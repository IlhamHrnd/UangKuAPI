using UangKuAPI.BusinessObjects.Base;
using UangKuAPI.BusinessObjects.Interface;

namespace UangKuAPI.BusinessObjects.Query
{
    public class AppParameter : BaseQuery, IAppParameter
    {
        public AppParameter(BaseFramework _context) : base(_context)
        {
            
        }

        public EF.AppParameter LoadByPrimaryKey(string parId)
        {
            if (string.IsNullOrEmpty(parId))
                return new EF.AppParameter();

            var query = (from ap in _context.AppParameters
                         where ap.ParameterId.Equals(parId)
                         select ap).FirstOrDefault();

            if (string.IsNullOrEmpty(query?.ParameterId))
                return new EF.AppParameter();

            return query;
        }

        public string ParameterString(string parId)
        {
            if (string.IsNullOrEmpty(parId))
                return string.Empty;

            var ap = LoadByPrimaryKey(parId);
            return ap.ParameterValue ?? string.Empty;
        }

        public int ParameterInteger(string parId)
        {
            if (string.IsNullOrEmpty(parId))
                return 0;

            var ap = LoadByPrimaryKey(parId);
            return !string.IsNullOrEmpty(ap.ParameterValue) ? Converter.StringToInt(ap.ParameterValue) : 0;
        }

        public bool ParameterBoolean(string parId)
        {
            if (string.IsNullOrEmpty(parId))
                return false;

            var ap = LoadByPrimaryKey(parId);
            return ap.ParameterValue != null && Converter.StringToBool(ap.ParameterValue);
        }
    }
}
