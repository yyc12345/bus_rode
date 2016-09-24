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

namespace bus_rode.UserInterface.Page {
    /// <summary>
    /// LinePage.xaml 的交互逻辑
    /// </summary>
    public partial class LinePage : System.Windows.Controls.UserControl, bus_rode.Kernel.Interface.IAutoTranslate {
        public LinePage() {
            InitializeComponent();
        }





        #region 翻译

        public void FlushTranslateData(StringGroup result) {

        }

        public StringGroup ProvideTranslateData() {
            return null;
        }

        #endregion
    }

    /// <summary>
    /// 线路页面模式
    /// </summary>
    public enum enumLinePageMode {
        /// <summary>
        /// 线路站台
        /// </summary>
        StopList = 0,
        /// <summary>
        /// 详细信息
        /// </summary>
        Detail = 1
    }

}
