using System.Collections.Generic;

namespace WpfDISample.Services
{
    public class NameListService : INameListService
    {
        public async Task<IEnumerable<string>> GetNamesAsync()
        {
            // �T���v���f�[�^��񓯊��ŕԂ�
            await Task.Delay(1000); // �f�[�^�擾�̃V�~�����[�V����
            return new List<string> { "Alice", "Bob", "Charlie" };
        }
    }
}
