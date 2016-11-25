using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bus_rode.Kernel.ResourcesReflection {

    /// <summary>
    /// 2个反射模块的命令行的制造器
    /// </summary>
    public static class DllCommandCreator {

        public static string CreateCommandOfMonitorDll(enumCommandOfMonitorDll mainType, string parameter) {
            return Enum.GetName(typeof(enumCommandOfMonitorDll), mainType) + parameter;
        }

        public static string CreateCommandOfResourcesDll(enumCommandOfResourcesDll mainType ,string parameter) {
            return Enum.GetName(typeof(enumCommandOfResourcesDll), mainType) + parameter;
        }

    }


    public enum enumCommandOfMonitorDll {
        /// <summary>
        /// 获取线路
        /// </summary>
        GetLine = 0
    }

    public enum enumCommandOfResourcesDll {
        /// <summary>
        /// 获取线路
        /// </summary>
        GetLine = 0,
        /// <summary>
        /// 获取车站
        /// </summary>
        GetStop = 1,
        /// <summary>
        /// 获取线路列表
        /// </summary>
        GetLineList = 2,
        /// <summary>
        /// 获取站台列表
        /// </summary>
        GetStopList = 3,
        /// <summary>
        /// 获取地铁站点
        /// </summary>
        GetSubway = 4
    }

}
