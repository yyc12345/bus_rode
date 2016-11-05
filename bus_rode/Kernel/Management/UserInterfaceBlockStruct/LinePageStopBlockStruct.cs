using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bus_rode.Kernel.Tools;
using System.Windows;
using System.Windows.Controls;

namespace bus_rode.Kernel.Management.UserInterfaceBlockStruct {

    /// <summary>
    /// 线路界面站台
    /// </summary>
    public class LinePageStopBlockStruct {

        public LinePageStopBlockStruct() {
            name = "";
            translateName = "";
            runtimeCount = 0;
        }

        /// <summary>
        /// 站台名
        /// </summary>
        private string name;
        /// <summary>
        /// 站台名翻译
        /// </summary>
        private string translateName;
        /// <summary>
        /// 站台名
        /// </summary>
        public string Name {
            //输出是给ui用得所以判定翻译，输入是给代码调用的，直接写name，重置translate
            get {
                return translateName == "" ? name : translateName;
            }
            set {
                name = value;
                //reset translate result
                translateName = "";
            }
        }

        /// <summary>
        /// 当前车站车辆数
        /// </summary>
        private int runtimeCount;
        /// <summary>
        /// 当前车站车辆数
        /// </summary>
        public int RuntimeCount { get { return runtimeCount; } set { runtimeCount = value; } }
        /// <summary>
        /// 当前车站车辆数文本
        /// </summary>
        public string RuntimeDescription {
            get {
                return runtimeCount == 0 ? "" :
                    Language.GetItem("langCodeLinePageStopItemRuntime", new StringGroup(runtimeCount.ToString(), ","));
            }
        }
        /// <summary>
        /// 当前车站车辆数是否显示，为0不显示
        /// </summary>
        public Visibility RuntimeVisibility {
            get {
                return runtimeCount == 0 ? Visibility.Collapsed : Visibility.Visible;
            }
        }

    }

}
