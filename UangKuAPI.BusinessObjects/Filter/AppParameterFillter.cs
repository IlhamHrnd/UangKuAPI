namespace UangKuAPI.BusinessObjects.Filter
{
    public class AppParameterFillter : Base
    {
        public string? ParameterID { get; set; }
        public AppParameterFillter() : base()
        {
            ParameterID = string.Empty;
        }

        public AppParameterFillter(int pageNumber, int pageSize, string parameterID) : base(pageNumber, pageSize)
        {
            ParameterID = !string.IsNullOrEmpty(parameterID) ? parameterID : string.Empty;
        }
    }
}
