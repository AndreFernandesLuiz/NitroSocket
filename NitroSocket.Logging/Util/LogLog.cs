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
using System.Collections;
using System.Configuration;
using System.Diagnostics;

namespace Library.Logging.Util
{
	/// <summary>
    /// Outputs log statements from within the  Logging assembly.
	/// </summary>
	/// <remarks>
	/// <para>
    ///  Logging components cannot make  Logging logging calls. However, it is
    /// sometimes useful for the user to learn about what  Logging is
	/// doing.
	/// </para>
	/// <para>
    /// All  Logging internal trace calls go to the standard output stream
	/// whereas internal error messages are sent to the standard error output 
	/// stream.
	/// </para>
	/// </remarks>
	/// <author>Nicko Cadell</author>
	/// <author>Gert Driesen</author>
	public sealed class LogLog
	{
		#region Private Instance Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="LogLog" /> class. 
		/// </summary>
		/// <remarks>
		/// <para>
		/// Uses a private access modifier to prevent instantiation of this class.
		/// </para>
		/// </remarks>
		private LogLog()
		{
		}

		#endregion Private Instance Constructors

		#region Static Constructor

		/// <summary>
		/// Static constructor that initializes logging by reading 
		/// settings from the application configuration file.
		/// </summary>
		/// <remarks>
		/// <para>
		/// The <c>Library.Logging.Internal.Trace</c> application setting
		/// controls internal debugging. This setting should be set
		/// to <c>true</c> to enable debugging.
		/// </para>
		/// <para>
		/// The <c>Library.Logging.Internal.Quiet</c> application setting
		/// suppresses all internal logging including error messages. 
		/// This setting should be set to <c>true</c> to enable message
		/// suppression.
		/// </para>
		/// </remarks>
		static LogLog()
		{
#if !NETCF
			try
			{
				InternalDebugging = OptionConverter.ToBoolean(SystemInfo.GetAppSetting("Library.Logging.Internal.Trace"), false);
				QuietMode = OptionConverter.ToBoolean(SystemInfo.GetAppSetting("Library.Logging.Internal.Quiet"), false);
			}
			catch(Exception ex)
			{
				// If an exception is thrown here then it looks like the config file does not
				// parse correctly.
				//
				// We will leave trace OFF and print an Error message
				Error("LogLog: Exception while reading ConfigurationSettings. Check your .config file is well formed XML.", ex);
			}
#endif
		}

		#endregion Static Constructor

		#region Public Static Properties

		/// <summary>
        /// Gets or sets a value indicating whether  Logging internal logging
		/// is enabled or disabled.
		/// </summary>
		/// <value>
        /// <c>true</c> if  Logging internal logging is enabled, otherwise 
		/// <c>false</c>.
		/// </value>
		/// <remarks>
		/// <para>
		/// When set to <c>true</c>, internal trace level logging will be 
		/// displayed.
		/// </para>
		/// <para>
		/// This value can be set by setting the application setting 
		/// <c>Library.Logging.Internal.Trace</c> in the application configuration
		/// file.
		/// </para>
		/// <para>
		/// The default value is <c>false</c>, i.e. debugging is
		/// disabled.
		/// </para>
		/// </remarks>
		/// <example>
		/// <para>
		/// The following example enables internal debugging using the 
		/// application configuration file :
		/// </para>
		/// <code lang="XML" escaped="true">
		/// <configuration>
		///		<appSettings>
		///			<add key="Library.Logging.Internal.Trace" value="true" />
		///		</appSettings>
		/// </configuration>
		/// </code>
		/// </example>
		public static bool InternalDebugging
		{
			get { return s_debugEnabled; }
			set { s_debugEnabled = value; }
		}

		/// <summary>
        /// Gets or sets a value indicating whether  Logging should generate no output
		/// from internal logging, not even for errors. 
		/// </summary>
		/// <value>
        /// <c>true</c> if  Logging should generate no output at all from internal 
		/// logging, otherwise <c>false</c>.
		/// </value>
		/// <remarks>
		/// <para>
		/// When set to <c>true</c> will cause internal logging at all levels to be 
		/// suppressed. This means that no warning or error reports will be logged. 
		/// This option overrides the <see cref="InternalDebugging"/> setting and 
		/// disables all trace also.
		/// </para>
		/// <para>This value can be set by setting the application setting
		/// <c>Library.Logging.Internal.Quiet</c> in the application configuration file.
		/// </para>
		/// <para>
		/// The default value is <c>false</c>, i.e. internal logging is not
		/// disabled.
		/// </para>
		/// </remarks>
		/// <example>
		/// The following example disables internal logging using the 
		/// application configuration file :
		/// <code lang="XML" escaped="true">
		/// <configuration>
		///		<appSettings>
		///			<add key="Library.Logging.Internal.Quiet" value="true" />
		///		</appSettings>
		/// </configuration>
		/// </code>
		/// </example>
		public static bool QuietMode
		{
			get { return s_quietMode; }
			set { s_quietMode = value; }
		}

		#endregion Public Static Properties

		#region Public Static Methods

		/// <summary>
		/// Test if LogLog.Trace is enabled for output.
		/// </summary>
		/// <value>
		/// <c>true</c> if Trace is enabled
		/// </value>
		/// <remarks>
		/// <para>
		/// Test if LogLog.Trace is enabled for output.
		/// </para>
		/// </remarks>
		public static bool IsDebugEnabled
		{
			get { return s_debugEnabled && !s_quietMode; }
		}

		/// <summary>
        /// Writes  Logging internal trace messages to the 
		/// standard output stream.
		/// </summary>
		/// <param name="message">The message to log.</param>
		/// <remarks>
		/// <para>
		///	All internal trace messages are prepended with 
        ///	the string " Logging: ".
		/// </para>
		/// </remarks>
		public static void Trace(string message) 
		{
			if (IsDebugEnabled) 
			{
				EmitOutLine(PREFIX + message);
			}
		}

		/// <summary>
        /// Writes  Logging internal trace messages to the 
		/// standard output stream.
		/// </summary>
		/// <param name="message">The message to log.</param>
		/// <param name="exception">An exception to log.</param>
		/// <remarks>
		/// <para>
		///	All internal trace messages are prepended with 
        ///	the string " Logging: ".
		/// </para>
		/// </remarks>
		public static void Trace(string message, Exception exception) 
		{
			if (IsDebugEnabled) 
			{
				EmitOutLine(PREFIX + message);
				if (exception != null)
				{
					EmitOutLine(exception.ToString());
				}
			}
		}
  
		/// <summary>
		/// Test if LogLog.Warn is enabled for output.
		/// </summary>
		/// <value>
		/// <c>true</c> if Warn is enabled
		/// </value>
		/// <remarks>
		/// <para>
		/// Test if LogLog.Warn is enabled for output.
		/// </para>
		/// </remarks>
		public static bool IsWarnEnabled
		{
			get { return !s_quietMode; }
		}

		/// <summary>
        /// Writes  Logging internal warning messages to the 
		/// standard error stream.
		/// </summary>
		/// <param name="message">The message to log.</param>
		/// <remarks>
		/// <para>
		///	All internal warning messages are prepended with 
        ///	the string " Logging:WARN ".
		/// </para>
		/// </remarks>
		public static void Warn(string message) 
		{
			if (IsWarnEnabled)
			{
				EmitErrorLine(WARN_PREFIX + message);
			}
		}  

		/// <summary>
        /// Writes  Logging internal warning messages to the 
		/// standard error stream.
		/// </summary>
		/// <param name="message">The message to log.</param>
		/// <param name="exception">An exception to log.</param>
		/// <remarks>
		/// <para>
		///	All internal warning messages are prepended with 
        ///	the string " Logging:WARN ".
		/// </para>
		/// </remarks>
		public static void Warn(string message, Exception exception) 
		{
			if (IsWarnEnabled)
			{
				EmitErrorLine(WARN_PREFIX + message);
				if (exception != null) 
				{
					EmitErrorLine(exception.ToString());
				}
			}
		} 

		/// <summary>
		/// Test if LogLog.Error is enabled for output.
		/// </summary>
		/// <value>
		/// <c>true</c> if Error is enabled
		/// </value>
		/// <remarks>
		/// <para>
		/// Test if LogLog.Error is enabled for output.
		/// </para>
		/// </remarks>
		public static bool IsErrorEnabled
		{
			get { return !s_quietMode; }
		}

		/// <summary>
        /// Writes  Logging internal error messages to the 
		/// standard error stream.
		/// </summary>
		/// <param name="message">The message to log.</param>
		/// <remarks>
		/// <para>
		///	All internal error messages are prepended with 
        ///	the string " Logging:ERROR ".
		/// </para>
		/// </remarks>
		public static void Error(string message) 
		{
			if (IsErrorEnabled)
			{
				EmitErrorLine(ERR_PREFIX + message);
			}
		}  

		/// <summary>
        /// Writes  Logging internal error messages to the 
		/// standard error stream.
		/// </summary>
		/// <param name="message">The message to log.</param>
		/// <param name="exception">An exception to log.</param>
		/// <remarks>
		/// <para>
		///	All internal trace messages are prepended with 
        ///	the string " Logging:ERROR ".
		/// </para>
		/// </remarks>
		public static void Error(string message, Exception exception) 
		{
			if (IsErrorEnabled)
			{
				EmitErrorLine(ERR_PREFIX + message);
				if (exception != null) 
				{
					EmitErrorLine(exception.ToString());
                    // read Data attribute in Exception
                    foreach (IDictionary dic in exception.Data)
                    {
                        EmitErrorLine(dic.Keys.ToString() + " - " + dic.Values.ToString());
                    }
				}
			}
		}

        /// <summary>
        /// Test if LogLog.BusinessError is enabled for output.
        /// </summary>
        /// <value>
        /// <c>true</c> if BusinessError is enabled
        /// </value>
        /// <remarks>
        /// <para>
        /// Test if LogLog.BusinessError is enabled for output.
        /// </para>
        /// </remarks>
        public static bool IsBusinessErrorEnabled
        {
            get { return !s_quietMode; }
        }

        /// <summary>
        /// Writes  Logging internal warning messages to the 
        /// standard error stream.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <remarks>
        /// <para>
        ///	All internal warning messages are prepended with 
        ///	the string " Logging:BUSINESSERROR ".
        /// </para>
        /// </remarks>
        public static void BusinessError(string message)
        {
            if (IsBusinessErrorEnabled)
            {
                EmitErrorLine(BIZ_PREFIX + message);
            }
        }

        /// <summary>
        /// Writes  Logging internal warning messages to the 
        /// standard error stream.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="exception">An exception to log.</param>
        /// <remarks>
        /// <para>
        ///	All internal warning messages are prepended with 
        ///	the string " Logging:WARN ".
        /// </para>
        /// </remarks>
        public static void BusinessError(string message, Exception exception)
        {
            if (IsBusinessErrorEnabled)
            {
                EmitErrorLine(BIZ_PREFIX + message);
                if (exception != null)
                {
                    EmitErrorLine(exception.ToString());
                }
            }
        } 

		#endregion Public Static Methods

		/// <summary>
		/// Writes output to the standard output stream.  
		/// </summary>
		/// <param name="message">The message to log.</param>
		/// <remarks>
		/// <para>
		/// Writes to both Console.Out and System.Diagnostics.Trace.
		/// Note that the System.Diagnostics.Trace is not supported
		/// on the Compact Framework.
		/// </para>
		/// <para>
		/// If the AppDomain is not configured with a config file then
		/// the call to System.Diagnostics.Trace may fail. This is only
		/// an issue if you are programmatically creating your own AppDomains.
		/// </para>
		/// </remarks>
		private static void EmitOutLine(string message)
		{
			try
			{
#if NETCF
				Console.WriteLine(message);
				//System.Diagnostics.Trace.WriteLine(message);
#else
				Console.Out.WriteLine(message);
                Debug.WriteLine(message);
#endif
			}
			catch
			{
				// Ignore exception, what else can we do? Not really a good idea to propagate back to the caller
                LogLog.Warn("Exception while trying to write in console.");
			}
		}

		/// <summary>
		/// Writes output to the standard error stream.  
		/// </summary>
		/// <param name="message">The message to log.</param>
		/// <remarks>
		/// <para>
		/// Writes to both Console.Error and System.Diagnostics.Trace.
		/// Note that the System.Diagnostics.Trace is not supported
		/// on the Compact Framework.
		/// </para>
		/// <para>
		/// If the AppDomain is not configured with a config file then
		/// the call to System.Diagnostics.Trace may fail. This is only
		/// an issue if you are programmatically creating your own AppDomains.
		/// </para>
		/// </remarks>
		private static void EmitErrorLine(string message)
		{
			try
			{
#if NETCF
				Console.WriteLine(message);
				//System.Diagnostics.Trace.WriteLine(message);
#else
				Console.Error.WriteLine(message);
                Debug.WriteLine(message);
#endif
			}
			catch
			{
				// Ignore exception, what else can we do? Not really a good idea to propagate back to the caller
                LogLog.Warn("Exception while trying to write in console");
			}
		}

		#region Private Static Fields

		/// <summary>
		///  Default trace level
		/// </summary>
		private static bool s_debugEnabled = false;

		/// <summary>
		/// In quietMode not even errors generate any output.
		/// </summary>
		private static bool s_quietMode = false;

        private const string PREFIX = " Logging: ";
        private const string ERR_PREFIX = " Logging:ERROR ";
        private const string WARN_PREFIX = " Logging:WARN ";
        private const string BIZ_PREFIX = " Logging:BUSINESSERROR ";

		#endregion Private Static Fields
	}
}
