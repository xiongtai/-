using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            MemCachedDemo.MemCache();
        }
    }

    static class AsyncMethods
    {
        internal static async Task<int> CallMethodAsync(string arg)
        {
            var result = await MethodAsync(arg);
            await Task.Delay(result);
            return result;
        }
        internal static async Task<int> MethodAsync(string arg)
        {
            var total = arg.First() + arg.Last();

            await Task.Delay(total);
            return total;
        }
    }

}
