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

// .NET Compact Framework 1.0 has no support for System.Runtime.Remoting.Messaging.CallContext
#if !NETCF

using System;
using System.Collections;

using System.Runtime.Remoting.Messaging;

namespace Library.Logging.Util
{
	/// <summary>
	/// Implementation of Properties collection for the <see cref="Library.Logging.LogicalThreadContext"/>
	/// </summary>
	/// <remarks>
	/// <para>
	/// Class implements a collection of properties that is specific to each thread.
	/// The class is not synchronized as each thread has its own <see cref="PropertiesDictionary"/>.
	/// </para>
	/// </remarks>
	/// <author>Nicko Cadell</author>
	public sealed class LogicalThreadContextProperties : ContextPropertiesBase
	{
		#region Public Instance Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <remarks>
		/// <para>
		/// Initializes a new instance of the <see cref="LogicalThreadContextProperties" /> class.
		/// </para>
		/// </remarks>
		internal LogicalThreadContextProperties()
		{
		}

		#endregion Public Instance Constructors

		#region Public Instance Properties

		/// <summary>
		/// Gets or sets the value of a property
		/// </summary>
		/// <value>
		/// The value for the property with the specified key
		/// </value>
		/// <remarks>
		/// <para>
		/// Get or set the property value for the <paramref name="key"/> specified.
		/// </para>
		/// </remarks>
		override public object this[string key]
		{
			get 
			{ 
				// Don't create the dictionary if it does not already exist
				PropertiesDictionary dictionary = GetProperties(false);
				if (dictionary != null)
				{
					return dictionary[key]; 
				}
				return null;
			}
			set 
			{ 
				// Force the dictionary to be created
				GetProperties(true)[key] = value; 
			}
		}

		#endregion Public Instance Properties

		#region Public Instance Methods

		/// <summary>
		/// Remove a property
		/// </summary>
		/// <param name="key">the key for the entry to remove</param>
		/// <remarks>
		/// <para>
		/// Remove the value for the specified <paramref name="key"/> from the context.
		/// </para>
		/// </remarks>
		public void Remove(string key)
		{
			PropertiesDictionary dictionary = GetProperties(false);
			if (dictionary != null)
			{
				dictionary.Remove(key);
			}
		}

		/// <summary>
		/// Clear all the context properties
		/// </summary>
		/// <remarks>
		/// <para>
		/// Clear all the context properties
		/// </para>
		/// </remarks>
		public void Clear()
		{
			PropertiesDictionary dictionary = GetProperties(false);
			if (dictionary != null)
			{
				dictionary.Clear();
			}
		}

		#endregion Public Instance Methods

		#region Internal Instance Methods

		/// <summary>
		/// Get the PropertiesDictionary stored in the LocalDataStoreSlot for this thread.
		/// </summary>
		/// <param name="create">create the dictionary if it does not exist, otherwise return null if is does not exist</param>
		/// <returns>the properties for this thread</returns>
		/// <remarks>
		/// <para>
		/// The collection returned is only to be used on the calling thread. If the
		/// caller needs to share the collection between different threads then the 
		/// caller must clone the collection before doings so.
		/// </para>
		/// </remarks>
		internal PropertiesDictionary GetProperties(bool create)
		{
			PropertiesDictionary properties = (PropertiesDictionary)CallContext.GetData("Library.Logging.Util.LogicalThreadContextProperties");
			if (properties == null && create)
			{
				properties  = new PropertiesDictionary();
				CallContext.SetData("Library.Logging.Util.LogicalThreadContextProperties", properties);
			}
			return properties;
		}

		#endregion Internal Instance Methods
	}
}

#endif
