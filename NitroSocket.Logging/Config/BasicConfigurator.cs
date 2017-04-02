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

using System.Reflection;

using Library.Logging.Appender;
using Library.Logging.Layout;
using Library.Logging.Util;
using Library.Logging.Repository;
using Library.Logging.Repository.Hierarchy;

namespace Library.Logging.Config
{
	/// <summary>
	/// Use this class to quickly configure a <see cref="Hierarchy"/>.
	/// </summary>
	/// <remarks>
	/// <para>
	/// Allows very simple programmatic configuration of Library.Logging.
	/// </para>
	/// <para>
	/// Only one appender can be configured using this configurator.
	/// The appender is set at the root of the hierarchy and all logging
	/// events will be delivered to that appender.
	/// </para>
	/// <para>
	/// Appenders can also implement the <see cref="Library.Logging.Core.IOptionHandler"/> interface. Therefore
	/// they would require that the <see cref="Library.Logging.Core.IOptionHandler.ActivateOptions()"/> method
	/// be called after the appenders properties have been configured.
	/// </para>
	/// </remarks>
	/// <author>Nicko Cadell</author>
	/// <author>Gert Driesen</author>
	public sealed class BasicConfigurator
	{
		#region Private Instance Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="BasicConfigurator" /> class. 
		/// </summary>
		/// <remarks>
		/// <para>
		/// Uses a private access modifier to prevent instantiation of this class.
		/// </para>
		/// </remarks>
		private BasicConfigurator()
		{
		}

		#endregion Private Instance Constructors

		#region Public Static Methods

		/// <summary>
        /// Initializes the  Logging system with a default configuration.
		/// </summary>
		/// <remarks>
		/// <para>
        /// Initializes the  Logging logging system using a <see cref="ConsoleAppender"/>
		/// that will write to <c>Console.Out</c>. The log messages are
		/// formatted using the <see cref="PatternLayout"/> layout object
		/// with the <see cref="PatternLayout.DetailConversionPattern"/>
		/// layout style.
		/// </para>
		/// </remarks>
		static public void Configure() 
		{
			BasicConfigurator.Configure(LogManager.GetRepository(Assembly.GetCallingAssembly()));
		}

		/// <summary>
        /// Initializes the  Logging system using the specified appender.
		/// </summary>
		/// <param name="appender">The appender to use to log all logging events.</param>
		/// <remarks>
		/// <para>
        /// Initializes the  Logging system using the specified appender.
		/// </para>
		/// </remarks>
		static public void Configure(IAppender appender) 
		{
			BasicConfigurator.Configure(LogManager.GetRepository(Assembly.GetCallingAssembly()), appender);
		}

		/// <summary>
		/// Initializes the <see cref="ILoggerRepository"/> with a default configuration.
		/// </summary>
		/// <param name="repository">The repository to configure.</param>
		/// <remarks>
		/// <para>
		/// Initializes the specified repository using a <see cref="ConsoleAppender"/>
		/// that will write to <c>Console.Out</c>. The log messages are
		/// formatted using the <see cref="PatternLayout"/> layout object
		/// with the <see cref="PatternLayout.DetailConversionPattern"/>
		/// layout style.
		/// </para>
		/// </remarks>
		static public void Configure(ILoggerRepository repository) 
		{
			// Create the layout
			PatternLayout layout = new PatternLayout();
			layout.ConversionPattern = PatternLayout.DetailConversionPattern;
			layout.ActivateOptions();

			// Create the appender
			ConsoleAppender appender = new ConsoleAppender();
			appender.Layout = layout;
			appender.ActivateOptions();

			BasicConfigurator.Configure(repository, appender);
		}

		/// <summary>
		/// Initializes the <see cref="ILoggerRepository"/> using the specified appender.
		/// </summary>
		/// <param name="repository">The repository to configure.</param>
		/// <param name="appender">The appender to use to log all logging events.</param>
		/// <remarks>
		/// <para>
		/// Initializes the <see cref="ILoggerRepository"/> using the specified appender.
		/// </para>
		/// </remarks>
		static public void Configure(ILoggerRepository repository, IAppender appender) 
		{
			IBasicRepositoryConfigurator configurableRepository = repository as IBasicRepositoryConfigurator;
			if (configurableRepository != null)
			{
				configurableRepository.Configure(appender);
			}
			else
			{
				LogLog.Warn("BasicConfigurator: Repository [" + repository + "] does not support the BasicConfigurator");
			}
		}

		#endregion Public Static Methods
	}
}
