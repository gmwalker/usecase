using System;

namespace UseCase.Boundary.Dependency.Stub
{
    public class NullLoggingProvider : ILoggingProvider
    {
        public void Log(Exception exception, LoggingMessageLevel loggingMessageLevel = LoggingMessageLevel.Fatal) { }

        public void Log(string message, LoggingMessageLevel loggingMessageLevel = LoggingMessageLevel.Info) { }

        public Func<LoggingSessionData> ReadSessionData { get; set; } = () => new LoggingSessionData();

    }
}