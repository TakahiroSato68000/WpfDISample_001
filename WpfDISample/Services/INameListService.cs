namespace WpfDISample.Services
{
    public interface INameListService
    {
        Task<IEnumerable<string>> GetNamesAsync();
    }
}
