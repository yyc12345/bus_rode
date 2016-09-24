using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bus_rode.Kernel.Interface
{
    /// <summary>
    /// 消息对话框接口
    /// </summary>
    public interface IMessageDialog
    {
        /// <summary>
        /// 标题
        /// </summary>
        string Title { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        string Message { get; set; }
        /// <summary>
        /// 左侧按钮文本
        /// </summary>
        string LeftButtonContant { get; set; }
        /// <summary>
        /// 右侧按钮文本
        /// </summary>
        string RightButtonContant { get; set; }
        /// <summary>
        /// 按钮个数（1-2）（1按钮时，只显示左侧按钮）
        /// </summary>
        byte ButtonCount { get; set; }
        /// <summary>
        /// 是否显示输入框
        /// </summary>
        bool ShowInputbox { get; set; }

        /// <summary>
        /// 展示对话框的函数
        /// </summary>
        void ShowDialog();
    }
}
