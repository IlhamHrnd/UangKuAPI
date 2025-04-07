namespace UangKuAPI.BusinessObjects.Interface
{
    public interface IAppParameter
    {
        EF.AppParameter LoadByPrimaryKey(string parId);
        string ParameterString(string parId);
        int ParameterInteger(string parId);
        bool ParameterBoolean(string parId);
    }
}
