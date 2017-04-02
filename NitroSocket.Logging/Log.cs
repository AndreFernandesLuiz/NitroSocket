using System;


[assembly: Library.Logging.Config.XmlConfigurator(Watch = true)]

namespace Library.Logging
{
    /// <summary>
    /// This class contains methods for logging at different levels and also
    /// has properties for determining if those logging levels are
    /// enabled in the current configuration.
    /// </summary>
    /// <example>Simple example of logging messages
    /// <code lang="C#">
    /// Log.Trace(typeof(TreatEffect), "return a list of accountability objects based on the datareader");
    /// Log.Trace("Meu nomespace", "return a list of accountability objects based on the datareader");
    /// Log.Trace(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, exception);
    /// </code>
    /// </example>
    public class Log
    {
        //private static readonly Library.Logging.log = Library.Logging.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static Library.Logging.ILog log;

        private static bool callStatementConfigure = true;

        public static bool CallStatementConfigure
        {
            get { return Log.callStatementConfigure; }
            set { Log.callStatementConfigure = value; }
        }

        #region Log Trace
        /// <summary>
        /// Helper methods to log a trace message
        /// </summary>
        /// <param name="type">The object <paramref name="type"/> will be used for the logger. Ex. System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, typeOf(Classname) or this.GetType();</param>
        /// <param name="message">The message string to log.</param>
        public static void Trace(Type type, string message)
        {
            log = Library.Logging.LogManager.GetLogger(type);
            if (log.IsDebugEnabled) log.Trace(message);
        }

        /// <summary>
        /// Helper methods to log a trace message
        /// </summary>
        /// <param name="type">The object <paramref name="type"/> will be used for the logger. Ex. System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, typeOf(Classname) or this.GetType();</param>
        /// <param name="message">The message string to log.</param>
        public static void Trace(string type, string message)
        {
            log = Library.Logging.LogManager.GetLogger(type);
            if (log.IsDebugEnabled) log.Trace(message);
        }

        /// <summary>
        /// Helper methods to log a trace message including
        /// the stack trace of the <see cref="Exception"/> passed
        /// as a parameter.
        /// </summary>
        /// <param name="type">The object <paramref name="type"/> will be used for the logger. Ex. typeOf(Classname) or this.GetType();</param>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        /// <param name="message">The message string to log.</param>
        public static void Trace(Type type, Exception exception, string message)
        {
            log = Library.Logging.LogManager.GetLogger(type);
            if (log.IsDebugEnabled) log.Trace(message, exception);
        }

        /// <summary>
        /// Helper methods to log a trace message including
        /// the stack trace of the <see cref="Exception"/> passed
        /// as a parameter.
        /// </summary>
        /// <param name="type">The object <paramref name="type"/> will be used for the logger. Ex. System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, typeOf(Classname) or this.GetType();</param>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        /// <param name="message">The message string to log.</param>
        public static void Trace(string type, Exception exception, string message)
        {
            log = Library.Logging.LogManager.GetLogger(type);
            if (log.IsDebugEnabled) log.Trace(message, exception);
        }

        /// <summary>
        /// Helper methods to log a trace with
        /// the stack trace of the <see cref="Exception"/> passed
        /// as a parameter.
        /// </summary>
        /// <param name="type">The object <paramref name="type"/> will be used for the logger. Ex. typeOf(Classname) or this.GetType();</param>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        public static void Trace(Type type, Exception exception)
        {
            log = Library.Logging.LogManager.GetLogger(type);
            if (log.IsDebugEnabled) log.Trace(exception.Message, exception);
        }

        /// <summary>
        /// Helper methods to log a trace with
        /// the stack trace of the <see cref="Exception"/> passed
        /// as a parameter.
        /// </summary>
        /// <param name="type">The object <paramref name="type"/> will be used for the logger. Ex. System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, typeOf(Classname) or this.GetType();</param>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        public static void Trace(string type, Exception exception)
        {
            log = Library.Logging.LogManager.GetLogger(type);
            if (log.IsDebugEnabled) log.Trace(exception.Message, exception);
        }

        /// <summary>
        /// Helper methods to log a trace message
        /// </summary>
        /// <param name="message">The message string to log.</param>
        public static void Trace(string message)
        {
            log = Library.Logging.LogManager.GetLogger(String.Empty);
            if (log.IsDebugEnabled) log.Trace(message);
        }

        /// <summary>
        /// Helper methods to log a trace message including
        /// the stack trace of the <see cref="Exception"/> passed
        /// as a parameter.
        /// </summary>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        /// <param name="message">The message string to log.</param>
        public static void Trace(Exception exception, string message)
        {
            log = Library.Logging.LogManager.GetLogger(String.Empty);
            if (log.IsDebugEnabled) log.Trace(message, exception);
        }

        /// <summary>
        /// Helper methods to log a trace with
        /// the stack trace of the <see cref="Exception"/> passed
        /// as a parameter.
        /// </summary>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        public static void Trace(Exception exception)
        {
            log = Library.Logging.LogManager.GetLogger(String.Empty);
            if (log.IsDebugEnabled) log.Trace(exception.Message, exception);
        }
        #endregion

        #region Log BusinessError
        
        public static void BusinessError(int eventId, Type type, string message)
        {
            log = Library.Logging.LogManager.GetLogger(type);
            if (log.IsBusinessErrorEnabled) log.BusinessError(eventId, message);
        }

        
        public static void BusinessError(int eventId, string type, string message)
        {
            log = Library.Logging.LogManager.GetLogger(type);
            if (log.IsBusinessErrorEnabled) log.BusinessError(eventId, message);
        }

        
        public static void BusinessError(int eventId, Type type, Exception exception, string message)
        {
            log = Library.Logging.LogManager.GetLogger(type);
            if (log.IsBusinessErrorEnabled) log.BusinessError(eventId, message, exception);
        }

        
        public static void BusinessError(int eventId, string type, Exception exception, string message)
        {
            log = Library.Logging.LogManager.GetLogger(type);
            if (log.IsBusinessErrorEnabled) log.BusinessError(eventId, message, exception);
        }

        
        public static void BusinessError(int eventId, Type type, Exception exception)
        {
            log = Library.Logging.LogManager.GetLogger(type);
            if (log.IsBusinessErrorEnabled) log.BusinessError(eventId, exception.Message, exception);
        }

        
        public static void BusinessError(int eventId, string type, Exception exception)
        {
            log = Library.Logging.LogManager.GetLogger(type);
            if (log.IsBusinessErrorEnabled) log.BusinessError(eventId, exception.Message, exception);
        }

        
        public static void BusinessError(int eventId, string message)
        {
            log = Library.Logging.LogManager.GetLogger(String.Empty);
            if (log.IsBusinessErrorEnabled) log.BusinessError(eventId, message);
        }

        
        public static void BusinessError(int eventId, Exception exception, string message)
        {
            log = Library.Logging.LogManager.GetLogger(String.Empty);
            if (log.IsBusinessErrorEnabled) log.BusinessError(eventId, message, exception);
        }

        
        public static void BusinessError(int eventId, Exception exception)
        {
            log = Library.Logging.LogManager.GetLogger(String.Empty);
            if (log.IsBusinessErrorEnabled) log.BusinessError(eventId, exception.Message, exception);
        }
        #endregion

        #region Log Information
        /// <summary>
        /// Helper methods to log a Information message
        /// </summary>
        /// <param name="type">The object <paramref name="type"/> will be used for the logger. Ex. typeOf(Classname) or this.GetType();</param>
        /// <param name="message">The message string to log.</param>
        public static void Information(Type type, string message)
        {
            log = Library.Logging.LogManager.GetLogger(type);
            if (log.IsInfoEnabled) log.Info(message);
        }

        /// <summary>
        /// Helper methods to log a Information message
        /// </summary>
        /// <param name="type">The object <paramref name="type"/> will be used for the logger. Ex. System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, typeOf(Classname) or this.GetType();</param>
        /// <param name="message">The message string to log.</param>
        public static void Information(string type, string message)
        {
            log = Library.Logging.LogManager.GetLogger(type);
            if (log.IsInfoEnabled) log.Info(message);
        }

        /// <summary>
        /// Helper methods to log a Information message including
        /// the stack trace of the <see cref="Exception"/> passed
        /// as a parameter.
        /// </summary>
        /// <param name="type">The object <paramref name="type"/> will be used for the logger. Ex. typeOf(Classname) or this.GetType();</param>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        /// <param name="message">The message string to log.</param>
        public static void Information(Type type, Exception exception, string message)
        {
            log = Library.Logging.LogManager.GetLogger(type);
            if (log.IsInfoEnabled) log.Info(message, exception);
        }

        /// <summary>
        /// Helper methods to log a Information message including
        /// the stack trace of the <see cref="Exception"/> passed
        /// as a parameter.
        /// </summary>
        /// <param name="type">The object <paramref name="type"/> will be used for the logger. Ex. System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, typeOf(Classname) or this.GetType();</param>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        /// <param name="message">The message string to log.</param>
        public static void Information(string type, Exception exception, string message)
        {
            log = Library.Logging.LogManager.GetLogger(type);
            if (log.IsInfoEnabled) log.Info(message, exception);
        }

        /// <summary>
        /// Helper methods to log a Information with
        /// the stack trace of the <see cref="Exception"/> passed
        /// as a parameter.
        /// </summary>
        /// <param name="type">The object <paramref name="type"/> will be used for the logger. Ex. typeOf(Classname) or this.GetType();</param>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        public static void Information(Type type, Exception exception)
        {
            log = Library.Logging.LogManager.GetLogger(type);
            if (log.IsInfoEnabled) log.Info(exception.Message, exception);
        }

        /// <summary>
        /// Helper methods to log a Information with
        /// the stack trace of the <see cref="Exception"/> passed
        /// as a parameter.
        /// </summary>
        /// <param name="type">The object <paramref name="type"/> will be used for the logger. Ex. System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, typeOf(Classname) or this.GetType();</param>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        public static void Information(string type, Exception exception)
        {
            log = Library.Logging.LogManager.GetLogger(type);
            if (log.IsInfoEnabled) log.Info(exception.Message, exception);
        }

        /// <summary>
        /// Helper methods to log a Information message
        /// </summary>
        /// <param name="message">The message string to log.</param>
        public static void Information(string message)
        {
            log = Library.Logging.LogManager.GetLogger(String.Empty);
            if (log.IsInfoEnabled) log.Info(message);
        }

        /// <summary>
        /// Helper methods to log a Information message including
        /// the stack trace of the <see cref="Exception"/> passed
        /// as a parameter.
        /// </summary>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        /// <param name="message">The message string to log.</param>
        public static void Information(Exception exception, string message)
        {
            log = Library.Logging.LogManager.GetLogger(String.Empty);
            if (log.IsInfoEnabled) log.Info(message, exception);
        }

        /// <summary>
        /// Helper methods to log a Information with
        /// the stack trace of the <see cref="Exception"/> passed
        /// as a parameter.
        /// </summary>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        public static void Information(Exception exception)
        {
            log = Library.Logging.LogManager.GetLogger(String.Empty);
            if (log.IsInfoEnabled) log.Info(exception.Message, exception);
        }
        #endregion

        #region Log Warning
        
        public static void Warning(int eventId, Type type, string message)
        {
            log = Library.Logging.LogManager.GetLogger(type);
            if (log.IsWarnEnabled) log.Warn(eventId, message);
        }

        
        public static void Warning(int eventId, string type, string message)
        {
            log = Library.Logging.LogManager.GetLogger(type);
            if (log.IsWarnEnabled) log.Warn(eventId, message);
        }

        
        public static void Warning(int eventId, Type type, Exception exception, string message)
        {
            log = Library.Logging.LogManager.GetLogger(type);
            if (log.IsWarnEnabled) log.Warn(eventId, message, exception);
        }

        
        public static void Warning(int eventId, string type, Exception exception, string message)
        {
            log = Library.Logging.LogManager.GetLogger(type);
            if (log.IsWarnEnabled) log.Warn(eventId, message, exception);
        }

        
        public static void Warning(int eventId, Type type, Exception exception)
        {
            log = Library.Logging.LogManager.GetLogger(type);
            if (log.IsWarnEnabled) log.Warn(eventId, exception.Message, exception);
        }

        
        public static void Warning(int eventId, string type, Exception exception)
        {
            log = Library.Logging.LogManager.GetLogger(type);
            if (log.IsWarnEnabled) log.Warn(eventId, exception.Message, exception);
        }

        
        public static void Warning(int eventId, string message)
        {
            log = Library.Logging.LogManager.GetLogger(String.Empty);
            if (log.IsWarnEnabled) log.Warn(eventId, message);
        }

        
        public static void Warning(int eventId, Exception exception, string message)
        {
            log = Library.Logging.LogManager.GetLogger(String.Empty);
            if (log.IsWarnEnabled) log.Warn(eventId, message, exception);
        }

        

        public static void Warning(int eventId, Exception exception)
        {
            log = Library.Logging.LogManager.GetLogger(String.Empty);
            if (log.IsWarnEnabled) log.Warn(eventId, exception.Message, exception);
        }
        #endregion

        #region Log Error
        public static void Error(int eventId, Type type, string message)
        {
            log = Library.Logging.LogManager.GetLogger(type);
            if (log.IsErrorEnabled) log.Error(eventId, message);
        }

        
        public static void Error(int eventId, string type, string message)
        {
            log = Library.Logging.LogManager.GetLogger(type);
            if (log.IsErrorEnabled) log.Error(eventId, message);
        }

        
        public static void Error(int eventId, Type type, Exception exception, string message)
        {
            log = Library.Logging.LogManager.GetLogger(type);
            if (log.IsErrorEnabled) log.Error(eventId, message, exception);
        }

        
        public static void Error(int eventId, string type, Exception exception, string message)
        {
            log = Library.Logging.LogManager.GetLogger(type);
            if (log.IsErrorEnabled) log.Error(eventId, message, exception);
        }

        
        public static void Error(int eventId, Type type, Exception exception)
        {
            log = Library.Logging.LogManager.GetLogger(type);
            if (log.IsErrorEnabled) log.Error(eventId, exception.Message, exception);
        }

        
        public static void Error(int eventId, string type, Exception exception)
        {
            log = Library.Logging.LogManager.GetLogger(type);
            if (log.IsErrorEnabled) log.Error(eventId, exception.Message, exception);
        }
        
        
        public static void Error(int eventId, string message)
        {
            log = Library.Logging.LogManager.GetLogger(String.Empty);
            if (log.IsErrorEnabled) log.Error(eventId, message);
        }

        
        public static void Error(int eventId, Exception exception, string message)
        {
            log = Library.Logging.LogManager.GetLogger(String.Empty);
            if (log.IsErrorEnabled) log.Error(eventId, message, exception);
        }

        
        public static void Error(int eventId, Exception exception)
        {
            log = Library.Logging.LogManager.GetLogger(String.Empty);
            if (log.IsErrorEnabled) log.Error(eventId, exception.Message, exception);
        }
        #endregion

        #region Log Fatal
        public static void Fatal(int eventId, Type type, string message)
        {
            log = Library.Logging.LogManager.GetLogger(type);
            if (log.IsFatalEnabled) log.Fatal(eventId, message);
        }

        
        public static void Fatal(int eventId, string type, string message)
        {
            log = Library.Logging.LogManager.GetLogger(type);
            if (log.IsFatalEnabled) log.Fatal(eventId, message);
        }

        
        public static void Fatal(int eventId, Type type, Exception exception, string message)
        {
            log = Library.Logging.LogManager.GetLogger(type);
            if (log.IsFatalEnabled) log.Fatal(eventId, message, exception);
        }

        
        public static void Fatal(int eventId, string type, Exception exception, string message)
        {
            log = Library.Logging.LogManager.GetLogger(type);
            if (log.IsFatalEnabled) log.Fatal(eventId, message, exception);
        }

        
        public static void Fatal(int eventId, Type type, Exception exception)
        {
            log = Library.Logging.LogManager.GetLogger(type);
            if (log.IsFatalEnabled) log.Fatal(eventId, exception.Message, exception);
        }

        
        public static void Fatal(int eventId, string type, Exception exception)
        {
            log = Library.Logging.LogManager.GetLogger(type);
            if (log.IsFatalEnabled) log.Fatal(eventId, exception.Message, exception);
        }

        
        public static void Fatal(int eventId, string message)
        {
            log = Library.Logging.LogManager.GetLogger(String.Empty);
            if (log.IsFatalEnabled) log.Fatal(eventId, message);
        }

        
        public static void Fatal(int eventId, Exception exception, string message)
        {
            log = Library.Logging.LogManager.GetLogger(String.Empty);
            if (log.IsFatalEnabled) log.Fatal(eventId, message, exception);
        }

        
        public static void Fatal(int eventId, Exception exception)
        {
            log = Library.Logging.LogManager.GetLogger(String.Empty);
            if (log.IsFatalEnabled) log.Fatal(eventId, exception.Message, exception);
        }
        #endregion

        #region Log ExternalSystemInformation
        /// <summary>
        /// Helper methods to log a ExternalSystemInformation message
        /// </summary>
        /// <param name="type">The object <paramref name="type"/> will be used for the logger. Ex. typeOf(Classname) or this.GetType();</param>
        /// <param name="message">The message string to log.</param>
        public static void ExternalSystemInformation(Type type, string message)
        {
            log = Library.Logging.LogManager.GetLogger(type);
            if (log.IsExternalSystemInformationEnabled) log.ExternalSystemInformation(message);
        }

        /// <summary>
        /// Helper methods to log a ExternalSystemInformation message
        /// </summary>
        /// <param name="type">The object <paramref name="type"/> will be used for the logger. Ex. System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, typeOf(Classname) or this.GetType();</param>
        /// <param name="message">The message string to log.</param>
        public static void ExternalSystemInformation(string type, string message)
        {
            log = Library.Logging.LogManager.GetLogger(type);
            if (log.IsExternalSystemInformationEnabled) log.ExternalSystemInformation(message);
        }

        /// <summary>
        /// Helper methods to log a ExternalSystemInformation message including
        /// the stack trace of the <see cref="Exception"/> passed
        /// as a parameter.
        /// </summary>
        /// <param name="type">The object <paramref name="type"/> will be used for the logger. Ex. typeOf(Classname) or this.GetType();</param>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        /// <param name="message">The message string to log.</param>
        public static void ExternalSystemInformation(Type type, Exception exception, string message)
        {
            log = Library.Logging.LogManager.GetLogger(type);
            if (log.IsExternalSystemInformationEnabled) log.ExternalSystemInformation(message, exception);
        }

        /// <summary>
        /// Helper methods to log a ExternalSystemInformation message including
        /// the stack trace of the <see cref="Exception"/> passed
        /// as a parameter.
        /// </summary>
        /// <param name="type">The object <paramref name="type"/> will be used for the logger. Ex. System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, typeOf(Classname) or this.GetType();</param>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        /// <param name="message">The message string to log.</param>
        public static void ExternalSystemInformation(string type, Exception exception, string message)
        {
            log = Library.Logging.LogManager.GetLogger(type);
            if (log.IsExternalSystemInformationEnabled) log.ExternalSystemInformation(message, exception);
        }

        /// <summary>
        /// Helper methods to log a ExternalSystemInformation with
        /// the stack trace of the <see cref="Exception"/> passed
        /// as a parameter.
        /// </summary>
        /// <param name="type">The object <paramref name="type"/> will be used for the logger. Ex. typeOf(Classname) or this.GetType();</param>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        public static void ExternalSystemInformation(Type type, Exception exception)
        {
            log = Library.Logging.LogManager.GetLogger(type);
            if (log.IsExternalSystemInformationEnabled) log.ExternalSystemInformation(exception.Message, exception);
        }

        /// <summary>
        /// Helper methods to log a ExternalSystemInformation with
        /// the stack trace of the <see cref="Exception"/> passed
        /// as a parameter.
        /// </summary>
        /// <param name="type">The object <paramref name="type"/> will be used for the logger. Ex. System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, typeOf(Classname) or this.GetType();</param>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        public static void ExternalSystemInformation(string type, Exception exception)
        {
            log = Library.Logging.LogManager.GetLogger(type);
            if (log.IsExternalSystemInformationEnabled) log.ExternalSystemInformation(exception.Message, exception);
        }

        /// <summary>
        /// Helper methods to log a ExternalSystemInformation message
        /// </summary>
        /// <param name="message">The message string to log.</param>
        public static void ExternalSystemInformation(string message)
        {
            log = Library.Logging.LogManager.GetLogger(String.Empty);
            if (log.IsExternalSystemInformationEnabled) log.ExternalSystemInformation(message);
        }

        /// <summary>
        /// Helper methods to log a ExternalSystemInformation message including
        /// the stack trace of the <see cref="Exception"/> passed
        /// as a parameter.
        /// </summary>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        /// <param name="message">The message string to log.</param>
        public static void ExternalSystemInformation(Exception exception, string message)
        {
            log = Library.Logging.LogManager.GetLogger(String.Empty);
            if (log.IsExternalSystemInformationEnabled) log.ExternalSystemInformation(message, exception);
        }

        /// <summary>
        /// Helper methods to log a ExternalSystemInformation with
        /// the stack trace of the <see cref="Exception"/> passed
        /// as a parameter.
        /// </summary>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        public static void ExternalSystemInformation(Exception exception)
        {
            log = Library.Logging.LogManager.GetLogger(String.Empty);
            if (log.IsExternalSystemInformationEnabled) log.ExternalSystemInformation(exception.Message, exception);
        }
        #endregion

    }
}
