﻿// Copyright (c) 2014 AlphaSierraPapa for the SharpDevelop Team
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this
// software and associated documentation files (the "Software"), to deal in the Software
// without restriction, including without limitation the rights to use, copy, modify, merge,
// publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons
// to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or
// substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR
// PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE
// FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
// OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.

using System;
using System.Runtime.Remoting.Messaging;
using ICSharpCode.Core;
using ICSharpCode.SharpDevelop;
using ICSharpCode.SharpDevelop.Gui;
using ICSharpCode.SharpDevelop.Project;

namespace ICSharpCode.TextTemplating
{
	public class TextTemplatingCustomToolContext : ITextTemplatingCustomToolContext
	{
		CustomToolContext context;

		public TextTemplatingCustomToolContext(CustomToolContext context)
		{
			this.context = context;
		}

		public FileProjectItem EnsureOutputFileIsInProject(FileProjectItem baseItem, string outputFileName)
		{
			return context.EnsureOutputFileIsInProject(baseItem, outputFileName);
		}

		public void ClearTasksExceptCommentTasks()
		{
			TaskService.ClearExceptCommentTasks();
		}

		public void AddTask(SDTask task)
		{
			TaskService.Add(task);
		}

		public void BringErrorsPadToFront()
		{
			SD.Workbench.GetPad(typeof(ErrorListPad)).BringPadToFront();
		}

		public void DebugLog(string message, Exception ex)
		{
			LoggingService.DebugFormatted("{0}\r\n{1}", message, ex.ToString());
		}

		public void SetLogicalCallContextData(string name, object data)
		{
			CallContext.LogicalSetData(name, data);
		}
	}
}