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
    /// SettingsPageNormalItem.xaml 的交互逻辑
    /// </summary>
    [System.Windows.Markup.ContentProperty("Title")]
    public partial class SettingsPageNormalItem : System.Windows.Controls.UserControl {

        public SettingsPageNormalItem() {
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
            DependencyProperty.Register("Title", typeof(string), typeof(SettingsPageNormalItem),
                        new PropertyMetadata("", new PropertyChangedCallback((DependencyObject d, DependencyPropertyChangedEventArgs e) => {
                            SettingsPageNormalItem v = d as SettingsPageNormalItem;
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
            DependencyProperty.Register("Description", typeof(string), typeof(SettingsPageNormalItem),
                        new PropertyMetadata("", new PropertyChangedCallback((DependencyObject d, DependencyPropertyChangedEventArgs e) => {
                            SettingsPageNormalItem v = d as SettingsPageNormalItem;
                            v.uiDescription.Text = e.NewValue.ToString();
                        })));


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
            OnDataChanged();
        }
    }
}
