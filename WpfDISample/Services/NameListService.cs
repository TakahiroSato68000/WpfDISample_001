using System.Collections.Generic;

namespace WpfDISample.Services
{
    public class NameListService : INameListService
    {
        public async Task<IEnumerable<string>> GetNamesAsync()
        {
            // サンプルデータを非同期で返す
            await Task.Delay(1000); // データ取得のシミュレーション
            return new List<string> { "Alice", "Bob", "Charlie" };
        }
    }
}
