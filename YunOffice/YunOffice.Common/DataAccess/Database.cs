using System.Data;
using System.Data.SqlClient;

namespace YunOffice.Common.DataAccess
{
    public abstract class Database : IDatabase
    {
        protected IDbConnection DbConnection { get; set; }

        public string ConnectionString { get; set; }

        public int ConnectionTimeout
        {
            get
            {
                return DbConnection.ConnectionTimeout;
            }
        }

        public ConnectionState State
        {
            get
            {
                return DbConnection.State;
            }
        }

        string IDbConnection.Database
        {
            get
            {
                return DbConnection.Database;
            }
        }


        public void Open()
        {
            DbConnection.Open();
        }

        public void Close()
        {
            DbConnection.Close();
        }

        public IDbTransaction BeginTransaction()
        {
            return DbConnection.BeginTransaction();
        }

        public IDbTransaction BeginTransaction(IsolationLevel il)
        {
            return DbConnection.BeginTransaction(il);
        }

        public IDbCommand CreateCommand()
        {
            return DbConnection.CreateCommand();
        }

        public void ChangeDatabase(string databaseName)
        {
            DbConnection.ChangeDatabase(databaseName);
        }

        public void Dispose()
        {
            if (DbConnection.State != ConnectionState.Closed) DbConnection.Close();
            DbConnection.Dispose();
        }
    }

    public class SqlServerDatabase : Database
    {
        public SqlServerDatabase()
        {
            ConnectionString = "data source=192.168.0.45;initial catalog=YeeOffice;user id=sa;password=123;";
            DbConnection = new SqlConnection(ConnectionString);
        }
    }
}
