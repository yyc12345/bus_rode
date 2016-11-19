using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bus_rode.Kernel.ResourcesReflection {
    /// <summary>
    /// dll的状态
    /// </summary>
    public enum enumDllReflectionState {
        /// <summary>
        /// 未知的，默认初始状态
        /// </summary>
        Unknow = 0,
        /// <summary>
        /// 检测文件通过的状态
        /// </summary>
        Legal = 1,
        /// <summary>
        /// 检测文件未通过状态
        /// </summary>
        Illegal = 2,
        /// <summary>
        /// 正在初始化
        /// </summary>
        Initiatlizing = 3,
        /// <summary>
        /// 初始化失败，无法继续调用的
        /// </summary>
        Disabled = 4,
        /// <summary>
        /// 准备就绪的，初始化成功后为此状态
        /// </summary>
        Ready = 5,
        /// <summary>
        /// 正在运行的
        /// </summary>
        Running = 6
    }

}
