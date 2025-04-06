namespace UangKuAPI.BusinessObjects.Base;
public class BaseQuery
{
    protected readonly BaseFramework _context;

    public BaseQuery(BaseFramework context)
    {
        _context = context;
    }
}
