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
using System.Text;
using System.IO;

using Library.Logging.Core;

namespace Library.Logging.Layout.Pattern
{
	/// <summary>
	/// Write the exception text to the output
	/// </summary>
	/// <remarks>
	/// <para>
	/// If an exception object is stored in the logging event
	/// it will be rendered into the pattern output with a
	/// trailing newline.
	/// </para>
	/// <para>
	/// If there is no exception then nothing will be output
	/// and no trailing newline will be appended.
	/// It is typical to put a newline before the exception
	/// and to have the exception as the last data in the pattern.
	/// </para>
	/// </remarks>
	/// <author>Nicko Cadell</author>
	internal sealed class ExceptionPatternConverter : PatternLayoutConverter 
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		public ExceptionPatternConverter()
		{
			// This converter handles the exception
			this.IgnoresException = false;
		}

		/// <summary>
		/// Write the exception text to the output
		/// </summary>
		/// <param name="writer"><see cref="TextWriter" /> that will receive the formatted result.</param>
		/// <param name="loggingEvent">the event being logged</param>
		/// <remarks>
		/// <para>
		/// If an exception object is stored in the logging event
		/// it will be rendered into the pattern output with a
		/// trailing newline.
		/// </para>
		/// <para>
		/// If there is no exception then nothing will be output
		/// and no trailing newline will be appended.
		/// It is typical to put a newline before the exception
		/// and to have the exception as the last data in the pattern.
		/// </para>
		/// </remarks>
		override protected void Convert(TextWriter writer, LoggingEvent loggingEvent)
		{
			string exceptionString = loggingEvent.GetExceptionString();
			if (exceptionString != null && exceptionString.Length > 0) 
			{
				writer.WriteLine(exceptionString);
			}
		}
	}
}
