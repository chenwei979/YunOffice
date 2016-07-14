using EmitMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YunOffice.UserCenter.UI.Admin
{
    public class EmitMapperFactory
    {
        public static EmitMapperFactory Singleton { get; private set; }

        static EmitMapperFactory()
        {
            Singleton = new EmitMapperFactory();
        }

        private IDictionary<string, object> _chche;

        private EmitMapperFactory()
        {
            _chche = new Dictionary<string, object>();
        }

        public TTo GetInstance<TFrom, TTo>(TFrom from)
        {
            var mapper = GetMapperFormCacheOrCreateNewInstance<TFrom, TTo>();
            return mapper.Map(from);
        }

        private ObjectsMapper<TFrom, TTo> GetMapperFormCacheOrCreateNewInstance<TFrom, TTo>()
        {
            var key = string.Format("{0},{1}", typeof(TFrom).FullName, typeof(TTo).FullName);
            if (_chche.ContainsKey(key))
            {
                return _chche[key] as ObjectsMapper<TFrom, TTo>;
            }

            var mapper = ObjectMapperManager.DefaultInstance.GetMapper<TFrom, TTo>();
            _chche.Add(key, mapper);
            return mapper;
        }
    }
}