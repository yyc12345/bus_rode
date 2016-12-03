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
using System.Diagnostics;

namespace bus_rodeException {
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }

        private string workPath {
            get {
                return Environment.CurrentDirectory[Environment.CurrentDirectory.Length - 1] == '\\' ?
Environment.CurrentDirectory.Substring(0, Environment.CurrentDirectory.Length - 1) :
Environment.CurrentDirectory;
            }
        }

        /// <summary>
        /// 获得错误报告
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e) {
            uiCreatedMessage.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// 仅重启
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e) {
            System.IO.File.Delete(workPath + @"\Error.log");
            if (System.IO.File.Exists(workPath + @"\bus_rode.exe")) Process.Start(workPath + @"\bus_rode.exe");
            Environment.Exit(0);
        }
    }
}
