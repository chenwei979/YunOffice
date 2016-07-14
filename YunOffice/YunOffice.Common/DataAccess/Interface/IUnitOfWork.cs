using System;
using System.Data;

namespace YunOffice.Common.DataAccess
{
    public interface IUnitOfWork : IDisposable
    {
        IDatabase Database { get; set; }
        IDbTransaction Transaction { get; set; }
        void SaveChanges();
    }
}
