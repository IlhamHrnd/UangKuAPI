﻿using EntitySpaces.Interfaces;

namespace UangKuAPI.BusinessObjects.Base
{
    public partial class Helper
    {
        public static void initES(string connection)
        {
            esProviderFactory.Factory = new EntitySpaces.Loader.esDataProviderFactory();
            esConnectionElement conn = new esConnectionElement();
            conn.DatabaseVersion = "2012";
            conn.ConnectionString = connection;
            esConfigSettings.ConnectionInfo.Connections.Add(conn);
        }
    }
}
