using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Diagnostics;


namespace bus_rode {
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application {

        /// <summary>
        /// Application Entry Point.
        /// </summary>
        [System.STAThreadAttribute()]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public static void Main() {

#if !DEBUG
            //binding exception processor
            //ui
            Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;
            //thread
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
#endif

            bus_rode.App app = new bus_rode.App();
            app.InitializeComponent();
            app.Run();
        }


#if !DEBUG
        //thread
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e) {
            WriteExceptionAndRestart(new Exception("the main thread(ui thread) have a unhandled exception, the object " + e.ExceptionObject.ToString() + " invoke this exception."));
        }
        //ui
        private static void Current_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e) {
            WriteExceptionAndRestart(e.Exception);
        }

        /// <summary>
        /// write exception and exit
        /// </summary>
        /// <param name="e"></param>
        private static void WriteExceptionAndRestart(Exception e) {
            System.IO.File.Delete(bus_rode.Kernel.Tools.SystemInformation.WorkingPath + @"\Error.log");
            var file = new System.IO.StreamWriter(bus_rode.Kernel.Tools.SystemInformation.WorkingPath + @"\Error.log",false,System.Text.Encoding.UTF8);

            file.WriteLine("bus_rode bus's report");
            file.WriteLine("---------system---------");
            file.WriteLine("date of write:" + System.DateTime.Now.ToString());
            file.WriteLine(Environment.OSVersion.ToString());
            file.WriteLine(Environment.SystemPageSize);

            file.WriteLine("---------bus_rode---------");
            file.WriteLine(Kernel.Tools.ApplicationInformation.AppTitleName);
            file.WriteLine(Kernel.Tools.ApplicationInformation.AppName);
            file.WriteLine(Kernel.Tools.ApplicationInformation.AppBuild);
            file.WriteLine(Kernel.Tools.ApplicationInformation.AppVersion);
            file.WriteLine(Kernel.Tools.ApplicationInformation.AppUpdateDate);

            file.WriteLine("---------bug detail---------");
            file.WriteLine(e.Message);
            file.WriteLine(e.Source);
            file.WriteLine(e.StackTrace);

            file.Dispose();

            //start and restart
            Process.Start(bus_rode.Kernel.Tools.SystemInformation.WorkingPath + @"\bus_rodeException.exe");
            Environment.Exit(0);
        }
#endif

    }
}
