using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace bus_rode.Kernel.Management {
    /// <summary>
    /// 设置管理器
    /// </summary>
    public class SettingManagement {

        /* 需要写入配置的设置
         * 讲述人，自动翻译，自动暗色主题，语言（写入RFC 4646 标识符），车辆跟踪，背景暗化度，主配色（写英文颜色字符串表达），副配色（写英文颜色字符串表达）
         * 仅需读取和启动时预设的属性
         * 所在城市（从资源包中取得，没有的话写入""）
         */

        #region 保存的配置

        /// <summary>
        /// 是否获取远程数据
        /// </summary>
        private bool getMonitorData;
        /// <summary>
        /// 是否获取远程数据
        /// </summary>
        public bool GetMonitorData { get { return getMonitorData; } set { getMonitorData = value; SaveSettings(); } }

        /// <summary>
        /// 是否语音
        /// </summary>
        private bool textToSpeech;
        /// <summary>
        /// 是否语音
        /// </summary>
        public bool TextToSpeech { get { return textToSpeech; } set { textToSpeech = value; SaveSettings(); } }

        /// <summary>
        /// 自动翻译
        /// </summary>
        private bool autoTranslate;
        /// <summary>
        /// 自动翻译
        /// </summary>
        public bool AutoTranslate { get { return autoTranslate; } set { autoTranslate = value; SaveSettings(); } }

        /// <summary>
        /// 自动暗色
        /// </summary>
        private bool autoDarkTheme;
        /// <summary>
        /// 自动暗色
        /// </summary>
        public bool AutoDarkTheme { get { return autoDarkTheme; } set { autoDarkTheme = value; SaveSettings(); } }

        /// <summary>
        /// 选择的语言
        /// </summary>
        private string selectedLanguage;
        /// <summary>
        /// 选择的语言
        /// </summary>
        public string SelectedLanguage { get { return selectedLanguage; } set { selectedLanguage = value; SaveSettings(); } }

        /// <summary>
        /// 背景暗化
        /// </summary>
        private int backgroundDarkness;
        /// <summary>
        /// 背景暗化
        /// </summary>
        public int DackgroundDarkness { get { return backgroundDarkness; } set { backgroundDarkness = value; SaveSettings(); } }

        /// <summary>
        /// 主配色
        /// </summary>
        private string primaryColor;
        /// <summary>
        /// 主配色
        /// </summary>
        public string PrimaryColor { get { return primaryColor; } set { primaryColor = value; SaveSettings(); } }

        /// <summary>
        /// 副配色
        /// </summary>
        private string accentColor;
        /// <summary>
        /// 副配色
        /// </summary>
        public string AccentColor { get { return accentColor; } set { accentColor = value; SaveSettings(); } }

        #endregion

        #region 启动时设置的设置

        /// <summary>
        /// 所在城市
        /// </summary>
        private string hostLocation;
        /// <summary>
        /// 所在城市
        /// </summary>
        public string HostLocation { get { return hostLocation; } set { hostLocation = value; } }

        #endregion

        public SettingManagement() {
            ReadSettings();
        }

        /// <summary>
        /// 读取设置
        /// </summary>
        public void ReadSettings() {

            //读取设置
            var cache = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            //初始化
            var cache4 = cache.AppSettings.Settings;
            try {
                //尝试读取
                getMonitorData = System.Convert.ToBoolean(cache4["getMonitorData"].Value);
                textToSpeech = System.Convert.ToBoolean(cache4["textToSpeech"].Value);
                autoTranslate = System.Convert.ToBoolean(cache4["autoTranslate"].Value);
                selectedLanguage = cache4["selectedLanguage"].Value;
                backgroundDarkness = System.Convert.ToInt32(cache4["backgroundDarkness"].Value);
                autoDarkTheme = System.Convert.ToBoolean(cache4["autoDarkTheme"].Value);
                primaryColor = cache4["primaryColor"].Value;
                accentColor = cache4["accentColor"].Value;

            } catch {
                //没有，全部重写
                getMonitorData = false;
                textToSpeech = false;
                autoTranslate = false;
                if (Kernel.Tools.SystemInformation.SupportedLanguages.Contains(System.Globalization.CultureInfo.InstalledUICulture.Name))
                    selectedLanguage = System.Globalization.CultureInfo.InstalledUICulture.Name;
                else selectedLanguage = "en-US";
                backgroundDarkness = 0;
                autoDarkTheme = true;
                primaryColor = "Indigo";
                accentColor = "Lime";

                cache4.Clear();
                cache4.Add("getMonitorData", getMonitorData.ToString());
                cache4.Add("textToSpeech", textToSpeech.ToString());
                cache4.Add("autoTranslate", autoTranslate.ToString());
                cache4.Add("selectedLanguage", selectedLanguage);
                cache4.Add("backgroundDarkness", backgroundDarkness.ToString());
                cache4.Add("autoDarkTheme", autoDarkTheme.ToString());
                cache4.Add("primaryColor", primaryColor);
                cache4.Add("accentColor", accentColor);
            }


        }

        /// <summary>
        /// 保存设置
        /// </summary>
        public void SaveSettings() {
            var config = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var cache = config.AppSettings.Settings;
            cache["getMonitorData"].Value = getMonitorData.ToString();
            cache["textToSpeech"].Value = textToSpeech.ToString();
            cache["autoTranslate"].Value = autoTranslate.ToString();
            cache["selectedLanguage"].Value = selectedLanguage;
            cache["backgroundDarkness"].Value = backgroundDarkness.ToString();
            cache["autoDarkTheme"].Value = autoDarkTheme.ToString();
            cache["primaryColor"].Value = primaryColor;
            cache["accentColor"].Value = accentColor;

            config.Save(ConfigurationSaveMode.Modified);
        }

    }
}
