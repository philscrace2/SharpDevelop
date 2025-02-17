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
using System.IO;
using System.Linq;
using System.Runtime.Versioning;
using NuGet;

namespace ICSharpCode.PackageManagement.Scripting
{
	public abstract class PackageScriptFileName : IPackageScriptFileName
	{
		IFileSystem fileSystem;
		string relativeScriptFilePath;

		public PackageScriptFileName(string packageInstallDirectory)
			: this(new PhysicalFileSystem(packageInstallDirectory))
		{
		}

		public PackageScriptFileName(IFileSystem fileSystem)
		{
			this.fileSystem = fileSystem;
			GetRelativeScriptFilePath();
		}

		void GetRelativeScriptFilePath()
		{
			relativeScriptFilePath = Path.Combine("tools", Name);
		}

		public abstract string Name { get; }

		public string PackageInstallDirectory
		{
			get { return fileSystem.Root; }
		}

		public override string ToString()
		{
			return fileSystem.GetFullPath(relativeScriptFilePath);
		}

		public bool ScriptDirectoryExists()
		{
			return fileSystem.DirectoryExists("tools");
		}

		public bool FileExists()
		{
			return fileSystem.FileExists(relativeScriptFilePath);
		}

		public string GetScriptDirectory()
		{
			return fileSystem.GetFullPath("tools");
		}

		public void UseTargetSpecificFileName(IPackage package, FrameworkName targetFramework)
		{
			IEnumerable<IPackageFile> files;
			if (VersionUtility.TryGetCompatibleItems(targetFramework, package.GetToolFiles(), out files))
			{
				IPackageFile matchingScriptFile = files.FirstOrDefault(file =>
					file.EffectivePath.Equals(Name, StringComparison.OrdinalIgnoreCase));
				if (matchingScriptFile != null)
				{
					relativeScriptFilePath = matchingScriptFile.Path;
				}
			}
		}
	}
}