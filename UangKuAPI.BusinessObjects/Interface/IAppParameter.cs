using UangKuAPI.BusinessObjects.DataTransfer;

namespace UangKuAPI.BusinessObjects.Interface
{
    public interface IAppParameter
    {
        AppParameterDto LoadByPrimaryKey(string parId);
        string ParameterString(string parId);
        int ParameterInteger(string parId);
        bool ParameterBoolean(string parId);
    }
}
