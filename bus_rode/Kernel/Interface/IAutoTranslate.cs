using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bus_rode.Kernel.Tools;

namespace bus_rode.Kernel.Interface {

    /// <summary>
    /// 自动翻译接口-用于整体
    /// </summary>
    public interface IAutoTranslate {

        /// <summary>
        /// 提供翻译数据
        /// </summary>
        /// <returns></returns>
        StringGroup ProvideTranslateData();
        /// <summary>
        /// 覆写翻译数据
        /// </summary>
        void FlushTranslateData(StringGroup result);

    }
}
