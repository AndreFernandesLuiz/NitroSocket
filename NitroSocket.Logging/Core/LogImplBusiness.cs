using System;
using System.Globalization;

using Library.Logging.Repository;
using Library.Logging.Util;

namespace Library.Logging.Core
{

    /// <summary>
    /// Implementation of <see cref="ILog"/> wrapper interface.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This implementation of the <see cref="ILog"/> interface
    /// forwards to the <see cref="ILogger"/> held by the base class.
    /// </para>
    /// <para>
    /// This logger has methods to allow the caller to log at the following
    /// levels:
    /// </para>
    /// <list type="definition">
    ///   <item>
    ///     <term>TRACE</term>
    ///     <description>
    ///     The <see cref="Trace(object)"/> and <see cref="DebugFormat(string, object[])"/> methods log messages
    ///     at the <c>TRACE</c> level. That is the level with that name defined in the
    ///     repositories <see cref="ILoggerRepository.LevelMap"/>. The default value
    ///     for this level is <see cref="Level.Trace"/>. The <see cref="IsDebugEnabled"/>
    ///     property tests if this level is enabled for logging.
    ///     </description>
    ///   </item>
    ///   <item>
    ///     <term>BUSINESSERROR</term>
    ///     <description>
    ///     The <see cref="BusinessError(object)"/> and <see cref="BusinessErrorFormat(string, object[])"/> methods log messages
    ///     at the <c>BUSINESSERROR</c> level. That is the level with that name defined in the
    ///     repositories <see cref="ILoggerRepository.LevelMap"/>. The default value
    ///     for this level is <see cref="Level.BusinessError"/>. The <see cref="IsBusinessErrorEnabled"/>
    ///     property tests if this level is enabled for logging.
    ///     </description>
    ///   </item>

    public partial class LogImpl : LoggerWrapperImpl, ILog
    {
        /// <summary>
        /// Logs a message object with the <c>BUSINESSERROR</c> level.
        /// </summary>
        /// <param name="message">the message object to log</param>
        /// <remarks>
        /// <para>
        /// This method first checks if this logger is <c>BUSINESSERROR</c>
        /// enabled by comparing the level of this logger with the 
        /// <c>BUSINESSERROR</c> level. If this logger is
        /// <c>BUSINESSERROR</c> enabled, then it converts the message object
        /// (passed as parameter) to a string by invoking the appropriate
        /// <see cref="Library.Logging.ObjectRenderer.IObjectRenderer"/>. It then 
        /// proceeds to call all the registered appenders in this logger and 
        /// also higher in the hierarchy depending on the value of the 
        /// additivity flag.
        /// </para>
        /// <para>
        /// <b>WARNING</b> Note that passing an <see cref="Exception"/> to this
        /// method will print the name of the <see cref="Exception"/> but no
        /// stack trace. To print a stack trace use the 
        /// <see cref="Warn(object,Exception)"/> form instead.
        /// </para>
        /// </remarks>
        virtual public void BusinessError(int eventId, object message)
        {
            Logger.Log(ThisDeclaringType, eventId, m_levelBusinessError, message, null);
        }

        /// <summary>
        /// Logs a message object with the <c>BUSINESSERROR</c> level
        /// </summary>
        /// <param name="message">The message object to log.</param>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        /// <remarks>
        /// <para>
        /// Logs a message object with the <c>BUSINESSERROR</c> level including
        /// the stack trace of the <see cref="Exception"/> <paramref name="exception"/> 
        /// passed as a parameter.
        /// </para>
        /// <para>
        /// See the <see cref="BusinessError(object)"/> form for more detailed information.
        /// </para>
        /// </remarks>
        /// <seealso cref="BusinessError(object)"/>
        virtual public void BusinessError(int eventId, object message, Exception exception)
        {
            Logger.Log(ThisDeclaringType, eventId, m_levelBusinessError, message, exception);
        }

        /// <summary>
        /// Logs a formatted message string with the <c>BUSINESSERROR</c> level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="args">An Object array containing zero or more objects to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <see cref="String.Format(IFormatProvider, string, object[])"/> method. See
        /// <c>String.Format</c> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// The string is formatted using the <see cref="CultureInfo.InvariantCulture"/>
        /// format provider. To specify a localized provider use the
        /// <see cref="WarnFormat(IFormatProvider,string,object[])"/> method.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="Exception"/> object to include in the
        /// log event. To pass an <see cref="Exception"/> use one of the <see cref="Warn(object)"/>
        /// methods instead.
        /// </para>
        /// </remarks>
        virtual public void BusinessErrorFormat(string format, params object[] args)
        {
            if (IsBusinessErrorEnabled)
            {
                Logger.Log(ThisDeclaringType, m_levelBusinessError, new SystemStringFormat(CultureInfo.InvariantCulture, format, args), null);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the <c>BUSINESSERROR</c> level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">An Object to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <see cref="String.Format(IFormatProvider, string, object[])"/> method. See
        /// <c>String.Format</c> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// The string is formatted using the <see cref="CultureInfo.InvariantCulture"/>
        /// format provider. To specify a localized provider use the
        /// <see cref="BusinessErrorFormat(IFormatProvider,string,object[])"/> method.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="Exception"/> object to include in the
        /// log event. To pass an <see cref="Exception"/> use one of the <see cref="BusinessError(object)"/>
        /// methods instead.
        /// </para>
        /// </remarks>
        virtual public void BusinessErrorFormat(string format, object arg0)
        {
            if (IsBusinessErrorEnabled)
            {
                Logger.Log(ThisDeclaringType, m_levelBusinessError, new SystemStringFormat(CultureInfo.InvariantCulture, format, new object[] { arg0 }), null);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the <c>BUSINESSERROR</c> level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">An Object to format</param>
        /// <param name="arg1">An Object to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <see cref="String.Format(IFormatProvider, string, object[])"/> method. See
        /// <c>String.Format</c> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// The string is formatted using the <see cref="CultureInfo.InvariantCulture"/>
        /// format provider. To specify a localized provider use the
        /// <see cref="WarnFormat(IFormatProvider,string,object[])"/> method.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="Exception"/> object to include in the
        /// log event. To pass an <see cref="Exception"/> use one of the <see cref="Warn(object)"/>
        /// methods instead.
        /// </para>
        /// </remarks>
        virtual public void BusinessErrorFormat(string format, object arg0, object arg1)
        {
            if (IsBusinessErrorEnabled)
            {
                Logger.Log(ThisDeclaringType, m_levelBusinessError, new SystemStringFormat(CultureInfo.InvariantCulture, format, new object[] { arg0, arg1 }), null);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the <c>BUSINESSERROR</c> level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">An Object to format</param>
        /// <param name="arg1">An Object to format</param>
        /// <param name="arg2">An Object to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <see cref="String.Format(IFormatProvider, string, object[])"/> method. See
        /// <c>String.Format</c> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// The string is formatted using the <see cref="CultureInfo.InvariantCulture"/>
        /// format provider. To specify a localized provider use the
        /// <see cref="WarnFormat(IFormatProvider,string,object[])"/> method.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="Exception"/> object to include in the
        /// log event. To pass an <see cref="Exception"/> use one of the <see cref="BusinessError(object)"/>
        /// methods instead.
        /// </para>
        /// </remarks>
        virtual public void BusinessErrorFormat(string format, object arg0, object arg1, object arg2)
        {
            if (IsBusinessErrorEnabled)
            {
                Logger.Log(ThisDeclaringType, m_levelBusinessError, new SystemStringFormat(CultureInfo.InvariantCulture, format, new object[] { arg0, arg1, arg2 }), null);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the <c>BUSINESSERROR</c> level.
        /// </summary>
        /// <param name="provider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information</param>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="args">An Object array containing zero or more objects to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <see cref="String.Format(IFormatProvider, string, object[])"/> method. See
        /// <c>String.Format</c> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="Exception"/> object to include in the
        /// log event. To pass an <see cref="Exception"/> use one of the <see cref="Warn(object)"/>
        /// methods instead.
        /// </para>
        /// </remarks>
        virtual public void BusinessErrorFormat(IFormatProvider provider, string format, params object[] args)
        {
            if (IsBusinessErrorEnabled)
            {
                Logger.Log(ThisDeclaringType, m_levelBusinessError, new SystemStringFormat(provider, format, args), null);
            }
        }

    }
}
