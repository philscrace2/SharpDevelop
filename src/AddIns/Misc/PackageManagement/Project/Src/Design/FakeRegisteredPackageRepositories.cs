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
using NuGet;

namespace ICSharpCode.PackageManagement.Design
{
	public class FakeRegisteredPackageRepositories : IRegisteredPackageRepositories
	{
		public FakeRegisteredPackageRepositories()
		{
			PackageSources = new RegisteredPackageSources(new PackageSource[0]);
		}

		public FakePackageRepository FakeActiveRepository = new FakePackageRepository();

		public virtual IPackageRepository ActiveRepository
		{
			get { return FakeActiveRepository; }
		}

		public FakePackageRepository FakeRecentPackageRepository = new FakePackageRepository();

		public virtual IRecentPackageRepository RecentPackageRepository
		{
			get { return FakeRecentPackageRepository; }
		}

		public bool HasMultiplePackageSources { get; set; }

		public PackageSource ActivePackageSource { get; set; }

		public RegisteredPackageSources PackageSources { get; set; }

		public FakePackageRepository FakePackageRepository = new FakePackageRepository();
		public PackageSource PackageSourcePassedToCreateRepository;

		public IPackageRepository CreateRepository(PackageSource source)
		{
			PackageSourcePassedToCreateRepository = source;
			return FakePackageRepository;
		}

		public FakePackageRepository FakeAggregateRepository = new FakePackageRepository();

		public IPackageRepository CreateAggregateRepository()
		{
			return FakeAggregateRepository;
		}

		public void ClearPackageSources()
		{
			PackageSources.Clear();
		}

		public PackageSource AddOnePackageSource()
		{
			return AddOnePackageSource("Test");
		}

		public PackageSource AddOnePackageSource(string name)
		{
			var source = new PackageSource("http://sharpdevelop.codeplex.com", name);
			PackageSources.Add(source);
			return source;
		}

		public void AddPackageSources(IEnumerable<PackageSource> sources)
		{
			PackageSources.AddRange(sources);
		}

		public FakePackage AddFakePackageWithVersionToActiveRepository(string version)
		{
			return AddFakePackageWithVersionToActiveRepository("Test", version);
		}

		public FakePackage AddFakePackageWithVersionToActiveRepository(string id, string version)
		{
			var package = FakePackage.CreatePackageWithVersion(id, version);
			FakeActiveRepository.FakePackages.Add(package);
			return package;
		}

		public FakePackage AddFakePackageWithVersionToAggregrateRepository(string id, string version)
		{
			var package = FakePackage.CreatePackageWithVersion(id, version);
			FakeAggregateRepository.FakePackages.Add(package);
			return package;
		}

		public void UpdatePackageSources(IEnumerable<PackageSource> updatedPackageSources)
		{
			throw new NotImplementedException();
		}
	}
}