namespace WpfDISample.Services
{
    public interface IGreetingService
    {
        Task<string> GreetAsync(string name);
    }
}
