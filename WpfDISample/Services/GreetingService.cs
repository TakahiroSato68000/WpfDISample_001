using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDISample.Services
{
    public class GreetingService : IGreetingService
    {
        public async Task<string> GreetAsync(string name)
        {
            // 非同期処理のシミュレーション
            await Task.Delay(500);
            return $"Hello, {name}!";
        }
    }
}
