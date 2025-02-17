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
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using ICSharpCode.Core;
using NuGet;

namespace ICSharpCode.PackageManagement
{
	public class PackageManagementOptions
	{
		const string PackageDirectoryPropertyName = "PackagesDirectory";
		const string RecentPackagesPropertyName = "RecentPackages";

		RegisteredPackageSourceSettings registeredPackageSourceSettings;
		Properties properties;
		ObservableCollection<RecentPackageInfo> recentPackages;
		PackageRestoreConsent packageRestoreConsent;

		public PackageManagementOptions(
			Properties properties,
			ISettingsProvider settingsProvider)
		{
			this.properties = properties;
			registeredPackageSourceSettings = new RegisteredPackageSourceSettings(settingsProvider);
			packageRestoreConsent = new PackageRestoreConsent(settingsProvider.LoadSettings());
		}

		public PackageManagementOptions(Properties properties)
			: this(properties, new SettingsProvider())
		{
		}

		public PackageManagementOptions()
			: this(PropertyService.NestedProperties("PackageManagementSettings"))
		{
		}

		public bool IsPackageRestoreEnabled
		{
			get { return packageRestoreConsent.IsGrantedInSettings; }
			set { packageRestoreConsent.IsGrantedInSettings = value; }
		}

		public RegisteredPackageSources PackageSources
		{
			get { return registeredPackageSourceSettings.PackageSources; }
		}

		public string PackagesDirectory
		{
			get { return properties.Get(PackageDirectoryPropertyName, "packages"); }
			set { properties.Set(PackageDirectoryPropertyName, value); }
		}

		public PackageSource ActivePackageSource
		{
			get { return registeredPackageSourceSettings.ActivePackageSource; }
			set { registeredPackageSourceSettings.ActivePackageSource = value; }
		}

		public IList<RecentPackageInfo> RecentPackages
		{
			get
			{
				if (recentPackages == null)
				{
					ReadRecentPackages();
				}

				return recentPackages;
			}
		}

		void ReadRecentPackages()
		{
			IReadOnlyList<RecentPackageInfo> savedRecentPackages =
				properties.GetList<RecentPackageInfo>(RecentPackagesPropertyName);

			recentPackages = new ObservableCollection<RecentPackageInfo>();
			recentPackages.AddRange(savedRecentPackages);
			recentPackages.CollectionChanged += RecentPackagesCollectionChanged;
		}

		void RecentPackagesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			properties.SetList(RecentPackagesPropertyName, recentPackages);
		}

		public string GetCustomPackagesDirectory()
		{
			return registeredPackageSourceSettings.Settings.GetRepositoryPath();
		}
	}
}