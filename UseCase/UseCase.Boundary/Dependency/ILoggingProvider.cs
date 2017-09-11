using System;
using System.Collections.Generic;

namespace UseCase.Boundary.Dependency
{
    /// <summary>
    /// The Logging class is a wrapper around the chosen log solution
    /// The log levels are DEBUG, INFO, WARN, ERROR, FATAL.
    /// </summary>
    public interface ILoggingProvider
    {
        void Log(Exception exception, LoggingMessageLevel loggingMessageLevel = LoggingMessageLevel.Fatal);

        void Log(string message, LoggingMessageLevel loggingMessageLevel = LoggingMessageLevel.Info);

        Func<LoggingSessionData> ReadSessionData { get; set; }
    }

    public enum LoggingMessageLevel
    {
        Debug,
        Info,
        Warn,
        Error,
        Fatal
    }

    public class LoggingSessionData
    {
        public int? UserId { get; set; }
        public string UserEmail { get; set; }
        public LoggingCustomData CustomData { get; set; }
    }

    public class LoggingCustomData : List<KeyValuePair<string, string>>
    {
        /// <summary>
        /// This method allows "compact object-initializer" syntax when declaring "LoggingCustomData",
        /// just like a Dictionary initializer, except there is no requirement for the "Keys" to be unique.
        /// </summary>
        /// <code>i.e. LoggingCustomData = new KeyValueList&lt;string, string&gt; { {"NameA", "ValueA"}, {"NameB", "ValueB"} };</code>
        public void Add(string key, string value) 
            => Add(new KeyValuePair<string, string>(key, value));
    }
}
