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
using System.Collections.Generic;
using System.Linq;

namespace ICSharpCode.PackageManagement.EnvDTE
{
	public class ProjectItemPropertyFactory : IPropertyFactory
	{
		ProjectItem projectItem;

		public ProjectItemPropertyFactory(ProjectItem projectItem)
		{
			this.projectItem = projectItem;
		}

		public Property CreateProperty(string name)
		{
			return new ProjectItemProperty(projectItem, name);
		}

		public IEnumerator<Property> GetEnumerator()
		{
			List<Property> properties = GetProperties().ToList();
			return properties.GetEnumerator();
		}

		IEnumerable<Property> GetProperties()
		{
			yield return new ProjectItemProperty(projectItem, ProjectItem.CopyToOutputDirectoryPropertyName);
			yield return new ProjectItemProperty(projectItem, ProjectItem.CustomToolPropertyName);
			yield return new ProjectItemProperty(projectItem, ProjectItem.FullPathPropertyName);
			yield return new ProjectItemProperty(projectItem, ProjectItem.LocalPathPropertyName);
		}
	}
}