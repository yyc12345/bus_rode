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

        /// <summary>
        /// 是否获取远程数据
        /// </summary>
        private bool getRemoteData;
        /// <summary>
        /// 是否获取远程数据
        /// </summary>
        public bool GetRemoteData { get { return getRemoteData; } set { getRemoteData = value; SaveSettings(); } }

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
            bool cache1 = false, cache2 = false, cache3 = false;
            var cache4 = cache.AppSettings.Settings;
            try {
                //尝试读取
                cache1 = System.Convert.ToBoolean(cache4["getRemoteData"].ToString());
                cache2 = System.Convert.ToBoolean(cache4["textToSpeech"].ToString());
                cache3 = System.Convert.ToBoolean(cache4["autoTranslate"].ToString());

                getRemoteData = cache1;
                textToSpeech = cache2;
                autoTranslate = cache3;
            } catch {
                //没有，全部重写
                cache4.Add("getRemoteData", false.ToString());
                cache4.Add("textToSpeech", false.ToString());
                cache4.Add("autoTranslate", false.ToString());
                cache.Save(ConfigurationSaveMode.Modified);

                getRemoteData = false;
                textToSpeech = false;
                autoTranslate = false;
            }


        }

        /// <summary>
        /// 保存设置
        /// </summary>
        public void SaveSettings() {
            var config = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var cache = config.AppSettings.Settings;
            cache["getRemoteData"].Value = getRemoteData.ToString();
            cache["textToSpeech"].Value = textToSpeech.ToString();
            cache["autoTranslate"].Value = autoTranslate.ToString();
            config.Save(ConfigurationSaveMode.Modified);
        }

    }
}
