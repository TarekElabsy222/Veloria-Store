namespace Veloria_Store.Application.Services.Interfaces.Logging
{
    public interface IAppLogger<T>
    {
        public void LogError(Exception ex, string message);
        public void LogInfo(string message);
        public void LogWarning(string message);
    }
}
