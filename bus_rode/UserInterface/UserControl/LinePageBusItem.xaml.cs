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

namespace bus_rode.UserInterface.UserControl {
    /// <summary>
    /// LinePageBusItem.xaml 的交互逻辑
    /// </summary>
    public partial class LinePageBusItem : System.Windows.Controls.UserControl {
        public LinePageBusItem() {
            InitializeComponent();
        }

        #region 依赖项属性

        /// <summary>
        /// 站台名
        /// </summary>
        public string StopName {
            get { return (string)GetValue(StopNameProperty); }
            set { SetValue(StopNameProperty, value); }
        }

        public static readonly DependencyProperty StopNameProperty =
            DependencyProperty.Register("StopName", typeof(string), typeof(LinePageBusItem),
                        new PropertyMetadata("", new PropertyChangedCallback((DependencyObject d, DependencyPropertyChangedEventArgs e) => {

                        })));


        /// <summary>
        /// 车辆列表
        /// </summary>
        public List<MonitorBusListItem> MonitorBusList {
            get { return (List<MonitorBusListItem>)GetValue(MonitorBusListProperty); }
            set { SetValue(MonitorBusListProperty, value); }
        }

        public static readonly DependencyProperty MonitorBusListProperty =
            DependencyProperty.Register("MonitorBusList", typeof(List<MonitorBusListItem>), typeof(LinePageBusItem),
                        new PropertyMetadata(new List<MonitorBusListItem>(), new PropertyChangedCallback((DependencyObject d, DependencyPropertyChangedEventArgs e) => {

                        })));


        /// <summary>
        /// 地铁列表
        /// </summary>
        public List<SubwayListItem> SubwayList {
            get { return (List<SubwayListItem>)GetValue(SubwayListProperty); }
            set { SetValue(SubwayListProperty, value); }
        }

        public static readonly DependencyProperty SubwayListProperty =
            DependencyProperty.Register("SubwayList", typeof(List<SubwayListItem>), typeof(LinePageBusItem),
                        new PropertyMetadata(new List<SubwayListItem>(), new PropertyChangedCallback((DependencyObject d, DependencyPropertyChangedEventArgs e) => {

                        })));


        #endregion







    }

    /// <summary>
    /// 监控车辆列表项
    /// </summary>
    public class MonitorBusListItem {
        public string name { get; set; }
    }
    /// <summary>
    /// 地铁列表项
    /// </summary>
    public class SubwayListItem {
        public string name { get; set; }
        public string toward { get; set; }
    }

}
