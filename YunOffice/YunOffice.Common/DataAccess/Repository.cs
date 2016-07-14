using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Dapper;
using Dapper.Contrib.Extensions;

namespace YunOffice.Common.DataAccess
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        public IUnitOfWork UnitOfWork { get; set; }

        public Repository(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        #region Save

        protected virtual void Insert(TEntity entity)
        {
            entity.ApplicationId = IdWorkerFactory.Singleton.NextId();
            var now = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("China Standard Time"));
            entity.CreateDate = now;
            entity.UpdateDate = now;

            UnitOfWork.Database.Insert(entity, UnitOfWork.Transaction);
        }

        protected virtual void Update(TEntity entity)
        {
            var orgEntity = GetItemById(entity.Id);
            entity.Id = orgEntity.Id;
            entity.CreatorName = orgEntity.CreatorName;
            entity.CreatorId = orgEntity.CreatorId;
            entity.CreateDate = orgEntity.CreateDate;
            entity.UpdateDate = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("China Standard Time"));

            UnitOfWork.Database.Update(entity, UnitOfWork.Transaction);
        }

        public virtual void Save(params TEntity[] entities)
        {
            foreach (var entity in entities)
            {
                if (entity.Id > 0) Update(entity);
                else Insert(entity);
            }
        }

        #endregion

        #region Delete

        protected virtual void Delete(long id)
        {
            var entity = GetItemById(id);
            if (entity == null) throw new Exception(string.Format("Delete failed, cannot find this item which Id is {0}.", id));

            UnitOfWork.Database.Delete(entity, UnitOfWork.Transaction);
        }

        public virtual void Delete(params long[] ids)
        {
            foreach (var id in ids)
            {
                Delete(id);
            }
        }

        #endregion

        #region Get

        public virtual TEntity GetItemById(long id)
        {
            return UnitOfWork.Database.Get<TEntity>(id, UnitOfWork.Transaction);
        }

        public virtual IList<TEntity> GetAllItems()
        {
            return UnitOfWork.Database.GetAll<TEntity>(UnitOfWork.Transaction).ToList();
        }

        #endregion

        public virtual void Dispose()
        {
            UnitOfWork.Dispose();
        }
    }
}
