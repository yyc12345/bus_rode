using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bus_rode.Kernel.Tools;

namespace bus_rode.Kernel.Interface
{
    /// <summary>
    /// 列表对话框接口
    /// </summary>
    public interface IListDialog
    {
        /// <summary>
        /// 标题
        /// </summary>
        string Title { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        string Description { get; set; }
        /// <summary>
        /// 列表项列表
        /// </summary>
        List<ListDialogItem> ItemsList { get; set; }
        /// <summary>
        /// 是否单选
        /// </summary>
        bool SingleList { get; set; }

        /// <summary>
        /// 显示对话框
        /// </summary>
        void ShowDialog();

    }

    /// <summary>
    /// 列表对话框每一项
    /// </summary>
    public class ListDialogItem {

        /// <summary>
        /// 主描述
        /// </summary>
        public string _mainDescription;
        /// <summary>
        /// 主描述
        /// </summary>
        public string MainDescription { get { return _mainDescription; } }
        /// <summary>
        /// 次要描述
        /// </summary>
        public string _assistanceDescription;
        /// <summary>
        /// 次要描述
        /// </summary>
        public string AssistanceDescription { get { return _assistanceDescription; } }

        public ListDialogItem() {
            _mainDescription = "";
            _assistanceDescription = "";
        }

    }

}
