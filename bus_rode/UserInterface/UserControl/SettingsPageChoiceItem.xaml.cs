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
    /// SettingsPageChoiceItem.xaml 的交互逻辑
    /// </summary>
    [System.Windows.Markup.ContentProperty("Title")]
    public partial class SettingsPageChoiceItem : System.Windows.Controls.UserControl {
        public SettingsPageChoiceItem() {
            InitializeComponent();

            FlagOfForcingToChange = false;
        }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(SettingsPageChoiceItem),
                        new PropertyMetadata("", new PropertyChangedCallback((DependencyObject d, DependencyPropertyChangedEventArgs e) => {
                            SettingsPageChoiceItem v = d as SettingsPageChoiceItem;
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
            DependencyProperty.Register("Description", typeof(string), typeof(SettingsPageChoiceItem),
                        new PropertyMetadata("", new PropertyChangedCallback((DependencyObject d, DependencyPropertyChangedEventArgs e) => {
                            SettingsPageChoiceItem v = d as SettingsPageChoiceItem;
                            v.uiDescription.Text = e.NewValue.ToString();
                        })));

        /// <summary>
        /// 显示的选择的信息
        /// </summary>
        public string ChoiceList {
            get { return (string)GetValue(ChoiceListProperty); }
            set { SetValue(ChoiceListProperty, value); }
        }

        public static readonly DependencyProperty ChoiceListProperty =
            DependencyProperty.Register("ChoiceList", typeof(string), typeof(SettingsPageChoiceItem),
                        new PropertyMetadata("", new PropertyChangedCallback((DependencyObject d, DependencyPropertyChangedEventArgs e) => {
                            SettingsPageChoiceItem v = d as SettingsPageChoiceItem;
                            v.uiChoice.ItemsSource = new StringGroup(e.NewValue.ToString(), ",").ToList();
                        })));

        /// <summary>
        /// 选择的项
        /// </summary>
        public int ChoiceSelectedIndex {
            get { return uiChoice.SelectedIndex; }
            set {
                FlagOfForcingToChange = true;
                uiChoice.SelectedIndex = value;
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

        private void fx_DataChange(object sender, SelectionChangedEventArgs e) {
            //judge flag
            if (FlagOfForcingToChange == true) {
                FlagOfForcingToChange = false;
                return;
            }else if (uiChoice.SelectedIndex == -1) {
                //judge index
                return;
            }else {
                OnDataChanged();
            }
        }

    }
}
