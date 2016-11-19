using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using bus_rode.Kernel.Tools;
using System.Globalization;
using System.Windows;

namespace bus_rode.Kernel.Tools {

    /// <summary>
    /// 内核语言管理类
    /// </summary>
    public class Language {

        /// <summary>
        /// 获取指定键的值，没有返回空
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public static string GetItem(string key) {

            var keyResult = from item in Application.Current.MainWindow.Resources.Keys.OfType<string>()
                            where item == key
                            select item;

            if (keyResult.Any() == false) {
                //nothing in this collection
                return "";
            }

            var cache = Application.Current.MainWindow.Resources[keyResult.ToList()[0]];
            if (cache != null) { return cache.ToString(); } else { return ""; }

        }

        /// <summary>
        /// 获取指定键的值，并按给定内容填充缺省部分，没有返回空
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public static string GetItem(string key, StringGroup replacedWordsList) {

            var cache = GetItem(key);
            if (cache == "") return "";

            string[] replaceWordsListGroup = replacedWordsList.ToStringGroup();
            if (replaceWordsListGroup.Length == 0) { return cache; } else {
                for (int a = 0; a < replaceWordsListGroup.Length; a++) {
                    if (cache.IndexOf("{" + a.ToString() + "}") <= -1) { break; } else {
                        cache.Replace("{" + a.ToString() + "}", replaceWordsListGroup[a]);
                    }
                }
            }
            return cache;

        }

        /// <summary>
        /// 加载当前语言资源，相当于刷新
        /// </summary>
        public static void LoadResources(CultureInfo languageSign) {
            //获取
            string sign = languageSign.Name;

            //加载
            ResourceDictionary langResources = null;
            if (System.IO.File.Exists(Kernel.Tools.SystemInformation.WorkingPath + @"\language\" + sign + ".xaml") == true) {
                try {
                    langResources = (ResourceDictionary)Application.LoadComponent(new Uri(@"language\" + sign + ".xaml", UriKind.Relative));
                } catch (Exception) { }
            }

            if (languageSign == null) {
                //加载en-us
                langResources = (ResourceDictionary)Application.LoadComponent(new Uri(@"language\en-US.xaml", UriKind.Relative));
            }

            ////读取
            //Application.Current.MainWindow.Resources.MergedDictionaries.Clear();
            //Application.Current.MainWindow.Resources.MergedDictionaries.Add(langResources);

            ////读取到字典
            //LanguageDictionary.Clear();
            //var cache = from item in Application.Current.MainWindow.Resources.Keys.OfType<string>()
            //            where (item.Substring(0, 8) == "langCode") || (item.Substring(0, 8) == "langUI")
            //            select item;

            //foreach (string item in cache) {
            //    langResources.Add(item, Application.Current.MainWindow.Resources[item]);
            //}


        }

    }


}
