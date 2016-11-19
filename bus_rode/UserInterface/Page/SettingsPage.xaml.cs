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

namespace bus_rode.UserInterface.Page {
    /// <summary>
    /// SettingsPage.xaml 的交互逻辑
    /// </summary>
    public partial class SettingsPage : System.Windows.Controls.UserControl {
        public SettingsPage() {
            InitializeComponent();
        }

        //TODO:内部所有控件的数值变化，由控件自身可以变化数值的控件的事件引发外部事件再供此处调用
        //外部强制设定时，可指定一个标识符，用于区分自身变化和强制改变

        //语言列表由启动时从Information读取输入，输入时输入NativeName以方便选择

        //颜色列表由启动时按固定顺序输入，输入时输入英文

    }


}
