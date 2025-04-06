using EntitySpaces.Interfaces;
using Loader = EntitySpaces.Loader;

namespace UangKuAPI.BusinessObjects.Base
{
    public class BaseSpaces
    {
        public static void initES(string connection)
        {
            esProviderFactory.Factory = new Loader.esDataProviderFactory();
            esConnectionElement conn = new esConnectionElement();
            conn.DatabaseVersion = "2012";
            conn.ConnectionString = connection;
            esConfigSettings.ConnectionInfo.Connections.Add(conn);
        }
    }
}
