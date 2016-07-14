using System;
using System.Collections.Generic;
using YunOffice.UserCenter.DataAccess;
using YunOffice.UserCenter.Entities;

namespace YunOffice.UserCenter.BusnissLogic
{
    public class UserBusnissLogic : IDisposable
    {
        public UserDataAccess DataAccess { get; set; }

        public UserBusnissLogic(UserDataAccess dataAccess)
        {
            DataAccess = dataAccess;
        }

        public void Save(params UserEntity[] entities)
        {
            DataAccess.Save(entities);
        }

        public bool Register(string displayname, string username, string password)
        {
            return DataAccess.Register(displayname, username, password);
        }

        public bool Login(string username, string password)
        {
            return DataAccess.Login(username, password);
        }

        public void GetItemById(long id)
        {
            DataAccess.GetItemById(id);
        }

        public IList<UserEntity> GetAllItems()
        {
            return DataAccess.GetAllItems();
        }

        public void Dispose()
        {
            DataAccess.Dispose();
        }
    }
}
