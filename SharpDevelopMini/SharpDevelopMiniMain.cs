using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Configuration;
using ICSharpCode.Core;
using ICSharpCode.SharpDevelopMini.Sda;
using ICSharpCode.SharpDevelopMini.Startup;

namespace SharpDevelopMini
{
	class SharpDevelopMiniMain
	{
		static void Main(string[] args)
		{
			Run();
		}

		private static void Run()
		{
			RunApplication();
		}

		private static void RunApplication()
		{
			LoggingService.Info("Starting Mini...");
			try
			{
				StartupSettings startup = new StartupSettings();
#if DEBUG
				//startup.UseSharpDevelopErrorHandler = UseExceptionBox;
#endif

				Assembly exe = typeof(SharpDevelopMiniMain).Assembly;
				startup.ApplicationRootPath = Path.Combine(Path.GetDirectoryName(exe.Location), "..");
				startup.AllowUserAddIns = true;

				string configDirectory = ConfigurationManager.AppSettings["settingsPath"];
				if (String.IsNullOrEmpty(configDirectory))
				{
					startup.ConfigDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
														   "ICSharpCode/SharpDevelop" + RevisionClass.Major);
				}
				else
				{
					startup.ConfigDirectory = Path.Combine(Path.GetDirectoryName(exe.Location), configDirectory);
				}

				startup.DomPersistencePath = ConfigurationManager.AppSettings["domPersistencePath"];
				if (string.IsNullOrEmpty(startup.DomPersistencePath))
				{
					startup.DomPersistencePath = Path.Combine(Path.GetTempPath(), "SharpDevelop" + RevisionClass.Major + "." + RevisionClass.Minor);
#if DEBUG
					startup.DomPersistencePath = Path.Combine(startup.DomPersistencePath, "Debug");
#endif
				}
				else if (startup.DomPersistencePath == "none")
				{
					startup.DomPersistencePath = null;
				}

				startup.AddAddInsFromDirectory(Path.Combine(startup.ApplicationRootPath, "AddIns"));

				// allows testing addins without having to install them
				//foreach (string parameter in SplashScreenForm.GetParameterList())
				//{
				//	if (parameter.StartsWith("addindir:", StringComparison.OrdinalIgnoreCase))
				//	{
				//		startup.AddAddInsFromDirectory(parameter.Substring(9));
				//	}
				//}

				SharpDevelopHost host = new SharpDevelopHost(AppDomain.CurrentDomain, startup);

				string[] fileList = SplashScreenForm.GetRequestedFileList();
				if (fileList.Length > 0)
				{
					if (LoadFilesInPreviousInstance(fileList))
					{
						LoggingService.Info("Aborting startup, arguments will be handled by previous instance");
						return;
					}
				}

				//host.BeforeRunWorkbench += delegate {
				//	if (SplashScreenForm.SplashScreen != null)
				//	{
				//		SplashScreenForm.SplashScreen.BeginInvoke(new MethodInvoker(SplashScreenForm.SplashScreen.Dispose));
				//		SplashScreenForm.SplashScreen = null;
				//	}
				//};

				WorkbenchSettings workbenchSettings = new WorkbenchSettings();
				workbenchSettings.RunOnNewThread = false;
				for (int i = 0; i < fileList.Length; i++)
				{
					workbenchSettings.InitialFileList.Add(fileList[i]);
				}
				//SDTraceListener.Install();
				host.RunWorkbench(workbenchSettings);
			}
			finally
			{
				LoggingService.Info("Leaving RunApplication()");
			}
		}
	}
}
