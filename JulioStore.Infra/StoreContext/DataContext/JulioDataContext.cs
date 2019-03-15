using System;
using System.Data;
using System.Data.SqlClient;
using JulioStore.shared;

namespace JulioStore.Infra.StoreContext.DataContext
{
    public class JulioStoreContext : IDisposable
    {
        public SqlConnection Connection { get; set; }


        public JulioStoreContext()
        {
            Connection = new SqlConnection(Settings.ConnectionString);
            Connection.Open();
        }
        public void Dispose()
        {
            if(Connection.State != ConnectionState.Open)
            {
                Connection.Close();
            }
        }
    }
}