using System;
using System.Collections.Generic;

namespace YunOffice.Common.DataAccess
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class, IEntity
    {
        IUnitOfWork UnitOfWork { get; }

        void Save(params TEntity[] entities);

        void Delete(params long[] ids);

        TEntity GetItemById(long id);

        IList<TEntity> GetAllItems();
    }
}
