using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bus_rode.Kernel.Tools {
    public class ApplicationInformation {
        /// <summary>
        /// 应用版本号
        /// </summary>
        public const string AppVersion = "9.0.0.0";

        /// <summary>
        /// 应用编译版本号
        /// </summary>
        public const string AppBuild = "build 9000";

        /// <summary>
        /// 应用名
        /// </summary>
        public const string AppName = "bus_rode Helium";

        /// <summary>
        /// 应用标题显示名称
        /// </summary>
        public const string AppTitleName = "bus_rode Helium";

        /// <summary>
        /// 应用最后更新时间
        /// </summary>
        public const string AppUpdateDate = "May 14, 2018";

        /// <summary>
        /// 应用编译版本数字
        /// </summary>
        public const int AppBuildNumber = 9000;

    }

    public class SystemInformation {

        /// <summary>
        /// 屏幕长
        /// </summary>
        public static double ScreenWidth { get { return System.Windows.SystemParameters.PrimaryScreenWidth; } }
        /// <summary>
        /// 屏幕高
        /// </summary>
        public static double ScreenHeight { get { return System.Windows.SystemParameters.PrimaryScreenHeight; } }
        /// <summary>
        /// 工作路径
        /// </summary>
        public static string WorkingPath {
            get {
                return Environment.CurrentDirectory[Environment.CurrentDirectory.Length - 1] == '\\' ?
Environment.CurrentDirectory.Substring(0, Environment.CurrentDirectory.Length - 1) :
Environment.CurrentDirectory;
            }
        }
        /// <summary>
        /// 支持的语言（RFC 4646 标识符）
        /// </summary>
        public static string SupportedLanguages { get { return "en-US,zh-CN,ja-JP,de-DE,ru-RU"; } }
    }
}
