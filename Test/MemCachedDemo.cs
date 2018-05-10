using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Memcached.Client;

namespace Test
{
    public class MemCachedDemo
    {
        public static void MemCache()
        {
            InitMemcache();
            MemcachedClient mc = new MemcachedClient();
            mc.EnableCompression = false;
            DisplayMenu();
            while (true)
            {
                string selected = Console.ReadLine();
                switch (selected)
                {
                    case "1":
                        mc.Set("test", "my value");
                        break;
                    case "2":
                        if (mc.KeyExists("test"))
                        {
                            Console.WriteLine(mc.Get("test").ToString());
                        }
                        else
                        {
                            Console.WriteLine("it not exists test");
                        }
                        break;
                    default:
                        break;
                }

            }


            
        }

        public static void InitMemcache()
        {
            SockIOPool pool = SockIOPool.GetInstance();
            pool.SetServers(new string[] { "127.0.0.1:11211" });

            pool.InitConnections = 3;
            pool.MinConnections = 3;
            pool.MaxConnections = 5;

            pool.SocketConnectTimeout = 1000;
            pool.SocketTimeout = 3000;

            pool.MaintenanceSleep = 30;
            pool.Failover = true;

            pool.Nagle = false;
            pool.Initialize();
        }

        public static void DisplayMenu()
        {
            Console.WriteLine("1 Set A Value");
            Console.WriteLine("2 Get A Value");
            Console.WriteLine("3 Quit");
        }

    }
}
