using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using bus_rode.Kernel.Tools;

//ui界面要用的转换器
namespace bus_rode.UserInterface.Converter {

    /// <summary>
    /// 辅助函数
    /// </summary>
    public class Assistance {

        /*标准协议：
         * 请以如下形式命名原始字符串残缺部分： {index}
         * 
         * index is int
         * index 从0起始
         * 
         * 与填充列表的index对应填充
         * 
         */

        /// <summary>
        /// 用字符列表填充字符串中的残缺部分-只有1项
        /// </summary>
        /// <param name="originalStr">要填充的字符串</param>
        /// <param name="blockStr">填充字符</param>
        /// <returns></returns>
        public static string FillString(string originalStr, string blockStr) {
            return originalStr.Replace("{0}", blockStr);
        }

        /// <summary>
        /// 用字符列表填充字符串中的残缺部分
        /// </summary>
        /// <param name="originalStr">要填充的字符串</param>
        /// <param name="blockList">填充字符列表</param>
        /// <returns></returns>
        public static string FillStringByList(string originalStr, List<string> blockList) {

            if (blockList.Count == 0) { return originalStr; }

            int index = 0;
            foreach (string item in blockList) {
                originalStr.Replace("{" + index.ToString() + "}", item);
                index++;
            }

            return originalStr;
        }

        /// <summary>
        /// 用字符列表填充字符串中的残缺部分
        /// </summary>
        /// <param name="originalStr">要填充的字符串</param>
        /// <param name="blockList">填充字符列表</param>
        /// <returns></returns>
        public static string FillStringByList(string originalStr, StringGroup blockList) {

            var cache = blockList.ToStringGroup();

            if (cache.Length == 0) { return originalStr; }

            int index = 0;
            foreach (string item in cache) {
                originalStr.Replace("{" + index.ToString() + "}", item);
                index++;
            }

            return originalStr;
        }

    }

    /// <summary>
    /// Page中间列表的绑定
    /// </summary>
    [ValueConversion(typeof(double), typeof(double))]
    public class MiddleListWidthConverter : IValueConverter {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if (value == null) { return 0; }

            //计算屏幕的二分之一
            var maxWidth = bus_rode.Kernel.Tools.SystemInformation.ScreenWidth / 2;

            //保证400为最小值，为移动端服务
            if (maxWidth > 400) {
                //可以
            } else {
                //还是用400
                maxWidth = 400;
            }

            //最大值限定
            double containerWidth = System.Convert.ToDouble(value);
            if (containerWidth >= maxWidth) {
                return maxWidth;
            } else { return containerWidth; }

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return null;
        }

    }

    /// <summary>
    /// 可滑动界面的透明值
    /// </summary>
    [ValueConversion(typeof(double),typeof(double))]
    public class SliderMessageGridOpacity : IValueConverter {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if (value == null) { return 0; }

            //获取滚动条的区域，除3以达到缓动的目的
            double scrollHeight = System.Convert.ToDouble(value) / 3;

            //获取还剩余的
            double resultHeight = 150 - scrollHeight;
            if (resultHeight < 0) {
                //过了
                return 0;
            }else {
                //还可以，返回占的百分比即可
                return resultHeight / 150;
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return null;
        }
    }

    /// <summary>
    /// 可滑动界面的高度
    /// </summary>
    [ValueConversion(typeof(double), typeof(double))]
    public class SliderMessageGridHeight : IValueConverter {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if (value == null) { return 0; }

            //获取滚动条的区域，除3以达到缓动的目的
            double scrollHeight = System.Convert.ToDouble(value) / 3;

            //获取还剩余的
            double resultHeight = 150 - scrollHeight;
            if (resultHeight < 0) {
                //过了
                return 0;
            } else {
                //还可以，返回即可
                return resultHeight;
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return null;
        }
    }


    /// <summary>
    /// SettingPageSliderItem滑动条的绑定转换器
    /// </summary>
    [ValueConversion(typeof(string), typeof(string))]
    public class SliderValueAddWordsConverter : IValueConverter {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if (value == null) { return ""; }

            string valueCache = System.Convert.ToDouble(value).ToString();
            string paramCache = System.Convert.ToString(parameter);

            return Assistance.FillString(paramCache, valueCache);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return null;
        }

    }

    /// <summary>
    /// SettingPageItem标题上标题与控件水平居中的转换器
    /// </summary>
    [ValueConversion(typeof(double), typeof(System.Windows.Thickness))]
    public class SettingsPageItemControlMargin : IMultiValueConverter {

        public object Convert(object[] values, Type targetTypes, object parameter, System.Globalization.CultureInfo culture) {
            //获取标题高
            double headHeight = System.Convert.ToDouble(values[0]);
            //获取控件高
            double controlHeight = System.Convert.ToDouble(values[1]);

            //返回
            return new Thickness(16, 16 + (headHeight / 2) - (controlHeight / 2), 16, 0);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture) {
            return null;
        }
    }


    #region 加减乘除的转换器

    /// <summary>
    /// 加法转换器，加数输入在values和param均可，param使用sg，分隔符,
    /// </summary>
    [ValueConversion(typeof(double), typeof(double))]
    public class AdditionConverter : IMultiValueConverter {

        public object Convert(object[] values, Type targetTypes, object parameter, System.Globalization.CultureInfo culture) {
            double result = 0;

            foreach(var item in values) {
                result += System.Convert.ToDouble(item);
            }

            //获取参数
            StringGroup paramValues = new StringGroup(System.Convert.ToString(parameter), ",");
            if (paramValues.ToString() != "") {
                foreach(var item in paramValues.ToStringGroup()) {
                    result += double.Parse(item);
                }
            }

            //返回
            return result;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture) {
            return null;
        }
    }

    /// <summary>
    /// 减法转换器，被减数输入在第一个，减数输入在第二个以后，可输入多个。减数数据在values和param均可，param使用sg，分隔符,
    /// </summary>
    [ValueConversion(typeof(double), typeof(double))]
    public class SubtractionConverter : IMultiValueConverter {

        public object Convert(object[] values, Type targetTypes, object parameter, System.Globalization.CultureInfo culture) {
            double result = System.Convert.ToDouble(values[0]);

            bool first = true;
            foreach (var item in values) {
                //去掉第一个
                if (first) { first = false; continue; }
                result -= System.Convert.ToDouble(item);
            }

            //获取参数
            StringGroup paramValues = new StringGroup(System.Convert.ToString(parameter), ",");
            if (paramValues.ToString() != "") {
                foreach (var item in paramValues.ToStringGroup()) {
                    result -= double.Parse(item);
                }
            }

            //返回
            return result;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture) {
            return null;
        }
    }

    /// <summary>
    /// 乘法转换器，乘数输入在values和param均可，param使用sg，分隔符,
    /// </summary>
    [ValueConversion(typeof(double), typeof(double))]
    public class MultiplicationConverter : IMultiValueConverter {

        public object Convert(object[] values, Type targetTypes, object parameter, System.Globalization.CultureInfo culture) {
            double result = 0;

            foreach (var item in values) {
                result *= System.Convert.ToDouble(item);
            }

            //获取参数
            StringGroup paramValues = new StringGroup(System.Convert.ToString(parameter), ",");
            if (paramValues.ToString() != "") {
                foreach (var item in paramValues.ToStringGroup()) {
                    result *= double.Parse(item);
                }
            }

            //返回
            return result;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture) {
            return null;
        }
    }

    /// <summary>
    /// 除法转换器，被除数输入在第一个，除数输入在第二个以后，可输入多个。除数数据在values和param均可，param使用sg，分隔符,
    /// </summary>
    [ValueConversion(typeof(double), typeof(double))]
    public class DivisionConverter : IMultiValueConverter {

        public object Convert(object[] values, Type targetTypes, object parameter, System.Globalization.CultureInfo culture) {
            double result = System.Convert.ToDouble(values[0]);

            bool first = true;
            foreach (var item in values) {
                //去掉第一个
                if (first) { first = false; continue; }
                result /= System.Convert.ToDouble(item);
            }

            //获取参数
            StringGroup paramValues = new StringGroup(System.Convert.ToString(parameter), ",");
            if (paramValues.ToString() != "") {
                foreach (var item in paramValues.ToStringGroup()) {
                    result /= double.Parse(item);
                }
            }

            //返回
            return result;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture) {
            return null;
        }
    }

    #endregion

}