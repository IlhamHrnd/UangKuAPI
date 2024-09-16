using UangKuAPI.BusinessObjects.Base;

namespace UangKuAPI.BusinessObjects.Filter
{
    public class AppParameterFilter : Base.Base
    {
        public string? ParameterID { get; set; }
        public AppParameterFilter() : base()
        {
            ParameterID = string.Empty;
        }
        public AppParameterFilter(int pageNumber, int pageSize, string parameterID) : base(pageNumber, pageSize)
        {
            ParameterID = !string.IsNullOrEmpty(parameterID) ? parameterID : string.Empty;
        }
    }
}
