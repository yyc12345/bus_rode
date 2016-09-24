using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bus_rode.Kernel.FileIO {
    /// <summary>
    /// 文件数据结构
    /// </summary>
    public interface IFileBlockStruct {

        /// <summary>
        /// 是否支持按步阅读
        /// </summary>
        bool SupportStepReader { get; }
        /// <summary>
        /// 是否支持随机阅读
        /// </summary>
        bool SupportSeekReader { get; }
        /// <summary>
        /// 是否支持块阅读
        /// </summary>
        bool SupportBlockReader { get; }
        /// <summary>
        /// 是否支持完全阅读
        /// </summary>
        bool SupportCompletelyReader { get; }

        /// <summary>
        /// 是否支持按步写入
        /// </summary>
        bool SupportStepWriter { get; }
        /// <summary>
        /// 是否支持按步写入并写入索引
        /// </summary>
        bool SupportSeekWriter { get; }
        /// <summary>
        /// 是否支持全部写入
        /// </summary>
        bool SupportCompletelyWriter { get; }

        /// <summary>
        /// 主文档
        /// </summary>
        string mainFilePath { get; }
        /// <summary>
        /// 索引文档
        /// </summary>
        string seekFilePath { get; }
        /// <summary>
        /// 按步阅读的阅读行数
        /// </summary>
        int stepReadLine { get; }
        /// <summary>
        /// 按块阅读的块中项目数
        /// </summary>
        int blockReadCount { get; }

        /// <summary>
        /// 转换至组
        /// </summary>
        /// <returns></returns>
        Tools.StringGroup ConvertToStringGroup();

        /// <summary>
        /// 转换至资源
        /// </summary>
        /// <param name="value">内容</param>
        /// <returns></returns>
        void ConvertToResources(Tools.StringGroup value);
    }
}
