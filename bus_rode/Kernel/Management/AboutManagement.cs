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
        /// 开发者反馈错误说明
        /// </summary>
        private string developerIssueDescriptionWords;
        /// <summary>
        /// 开发者反馈错误说明
        /// </summary>
        public string DeveloperIssueDescriptionWords { get { return developerIssueDescriptionWords; } }
        /// <summary>
        /// 客户反馈错误说明
        /// </summary>
        private string customerIssueDescriptionWords;
        /// <summary>
        /// 客户反馈错误说明
        /// </summary>
        public string CustomerIssueDescriptionWords { get { return customerIssueDescriptionWords; } }

        /// <summary>
        /// 开源说明
        /// </summary>
        private string openSourceDescriptionWords;
        /// <summary>
        /// 开源说明
        /// </summary>
        public string OpenSourceDescriptionWords { get { return openSourceDescriptionWords; } }


        public AboutManagement() { 
            descriptionWords = "Programmer : Tad Wiliam" + Environment.NewLine +
"Insider : Nothing" + Environment.NewLine +
"Previous insider : Tianyue Sun" + Environment.NewLine +
"Responder : Xianlei Bian，Yi Gao，Zechen Li，Junzhe Jiang" + Environment.NewLine +
"Version : " + Kernel.Tools.ApplicationInformation.AppVersion + " " + Kernel.Tools.ApplicationInformation.AppBuild + Environment.NewLine +
"Last update date : " + Kernel.Tools.ApplicationInformation.AppUpdateDate + Environment.NewLine +
Environment.NewLine +
"User environment" + Environment.NewLine +
".NET Framework version : " + Environment.Version.ToString() + Environment.NewLine +
"Sign up user : " + Environment.UserName.ToString() + Environment.NewLine +
"OS version : " + Environment.OSVersion.ToString() + Environment.NewLine +
Environment.NewLine +
"CHMOSGroup Copyright 2012-" + DateTime.Today.Year.ToString();

            openSourceDescriptionWords = "This is a open source project. You can see and download all code on github. Project's link is https://github.com/yyc12345/bus_rode";

            developerIssueDescriptionWords = "If you are a developer and you find some bugs or advice which need feed back to CHMOSGroup. You can visit the github page and create a new issue. This is a absolute and unique way to tell CHMOSGroup the existence of bugs and advice.";
            customerIssueDescriptionWords = @"If you are a customer or bus_rode fan and you have some bugs or advice. You can broadcast a tweet including @yyc12321, or you can send a email to yyc12321@outlook.com to tell CHMOSGroup the words what you want to say.";

        }

    }
}
