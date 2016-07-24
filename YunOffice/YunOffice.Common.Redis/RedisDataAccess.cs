using Newtonsoft.Json;
using StackExchange.Redis;
using System;

namespace YunOffice.Common.Redis
{
    public class RedisDataAccess<TMessage>
    {
        protected ConnectionMultiplexer RedisManager { get; set; }

        public RedisDataAccess(ConnectionMultiplexer redisManager)
        {
            RedisManager = redisManager;
        }

        public virtual TMessage Deserialize(string message)
        {
            return JsonConvert.DeserializeObject<TMessage>(message);
        }

        public virtual string Serialize(TMessage message)
        {
            return JsonConvert.SerializeObject(message);
        }


        public bool HasKey(string key)
        {
            return RedisManager.GetDatabase().KeyExists(key);
        }

        public TMessage Get(string key)
        {
            string value = RedisManager.GetDatabase().StringGet(key);
            return Deserialize(value);
        }

        public bool Set(string key, TMessage message, int expireMinutes = 0)
        {
            var value = Serialize(message);
            var db = RedisManager.GetDatabase();
            if (expireMinutes > 0)
            {
                return db.StringSet(key, value, TimeSpan.FromMinutes(expireMinutes));
            }
            else
            {
                return db.StringSet(key, value);
            }
        }

        public bool Remove(string key)
        {
            if (HasKey(key)) return RedisManager.GetDatabase().KeyDelete(key);

            return true;
        }
    }
}
