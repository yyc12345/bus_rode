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
    /// SettingsPageToggleButtonItem.xaml 的交互逻辑
    /// </summary>
    [System.Windows.Markup.ContentProperty("Title")]
    public partial class SettingsPageToggleButtonItem : System.Windows.Controls.UserControl {
        public SettingsPageToggleButtonItem() {
            InitializeComponent();
        }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(SettingsPageToggleButtonItem),
                        new PropertyMetadata("", new PropertyChangedCallback((DependencyObject d, DependencyPropertyChangedEventArgs e) => {
                            SettingsPageToggleButtonItem v = d as SettingsPageToggleButtonItem;
                            v.uiTitle.Text = e.NewValue.ToString();
                        })));

        /// <summary>
        /// 描述
        /// </summary>
        public string Description {
            get { return (string)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }

        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register("Description", typeof(string), typeof(SettingsPageToggleButtonItem),
                        new PropertyMetadata("", new PropertyChangedCallback((DependencyObject d, DependencyPropertyChangedEventArgs e) => {
                            SettingsPageToggleButtonItem v = d as SettingsPageToggleButtonItem;
                            v.uiDescription.Text = e.NewValue.ToString();
                        })));


        /// <summary>
        /// 是否选中
        /// </summary>
        public bool? IsChecked {
            get { return uiToggleButton.IsChecked; }
            set {
                FlagOfForcingToChange = true;
                uiToggleButton.IsChecked = value;
            }
        }

        /// <summary>
        /// 强制改变数值的标志
        /// </summary>
        private bool FlagOfForcingToChange;
        /// <summary>
        /// 选项改变的事件
        /// </summary>
        public event Action DataChanged;
        /// <summary>
        /// 引发事件检查
        /// </summary>
        private void OnDataChanged() {
            if (DataChanged != null) DataChanged();
        }

        private void fx_DataChange(object sender, RoutedEventArgs e) {
            //judge flag
            if (FlagOfForcingToChange == true) {
                FlagOfForcingToChange = false;
                return;

            } else OnDataChanged();
        }

    }
}
