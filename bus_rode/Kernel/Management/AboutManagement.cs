using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bus_rode.Kernel.Management {
    /// <summary>
    /// 关于界面
    /// </summary>
    public class AboutManagement {

        /// <summary>
        /// 描述性文字
        /// </summary>
        private string descriptionWords;
        /// <summary>
        /// 描述性文字
        /// </summary>
        public string DescriptionWords { get { return descriptionWords; } }

        /// <summary>
        /// github的地址
        /// </summary>
        private string githubPath;
        /// <summary>
        /// github的地址
        /// </summary>
        public string GithubPath { get { return githubPath; } }

        public AboutManagement() { 
            descriptionWords = "Programmer : Tad Wiliam" + Environment.NewLine +
"Insider : Nothing" + Environment.NewLine +
"Previous insider : Tianyue Sun" + Environment.NewLine +
"Provider of feedback : Xianlei Bian，Yi Gao，Zechen Li，Junzhe Jiang" + Environment.NewLine +
"Version : " + Kernel.Tools.ApplicationInformation.AppVersion + " " + Kernel.Tools.ApplicationInformation.AppBuild + Environment.NewLine +
"Last update date : " + Kernel.Tools.ApplicationInformation.AppUpdateDate + Environment.NewLine +
Environment.NewLine +
"User environment" + Environment.NewLine +
".NET Framework version : " + Environment.Version.ToString() + Environment.NewLine +
"Sign up user : " + Environment.UserName.ToString() + Environment.NewLine +
"OS version : " + Environment.OSVersion.ToString() + Environment.NewLine +
Environment.NewLine +
"CHMOSGroup Copyright 2012-" + DateTime.Today.Year.ToString();
            
            githubPath = "https://github.com/yyc12345/bus_rode_all";
        }

    }
}
