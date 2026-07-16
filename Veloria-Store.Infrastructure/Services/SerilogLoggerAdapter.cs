using Microsoft.Extensions.Logging;
using Veloria_Store.Application.Services.Interfaces.Logging;

namespace Veloria_Store.Infrastructure.Services
{
    public class SerilogLoggerAdapter<T>(ILogger<T> logger) : IAppLogger<T>
    {
        public void LogError(Exception ex, string message) => logger.LogError(ex, message);

        public void LogInfo(string message) => logger.LogInformation(message);
        public void LogWarning(string message) => logger.LogWarning(message);
    }
}
