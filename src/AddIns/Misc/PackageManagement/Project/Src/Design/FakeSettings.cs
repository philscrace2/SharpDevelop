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
using NuGet;

namespace ICSharpCode.PackageManagement.Design
{
	public class FakeSettings : ISettings
	{
		public List<SettingValue> PackageSources
			= new List<SettingValue>();

		public List<SettingValue> DisabledPackageSources
			= new List<SettingValue>();

		public List<SettingValue> ActivePackageSourceSettings =
			new List<SettingValue>();

		public Dictionary<string, IList<SettingValue>> Sections
			= new Dictionary<string, IList<SettingValue>>();

		public const string ConfigSectionName = "config";

		public FakeSettings()
		{
			Sections.Add(RegisteredPackageSourceSettings.PackageSourcesSectionName, PackageSources);
			Sections.Add(RegisteredPackageSourceSettings.ActivePackageSourceSectionName, ActivePackageSourceSettings);
			Sections.Add(RegisteredPackageSourceSettings.DisabledPackageSourceSectionName, DisabledPackageSources);
		}

		public string GetValue(string section, string key, bool isPath)
		{
			if (Sections.ContainsKey(section))
			{
				var matchedSection = Sections[section];
				return matchedSection.FirstOrDefault(item => item.Key == key).Value;
			}

			return null;
		}

		public IList<SettingValue> GetValues(string section, bool isPath)
		{
			return Sections[section];
		}

		public void AddFakePackageSource(PackageSource packageSource)
		{
			var setting = new SettingValue(packageSource.Name, packageSource.Source, false);
			PackageSources.Add(setting);
		}

		public Dictionary<string, SettingValue> SavedSectionValues =
			new Dictionary<string, SettingValue>();

		public void SetValue(string section, string key, string value)
		{
			SavedSectionValues.Remove(section);
			SavedSectionValues.Add(section, new SettingValue(key, value, false));
		}

		public SettingValue GetValuePassedToSetValueForActivePackageSourceSection()
		{
			return SavedSectionValues[RegisteredPackageSourceSettings.ActivePackageSourceSectionName];
		}

		public void SetValues(string section, IList<SettingValue> values)
		{
			SavedSectionValueLists.Remove(section);
			SavedSectionValueLists.Add(section, values);
		}

		public Dictionary<string, IList<SettingValue>> SavedSectionValueLists
			= new Dictionary<string, IList<SettingValue>>();

		public IList<SettingValue> GetValuesPassedToSetValuesForPackageSourcesSection()
		{
			return SavedSectionValueLists[RegisteredPackageSourceSettings.PackageSourcesSectionName];
		}

		public virtual bool DeleteValue(string section, string key)
		{
			throw new NotImplementedException();
		}

		public List<string> SectionsDeleted = new List<string>();

		public virtual bool DeleteSection(string section)
		{
			SectionsDeleted.Add(section);
			return true;
		}

		public bool IsPackageSourcesSectionDeleted
		{
			get { return SectionsDeleted.Contains(RegisteredPackageSourceSettings.PackageSourcesSectionName); }
		}

		public bool IsDisabledPackageSourcesSectionDeleted
		{
			get { return SectionsDeleted.Contains(RegisteredPackageSourceSettings.DisabledPackageSourceSectionName); }
		}

		public bool IsActivePackageSourceSectionDeleted
		{
			get { return SectionsDeleted.Contains(RegisteredPackageSourceSettings.ActivePackageSourceSectionName); }
		}

		public void SetFakeActivePackageSource(PackageSource packageSource)
		{
			ActivePackageSourceSettings.Clear();
			var setting = new SettingValue(packageSource.Name, packageSource.Source, false);
			ActivePackageSourceSettings.Add(setting);
		}

		public void MakeActivePackageSourceSectionNull()
		{
			Sections.Remove(RegisteredPackageSourceSettings.ActivePackageSourceSectionName);
			Sections.Add(RegisteredPackageSourceSettings.ActivePackageSourceSectionName, null);
		}

		public void MakePackageSourceSectionsNull()
		{
			Sections.Remove(RegisteredPackageSourceSettings.PackageSourcesSectionName);
			Sections.Add(RegisteredPackageSourceSettings.PackageSourcesSectionName, null);
		}

		public void AddFakePackageSources(IEnumerable<PackageSource> packageSources)
		{
			foreach (PackageSource packageSource in packageSources)
			{
				AddFakePackageSource(packageSource);
			}
		}

		public IList<SettingValue> GetNestedValues(string section, string key)
		{
			return new List<SettingValue>();
		}

		public virtual void SetNestedValues(string section, string key, IList<KeyValuePair<string, string>> values)
		{
			throw new NotImplementedException();
		}

		public void AddDisabledPackageSource(PackageSource packageSource)
		{
			var setting = new SettingValue(packageSource.Name, packageSource.Source, false);
			DisabledPackageSources.Add(setting);
		}

		public IList<SettingValue> GetValuesPassedToSetValuesForDisabledPackageSourcesSection()
		{
			return SavedSectionValueLists[RegisteredPackageSourceSettings.DisabledPackageSourceSectionName];
		}

		public bool AnyValuesPassedToSetValuesForDisabledPackageSourcesSection
		{
			get
			{
				return SavedSectionValueLists.ContainsKey(RegisteredPackageSourceSettings
					.DisabledPackageSourceSectionName);
			}
		}

		public virtual void SetPackageRestoreSetting(bool enabled)
		{
			var items = new List<SettingValue>();
			items.Add(new SettingValue("enabled", enabled.ToString(), false));
			Sections.Add("packageRestore", items);
		}

		public SettingValue GetValuePassedToSetValueForPackageRestoreSection()
		{
			return SavedSectionValues["packageRestore"];
		}

		public bool IsPackageRestoreSectionDeleted
		{
			get { return SectionsDeleted.Contains("packageRestore"); }
		}

		public void SetRepositoryPathSetting(string fullPath)
		{
			var items = new List<SettingValue>();
			items.Add(new SettingValue("repositoryPath", fullPath, false));
			Sections.Add(ConfigSectionName, items);
		}

		public Dictionary<string, IList<SettingValue>> SectionsUpdated =
			new Dictionary<string, IList<SettingValue>>();

		public void UpdateSections(string section, IList<SettingValue> values)
		{
			SectionsUpdated.Remove(section);
			SectionsUpdated.Add(section, values);
			SetValues(section, values);
		}
	}
}