using System;
using System.Data;

namespace YunOffice.Common.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IDatabase database)
        {
            Database = database;
            Database.Open();
            Transaction = Database.BeginTransaction();
        }

        public IDatabase Database { get; set; }
        public IDbTransaction Transaction { get; set; }


        public void SaveChanges()
        {
            try
            {
                Transaction.Commit();
            }
            catch
            {
                Transaction.Rollback();
            }
            finally
            {
                Database.Close();
            }
        }

        public void Dispose()
        {
            Transaction.Dispose();
            Database.Dispose();
        }
    }
}
