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
using System.Reflection;

using Library.Logging.Core;

namespace Library.Logging
{
	/// <summary>
	/// The ILog interface is use by application to log messages into
	/// the  Logging framework.
	/// </summary>
	/// <remarks>
	/// <para>
	/// Use the <see cref="LogManager"/> to obtain logger instances
	/// that implement this interface. The <see cref="LogManager.GetLogger(Assembly,Type)"/>
	/// static method is used to get logger instances.
	/// </para>
	/// <para>
	/// This class contains methods for logging at different levels and also
	/// has properties for determining if those logging levels are
	/// enabled in the current configuration.
	/// </para>
	/// <para>
	/// This interface can be implemented in different ways. This documentation
	/// specifies reasonable behavior that a caller can expect from the actual
	/// implementation, however different implementations reserve the right to
	/// do things differently.
	/// </para>
	/// </remarks>
	/// <example>Simple example of logging messages
	/// <code lang="C#">
	/// ILog log = LogManager.GetLogger("application-log");
	/// 
	/// log.Info("Application Start");
	/// log.Trace("This is a trace message");
	/// 
	/// if (log.IsDebugEnabled)
	/// {
	///		log.Trace("This is another trace message");
	/// }
	/// </code>
	/// </example>
	/// <seealso cref="LogManager"/>
	/// <seealso cref="LogManager.GetLogger(Assembly, Type)"/>
	public interface ILog : ILoggerWrapper
    {
        #region Trace
        /// <overloads>Log a message object with the <see cref="Level.Trace"/> level.</overloads>
		/// <summary>
		/// Log a message object with the <see cref="Level.Trace"/> level.
		/// </summary>
		/// <param name="message">The message object to log.</param>
		/// <remarks>
		/// <para>
		/// This method first checks if this logger is <c>TRACE</c>
		/// enabled by comparing the level of this logger with the 
		/// <see cref="Level.Trace"/> level. If this logger is
		/// <c>TRACE</c> enabled, then it converts the message object
		/// (passed as parameter) to a string by invoking the appropriate
		/// <see cref="Library.Logging.ObjectRenderer.IObjectRenderer"/>. It then 
		/// proceeds to call all the registered appenders in this logger 
		/// and also higher in the hierarchy depending on the value of 
		/// the additivity flag.
		/// </para>
		/// <para><b>WARNING</b> Note that passing an <see cref="Exception"/> 
		/// to this method will print the name of the <see cref="Exception"/> 
		/// but no stack trace. To print a stack trace use the 
		/// <see cref="Trace(object,Exception)"/> form instead.
		/// </para>
		/// </remarks>
		/// <seealso cref="Trace(object,Exception)"/>
		/// <seealso cref="IsDebugEnabled"/>
		void Trace(object message);
  
		/// <summary>
		/// Log a message object with the <see cref="Level.Trace"/> level including
		/// the stack trace of the <see cref="Exception"/> passed
		/// as a parameter.
		/// </summary>
		/// <param name="message">The message object to log.</param>
		/// <param name="exception">The exception to log, including its stack trace.</param>
		/// <remarks>
		/// <para>
		/// See the <see cref="Trace(object)"/> form for more detailed information.
		/// </para>
		/// </remarks>
		/// <seealso cref="Trace(object)"/>
		/// <seealso cref="IsDebugEnabled"/>
		void Trace(object message, Exception exception);

		/// <overloads>Log a formatted string with the <see cref="Level.Trace"/> level.</overloads>
		/// <summary>
		/// Logs a formatted message string with the <see cref="Level.Trace"/> level.
		/// </summary>
		/// <param name="format">A String containing zero or more format items</param>
		/// <param name="args">An Object array containing zero or more objects to format</param>
		/// <remarks>
		/// <para>
		/// The message is formatted using the <c>String.Format</c> method. See
		/// <see cref="String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
		/// of the formatting.
		/// </para>
		/// <para>
		/// This method does not take an <see cref="Exception"/> object to include in the
		/// log event. To pass an <see cref="Exception"/> use one of the <see cref="Trace(object,Exception)"/>
		/// methods instead.
		/// </para>
		/// </remarks>
		/// <seealso cref="Trace(object)"/>
		/// <seealso cref="IsDebugEnabled"/>
		void DebugFormat(string format, params object[] args); 

		/// <summary>
		/// Logs a formatted message string with the <see cref="Level.Trace"/> level.
		/// </summary>
		/// <param name="format">A String containing zero or more format items</param>
		/// <param name="arg0">An Object to format</param>
		/// <remarks>
		/// <para>
		/// The message is formatted using the <c>String.Format</c> method. See
		/// <see cref="String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
		/// of the formatting.
		/// </para>
		/// <para>
		/// This method does not take an <see cref="Exception"/> object to include in the
		/// log event. To pass an <see cref="Exception"/> use one of the <see cref="Trace(object,Exception)"/>
		/// methods instead.
		/// </para>
		/// </remarks>
		/// <seealso cref="Trace(object)"/>
		/// <seealso cref="IsDebugEnabled"/>
		void DebugFormat(string format, object arg0); 

		/// <summary>
		/// Logs a formatted message string with the <see cref="Level.Trace"/> level.
		/// </summary>
		/// <param name="format">A String containing zero or more format items</param>
		/// <param name="arg0">An Object to format</param>
		/// <param name="arg1">An Object to format</param>
		/// <remarks>
		/// <para>
		/// The message is formatted using the <c>String.Format</c> method. See
		/// <see cref="String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
		/// of the formatting.
		/// </para>
		/// <para>
		/// This method does not take an <see cref="Exception"/> object to include in the
		/// log event. To pass an <see cref="Exception"/> use one of the <see cref="Trace(object,Exception)"/>
		/// methods instead.
		/// </para>
		/// </remarks>
		/// <seealso cref="Trace(object)"/>
		/// <seealso cref="IsDebugEnabled"/>
		void DebugFormat(string format, object arg0, object arg1); 

		/// <summary>
		/// Logs a formatted message string with the <see cref="Level.Trace"/> level.
		/// </summary>
		/// <param name="format">A String containing zero or more format items</param>
		/// <param name="arg0">An Object to format</param>
		/// <param name="arg1">An Object to format</param>
		/// <param name="arg2">An Object to format</param>
		/// <remarks>
		/// <para>
		/// The message is formatted using the <c>String.Format</c> method. See
		/// <see cref="String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
		/// of the formatting.
		/// </para>
		/// <para>
		/// This method does not take an <see cref="Exception"/> object to include in the
		/// log event. To pass an <see cref="Exception"/> use one of the <see cref="Trace(object,Exception)"/>
		/// methods instead.
		/// </para>
		/// </remarks>
		/// <seealso cref="Trace(object)"/>
		/// <seealso cref="IsDebugEnabled"/>
		void DebugFormat(string format, object arg0, object arg1, object arg2); 

		/// <summary>
		/// Logs a formatted message string with the <see cref="Level.Trace"/> level.
		/// </summary>
		/// <param name="provider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information</param>
		/// <param name="format">A String containing zero or more format items</param>
		/// <param name="args">An Object array containing zero or more objects to format</param>
		/// <remarks>
		/// <para>
		/// The message is formatted using the <c>String.Format</c> method. See
		/// <see cref="String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
		/// of the formatting.
		/// </para>
		/// <para>
		/// This method does not take an <see cref="Exception"/> object to include in the
		/// log event. To pass an <see cref="Exception"/> use one of the <see cref="Trace(object,Exception)"/>
		/// methods instead.
		/// </para>
		/// </remarks>
		/// <seealso cref="Trace(object)"/>
		/// <seealso cref="IsDebugEnabled"/>
		void DebugFormat(IFormatProvider provider, string format, params object[] args);
        #endregion

        #region BusinessError
        /// <overloads>Log a message object with the <see cref="Level.BusinessError"/> level.</overloads>
        /// <summary>
        /// Logs a message object with the <see cref="Level.BusinessError"/> level.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method first checks if this logger is <c>BUSINESSERROR</c>
        /// enabled by comparing the level of this logger with the 
        /// <see cref="Level.BusinessError"/> level. If this logger is
        /// <c>BUSINESSERROR</c> enabled, then it converts the message object
        /// (passed as parameter) to a string by invoking the appropriate
        /// <see cref="Library.Logging.ObjectRenderer.IObjectRenderer"/>. It then 
        /// proceeds to call all the registered appenders in this logger 
        /// and also higher in the hierarchy depending on the value of the 
        /// additivity flag.
        /// </para>
        /// <para><b>WARNING</b> Note that passing an <see cref="Exception"/> 
        /// to this method will print the name of the <see cref="Exception"/> 
        /// but no stack trace. To print a stack trace use the 
        /// <see cref="Info(object,Exception)"/> form instead.
        /// </para>
        /// </remarks>
        /// <param name="message">The message object to log.</param>
        /// <seealso cref="Info(object,Exception)"/>
        /// <seealso cref="IsBusinessErrorEnabled"/>
        void BusinessError(int eventId, object message);

        /// <summary>
        /// Logs a message object with the <c>BUSINESSERROR</c> level including
        /// the stack trace of the <see cref="Exception"/> passed
        /// as a parameter.
        /// </summary>
        /// <param name="message">The message object to log.</param>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        /// <remarks>
        /// <para>
        /// See the <see cref="BusinessError(int eventId, int eventId, object)"/> form for more detailed information.
        /// </para>
        /// </remarks>
        /// <seealso cref="BusinessError(int eventId, int eventId, object)"/>
        /// <seealso cref="IsBusinessErrorEnabled"/>
        void BusinessError(int eventId, object message, Exception exception);

        /// <overloads>Log a formatted message string with the <see cref="Level.BusinessError"/> level.</overloads>
        /// <summary>
        /// Logs a formatted message string with the <see cref="Level.BusinessError"/> level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="args">An Object array containing zero or more objects to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="Exception"/> object to include in the
        /// log event. To pass an <see cref="Exception"/> use one of the <see cref="Info(object)"/>
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="BusinessError(int eventId, int eventId, object,Exception)"/>
        /// <seealso cref="IsBusinessErrorEnabled"/>
        void BusinessErrorFormat(string format, params object[] args);

        /// <summary>
        /// Logs a formatted message string with the <see cref="Level.BusinessError"/> level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">An Object to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="Exception"/> object to include in the
        /// log event. To pass an <see cref="Exception"/> use one of the <see cref="BusinessError(int eventId, object,Exception)"/>
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="BusinessError(int eventId, object)"/>
        /// <seealso cref="IsBusinessErrorEnabled"/>
        void BusinessErrorFormat(string format, object arg0);

        /// <summary>
        /// Logs a formatted message string with the <see cref="Level.BusinessError"/> level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">An Object to format</param>
        /// <param name="arg1">An Object to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="Exception"/> object to include in the
        /// log event. To pass an <see cref="Exception"/> use one of the <see cref="BusinessError(int eventId, object,Exception)"/>
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="BusinessError(int eventId, object)"/>
        /// <seealso cref="IsBusinessErrorEnabled"/>
        void BusinessErrorFormat(string format, object arg0, object arg1);

        /// <summary>
        /// Logs a formatted message string with the <see cref="Level.Info"/> level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">An Object to format</param>
        /// <param name="arg1">An Object to format</param>
        /// <param name="arg2">An Object to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="Exception"/> object to include in the
        /// log event. To pass an <see cref="Exception"/> use one of the <see cref="BusinessError(int eventId, object,Exception)"/>
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="BusinessError(int eventId, object)"/>
        /// <seealso cref="IsBusinessErrorEnabled"/>
        void BusinessErrorFormat(string format, object arg0, object arg1, object arg2);

        /// <summary>
        /// Logs a formatted message string with the <see cref="Level.BusinessError"/> level.
        /// </summary>
        /// <param name="provider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information</param>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="args">An Object array containing zero or more objects to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="Exception"/> object to include in the
        /// log event. To pass an <see cref="Exception"/> use one of the <see cref="BusinessError(int eventId, object)"/>
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="BusinessError(int eventId, object,Exception)"/>
        /// <seealso cref="IsBusinessErrorEnabled"/>
        void BusinessErrorFormat(IFormatProvider provider, string format, params object[] args);

        #endregion

        #region Info
        /// <overloads>Log a message object with the <see cref="Level.Info"/> level.</overloads>
		/// <summary>
		/// Logs a message object with the <see cref="Level.Info"/> level.
		/// </summary>
		/// <remarks>
		/// <para>
		/// This method first checks if this logger is <c>INFO</c>
		/// enabled by comparing the level of this logger with the 
		/// <see cref="Level.Info"/> level. If this logger is
		/// <c>INFO</c> enabled, then it converts the message object
		/// (passed as parameter) to a string by invoking the appropriate
		/// <see cref="Library.Logging.ObjectRenderer.IObjectRenderer"/>. It then 
		/// proceeds to call all the registered appenders in this logger 
		/// and also higher in the hierarchy depending on the value of the 
		/// additivity flag.
		/// </para>
		/// <para><b>WARNING</b> Note that passing an <see cref="Exception"/> 
		/// to this method will print the name of the <see cref="Exception"/> 
		/// but no stack trace. To print a stack trace use the 
		/// <see cref="Info(object,Exception)"/> form instead.
		/// </para>
		/// </remarks>
		/// <param name="message">The message object to log.</param>
		/// <seealso cref="Info(object,Exception)"/>
		/// <seealso cref="IsInfoEnabled"/>
		void Info(object message);
  
		/// <summary>
		/// Logs a message object with the <c>INFO</c> level including
		/// the stack trace of the <see cref="Exception"/> passed
		/// as a parameter.
		/// </summary>
		/// <param name="message">The message object to log.</param>
		/// <param name="exception">The exception to log, including its stack trace.</param>
		/// <remarks>
		/// <para>
		/// See the <see cref="Info(object)"/> form for more detailed information.
		/// </para>
		/// </remarks>
		/// <seealso cref="Info(object)"/>
		/// <seealso cref="IsInfoEnabled"/>
		void Info(object message, Exception exception);

		/// <overloads>Log a formatted message string with the <see cref="Level.Info"/> level.</overloads>
		/// <summary>
		/// Logs a formatted message string with the <see cref="Level.Info"/> level.
		/// </summary>
		/// <param name="format">A String containing zero or more format items</param>
		/// <param name="args">An Object array containing zero or more objects to format</param>
		/// <remarks>
		/// <para>
		/// The message is formatted using the <c>String.Format</c> method. See
		/// <see cref="String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
		/// of the formatting.
		/// </para>
		/// <para>
		/// This method does not take an <see cref="Exception"/> object to include in the
		/// log event. To pass an <see cref="Exception"/> use one of the <see cref="Info(object)"/>
		/// methods instead.
		/// </para>
		/// </remarks>
		/// <seealso cref="Info(object,Exception)"/>
		/// <seealso cref="IsInfoEnabled"/>
		void InfoFormat(string format, params object[] args);

		/// <summary>
		/// Logs a formatted message string with the <see cref="Level.Info"/> level.
		/// </summary>
		/// <param name="format">A String containing zero or more format items</param>
		/// <param name="arg0">An Object to format</param>
		/// <remarks>
		/// <para>
		/// The message is formatted using the <c>String.Format</c> method. See
		/// <see cref="String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
		/// of the formatting.
		/// </para>
		/// <para>
		/// This method does not take an <see cref="Exception"/> object to include in the
		/// log event. To pass an <see cref="Exception"/> use one of the <see cref="Info(object,Exception)"/>
		/// methods instead.
		/// </para>
		/// </remarks>
		/// <seealso cref="Info(object)"/>
		/// <seealso cref="IsInfoEnabled"/>
		void InfoFormat(string format, object arg0); 

		/// <summary>
		/// Logs a formatted message string with the <see cref="Level.Info"/> level.
		/// </summary>
		/// <param name="format">A String containing zero or more format items</param>
		/// <param name="arg0">An Object to format</param>
		/// <param name="arg1">An Object to format</param>
		/// <remarks>
		/// <para>
		/// The message is formatted using the <c>String.Format</c> method. See
		/// <see cref="String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
		/// of the formatting.
		/// </para>
		/// <para>
		/// This method does not take an <see cref="Exception"/> object to include in the
		/// log event. To pass an <see cref="Exception"/> use one of the <see cref="Info(object,Exception)"/>
		/// methods instead.
		/// </para>
		/// </remarks>
		/// <seealso cref="Info(object)"/>
		/// <seealso cref="IsInfoEnabled"/>
		void InfoFormat(string format, object arg0, object arg1); 

		/// <summary>
		/// Logs a formatted message string with the <see cref="Level.Info"/> level.
		/// </summary>
		/// <param name="format">A String containing zero or more format items</param>
		/// <param name="arg0">An Object to format</param>
		/// <param name="arg1">An Object to format</param>
		/// <param name="arg2">An Object to format</param>
		/// <remarks>
		/// <para>
		/// The message is formatted using the <c>String.Format</c> method. See
		/// <see cref="String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
		/// of the formatting.
		/// </para>
		/// <para>
		/// This method does not take an <see cref="Exception"/> object to include in the
		/// log event. To pass an <see cref="Exception"/> use one of the <see cref="Info(object,Exception)"/>
		/// methods instead.
		/// </para>
		/// </remarks>
		/// <seealso cref="Info(object)"/>
		/// <seealso cref="IsInfoEnabled"/>
		void InfoFormat(string format, object arg0, object arg1, object arg2); 

		/// <summary>
		/// Logs a formatted message string with the <see cref="Level.Info"/> level.
		/// </summary>
		/// <param name="provider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information</param>
		/// <param name="format">A String containing zero or more format items</param>
		/// <param name="args">An Object array containing zero or more objects to format</param>
		/// <remarks>
		/// <para>
		/// The message is formatted using the <c>String.Format</c> method. See
		/// <see cref="String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
		/// of the formatting.
		/// </para>
		/// <para>
		/// This method does not take an <see cref="Exception"/> object to include in the
		/// log event. To pass an <see cref="Exception"/> use one of the <see cref="Info(object)"/>
		/// methods instead.
		/// </para>
		/// </remarks>
		/// <seealso cref="Info(object,Exception)"/>
		/// <seealso cref="IsInfoEnabled"/>
		void InfoFormat(IFormatProvider provider, string format, params object[] args);

        #endregion

        #region Warning
        /// <overloads>Log a message object with the <see cref="Level.Warn"/> level.</overloads>
		/// <summary>
		/// Log a message object with the <see cref="Level.Warn"/> level.
		/// </summary>
		/// <remarks>
		/// <para>
		/// This method first checks if this logger is <c>WARN</c>
		/// enabled by comparing the level of this logger with the 
		/// <see cref="Level.Warn"/> level. If this logger is
		/// <c>WARN</c> enabled, then it converts the message object
		/// (passed as parameter) to a string by invoking the appropriate
		/// <see cref="Library.Logging.ObjectRenderer.IObjectRenderer"/>. It then 
		/// proceeds to call all the registered appenders in this logger 
		/// and also higher in the hierarchy depending on the value of the 
		/// additivity flag.
		/// </para>
		/// <para><b>WARNING</b> Note that passing an <see cref="Exception"/> 
		/// to this method will print the name of the <see cref="Exception"/> 
		/// but no stack trace. To print a stack trace use the 
		/// <see cref="Warn(object,Exception)"/> form instead.
		/// </para>
		/// </remarks>
		/// <param name="message">The message object to log.</param>
		/// <seealso cref="Warn(object,Exception)"/>
		/// <seealso cref="IsWarnEnabled"/>
        void Warn(int eventId, object message);
  
		/// <summary>
		/// Log a message object with the <see cref="Level.Warn"/> level including
		/// the stack trace of the <see cref="Exception"/> passed
		/// as a parameter.
		/// </summary>
		/// <param name="message">The message object to log.</param>
		/// <param name="exception">The exception to log, including its stack trace.</param>
		/// <remarks>
		/// <para>
		/// See the <see cref="Warn(object)"/> form for more detailed information.
		/// </para>
		/// </remarks>
		/// <seealso cref="Warn(object)"/>
		/// <seealso cref="IsWarnEnabled"/>
        void Warn(int eventId, object message, Exception exception);

		/// <overloads>Log a formatted message string with the <see cref="Level.Warn"/> level.</overloads>
		/// <summary>
		/// Logs a formatted message string with the <see cref="Level.Warn"/> level.
		/// </summary>
		/// <param name="format">A String containing zero or more format items</param>
		/// <param name="args">An Object array containing zero or more objects to format</param>
		/// <remarks>
		/// <para>
		/// The message is formatted using the <c>String.Format</c> method. See
		/// <see cref="String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
		/// of the formatting.
		/// </para>
		/// <para>
		/// This method does not take an <see cref="Exception"/> object to include in the
		/// log event. To pass an <see cref="Exception"/> use one of the <see cref="Warn(object)"/>
		/// methods instead.
		/// </para>
		/// </remarks>
		/// <seealso cref="Warn(object,Exception)"/>
		/// <seealso cref="IsWarnEnabled"/>
		void WarnFormat(string format, params object[] args);

		/// <summary>
		/// Logs a formatted message string with the <see cref="Level.Warn"/> level.
		/// </summary>
		/// <param name="format">A String containing zero or more format items</param>
		/// <param name="arg0">An Object to format</param>
		/// <remarks>
		/// <para>
		/// The message is formatted using the <c>String.Format</c> method. See
		/// <see cref="String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
		/// of the formatting.
		/// </para>
		/// <para>
		/// This method does not take an <see cref="Exception"/> object to include in the
		/// log event. To pass an <see cref="Exception"/> use one of the <see cref="Warn(object,Exception)"/>
		/// methods instead.
		/// </para>
		/// </remarks>
		/// <seealso cref="Warn(object)"/>
		/// <seealso cref="IsWarnEnabled"/>
		void WarnFormat(string format, object arg0); 

		/// <summary>
		/// Logs a formatted message string with the <see cref="Level.Warn"/> level.
		/// </summary>
		/// <param name="format">A String containing zero or more format items</param>
		/// <param name="arg0">An Object to format</param>
		/// <param name="arg1">An Object to format</param>
		/// <remarks>
		/// <para>
		/// The message is formatted using the <c>String.Format</c> method. See
		/// <see cref="String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
		/// of the formatting.
		/// </para>
		/// <para>
		/// This method does not take an <see cref="Exception"/> object to include in the
		/// log event. To pass an <see cref="Exception"/> use one of the <see cref="Warn(object,Exception)"/>
		/// methods instead.
		/// </para>
		/// </remarks>
		/// <seealso cref="Warn(object)"/>
		/// <seealso cref="IsWarnEnabled"/>
		void WarnFormat(string format, object arg0, object arg1); 

		/// <summary>
		/// Logs a formatted message string with the <see cref="Level.Warn"/> level.
		/// </summary>
		/// <param name="format">A String containing zero or more format items</param>
		/// <param name="arg0">An Object to format</param>
		/// <param name="arg1">An Object to format</param>
		/// <param name="arg2">An Object to format</param>
		/// <remarks>
		/// <para>
		/// The message is formatted using the <c>String.Format</c> method. See
		/// <see cref="String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
		/// of the formatting.
		/// </para>
		/// <para>
		/// This method does not take an <see cref="Exception"/> object to include in the
		/// log event. To pass an <see cref="Exception"/> use one of the <see cref="Warn(object,Exception)"/>
		/// methods instead.
		/// </para>
		/// </remarks>
		/// <seealso cref="Warn(object)"/>
		/// <seealso cref="IsWarnEnabled"/>
		void WarnFormat(string format, object arg0, object arg1, object arg2); 

		/// <summary>
		/// Logs a formatted message string with the <see cref="Level.Warn"/> level.
		/// </summary>
		/// <param name="provider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information</param>
		/// <param name="format">A String containing zero or more format items</param>
		/// <param name="args">An Object array containing zero or more objects to format</param>
		/// <remarks>
		/// <para>
		/// The message is formatted using the <c>String.Format</c> method. See
		/// <see cref="String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
		/// of the formatting.
		/// </para>
		/// <para>
		/// This method does not take an <see cref="Exception"/> object to include in the
		/// log event. To pass an <see cref="Exception"/> use one of the <see cref="Warn(object)"/>
		/// methods instead.
		/// </para>
		/// </remarks>
		/// <seealso cref="Warn(object,Exception)"/>
		/// <seealso cref="IsWarnEnabled"/>
		void WarnFormat(IFormatProvider provider, string format, params object[] args);
        #endregion

        #region Error
        /// <overloads>Log a message object with the <see cref="Level.Error"/> level.</overloads>
		/// <summary>
		/// Logs a message object with the <see cref="Level.Error"/> level.
		/// </summary>
		/// <param name="message">The message object to log.</param>
		/// <remarks>
		/// <para>
		/// This method first checks if this logger is <c>ERROR</c>
		/// enabled by comparing the level of this logger with the 
		/// <see cref="Level.Error"/> level. If this logger is
		/// <c>ERROR</c> enabled, then it converts the message object
		/// (passed as parameter) to a string by invoking the appropriate
		/// <see cref="Library.Logging.ObjectRenderer.IObjectRenderer"/>. It then 
		/// proceeds to call all the registered appenders in this logger 
		/// and also higher in the hierarchy depending on the value of the 
		/// additivity flag.
		/// </para>
		/// <para><b>WARNING</b> Note that passing an <see cref="Exception"/> 
		/// to this method will print the name of the <see cref="Exception"/> 
		/// but no stack trace. To print a stack trace use the 
		/// <see cref="Error(object,Exception)"/> form instead.
		/// </para>
		/// </remarks>
		/// <seealso cref="Error(object,Exception)"/>
		/// <seealso cref="IsErrorEnabled"/>
		void Error(int eventId, object message);

		/// <summary>
		/// Log a message object with the <see cref="Level.Error"/> level including
		/// the stack trace of the <see cref="Exception"/> passed
		/// as a parameter.
		/// </summary>
		/// <param name="message">The message object to log.</param>
		/// <param name="exception">The exception to log, including its stack trace.</param>
		/// <remarks>
		/// <para>
		/// See the <see cref="Error(object)"/> form for more detailed information.
		/// </para>
		/// </remarks>
		/// <seealso cref="Error(object)"/>
		/// <seealso cref="IsErrorEnabled"/>
		void Error(int eventId, object message, Exception exception);

		/// <overloads>Log a formatted message string with the <see cref="Level.Error"/> level.</overloads>
		/// <summary>
		/// Logs a formatted message string with the <see cref="Level.Error"/> level.
		/// </summary>
		/// <param name="format">A String containing zero or more format items</param>
		/// <param name="args">An Object array containing zero or more objects to format</param>
		/// <remarks>
		/// <para>
		/// The message is formatted using the <c>String.Format</c> method. See
		/// <see cref="String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
		/// of the formatting.
		/// </para>
		/// <para>
		/// This method does not take an <see cref="Exception"/> object to include in the
		/// log event. To pass an <see cref="Exception"/> use one of the <see cref="Error(object)"/>
		/// methods instead.
		/// </para>
		/// </remarks>
		/// <seealso cref="Error(object,Exception)"/>
		/// <seealso cref="IsErrorEnabled"/>
		void ErrorFormat(string format, params object[] args);

		/// <summary>
		/// Logs a formatted message string with the <see cref="Level.Error"/> level.
		/// </summary>
		/// <param name="format">A String containing zero or more format items</param>
		/// <param name="arg0">An Object to format</param>
		/// <remarks>
		/// <para>
		/// The message is formatted using the <c>String.Format</c> method. See
		/// <see cref="String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
		/// of the formatting.
		/// </para>
		/// <para>
		/// This method does not take an <see cref="Exception"/> object to include in the
		/// log event. To pass an <see cref="Exception"/> use one of the <see cref="Error(object,Exception)"/>
		/// methods instead.
		/// </para>
		/// </remarks>
		/// <seealso cref="Error(object)"/>
		/// <seealso cref="IsErrorEnabled"/>
		void ErrorFormat(string format, object arg0); 

		/// <summary>
		/// Logs a formatted message string with the <see cref="Level.Error"/> level.
		/// </summary>
		/// <param name="format">A String containing zero or more format items</param>
		/// <param name="arg0">An Object to format</param>
		/// <param name="arg1">An Object to format</param>
		/// <remarks>
		/// <para>
		/// The message is formatted using the <c>String.Format</c> method. See
		/// <see cref="String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
		/// of the formatting.
		/// </para>
		/// <para>
		/// This method does not take an <see cref="Exception"/> object to include in the
		/// log event. To pass an <see cref="Exception"/> use one of the <see cref="Error(object,Exception)"/>
		/// methods instead.
		/// </para>
		/// </remarks>
		/// <seealso cref="Error(object)"/>
		/// <seealso cref="IsErrorEnabled"/>
		void ErrorFormat(string format, object arg0, object arg1); 

		/// <summary>
		/// Logs a formatted message string with the <see cref="Level.Error"/> level.
		/// </summary>
		/// <param name="format">A String containing zero or more format items</param>
		/// <param name="arg0">An Object to format</param>
		/// <param name="arg1">An Object to format</param>
		/// <param name="arg2">An Object to format</param>
		/// <remarks>
		/// <para>
		/// The message is formatted using the <c>String.Format</c> method. See
		/// <see cref="String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
		/// of the formatting.
		/// </para>
		/// <para>
		/// This method does not take an <see cref="Exception"/> object to include in the
		/// log event. To pass an <see cref="Exception"/> use one of the <see cref="Error(object,Exception)"/>
		/// methods instead.
		/// </para>
		/// </remarks>
		/// <seealso cref="Error(object)"/>
		/// <seealso cref="IsErrorEnabled"/>
		void ErrorFormat(string format, object arg0, object arg1, object arg2); 

		/// <summary>
		/// Logs a formatted message string with the <see cref="Level.Error"/> level.
		/// </summary>
		/// <param name="provider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information</param>
		/// <param name="format">A String containing zero or more format items</param>
		/// <param name="args">An Object array containing zero or more objects to format</param>
		/// <remarks>
		/// <para>
		/// The message is formatted using the <c>String.Format</c> method. See
		/// <see cref="String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
		/// of the formatting.
		/// </para>
		/// <para>
		/// This method does not take an <see cref="Exception"/> object to include in the
		/// log event. To pass an <see cref="Exception"/> use one of the <see cref="Error(object)"/>
		/// methods instead.
		/// </para>
		/// </remarks>
		/// <seealso cref="Error(object,Exception)"/>
		/// <seealso cref="IsErrorEnabled"/>
		void ErrorFormat(IFormatProvider provider, string format, params object[] args);

        #endregion

        #region Fatal
        /// <overloads>Log a message object with the <see cref="Level.Fatal"/> level.</overloads>
		/// <summary>
		/// Log a message object with the <see cref="Level.Fatal"/> level.
		/// </summary>
		/// <remarks>
		/// <para>
		/// This method first checks if this logger is <c>FATAL</c>
		/// enabled by comparing the level of this logger with the 
		/// <see cref="Level.Fatal"/> level. If this logger is
		/// <c>FATAL</c> enabled, then it converts the message object
		/// (passed as parameter) to a string by invoking the appropriate
		/// <see cref="Library.Logging.ObjectRenderer.IObjectRenderer"/>. It then 
		/// proceeds to call all the registered appenders in this logger 
		/// and also higher in the hierarchy depending on the value of the 
		/// additivity flag.
		/// </para>
		/// <para><b>WARNING</b> Note that passing an <see cref="Exception"/> 
		/// to this method will print the name of the <see cref="Exception"/> 
		/// but no stack trace. To print a stack trace use the 
		/// <see cref="Fatal(object,Exception)"/> form instead.
		/// </para>
		/// </remarks>
		/// <param name="message">The message object to log.</param>
		/// <seealso cref="Fatal(object,Exception)"/>
		/// <seealso cref="IsFatalEnabled"/>
		void Fatal(int eventId, object message);
  
		/// <summary>
		/// Log a message object with the <see cref="Level.Fatal"/> level including
		/// the stack trace of the <see cref="Exception"/> passed
		/// as a parameter.
		/// </summary>
		/// <param name="message">The message object to log.</param>
		/// <param name="exception">The exception to log, including its stack trace.</param>
		/// <remarks>
		/// <para>
		/// See the <see cref="Fatal(object)"/> form for more detailed information.
		/// </para>
		/// </remarks>
		/// <seealso cref="Fatal(object)"/>
		/// <seealso cref="IsFatalEnabled"/>
		void Fatal(int eventId, object message, Exception exception);

		/// <overloads>Log a formatted message string with the <see cref="Level.Fatal"/> level.</overloads>
		/// <summary>
		/// Logs a formatted message string with the <see cref="Level.Fatal"/> level.
		/// </summary>
		/// <param name="format">A String containing zero or more format items</param>
		/// <param name="args">An Object array containing zero or more objects to format</param>
		/// <remarks>
		/// <para>
		/// The message is formatted using the <c>String.Format</c> method. See
		/// <see cref="String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
		/// of the formatting.
		/// </para>
		/// <para>
		/// This method does not take an <see cref="Exception"/> object to include in the
		/// log event. To pass an <see cref="Exception"/> use one of the <see cref="Fatal(object)"/>
		/// methods instead.
		/// </para>
		/// </remarks>
		/// <seealso cref="Fatal(object,Exception)"/>
		/// <seealso cref="IsFatalEnabled"/>
		void FatalFormat(string format, params object[] args);

		/// <summary>
		/// Logs a formatted message string with the <see cref="Level.Fatal"/> level.
		/// </summary>
		/// <param name="format">A String containing zero or more format items</param>
		/// <param name="arg0">An Object to format</param>
		/// <remarks>
		/// <para>
		/// The message is formatted using the <c>String.Format</c> method. See
		/// <see cref="String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
		/// of the formatting.
		/// </para>
		/// <para>
		/// This method does not take an <see cref="Exception"/> object to include in the
		/// log event. To pass an <see cref="Exception"/> use one of the <see cref="Fatal(object,Exception)"/>
		/// methods instead.
		/// </para>
		/// </remarks>
		/// <seealso cref="Fatal(object)"/>
		/// <seealso cref="IsFatalEnabled"/>
		void FatalFormat(string format, object arg0); 

		/// <summary>
		/// Logs a formatted message string with the <see cref="Level.Fatal"/> level.
		/// </summary>
		/// <param name="format">A String containing zero or more format items</param>
		/// <param name="arg0">An Object to format</param>
		/// <param name="arg1">An Object to format</param>
		/// <remarks>
		/// <para>
		/// The message is formatted using the <c>String.Format</c> method. See
		/// <see cref="String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
		/// of the formatting.
		/// </para>
		/// <para>
		/// This method does not take an <see cref="Exception"/> object to include in the
		/// log event. To pass an <see cref="Exception"/> use one of the <see cref="Fatal(object,Exception)"/>
		/// methods instead.
		/// </para>
		/// </remarks>
		/// <seealso cref="Fatal(object)"/>
		/// <seealso cref="IsFatalEnabled"/>
		void FatalFormat(string format, object arg0, object arg1); 

		/// <summary>
		/// Logs a formatted message string with the <see cref="Level.Fatal"/> level.
		/// </summary>
		/// <param name="format">A String containing zero or more format items</param>
		/// <param name="arg0">An Object to format</param>
		/// <param name="arg1">An Object to format</param>
		/// <param name="arg2">An Object to format</param>
		/// <remarks>
		/// <para>
		/// The message is formatted using the <c>String.Format</c> method. See
		/// <see cref="String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
		/// of the formatting.
		/// </para>
		/// <para>
		/// This method does not take an <see cref="Exception"/> object to include in the
		/// log event. To pass an <see cref="Exception"/> use one of the <see cref="Fatal(object,Exception)"/>
		/// methods instead.
		/// </para>
		/// </remarks>
		/// <seealso cref="Fatal(object)"/>
		/// <seealso cref="IsFatalEnabled"/>
		void FatalFormat(string format, object arg0, object arg1, object arg2); 

		/// <summary>
		/// Logs a formatted message string with the <see cref="Level.Fatal"/> level.
		/// </summary>
		/// <param name="provider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information</param>
		/// <param name="format">A String containing zero or more format items</param>
		/// <param name="args">An Object array containing zero or more objects to format</param>
		/// <remarks>
		/// <para>
		/// The message is formatted using the <c>String.Format</c> method. See
		/// <see cref="String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
		/// of the formatting.
		/// </para>
		/// <para>
		/// This method does not take an <see cref="Exception"/> object to include in the
		/// log event. To pass an <see cref="Exception"/> use one of the <see cref="Fatal(object)"/>
		/// methods instead.
		/// </para>
		/// </remarks>
		/// <seealso cref="Fatal(object,Exception)"/>
		/// <seealso cref="IsFatalEnabled"/>
		void FatalFormat(IFormatProvider provider, string format, params object[] args);

        #endregion

        #region Notice

        /// <overloads>Log a message object with the <see cref="Level.Notice"/> level.</overloads>
        /// <summary>
        /// Log a message object with the <see cref="Level.Notice"/> level.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method first checks if this logger is <c>NOTICE</c>
        /// enabled by comparing the level of this logger with the 
        /// <see cref="Level.Notice"/> level. If this logger is
        /// <c>NOTICE</c> enabled, then it converts the message object
        /// (passed as parameter) to a string by invoking the appropriate
        /// <see cref="Library.Logging.ObjectRenderer.IObjectRenderer"/>. It then 
        /// proceeds to call all the registered appenders in this logger 
        /// and also higher in the hierarchy depending on the value of the 
        /// additivity flag.
        /// </para>
        /// <para><b>WARNING</b> Note that passing an <see cref="Exception"/> 
        /// to this method will print the name of the <see cref="Exception"/> 
        /// but no stack trace. To print a stack trace use the 
        /// <see cref="Notice(object,Exception)"/> form instead.
        /// </para>
        /// </remarks>
        /// <param name="message">The message object to log.</param>
        /// <seealso cref="Notice(object,Exception)"/>
        /// <seealso cref="IsNoticeEnabled"/>
        void Notice(object message);

        /// <summary>
        /// Log a message object with the <see cref="Level.Notice"/> level including
        /// the stack trace of the <see cref="Exception"/> passed
        /// as a parameter.
        /// </summary>
        /// <param name="message">The message object to log.</param>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        /// <remarks>
        /// <para>
        /// See the <see cref="Notice(object)"/> form for more detailed information.
        /// </para>
        /// </remarks>
        /// <seealso cref="Notice(object)"/>
        /// <seealso cref="IsNoticeEnabled"/>
        void Notice(object message, Exception exception);

        /// <overloads>Log a formatted message string with the <see cref="Level.Notice"/> level.</overloads>
        /// <summary>
        /// Logs a formatted message string with the <see cref="Level.Notice"/> level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="args">An Object array containing zero or more objects to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="Exception"/> object to include in the
        /// log event. To pass an <see cref="Exception"/> use one of the <see cref="Notice(object)"/>
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="Notice(object,Exception)"/>
        /// <seealso cref="IsNoticeEnabled"/>
        void NoticeFormat(string format, params object[] args);

        /// <summary>
        /// Logs a formatted message string with the <see cref="Level.Notice"/> level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">An Object to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="Exception"/> object to include in the
        /// log event. To pass an <see cref="Exception"/> use one of the <see cref="Notice(object,Exception)"/>
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="Notice(object)"/>
        /// <seealso cref="IsNoticeEnabled"/>
        void NoticeFormat(string format, object arg0);

        /// <summary>
        /// Logs a formatted message string with the <see cref="Level.Notice"/> level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">An Object to format</param>
        /// <param name="arg1">An Object to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="Exception"/> object to include in the
        /// log event. To pass an <see cref="Exception"/> use one of the <see cref="Notice(object,Exception)"/>
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="Notice(object)"/>
        /// <seealso cref="IsNoticeEnabled"/>
        void NoticeFormat(string format, object arg0, object arg1);

        /// <summary>
        /// Logs a formatted message string with the <see cref="Level.Notice"/> level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">An Object to format</param>
        /// <param name="arg1">An Object to format</param>
        /// <param name="arg2">An Object to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="Exception"/> object to include in the
        /// log event. To pass an <see cref="Exception"/> use one of the <see cref="Notice(object,Exception)"/>
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="Notice(object)"/>
        /// <seealso cref="IsNoticeEnabled"/>
        void NoticeFormat(string format, object arg0, object arg1, object arg2);

        /// <summary>
        /// Logs a formatted message string with the <see cref="Level.Notice"/> level.
        /// </summary>
        /// <param name="provider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information</param>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="args">An Object array containing zero or more objects to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="Exception"/> object to include in the
        /// log event. To pass an <see cref="Exception"/> use one of the <see cref="Notice(object)"/>
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="Notice(object,Exception)"/>
        /// <seealso cref="IsNoticeEnabled"/>
        void NoticeFormat(IFormatProvider provider, string format, params object[] args);

        #endregion

        #region ExternalSystemInformation
        /// <overloads>Log a message object with the <see cref="Level.ExternalSystemInformation"/> level.</overloads>
        /// <summary>
        /// Logs a message object with the <see cref="Level.ExternalSystemInformation"/> level.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method first checks if this logger is <c>ExternalSystemInformation</c>
        /// enabled by comparing the level of this logger with the 
        /// <see cref="Level.ExternalSystemInformation"/> level. If this logger is
        /// <c>ExternalSystemInformation</c> enabled, then it converts the message object
        /// (passed as parameter) to a string by invoking the appropriate
        /// <see cref="Library.Logging.ObjectRenderer.IObjectRenderer"/>. It then 
        /// proceeds to call all the registered appenders in this logger 
        /// and also higher in the hierarchy depending on the value of the 
        /// additivity flag.
        /// </para>
        /// <para><b>WARNING</b> Note that passing an <see cref="Exception"/> 
        /// to this method will print the name of the <see cref="Exception"/> 
        /// but no stack trace. To print a stack trace use the 
        /// <see cref="ExternalSystemInformation(object,Exception)"/> form instead.
        /// </para>
        /// </remarks>
        /// <param name="message">The message object to log.</param>
        /// <seealso cref="ExternalSystemInformation(object,Exception)"/>
        /// <seealso cref="IsExternalSystemInformationEnabled"/>
        void ExternalSystemInformation(object message);

        /// <summary>
        /// Logs a message object with the <c>ExternalSystemInformation</c> level including
        /// the stack trace of the <see cref="Exception"/> passed
        /// as a parameter.
        /// </summary>
        /// <param name="message">The message object to log.</param>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        /// <remarks>
        /// <para>
        /// See the <see cref="ExternalSystemInformation(object)"/> form for more detailed ExternalSystemInformationrmation.
        /// </para>
        /// </remarks>
        /// <seealso cref="ExternalSystemInformation(object)"/>
        /// <seealso cref="IsExternalSystemInformationEnabled"/>
        void ExternalSystemInformation(object message, Exception exception);

        /// <overloads>Log a formatted message string with the <see cref="Level.ExternalSystemInformation"/> level.</overloads>
        /// <summary>
        /// Logs a formatted message string with the <see cref="Level.ExternalSystemInformation"/> level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="args">An Object array containing zero or more objects to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="Exception"/> object to include in the
        /// log event. To pass an <see cref="Exception"/> use one of the <see cref="ExternalSystemInformation(object)"/>
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="ExternalSystemInformation(object,Exception)"/>
        /// <seealso cref="IsExternalSystemInformationEnabled"/>
        void ExternalSystemInformationFormat(string format, params object[] args);

        /// <summary>
        /// Logs a formatted message string with the <see cref="Level.ExternalSystemInformation"/> level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">An Object to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="Exception"/> object to include in the
        /// log event. To pass an <see cref="Exception"/> use one of the <see cref="ExternalSystemInformation(object,Exception)"/>
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="ExternalSystemInformation(object)"/>
        /// <seealso cref="IsExternalSystemInformationEnabled"/>
        void ExternalSystemInformationFormat(string format, object arg0);

        /// <summary>
        /// Logs a formatted message string with the <see cref="Level.ExternalSystemInformation"/> level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">An Object to format</param>
        /// <param name="arg1">An Object to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="Exception"/> object to include in the
        /// log event. To pass an <see cref="Exception"/> use one of the <see cref="ExternalSystemInformation(object,Exception)"/>
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="ExternalSystemInformation(object)"/>
        /// <seealso cref="IsExternalSystemInformationEnabled"/>
        void ExternalSystemInformationFormat(string format, object arg0, object arg1);

        /// <summary>
        /// Logs a formatted message string with the <see cref="Level.ExternalSystemInformation"/> level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">An Object to format</param>
        /// <param name="arg1">An Object to format</param>
        /// <param name="arg2">An Object to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="Exception"/> object to include in the
        /// log event. To pass an <see cref="Exception"/> use one of the <see cref="ExternalSystemInformation(object,Exception)"/>
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="ExternalSystemInformation(object)"/>
        /// <seealso cref="IsExternalSystemInformationEnabled"/>
        void ExternalSystemInformationFormat(string format, object arg0, object arg1, object arg2);

        /// <summary>
        /// Logs a formatted message string with the <see cref="Level.ExternalSystemInformation"/> level.
        /// </summary>
        /// <param name="provider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting ExternalSystemInformationrmation</param>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="args">An Object array containing zero or more objects to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="Exception"/> object to include in the
        /// log event. To pass an <see cref="Exception"/> use one of the <see cref="ExternalSystemInformation(object)"/>
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="ExternalSystemInformation(object,Exception)"/>
        /// <seealso cref="IsExternalSystemInformationEnabled"/>
        void ExternalSystemInformationFormat(IFormatProvider provider, string format, params object[] args);

        #endregion

        #region Log decision variables
        /// <summary>
		/// Checks if this logger is enabled for the <see cref="Level.Trace"/> level.
		/// </summary>
		/// <value>
		/// <c>true</c> if this logger is enabled for <see cref="Level.Trace"/> events, <c>false</c> otherwise.
		/// </value>
		/// <remarks>
		/// <para>
		/// This function is intended to lessen the computational cost of
		/// disabled log trace statements.
		/// </para>
		/// <para> For some ILog interface <c>log</c>, when you write:</para>
		/// <code lang="C#">
		/// log.Trace("This is entry number: " + i );
		/// </code>
		/// <para>
		/// You incur the cost constructing the message, string construction and concatenation in
		/// this case, regardless of whether the message is logged or not.
		/// </para>
		/// <para>
		/// If you are worried about speed (who isn't), then you should write:
		/// </para>
		/// <code lang="C#">
		/// if (log.IsDebugEnabled)
		/// { 
		///     log.Trace("This is entry number: " + i );
		/// }
		/// </code>
		/// <para>
		/// This way you will not incur the cost of parameter
		/// construction if debugging is disabled for <c>log</c>. On
		/// the other hand, if the <c>log</c> is trace enabled, you
		/// will incur the cost of evaluating whether the logger is trace
		/// enabled twice. Once in <see cref="IsDebugEnabled"/> and once in
		/// the <see cref="Trace(object)"/>.  This is an insignificant overhead
		/// since evaluating a logger takes about 1% of the time it
		/// takes to actually log. This is the preferred style of logging.
		/// </para>
		/// <para>Alternatively if your logger is available statically then the is trace
		/// enabled state can be stored in a static variable like this:
		/// </para>
		/// <code lang="C#">
		/// private static readonly bool isDebugEnabled = log.IsDebugEnabled;
		/// </code>
		/// <para>
		/// Then when you come to log you can write:
		/// </para>
		/// <code lang="C#">
		/// if (isDebugEnabled)
		/// { 
		///     log.Trace("This is entry number: " + i );
		/// }
		/// </code>
		/// <para>
		/// This way the trace enabled state is only queried once
		/// when the class is loaded. Using a <c>private static readonly</c>
		/// variable is the most efficient because it is a run time constant
		/// and can be heavily optimized by the JIT compiler.
		/// </para>
		/// <para>
		/// Of course if you use a static readonly variable to
		/// hold the enabled state of the logger then you cannot
		/// change the enabled state at runtime to vary the logging
		/// that is produced. You have to decide if you need absolute
		/// speed or runtime flexibility.
		/// </para>
		/// </remarks>
		/// <seealso cref="Trace(object)"/>
		/// <seealso cref="DebugFormat(IFormatProvider, string, object[])"/>
		bool IsDebugEnabled { get; }
  
		/// <summary>
		/// Checks if this logger is enabled for the <see cref="Level.Info"/> level.
		/// </summary>
		/// <value>
		/// <c>true</c> if this logger is enabled for <see cref="Level.Info"/> events, <c>false</c> otherwise.
		/// </value>
		/// <remarks>
		/// For more information see <see cref="ILog.IsDebugEnabled"/>.
		/// </remarks>
		/// <seealso cref="Info(object)"/>
		/// <seealso cref="InfoFormat(IFormatProvider, string, object[])"/>
		/// <seealso cref="ILog.IsDebugEnabled"/>
		bool IsInfoEnabled { get; }

		/// <summary>
		/// Checks if this logger is enabled for the <see cref="Level.Warn"/> level.
		/// </summary>
		/// <value>
		/// <c>true</c> if this logger is enabled for <see cref="Level.Warn"/> events, <c>false</c> otherwise.
		/// </value>
		/// <remarks>
		/// For more information see <see cref="ILog.IsDebugEnabled"/>.
		/// </remarks>
		/// <seealso cref="Warn(object)"/>
		/// <seealso cref="WarnFormat(IFormatProvider, string, object[])"/>
		/// <seealso cref="ILog.IsDebugEnabled"/>
		bool IsWarnEnabled { get; }

		/// <summary>
		/// Checks if this logger is enabled for the <see cref="Level.Error"/> level.
		/// </summary>
		/// <value>
		/// <c>true</c> if this logger is enabled for <see cref="Level.Error"/> events, <c>false</c> otherwise.
		/// </value>
		/// <remarks>
		/// For more information see <see cref="ILog.IsDebugEnabled"/>.
		/// </remarks>
		/// <seealso cref="Error(object)"/>
		/// <seealso cref="ErrorFormat(IFormatProvider, string, object[])"/>
		/// <seealso cref="ILog.IsDebugEnabled"/>
		bool IsErrorEnabled { get; }

		/// <summary>
		/// Checks if this logger is enabled for the <see cref="Level.Fatal"/> level.
		/// </summary>
		/// <value>
		/// <c>true</c> if this logger is enabled for <see cref="Level.Fatal"/> events, <c>false</c> otherwise.
		/// </value>
		/// <remarks>
		/// For more information see <see cref="ILog.IsDebugEnabled"/>.
		/// </remarks>
		/// <seealso cref="Fatal(object)"/>
		/// <seealso cref="FatalFormat(IFormatProvider, string, object[])"/>
		/// <seealso cref="ILog.IsDebugEnabled"/>
		bool IsFatalEnabled { get; }

        /// <summary>
        /// Checks if this logger is enabled for the <see cref="Level.Notice"/> level.
        /// </summary>
        /// <value>
        /// <c>true</c> if this logger is enabled for <see cref="Level.Notice"/> events, <c>false</c> otherwise.
        /// </value>
        /// <remarks>
        /// For more information see <see cref="ILog.IsNoticeEnabled"/>.
        /// </remarks>
        /// <seealso cref="Notice(object)"/>
        /// <seealso cref="NoticeFormat(IFormatProvider, string, object[])"/>
        /// <seealso cref="ILog.IsDebugEnabled"/>
        bool IsNoticeEnabled { get; }

        /// <summary>
        /// Checks if this logger is enabled for the <see cref="Level.BusinessError"/> level.
        /// </summary>
        /// <value>
        /// <c>true</c> if this logger is enabled for <see cref="Level.BusinessError"/> events, <c>false</c> otherwise.
        /// </value>
        /// <remarks>
        /// For more information see <see cref="ILog.IsBusinessErrorEnabled"/>.
        /// </remarks>
        /// <seealso cref="BusinessError(int eventId, object)"/>
        /// <seealso cref="BusinessErrorFormat(IFormatProvider, string, object[])"/>
        /// <seealso cref="ILog.IsBusinessErrorEnabled"/>
        bool IsBusinessErrorEnabled { get; }

        /// <summary>
        /// Checks if this logger is enabled for the <see cref="Level.All"/> level.
        /// </summary>
        /// <value>
        /// <c>true</c> if this logger is enabled for <see cref="Level.All"/> events, <c>false</c> otherwise.
        /// </value>
        /// <remarks>
        /// For more information see <see cref="ILog.IsAllEnabled"/>.
        /// </remarks>
        /// <seealso cref="All(object)"/>
        /// <seealso cref="All(IFormatProvider, string, object[])"/>
        /// <seealso cref="ILog.IsAllEnabled"/>
        bool IsAllEnabled { get; }


        /// <summary>
        /// Checks if this logger is enabled for the <see cref="Level.ExternalSystemInformation"/> level.
        /// </summary>
        /// <value>
        /// <c>true</c> if this logger is enabled for <see cref="Level.ExternalSystemInformation"/> events, <c>false</c> otherwise.
        /// </value>
        /// <remarks>
        /// For more information see <see cref="ILog.IsDebugEnabled"/>.
        /// </remarks>
        /// <seealso cref="ExternalSystemInformation(object)"/>
        /// <seealso cref="ExternalSystemInformation(IFormatProvider, string, object[])"/>
        /// <seealso cref="ILog.IsExternalSystemInformationEnabled"/>
        bool IsExternalSystemInformationEnabled { get; }
        #endregion
    }
}
