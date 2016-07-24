using Newtonsoft.Json;
using ProtoBuf;
using StackExchange.Redis;
using System;
using System.IO;

namespace YunOffice.Common.Redis
{
    public class RedisDataAccess<TMessage>
    {
        protected ConnectionMultiplexer RedisManager { get; set; }

        public RedisDataAccess(ConnectionMultiplexer redisManager)
        {
            RedisManager = redisManager;
        }

        protected virtual TMessage Deserialize(byte[] message)
        {
            using (var stream = new MemoryStream(message, false))
            {
                stream.Position = 0;
                return Serializer.Deserialize<TMessage>(stream);
            }
        }
        protected virtual byte[] Serialize(TMessage message)
        {
            using (var stream = new MemoryStream())
            {
                Serializer.Serialize(stream, message);
                return stream.ToArray();
            }
        }

        //protected virtual TMessage Deserialize(string message)
        //{
        //    return JsonConvert.DeserializeObject<TMessage>(message);
        //}

        //protected virtual string Serialize(TMessage message)
        //{
        //    return JsonConvert.SerializeObject(message);
        //}

        public bool HasKey(string key)
        {
            return RedisManager.GetDatabase().KeyExists(key);
        }

        public TMessage Get(string key)
        {
            if (!HasKey(key)) return default(TMessage);

            byte[] value = RedisManager.GetDatabase().StringGet(key);
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
