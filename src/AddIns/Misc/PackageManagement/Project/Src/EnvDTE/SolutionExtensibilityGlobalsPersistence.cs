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
using SD = ICSharpCode.SharpDevelop.Project;

namespace ICSharpCode.PackageManagement.EnvDTE
{
	public class SolutionExtensibilityGlobalsPersistence
	{
		SolutionExtensibilityGlobals globals;

		public SolutionExtensibilityGlobalsPersistence(SolutionExtensibilityGlobals globals)
		{
			this.globals = globals;
		}

		/// <summary>
		/// Returns true if the item exists in the solution.
		/// Returns false if the item exists but not in the solution.
		/// Otherwise throws an exception.
		/// </summary>
		public bool this[string name]
		{
			get
			{
				if (globals.ItemExistsInSolution(name))
				{
					return true;
				}
				else if (globals.ItemExists(name))
				{
					return false;
				}

				throw new ArgumentException("Variable not found.", name);
			}
			set
			{
				if (value)
				{
					globals.AddItemToSolution(name);
				}
				else
				{
					globals.RemoveItemFromSolution(name);
				}
			}
		}
	}
}