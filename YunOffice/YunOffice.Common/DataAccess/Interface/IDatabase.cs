using System;
using System.Data;

namespace YunOffice.Common.DataAccess
{
    public interface IDatabase : IDbConnection, IDisposable
    {
    }
}
