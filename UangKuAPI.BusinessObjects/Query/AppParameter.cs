using UangKuAPI.BusinessObjects.Base;
using UangKuAPI.BusinessObjects.DataTransfer;
using UangKuAPI.BusinessObjects.Interface;

namespace UangKuAPI.BusinessObjects.Query
{
    public class AppParameter : BaseQuery, IAppParameter
    {
        public AppParameter(BaseFramework _context) : base(_context)
        {
            
        }

        public AppParameterDto LoadByPrimaryKey(string parId)
        {
            if (string.IsNullOrEmpty(parId))
                return new AppParameterDto
                {
                    appParameter = new EntityFramework.Models.AppParameter()
                };

            var query = (from ap in _context.AppParameters
                         where ap.ParameterId.Equals(parId)
                         select ap).FirstOrDefault();

            if (string.IsNullOrEmpty(query?.ParameterId))
                return new AppParameterDto
                {
                    appParameter = new EntityFramework.Models.AppParameter()
                };

            return new AppParameterDto
            {
                appParameter = query
            };
        }

        public string ParameterString(string parId)
        {
            if (string.IsNullOrEmpty(parId))
                return string.Empty;

            var ap = LoadByPrimaryKey(parId);
            return ap?.appParameter?.ParameterValue ?? string.Empty;
        }

        public int ParameterInteger(string parId)
        {
            if (string.IsNullOrEmpty(parId))
                return 0;

            var ap = LoadByPrimaryKey(parId);
            return ap?.appParameter?.ParameterValue == null ? 0 : Converter.StringToInt(ap.appParameter.ParameterValue);
        }

        public bool ParameterBoolean(string parId)
        {
            if (string.IsNullOrEmpty(parId))
                return false;

            var ap = LoadByPrimaryKey(parId);
            return ap?.appParameter?.ParameterValue == null ? false : Converter.StringToBool(ap.appParameter.ParameterValue);
        }
    }
}
