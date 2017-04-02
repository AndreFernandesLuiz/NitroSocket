#region Apache License
//
// Licensed to the Apache Software Foundation (ASF) under one or more 
// contributor license agreements. See the NOTICE file distributed with
// this work for additional information regarding copyright ownership. 
// The ASF licenses this file to you under the Apache License, Version 2.0
// (the "License"); you may not use this file except in compliance with 
// the License. You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion


#region Copyright & License
//
// Copyright 2012   Incorporated
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// This software is a derivative work of Apache Log4Net software; 
//
#endregion

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
    ///   <item>
	///     <term>INFO</term>
	///     <description>
	///     The <see cref="Info(object)"/> and <see cref="InfoFormat(string, object[])"/> methods log messages
	///     at the <c>INFO</c> level. That is the level with that name defined in the
	///     repositories <see cref="ILoggerRepository.LevelMap"/>. The default value
	///     for this level is <see cref="Level.Info"/>. The <see cref="IsInfoEnabled"/>
	///     property tests if this level is enabled for logging.
	///     </description>
	///   </item>
	///   <item>
	///     <term>WARN</term>
	///     <description>
	///     The <see cref="Warn(object)"/> and <see cref="WarnFormat(string, object[])"/> methods log messages
	///     at the <c>WARN</c> level. That is the level with that name defined in the
	///     repositories <see cref="ILoggerRepository.LevelMap"/>. The default value
	///     for this level is <see cref="Level.Warn"/>. The <see cref="IsWarnEnabled"/>
	///     property tests if this level is enabled for logging.
	///     </description>
	///   </item>
	///   <item>
	///     <term>ERROR</term>
	///     <description>
	///     The <see cref="Error(object)"/> and <see cref="ErrorFormat(string, object[])"/> methods log messages
	///     at the <c>ERROR</c> level. That is the level with that name defined in the
	///     repositories <see cref="ILoggerRepository.LevelMap"/>. The default value
	///     for this level is <see cref="Level.Error"/>. The <see cref="IsErrorEnabled"/>
	///     property tests if this level is enabled for logging.
	///     </description>
	///   </item>
	///   <item>
	///     <term>FATAL</term>
	///     <description>
	///     The <see cref="Fatal(object)"/> and <see cref="FatalFormat(string, object[])"/> methods log messages
	///     at the <c>FATAL</c> level. That is the level with that name defined in the
	///     repositories <see cref="ILoggerRepository.LevelMap"/>. The default value
	///     for this level is <see cref="Level.Fatal"/>. The <see cref="IsFatalEnabled"/>
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
    /// </list>
	/// <para>
	/// The values for these levels and their semantic meanings can be changed by 
	/// configuring the <see cref="ILoggerRepository.LevelMap"/> for the repository.
	/// </para>
	/// </remarks>
	public partial class LogImpl : LoggerWrapperImpl, ILog
	{
		#region Public Instance Constructors

		/// <summary>
		/// Construct a new wrapper for the specified logger.
		/// </summary>
		/// <param name="logger">The logger to wrap.</param>
		/// <remarks>
		/// <para>
		/// Construct a new wrapper for the specified logger.
		/// </para>
		/// </remarks>
		public LogImpl(ILogger logger) : base(logger)
		{
			// Listen for changes to the repository
			logger.Repository.ConfigurationChanged += new LoggerRepositoryConfigurationChangedEventHandler(LoggerRepositoryConfigurationChanged);

			// load the current levels
			ReloadLevels(logger.Repository);
		}

		#endregion Public Instance Constructors

		/// <summary>
		/// Virtual method called when the configuration of the repository changes
		/// </summary>
		/// <param name="repository">the repository holding the levels</param>
		/// <remarks>
		/// <para>
		/// Virtual method called when the configuration of the repository changes
		/// </para>
		/// </remarks>
		protected virtual void ReloadLevels(ILoggerRepository repository)
		{
			LevelMap levelMap = repository.LevelMap;

            m_levelAll = levelMap.LookupWithDefault(Level.All);
            m_levelBusinessError = levelMap.LookupWithDefault(Level.BusinessError);
            m_levelDebug = levelMap.LookupWithDefault(Level.Trace);
			m_levelInfo = levelMap.LookupWithDefault(Level.Info);
			m_levelWarn = levelMap.LookupWithDefault(Level.Warn);
			m_levelError = levelMap.LookupWithDefault(Level.Error);
			m_levelFatal = levelMap.LookupWithDefault(Level.Fatal);
            m_levelNotice = levelMap.LookupWithDefault(Level.Notice);
            m_levelOff = levelMap.LookupWithDefault(Level.Off);
		}

		#region Implementation of ILog

		/// <summary>
		/// Logs a message object with the <c>TRACE</c> level.
		/// </summary>
		/// <param name="message">The message object to log.</param>
		/// <remarks>
		/// <para>
		/// This method first checks if this logger is <c>TRACE</c>
		/// enabled by comparing the level of this logger with the 
		/// <c>TRACE</c> level. If this logger is
		/// <c>TRACE</c> enabled, then it converts the message object
		/// (passed as parameter) to a string by invoking the appropriate
		/// <see cref="Library.Logging.ObjectRenderer.IObjectRenderer"/>. It then 
		/// proceeds to call all the registered appenders in this logger 
		/// and also higher in the hierarchy depending on the value of the 
		/// additivity flag.
		/// </para>
		/// <para>
		/// <b>WARNING</b> Note that passing an <see cref="Exception"/> 
		/// to this method will print the name of the <see cref="Exception"/> 
		/// but no stack trace. To print a stack trace use the 
		/// <see cref="Trace(object,Exception)"/> form instead.
		/// </para>
		/// </remarks>
		virtual public void Trace(object message)
		{
			Logger.Log(ThisDeclaringType, m_levelDebug, message, null);
		}
		/// <summary>
		/// Logs a message object with the <c>TRACE</c> level
		/// </summary>
		/// <param name="message">The message object to log.</param>
		/// <param name="exception">The exception to log, including its stack trace.</param>
		/// <remarks>
		/// <para>
		/// Logs a message object with the <c>TRACE</c> level including
		/// the stack trace of the <see cref="Exception"/> <paramref name="exception"/> passed
		/// as a parameter.
		/// </para>
		/// <para>
		/// See the <see cref="Trace(object)"/> form for more detailed information.
		/// </para>
		/// </remarks>
		/// <seealso cref="Trace(object)"/>
		virtual public void Trace(object message, Exception exception) 
		{
			Logger.Log(ThisDeclaringType, m_levelDebug, message, exception);
		}

		/// <summary>
		/// Logs a formatted message string with the <c>TRACE</c> level.
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
		/// <see cref="DebugFormat(IFormatProvider,string,object[])"/> method.
		/// </para>
		/// <para>
		/// This method does not take an <see cref="Exception"/> object to include in the
		/// log event. To pass an <see cref="Exception"/> use one of the <see cref="Trace(object)"/>
		/// methods instead.
		/// </para>
		/// </remarks>
		virtual public void DebugFormat(string format, params object[] args) 
		{
			if (IsDebugEnabled)
			{
				Logger.Log(ThisDeclaringType, m_levelDebug, new SystemStringFormat(CultureInfo.InvariantCulture, format, args), null);
			}
		}

		/// <summary>
		/// Logs a formatted message string with the <c>TRACE</c> level.
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
		/// <see cref="DebugFormat(IFormatProvider,string,object[])"/> method.
		/// </para>
		/// <para>
		/// This method does not take an <see cref="Exception"/> object to include in the
		/// log event. To pass an <see cref="Exception"/> use one of the <see cref="Trace(object)"/>
		/// methods instead.
		/// </para>
		/// </remarks>
		virtual public void DebugFormat(string format, object arg0) 
		{
			if (IsDebugEnabled)
			{
				Logger.Log(ThisDeclaringType, m_levelDebug, new SystemStringFormat(CultureInfo.InvariantCulture, format, new object[] { arg0 }), null);
			}
		}

		/// <summary>
		/// Logs a formatted message string with the <c>TRACE</c> level.
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
		/// <see cref="DebugFormat(IFormatProvider,string,object[])"/> method.
		/// </para>
		/// <para>
		/// This method does not take an <see cref="Exception"/> object to include in the
		/// log event. To pass an <see cref="Exception"/> use one of the <see cref="Trace(object)"/>
		/// methods instead.
		/// </para>
		/// </remarks>
		virtual public void DebugFormat(string format, object arg0, object arg1) 
		{
			if (IsDebugEnabled)
			{
				Logger.Log(ThisDeclaringType, m_levelDebug, new SystemStringFormat(CultureInfo.InvariantCulture, format, new object[] { arg0, arg1 }), null);
			}
		}

		/// <summary>
		/// Logs a formatted message string with the <c>TRACE</c> level.
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
		/// <see cref="DebugFormat(IFormatProvider,string,object[])"/> method.
		/// </para>
		/// <para>
		/// This method does not take an <see cref="Exception"/> object to include in the
		/// log event. To pass an <see cref="Exception"/> use one of the <see cref="Trace(object)"/>
		/// methods instead.
		/// </para>
		/// </remarks>
		virtual public void DebugFormat(string format, object arg0, object arg1, object arg2) 
		{
			if (IsDebugEnabled)
			{
				Logger.Log(ThisDeclaringType, m_levelDebug, new SystemStringFormat(CultureInfo.InvariantCulture, format, new object[] { arg0, arg1, arg2 }), null);
			}
		}

		/// <summary>
		/// Logs a formatted message string with the <c>TRACE</c> level.
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
		/// log event. To pass an <see cref="Exception"/> use one of the <see cref="Trace(object)"/>
		/// methods instead.
		/// </para>
		/// </remarks>
		virtual public void DebugFormat(IFormatProvider provider, string format, params object[] args) 
		{
			if (IsDebugEnabled)
			{
				Logger.Log(ThisDeclaringType, m_levelDebug, new SystemStringFormat(provider, format, args), null);
			}
		}

		/// <summary>
		/// Logs a message object with the <c>INFO</c> level.
		/// </summary>
		/// <param name="message">The message object to log.</param>
		/// <remarks>
		/// <para>
		/// This method first checks if this logger is <c>INFO</c>
		/// enabled by comparing the level of this logger with the 
		/// <c>INFO</c> level. If this logger is
		/// <c>INFO</c> enabled, then it converts the message object
		/// (passed as parameter) to a string by invoking the appropriate
		/// <see cref="Library.Logging.ObjectRenderer.IObjectRenderer"/>. It then 
		/// proceeds to call all the registered appenders in this logger 
		/// and also higher in the hierarchy depending on the value of 
		/// the additivity flag.
		/// </para>
		/// <para>
		/// <b>WARNING</b> Note that passing an <see cref="Exception"/> 
		/// to this method will print the name of the <see cref="Exception"/> 
		/// but no stack trace. To print a stack trace use the 
		/// <see cref="Info(object,Exception)"/> form instead.
		/// </para>
		/// </remarks>
		virtual public void Info(object message) 
		{
			Logger.Log(ThisDeclaringType, m_levelInfo, message, null);
		}
  
		/// <summary>
		/// Logs a message object with the <c>INFO</c> level.
		/// </summary>
		/// <param name="message">The message object to log.</param>
		/// <param name="exception">The exception to log, including its stack trace.</param>
		/// <remarks>
		/// <para>
		/// Logs a message object with the <c>INFO</c> level including
		/// the stack trace of the <see cref="Exception"/> <paramref name="exception"/> 
		/// passed as a parameter.
		/// </para>
		/// <para>
		/// See the <see cref="Info(object)"/> form for more detailed information.
		/// </para>
		/// </remarks>
		/// <seealso cref="Info(object)"/>
		virtual public void Info(object message, Exception exception) 
		{
			Logger.Log(ThisDeclaringType, m_levelInfo, message, exception);
		}

		/// <summary>
		/// Logs a formatted message string with the <c>INFO</c> level.
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
		/// <see cref="InfoFormat(IFormatProvider,string,object[])"/> method.
		/// </para>
		/// <para>
		/// This method does not take an <see cref="Exception"/> object to include in the
		/// log event. To pass an <see cref="Exception"/> use one of the <see cref="Info(object)"/>
		/// methods instead.
		/// </para>
		/// </remarks>
		virtual public void InfoFormat(string format, params object[] args) 
		{
			if (IsInfoEnabled)
			{
				Logger.Log(ThisDeclaringType, m_levelInfo, new SystemStringFormat(CultureInfo.InvariantCulture, format, args), null);
			}
		}

		/// <summary>
		/// Logs a formatted message string with the <c>INFO</c> level.
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
		/// <see cref="InfoFormat(IFormatProvider,string,object[])"/> method.
		/// </para>
		/// <para>
		/// This method does not take an <see cref="Exception"/> object to include in the
		/// log event. To pass an <see cref="Exception"/> use one of the <see cref="Info(object)"/>
		/// methods instead.
		/// </para>
		/// </remarks>
		virtual public void InfoFormat(string format, object arg0) 
		{
			if (IsInfoEnabled)
			{
				Logger.Log(ThisDeclaringType, m_levelInfo, new SystemStringFormat(CultureInfo.InvariantCulture, format, new object[] { arg0 }), null);
			}
		}

		/// <summary>
		/// Logs a formatted message string with the <c>INFO</c> level.
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
		/// <see cref="InfoFormat(IFormatProvider,string,object[])"/> method.
		/// </para>
		/// <para>
		/// This method does not take an <see cref="Exception"/> object to include in the
		/// log event. To pass an <see cref="Exception"/> use one of the <see cref="Info(object)"/>
		/// methods instead.
		/// </para>
		/// </remarks>
		virtual public void InfoFormat(string format, object arg0, object arg1) 
		{
			if (IsInfoEnabled)
			{
				Logger.Log(ThisDeclaringType, m_levelInfo, new SystemStringFormat(CultureInfo.InvariantCulture, format, new object[] { arg0, arg1 }), null);
			}
		}

		/// <summary>
		/// Logs a formatted message string with the <c>INFO</c> level.
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
		/// <see cref="InfoFormat(IFormatProvider,string,object[])"/> method.
		/// </para>
		/// <para>
		/// This method does not take an <see cref="Exception"/> object to include in the
		/// log event. To pass an <see cref="Exception"/> use one of the <see cref="Info(object)"/>
		/// methods instead.
		/// </para>
		/// </remarks>
		virtual public void InfoFormat(string format, object arg0, object arg1, object arg2) 
		{
			if (IsInfoEnabled)
			{
				Logger.Log(ThisDeclaringType, m_levelInfo, new SystemStringFormat(CultureInfo.InvariantCulture, format, new object[] { arg0, arg1, arg2 }), null);
			}
		}

		/// <summary>
		/// Logs a formatted message string with the <c>INFO</c> level.
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
		/// log event. To pass an <see cref="Exception"/> use one of the <see cref="Info(object)"/>
		/// methods instead.
		/// </para>
		/// </remarks>
		virtual public void InfoFormat(IFormatProvider provider, string format, params object[] args) 
		{
			if (IsInfoEnabled)
			{
				Logger.Log(ThisDeclaringType, m_levelInfo, new SystemStringFormat(provider, format, args), null);
			}
		}

		/// <summary>
		/// Logs a message object with the <c>WARN</c> level.
		/// </summary>
		/// <param name="message">the message object to log</param>
		/// <remarks>
		/// <para>
		/// This method first checks if this logger is <c>WARN</c>
		/// enabled by comparing the level of this logger with the 
		/// <c>WARN</c> level. If this logger is
		/// <c>WARN</c> enabled, then it converts the message object
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
        virtual public void Warn(int eventId, object message) 
		{
            Logger.Log(ThisDeclaringType, eventId, m_levelWarn, message, null);
		}
  
		/// <summary>
		/// Logs a message object with the <c>WARN</c> level
		/// </summary>
		/// <param name="message">The message object to log.</param>
		/// <param name="exception">The exception to log, including its stack trace.</param>
		/// <remarks>
		/// <para>
		/// Logs a message object with the <c>WARN</c> level including
		/// the stack trace of the <see cref="Exception"/> <paramref name="exception"/> 
		/// passed as a parameter.
		/// </para>
		/// <para>
		/// See the <see cref="Warn(object)"/> form for more detailed information.
		/// </para>
		/// </remarks>
		/// <seealso cref="Warn(object)"/>
        virtual public void Warn(int eventId, object message, Exception exception) 
		{
            Logger.Log(ThisDeclaringType, eventId, m_levelWarn, message, exception);
		}

		/// <summary>
		/// Logs a formatted message string with the <c>WARN</c> level.
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
		virtual public void WarnFormat(string format, params object[] args) 
		{
			if (IsWarnEnabled)
			{
				Logger.Log(ThisDeclaringType, m_levelWarn, new SystemStringFormat(CultureInfo.InvariantCulture, format, args), null);
			}
		}

		/// <summary>
		/// Logs a formatted message string with the <c>WARN</c> level.
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
		/// <see cref="WarnFormat(IFormatProvider,string,object[])"/> method.
		/// </para>
		/// <para>
		/// This method does not take an <see cref="Exception"/> object to include in the
		/// log event. To pass an <see cref="Exception"/> use one of the <see cref="Warn(object)"/>
		/// methods instead.
		/// </para>
		/// </remarks>
		virtual public void WarnFormat(string format, object arg0) 
		{
			if (IsWarnEnabled)
			{
				Logger.Log(ThisDeclaringType, m_levelWarn, new SystemStringFormat(CultureInfo.InvariantCulture, format, new object[] { arg0 }), null);
			}
		}

		/// <summary>
		/// Logs a formatted message string with the <c>WARN</c> level.
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
		virtual public void WarnFormat(string format, object arg0, object arg1) 
		{
			if (IsWarnEnabled)
			{
				Logger.Log(ThisDeclaringType, m_levelWarn, new SystemStringFormat(CultureInfo.InvariantCulture, format, new object[] { arg0, arg1 }), null);
			}
		}

		/// <summary>
		/// Logs a formatted message string with the <c>WARN</c> level.
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
		/// log event. To pass an <see cref="Exception"/> use one of the <see cref="Warn(object)"/>
		/// methods instead.
		/// </para>
		/// </remarks>
		virtual public void WarnFormat(string format, object arg0, object arg1, object arg2) 
		{
			if (IsWarnEnabled)
			{
				Logger.Log(ThisDeclaringType, m_levelWarn, new SystemStringFormat(CultureInfo.InvariantCulture, format, new object[] { arg0, arg1, arg2 }), null);
			}
		}

		/// <summary>
		/// Logs a formatted message string with the <c>WARN</c> level.
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
		virtual public void WarnFormat(IFormatProvider provider, string format, params object[] args) 
		{
			if (IsWarnEnabled)
			{
				Logger.Log(ThisDeclaringType, m_levelWarn, new SystemStringFormat(provider, format, args), null);
			}
		}

		/// <summary>
		/// Logs a message object with the <c>ERROR</c> level.
		/// </summary>
		/// <param name="message">The message object to log.</param>
		/// <remarks>
		/// <para>
		/// This method first checks if this logger is <c>ERROR</c>
		/// enabled by comparing the level of this logger with the 
		/// <c>ERROR</c> level. If this logger is
		/// <c>ERROR</c> enabled, then it converts the message object
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
		/// <see cref="Error(object,Exception)"/> form instead.
		/// </para>
		/// </remarks>
        virtual public void Error(int eventId, object message) 
		{
            Logger.Log(ThisDeclaringType, eventId, m_levelError, message, null);
		}

		/// <summary>
		/// Logs a message object with the <c>ERROR</c> level
		/// </summary>
		/// <param name="message">The message object to log.</param>
		/// <param name="exception">The exception to log, including its stack trace.</param>
		/// <remarks>
		/// <para>
		/// Logs a message object with the <c>ERROR</c> level including
		/// the stack trace of the <see cref="Exception"/> <paramref name="exception"/> 
		/// passed as a parameter.
		/// </para>
		/// <para>
		/// See the <see cref="Error(object)"/> form for more detailed information.
		/// </para>
		/// </remarks>
		/// <seealso cref="Error(object)"/>
        virtual public void Error(int eventId, object message, Exception exception) 
		{
            Logger.Log(ThisDeclaringType, eventId, m_levelError, message, exception);
		}

		/// <summary>
		/// Logs a formatted message string with the <c>ERROR</c> level.
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
		/// <see cref="ErrorFormat(IFormatProvider,string,object[])"/> method.
		/// </para>
		/// <para>
		/// This method does not take an <see cref="Exception"/> object to include in the
		/// log event. To pass an <see cref="Exception"/> use one of the <see cref="Error(object)"/>
		/// methods instead.
		/// </para>
		/// </remarks>
		virtual public void ErrorFormat(string format, params object[] args) 
		{
			if (IsErrorEnabled)
			{
				Logger.Log(ThisDeclaringType, m_levelError, new SystemStringFormat(CultureInfo.InvariantCulture, format, args), null);
			}
		}

		/// <summary>
		/// Logs a formatted message string with the <c>ERROR</c> level.
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
		/// <see cref="ErrorFormat(IFormatProvider,string,object[])"/> method.
		/// </para>
		/// <para>
		/// This method does not take an <see cref="Exception"/> object to include in the
		/// log event. To pass an <see cref="Exception"/> use one of the <see cref="Error(object)"/>
		/// methods instead.
		/// </para>
		/// </remarks>
		virtual public void ErrorFormat(string format, object arg0) 
		{
			if (IsErrorEnabled)
			{
				Logger.Log(ThisDeclaringType, m_levelError, new SystemStringFormat(CultureInfo.InvariantCulture, format, new object[] { arg0 }), null);
			}
		}

		/// <summary>
		/// Logs a formatted message string with the <c>ERROR</c> level.
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
		/// <see cref="ErrorFormat(IFormatProvider,string,object[])"/> method.
		/// </para>
		/// <para>
		/// This method does not take an <see cref="Exception"/> object to include in the
		/// log event. To pass an <see cref="Exception"/> use one of the <see cref="Error(object)"/>
		/// methods instead.
		/// </para>
		/// </remarks>
		virtual public void ErrorFormat(string format, object arg0, object arg1) 
		{
			if (IsErrorEnabled)
			{
				Logger.Log(ThisDeclaringType, m_levelError, new SystemStringFormat(CultureInfo.InvariantCulture, format, new object[] { arg0, arg1 }), null);
			}
		}

		/// <summary>
		/// Logs a formatted message string with the <c>ERROR</c> level.
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
		/// <see cref="ErrorFormat(IFormatProvider,string,object[])"/> method.
		/// </para>
		/// <para>
		/// This method does not take an <see cref="Exception"/> object to include in the
		/// log event. To pass an <see cref="Exception"/> use one of the <see cref="Error(object)"/>
		/// methods instead.
		/// </para>
		/// </remarks>
		virtual public void ErrorFormat(string format, object arg0, object arg1, object arg2) 
		{
			if (IsErrorEnabled)
			{
				Logger.Log(ThisDeclaringType, m_levelError, new SystemStringFormat(CultureInfo.InvariantCulture, format, new object[] { arg0, arg1, arg2 }), null);
			}
		}

		/// <summary>
		/// Logs a formatted message string with the <c>ERROR</c> level.
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
		/// log event. To pass an <see cref="Exception"/> use one of the <see cref="Error(object)"/>
		/// methods instead.
		/// </para>
		/// </remarks>
		virtual public void ErrorFormat(IFormatProvider provider, string format, params object[] args) 
		{
			if (IsErrorEnabled)
			{
				Logger.Log(ThisDeclaringType, m_levelError, new SystemStringFormat(provider, format, args), null);
			}
		}

		/// <summary>
		/// Logs a message object with the <c>FATAL</c> level.
		/// </summary>
		/// <param name="message">The message object to log.</param>
		/// <remarks>
		/// <para>
		/// This method first checks if this logger is <c>FATAL</c>
		/// enabled by comparing the level of this logger with the 
		/// <c>FATAL</c> level. If this logger is
		/// <c>FATAL</c> enabled, then it converts the message object
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
		/// <see cref="Fatal(object,Exception)"/> form instead.
		/// </para>
		/// </remarks>
        virtual public void Fatal(int eventId, object message) 
		{
            Logger.Log(ThisDeclaringType, eventId, m_levelFatal, message, null);
		}
  
		/// <summary>
		/// Logs a message object with the <c>FATAL</c> level
		/// </summary>
		/// <param name="message">The message object to log.</param>
		/// <param name="exception">The exception to log, including its stack trace.</param>
		/// <remarks>
		/// <para>
		/// Logs a message object with the <c>FATAL</c> level including
		/// the stack trace of the <see cref="Exception"/> <paramref name="exception"/> 
		/// passed as a parameter.
		/// </para>
		/// <para>
		/// See the <see cref="Fatal(object)"/> form for more detailed information.
		/// </para>
		/// </remarks>
		/// <seealso cref="Fatal(object)"/>
		virtual public void Fatal(int eventId, object message, Exception exception) 
		{
			Logger.Log(ThisDeclaringType, eventId, m_levelFatal, message, exception);
		}

		/// <summary>
		/// Logs a formatted message string with the <c>FATAL</c> level.
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
		/// <see cref="FatalFormat(IFormatProvider,string,object[])"/> method.
		/// </para>
		/// <para>
		/// This method does not take an <see cref="Exception"/> object to include in the
		/// log event. To pass an <see cref="Exception"/> use one of the <see cref="Fatal(object)"/>
		/// methods instead.
		/// </para>
		/// </remarks>
		virtual public void FatalFormat(string format, params object[] args) 
		{
			if (IsFatalEnabled)
			{
				Logger.Log(ThisDeclaringType, m_levelFatal, new SystemStringFormat(CultureInfo.InvariantCulture, format, args), null);
			}
		}

		/// <summary>
		/// Logs a formatted message string with the <c>FATAL</c> level.
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
		/// <see cref="FatalFormat(IFormatProvider,string,object[])"/> method.
		/// </para>
		/// <para>
		/// This method does not take an <see cref="Exception"/> object to include in the
		/// log event. To pass an <see cref="Exception"/> use one of the <see cref="Fatal(object)"/>
		/// methods instead.
		/// </para>
		/// </remarks>
		virtual public void FatalFormat(string format, object arg0) 
		{
			if (IsFatalEnabled)
			{
				Logger.Log(ThisDeclaringType, m_levelFatal, new SystemStringFormat(CultureInfo.InvariantCulture, format, new object[] { arg0 }), null);
			}
		}

		/// <summary>
		/// Logs a formatted message string with the <c>FATAL</c> level.
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
		/// <see cref="FatalFormat(IFormatProvider,string,object[])"/> method.
		/// </para>
		/// <para>
		/// This method does not take an <see cref="Exception"/> object to include in the
		/// log event. To pass an <see cref="Exception"/> use one of the <see cref="Fatal(object)"/>
		/// methods instead.
		/// </para>
		/// </remarks>
		virtual public void FatalFormat(string format, object arg0, object arg1) 
		{
			if (IsFatalEnabled)
			{
				Logger.Log(ThisDeclaringType, m_levelFatal, new SystemStringFormat(CultureInfo.InvariantCulture, format, new object[] { arg0, arg1 }), null);
			}
		}

		/// <summary>
		/// Logs a formatted message string with the <c>FATAL</c> level.
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
		/// <see cref="FatalFormat(IFormatProvider,string,object[])"/> method.
		/// </para>
		/// <para>
		/// This method does not take an <see cref="Exception"/> object to include in the
		/// log event. To pass an <see cref="Exception"/> use one of the <see cref="Fatal(object)"/>
		/// methods instead.
		/// </para>
		/// </remarks>
		virtual public void FatalFormat(string format, object arg0, object arg1, object arg2) 
		{
			if (IsFatalEnabled)
			{
				Logger.Log(ThisDeclaringType, m_levelFatal, new SystemStringFormat(CultureInfo.InvariantCulture, format, new object[] { arg0, arg1, arg2 }), null);
			}
		}

		/// <summary>
		/// Logs a formatted message string with the <c>FATAL</c> level.
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
		/// log event. To pass an <see cref="Exception"/> use one of the <see cref="Fatal(object)"/>
		/// methods instead.
		/// </para>
		/// </remarks>
		virtual public void FatalFormat(IFormatProvider provider, string format, params object[] args) 
		{
			if (IsFatalEnabled)
			{
				Logger.Log(ThisDeclaringType, m_levelFatal, new SystemStringFormat(provider, format, args), null);
			}
        }

        /// <summary>
        /// Logs a message object with the <c>ExternalSystemInformation</c> level.
        /// </summary>
        /// <param name="message">The message object to log.</param>
        /// <remarks>
        /// <para>
        /// This method first checks if this logger is <c>ExternalSystemInformation</c>
        /// enabled by comparing the level of this logger with the 
        /// <c>ExternalSystemInformation</c> level. If this logger is
        /// <c>ExternalSystemInformation</c> enabled, then it converts the message object
        /// (passed as parameter) to a string by invoking the appropriate
        /// <see cref="Library.Logging.ObjectRenderer.IObjectRenderer"/>. It then 
        /// proceeds to call all the registered appenders in this logger 
        /// and also higher in the hierarchy depending on the value of 
        /// the additivity flag.
        /// </para>
        /// <para>
        /// <b>WARNING</b> Note that passing an <see cref="Exception"/> 
        /// to this method will print the name of the <see cref="Exception"/> 
        /// but no stack trace. To print a stack trace use the 
        /// <see cref="ExternalSystemInformation(object,Exception)"/> form instead.
        /// </para>
        /// </remarks>
        virtual public void ExternalSystemInformation(object message)
        {
            Logger.Log(ThisDeclaringType, m_levelExternalSystemInformation, message, null);
        }

        /// <summary>
        /// Logs a message object with the <c>ExternalSystemInformation</c> level.
        /// </summary>
        /// <param name="message">The message object to log.</param>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        /// <remarks>
        /// <para>
        /// Logs a message object with the <c>ExternalSystemInformation</c> level including
        /// the stack trace of the <see cref="Exception"/> <paramref name="exception"/> 
        /// passed as a parameter.
        /// </para>
        /// <para>
        /// See the <see cref="ExternalSystemInformation(object)"/> form for more detailed ExternalSystemInformationrmation.
        /// </para>
        /// </remarks>
        /// <seealso cref="ExternalSystemInformation(object)"/>
        virtual public void ExternalSystemInformation(object message, Exception exception)
        {
            Logger.Log(ThisDeclaringType, m_levelExternalSystemInformation, message, exception);
        }

        /// <summary>
        /// Logs a formatted message string with the <c>ExternalSystemInformation</c> level.
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
        /// <see cref="ExternalSystemInformationFormat(IFormatProvider,string,object[])"/> method.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="Exception"/> object to include in the
        /// log event. To pass an <see cref="Exception"/> use one of the <see cref="ExternalSystemInformation(object)"/>
        /// methods instead.
        /// </para>
        /// </remarks>
        virtual public void ExternalSystemInformationFormat(string format, params object[] args)
        {
            if (IsExternalSystemInformationEnabled)
            {
                Logger.Log(ThisDeclaringType, m_levelExternalSystemInformation, new SystemStringFormat(CultureInfo.InvariantCulture, format, args), null);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the <c>ExternalSystemInformation</c> level.
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
        /// <see cref="ExternalSystemInformationFormat(IFormatProvider,string,object[])"/> method.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="Exception"/> object to include in the
        /// log event. To pass an <see cref="Exception"/> use one of the <see cref="ExternalSystemInformation(object)"/>
        /// methods instead.
        /// </para>
        /// </remarks>
        virtual public void ExternalSystemInformationFormat(string format, object arg0)
        {
            if (IsExternalSystemInformationEnabled)
            {
                Logger.Log(ThisDeclaringType, m_levelExternalSystemInformation, new SystemStringFormat(CultureInfo.InvariantCulture, format, new object[] { arg0 }), null);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the <c>ExternalSystemInformation</c> level.
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
        /// <see cref="ExternalSystemInformationFormat(IFormatProvider,string,object[])"/> method.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="Exception"/> object to include in the
        /// log event. To pass an <see cref="Exception"/> use one of the <see cref="ExternalSystemInformation(object)"/>
        /// methods instead.
        /// </para>
        /// </remarks>
        virtual public void ExternalSystemInformationFormat(string format, object arg0, object arg1)
        {
            if (IsExternalSystemInformationEnabled)
            {
                Logger.Log(ThisDeclaringType, m_levelExternalSystemInformation, new SystemStringFormat(CultureInfo.InvariantCulture, format, new object[] { arg0, arg1 }), null);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the <c>ExternalSystemInformation</c> level.
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
        /// <see cref="ExternalSystemInformationFormat(IFormatProvider,string,object[])"/> method.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="Exception"/> object to include in the
        /// log event. To pass an <see cref="Exception"/> use one of the <see cref="ExternalSystemInformation(object)"/>
        /// methods instead.
        /// </para>
        /// </remarks>
        virtual public void ExternalSystemInformationFormat(string format, object arg0, object arg1, object arg2)
        {
            if (IsExternalSystemInformationEnabled)
            {
                Logger.Log(ThisDeclaringType, m_levelExternalSystemInformation, new SystemStringFormat(CultureInfo.InvariantCulture, format, new object[] { arg0, arg1, arg2 }), null);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the <c>ExternalSystemInformation</c> level.
        /// </summary>
        /// <param name="provider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting ExternalSystemInformationrmation</param>
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
        /// log event. To pass an <see cref="Exception"/> use one of the <see cref="ExternalSystemInformation(object)"/>
        /// methods instead.
        /// </para>
        /// </remarks>
        virtual public void ExternalSystemInformationFormat(IFormatProvider provider, string format, params object[] args)
        {
            if (IsExternalSystemInformationEnabled)
            {
                Logger.Log(ThisDeclaringType, m_levelExternalSystemInformation, new SystemStringFormat(provider, format, args), null);
            }
        }

        #region Notice
        /// <summary>
        /// Logs a message object with the <c>NOTICE</c> level.
        /// </summary>
        /// <param name="message">The message object to log.</param>
        /// <remarks>
        /// <para>
        /// This method first checks if this logger is <c>NOTICE</c>
        /// enabled by comparing the level of this logger with the 
        /// <c>NOTICE</c> level. If this logger is
        /// <c>NOTICE</c> enabled, then it converts the message object
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
        /// <see cref="Notice(object,Exception)"/> form instead.
        /// </para>
        /// </remarks>
        virtual public void Notice(object message)
        {
            Logger.Log(ThisDeclaringType, m_levelNotice, message, null);
        }

        /// <summary>
        /// Logs a message object with the <c>NOTICE</c> level
        /// </summary>
        /// <param name="message">The message object to log.</param>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        /// <remarks>
        /// <para>
        /// Logs a message object with the <c>NOTICE</c> level including
        /// the stack trace of the <see cref="Exception"/> <paramref name="exception"/> 
        /// passed as a parameter.
        /// </para>
        /// <para>
        /// See the <see cref="Notice(object)"/> form for more detailed information.
        /// </para>
        /// </remarks>
        /// <seealso cref="Notice(object)"/>
        virtual public void Notice(object message, Exception exception)
        {
            Logger.Log(ThisDeclaringType, m_levelNotice, message, exception);
        }

        /// <summary>
        /// Logs a formatted message string with the <c>NOTICE</c> level.
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
        /// <see cref="NoticeFormat(IFormatProvider,string,object[])"/> method.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="Exception"/> object to include in the
        /// log event. To pass an <see cref="Exception"/> use one of the <see cref="Notice(object)"/>
        /// methods instead.
        /// </para>
        /// </remarks>
        virtual public void NoticeFormat(string format, params object[] args)
        {
            if (IsNoticeEnabled)
            {
                Logger.Log(ThisDeclaringType, m_levelNotice, new SystemStringFormat(CultureInfo.InvariantCulture, format, args), null);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the <c>NOTICE</c> level.
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
        /// <see cref="FatalFormat(IFormatProvider,string,object[])"/> method.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="Exception"/> object to include in the
        /// log event. To pass an <see cref="Exception"/> use one of the <see cref="Notice(object)"/>
        /// methods instead.
        /// </para>
        /// </remarks>
        virtual public void NoticeFormat(string format, object arg0)
        {
            if (IsNoticeEnabled)
            {
                Logger.Log(ThisDeclaringType, m_levelNotice, new SystemStringFormat(CultureInfo.InvariantCulture, format, new object[] { arg0 }), null);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the <c>NOTICE</c> level.
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
        /// <see cref="FatalFormat(IFormatProvider,string,object[])"/> method.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="Exception"/> object to include in the
        /// log event. To pass an <see cref="Exception"/> use one of the <see cref="Fatal(object)"/>
        /// methods instead.
        /// </para>
        /// </remarks>
        virtual public void NoticeFormat(string format, object arg0, object arg1)
        {
            if (IsNoticeEnabled)
            {
                Logger.Log(ThisDeclaringType, m_levelNotice, new SystemStringFormat(CultureInfo.InvariantCulture, format, new object[] { arg0, arg1 }), null);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the <c>NOTICE</c> level.
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
        /// <see cref="FatalFormat(IFormatProvider,string,object[])"/> method.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="Exception"/> object to include in the
        /// log event. To pass an <see cref="Exception"/> use one of the <see cref="Fatal(object)"/>
        /// methods instead.
        /// </para>
        /// </remarks>
        virtual public void NoticeFormat(string format, object arg0, object arg1, object arg2)
        {
            if (IsNoticeEnabled)
            {
                Logger.Log(ThisDeclaringType, m_levelNotice, new SystemStringFormat(CultureInfo.InvariantCulture, format, new object[] { arg0, arg1, arg2 }), null);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the <c>FATAL</c> level.
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
        /// log event. To pass an <see cref="Exception"/> use one of the <see cref="Fatal(object)"/>
        /// methods instead.
        /// </para>
        /// </remarks>
        virtual public void NoticeFormat(IFormatProvider provider, string format, params object[] args)
        {
            if (IsNoticeEnabled)
            {
                Logger.Log(ThisDeclaringType, m_levelNotice, new SystemStringFormat(provider, format, args), null);
            }
        }
        #endregion

        /// <summary>
		/// Checks if this logger is enabled for the <c>TRACE</c>
		/// level.
		/// </summary>
		/// <value>
		/// <c>true</c> if this logger is enabled for <c>TRACE</c> events,
		/// <c>false</c> otherwise.
		/// </value>
		/// <remarks>
		/// <para>
		/// This function is intended to lessen the computational cost of
		/// disabled log trace statements.
		/// </para>
		/// <para>
		/// For some <c>log</c> Logger object, when you write:
		/// </para>
		/// <code lang="C#">
		/// log.Trace("This is entry number: " + i );
		/// </code>
		/// <para>
		/// You incur the cost constructing the message, concatenation in
		/// this case, regardless of whether the message is logged or not.
		/// </para>
		/// <para>
		/// If you are worried about speed, then you should write:
		/// </para>
		/// <code lang="C#">
		/// if (log.IsDebugEnabled())
		/// { 
		///	 log.Trace("This is entry number: " + i );
		/// }
		/// </code>
		/// <para>
		/// This way you will not incur the cost of parameter
		/// construction if debugging is disabled for <c>log</c>. On
		/// the other hand, if the <c>log</c> is trace enabled, you
		/// will incur the cost of evaluating whether the logger is trace
		/// enabled twice. Once in <c>IsDebugEnabled</c> and once in
		/// the <c>Trace</c>.  This is an insignificant overhead
		/// since evaluating a logger takes about 1% of the time it
		/// takes to actually log.
		/// </para>
		/// </remarks>
		virtual public bool IsDebugEnabled
		{
			get { return Logger.IsEnabledFor(m_levelDebug); }
		}

        /// <summary>
        /// Checks if this logger is enabled for the <c>BUSINESSERROR</c> level.
        /// </summary>
        /// <value>
        /// <c>true</c> if this logger is enabled for <c>BUSINESSERROR</c> events,
        /// <c>false</c> otherwise.
        /// </value>
        /// <remarks>
        /// <para>
        /// See <see cref="IsDebugEnabled"/> for more information and examples 
        /// of using this method.
        /// </para>
        /// </remarks>
        /// <seealso cref="LogImpl.IsBusinessErrorEnabled"/>
        virtual public bool IsBusinessErrorEnabled
        {
            get { return Logger.IsEnabledFor(m_levelBusinessError); }
        }

		/// <summary>
		/// Checks if this logger is enabled for the <c>INFO</c> level.
		/// </summary>
		/// <value>
		/// <c>true</c> if this logger is enabled for <c>INFO</c> events,
		/// <c>false</c> otherwise.
		/// </value>
		/// <remarks>
		/// <para>
		/// See <see cref="IsDebugEnabled"/> for more information and examples 
		/// of using this method.
		/// </para>
		/// </remarks>
		/// <seealso cref="LogImpl.IsDebugEnabled"/>
		virtual public bool IsInfoEnabled
		{
			get { return Logger.IsEnabledFor(m_levelInfo); }
		}

		/// <summary>
		/// Checks if this logger is enabled for the <c>WARN</c> level.
		/// </summary>
		/// <value>
		/// <c>true</c> if this logger is enabled for <c>WARN</c> events,
		/// <c>false</c> otherwise.
		/// </value>
		/// <remarks>
		/// <para>
		/// See <see cref="IsDebugEnabled"/> for more information and examples 
		/// of using this method.
		/// </para>
		/// </remarks>
		/// <seealso cref="ILog.IsDebugEnabled"/>
		virtual public bool IsWarnEnabled
		{
			get { return Logger.IsEnabledFor(m_levelWarn); }
		}

        /// <summary>
        /// Checks if this logger is enabled for the <c>EXTERNALSYSTEMINFORMATION</c> level.
        /// </summary>
        /// <value>
        /// <c>true</c> if this logger is enabled for <c>EXTERNALSYSTEMINFORMATION</c> events,
        /// <c>false</c> otherwise.
        /// </value>
        /// <remarks>
        /// <para>
        /// See <see cref="IsDebugEnabled"/> for more information and examples 
        /// of using this method.
        /// </para>
        /// </remarks>
        /// <seealso cref="LogImpl.IsExternalSystemInformationEnabled"/>
        virtual public bool IsExternalSystemInformationEnabled
        {
            get { return Logger.IsEnabledFor(m_levelExternalSystemInformation); }
        }

		/// <summary>
		/// Checks if this logger is enabled for the <c>ERROR</c> level.
		/// </summary>
		/// <value>
		/// <c>true</c> if this logger is enabled for <c>ERROR</c> events,
		/// <c>false</c> otherwise.
		/// </value>
		/// <remarks>
		/// <para>
		/// See <see cref="IsDebugEnabled"/> for more information and examples of using this method.
		/// </para>
		/// </remarks>
		/// <seealso cref="ILog.IsDebugEnabled"/>
		virtual public bool IsErrorEnabled
		{
			get { return Logger.IsEnabledFor(m_levelError); }
		}

		/// <summary>
		/// Checks if this logger is enabled for the <c>FATAL</c> level.
		/// </summary>
		/// <value>
		/// <c>true</c> if this logger is enabled for <c>FATAL</c> events,
		/// <c>false</c> otherwise.
		/// </value>
		/// <remarks>
		/// <para>
		/// See <see cref="IsDebugEnabled"/> for more information and examples of using this method.
		/// </para>
		/// </remarks>
		/// <seealso cref="ILog.IsDebugEnabled"/>
		virtual public bool IsFatalEnabled
		{
			get { return Logger.IsEnabledFor(m_levelFatal); }
		}

        /// <summary>
        /// Checks if this logger is enabled for the <c>NOTICE</c> level.
        /// </summary>
        /// <value>
        /// <c>true</c> if this logger is enabled for <c>NOTICE</c> events,
        /// <c>false</c> otherwise.
        /// </value>
        /// <remarks>
        /// <para>
        /// See <see cref="IsDebugEnabled"/> for more information and examples of using this method.
        /// </para>
        /// </remarks>
        /// <seealso cref="ILog.IsDebugEnabled"/>
        virtual public bool IsNoticeEnabled
        {
            get { return Logger.IsEnabledFor(m_levelNotice); }
        }

        /// <summary>
        /// Checks if this logger is enabled for the <c>ALL</c> level.
        /// </summary>
        /// <value>
        /// <c>true</c> if this logger is enabled for <c>ALL</c> events,
        /// <c>false</c> otherwise.
        /// </value>
        /// <remarks>
        /// <para>
        /// See <see cref="IsAllEnabled"/> for more information and examples of using this method.
        /// </para>
        /// </remarks>
        /// <seealso cref="ILog.IsDebugEnabled"/>
        virtual public bool IsAllEnabled
        {
            get { return Logger.IsEnabledFor(m_levelAll); }
        }
        #endregion Implementation of ILog

		#region Private Methods

		/// <summary>
		/// Event handler for the <see cref="Library.Logging.Repository.ILoggerRepository.ConfigurationChanged"/> event
		/// </summary>
		/// <param name="sender">the repository</param>
		/// <param name="e">Empty</param>
		private void LoggerRepositoryConfigurationChanged(object sender, EventArgs e)
		{
			ILoggerRepository repository = sender as ILoggerRepository;
			if (repository != null)
			{
				ReloadLevels(repository);
			}
		}

		#endregion

		#region Private Static Instance Fields

		/// <summary>
		/// The fully qualified name of this declaring type not the type of any subclass.
		/// </summary>
		private readonly static Type ThisDeclaringType = typeof(LogImpl);

		#endregion Private Static Instance Fields

		#region Private Fields

        private Level m_levelAll;
		private Level m_levelDebug;
        private Level m_levelBusinessError;
		private Level m_levelInfo;
		private Level m_levelWarn;
        private Level m_levelExternalSystemInformation = null;
		private Level m_levelError;
		private Level m_levelFatal;
        private Level m_levelOff;
        private Level m_levelNotice;

		#endregion
	}
}
