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

namespace bus_rode.UserInterface.UserControl {
    /// <summary>
    /// LinePageBusPreview.xaml 的交互逻辑
    /// </summary>
    public partial class LinePageBusPreview : System.Windows.Controls.UserControl {
        public LinePageBusPreview() {
            InitializeComponent();

            colorShowGrid = new List<Rectangle>();
            stopsCountList = new List<int>();
        }

        #region 依赖项属性

        /// <summary>
        /// 运行时数据，分隔符, 每一项是某车所在坐标，这边会自行统计的-更改此数值会导致控件被刷新
        /// </summary>
        public string RuntimeBusMsg {
            get { return (string)GetValue(RuntimeBusMsgProperty); }
            set { SetValue(RuntimeBusMsgProperty, value); }
        }

        public static readonly DependencyProperty RuntimeBusMsgProperty =
            DependencyProperty.Register("RuntimeBusMsg", typeof(string), typeof(LinePageBusPreview),
                        new PropertyMetadata("", new PropertyChangedCallback((DependencyObject d, DependencyPropertyChangedEventArgs e) => {
                            LinePageBusPreview v = d as LinePageBusPreview;
                            v.SetColor();
                        })));

        /// <summary>
        /// 当前选定站台位点，设置为-1表明当前线路中没有选中站点
        /// </summary>
        public int NowPosition {
            get { return (int)GetValue(NowPositionProperty); }
            set { SetValue(NowPositionProperty, value); }
        }

        public static readonly DependencyProperty NowPositionProperty =
            DependencyProperty.Register("NowPosition", typeof(int), typeof(LinePageBusPreview),
                        new PropertyMetadata(-1, new PropertyChangedCallback((DependencyObject d, DependencyPropertyChangedEventArgs e) => {
                            LinePageBusPreview v = d as LinePageBusPreview;
                            v.SetPosition();
                        })));

        /// <summary>
        /// 站台数量-更改此数字会导致控件被重置
        /// </summary>
        public int StopCount {
            get { return (int)GetValue(StopCountProperty); }
            set { SetValue(StopCountProperty, value); }
        }

        public static readonly DependencyProperty StopCountProperty =
            DependencyProperty.Register("StopCount", typeof(int), typeof(LinePageBusPreview),
                        new PropertyMetadata(0, new PropertyChangedCallback((DependencyObject d, DependencyPropertyChangedEventArgs e) => {
                            LinePageBusPreview v = d as LinePageBusPreview;
                            v.SetStopCount();
                        })));

        #endregion

        /// <summary>
        /// 显示的车辆数据的框
        /// </summary>
        private List<Rectangle> colorShowGrid;
        /// <summary>
        /// 当前车站计数器
        /// </summary>
        private List<int> stopsCountList;

        /// <summary>
        /// 重新设置站台个数，该方法同时会清空所有现有数据，隐藏箭头
        /// </summary>
        private void SetStopCount() {
            //remove all
            foreach (var item in colorShowGrid) { uiUserControlBackground.Children.Remove(item); }
            colorShowGrid.Clear();
            if (StopCount == 0) { } else {
                for (int i = 0; i < StopCount; i++) {
                    //set property
                    var cache = new Rectangle();
                    cache.StrokeThickness = 0;
                    cache.Fill = new SolidColorBrush(Color.FromArgb(0, 30, 144, 255));
                    Grid.SetRow(cache, i);
                    Grid.SetColumn(cache, 1);
                    //add to list
                    colorShowGrid.Add(cache);
                    //add to ui
                    uiUserControlBackground.Children.Add(cache);
                }
            }

            //reset 
            stopsCountList.Clear();

            //set row
            uiUserControlBackground.RowDefinitions.Clear();
            if (StopCount == 0) { } else {
                for (int i = 0; i < StopCount; i++) {
                    uiUserControlBackground.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
                }
            }

            //hide arrow
            NowPosition = -1;

        }
        /// <summary>
        /// 设置坐标点
        /// </summary>
        private void SetPosition() {
            if (NowPosition == -1) {
                Grid.SetRow(uiSign, 0);
                uiSign.Visibility = Visibility.Collapsed;
            } else {
                Grid.SetRow(uiSign, NowPosition);
                uiSign.Visibility = Visibility.Visible;
            }
        }
        /// <summary>
        /// 设置颜色
        /// </summary>
        private void SetColor() {
            //如果没有数据，全部刷为空白
            if (RuntimeBusMsg == "") {
                foreach (var item in colorShowGrid) {
                    item.Fill = new SolidColorBrush(Color.FromArgb(0, 30, 144, 255));
                }

                return;
            }

            //如果序列为空，退出之
            if (StopCount == 0) { return; }

            //计算
            var cache = new StringGroup(RuntimeBusMsg, ",").ToStringGroup();

            stopsCountList.Clear();
            //填充数组
            for (int i = 0; i < StopCount; i++) {
                stopsCountList.Add(0);
            }

            //统计数组
            foreach (string item in cache) {
                if (int.Parse(item) < StopCount) { stopsCountList[int.Parse(item)]++; }
            }

            //获得区间
            double countAreaUnit = (stopsCountList.Max((numberItem) => { return numberItem; }) -
                stopsCountList.Min((numberItem) => { return numberItem; })) / 5f;
            //set color
            //0->105->180->225->255为二次函数式渐变，目的在于区分无车和有车状态下颜色过于相似问题
            int index = 0;
            foreach (var item in stopsCountList) {
                if (item >= countAreaUnit * 4) colorShowGrid[index].Fill = new SolidColorBrush(Color.FromArgb(255, 30, 144, 255));
                else if (item >= countAreaUnit * 3) colorShowGrid[index].Fill = new SolidColorBrush(Color.FromArgb(225, 30, 144, 255));
                else if (item >= countAreaUnit * 2) colorShowGrid[index].Fill = new SolidColorBrush(Color.FromArgb(180, 30, 144, 255));
                else if (item >= countAreaUnit * 1) colorShowGrid[index].Fill = new SolidColorBrush(Color.FromArgb(105, 30, 144, 255));
                else colorShowGrid[index].Fill = new SolidColorBrush(Color.FromArgb(0, 30, 144, 255));
                index ++;
            }

        }

    }
}
