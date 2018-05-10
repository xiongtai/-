using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Memcached.Client;

namespace OA.Common
{
    public class MemcacheHelper
    {
        private static readonly MemcachedClient mc;
        static MemcacheHelper()
        {
            string[] serverList = { "re" };

            SockIOPool pool = SockIOPool.GetInstance();
            pool.SetServers(serverList);

            pool.InitConnections = 3;
            pool.MinConnections = 3;
            pool.MaxConnections = 5;

            pool.SocketConnectTimeout = 1000;
            pool.SocketTimeout = 3000;

            pool.MaintenanceSleep = 30;
            pool.Failover = true;

            pool.Nagle = false;
            pool.Initialize();

            mc = new MemcachedClient();
            mc.EnableCompression = false;
        }

        public static bool Set(string key, object value)
        {
            return mc.Set(key, value);
        }
        public static bool Set(string key,object value,DateTime date)
        {
            return mc.Set(key, value, date);
        }

        public static object Get(string key)
        {
            return mc.Get(key);
        }

        public static bool Delete(string key)
        {
            if (mc.KeyExists(key))
            {
               return mc.Delete(key);
            }
            return false;
        }
    }
}
