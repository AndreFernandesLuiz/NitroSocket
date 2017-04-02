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

// .NET Compact Framework 1.0 has no support for application .config files
#if !NETCF

using System.Configuration;
using System.Xml;

namespace Library.Logging.Config
{
	/// <summary>
	/// Class to register for the  Logging section of the configuration file
	/// </summary>
	/// <remarks>
	/// The  Logging section of the configuration file needs to have a section
	/// handler registered. This is the section handler used. It simply returns
	/// the XML element that is the root of the section.
	/// </remarks>
	/// <example>
	/// Example of registering the  Logging section handler :
	/// <code lang="XML" escaped="true">
	/// <configuration>
	///		<configSections>
	///			<section name="Logging" type="Library.Logging.Config. LoggingConfigurationSectionHandler,  Logging" />
	///		</configSections>
	///		< Logging>
	///			 Logging configuration XML goes here
	///		</ Logging>
	/// </configuration>
	/// </code>
	/// </example>
	/// <author>Nicko Cadell</author>
	/// <author>Gert Driesen</author>
	public class  LoggingConfigurationSectionHandler : IConfigurationSectionHandler
	{
		#region Public Instance Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref=" LoggingConfigurationSectionHandler"/> class.
		/// </summary>
		/// <remarks>
		/// <para>
		/// Default constructor.
		/// </para>
		/// </remarks>
        public  LoggingConfigurationSectionHandler()
		{
		}

		#endregion Public Instance Constructors

		#region Implementation of IConfigurationSectionHandler

		/// <summary>
		/// Parses the configuration section.
		/// </summary>
		/// <param name="parent">The configuration settings in a corresponding parent configuration section.</param>
		/// <param name="configContext">The configuration context when called from the ASP.NET configuration system. Otherwise, this parameter is reserved and is a null reference.</param>
		/// <param name="section">The <see cref="XmlNode" /> for the  Logging section.</param>
		/// <returns>The <see cref="XmlNode" /> for the  Logging section.</returns>
		/// <remarks>
		/// <para>
		/// Returns the <see cref="XmlNode"/> containing the configuration data,
		/// </para>
		/// </remarks>
		public object Create(object parent, object configContext, XmlNode section)
		{
			return section;
		}

		#endregion Implementation of IConfigurationSectionHandler
	}
}

#endif // !NETCF