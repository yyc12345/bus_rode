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

            //设置绑定
            Binding bTitle = new Binding(), bDescription = new Binding();
            bTitle.Mode = BindingMode.OneWay;
            bDescription.Mode = BindingMode.OneWay;

            bTitle.Source = this;
            bTitle.Path = new PropertyPath("Title");
            bDescription.Source = this;
            bDescription.Path = new PropertyPath("Description");

            this.uiTitle.SetBinding(TextBlock.TextProperty, bTitle);
            this.uiDescription.SetBinding(TextBlock.TextProperty, bDescription);

            //设置绑定
            Binding bIsChecked = new Binding();
            bIsChecked.Mode = BindingMode.TwoWay;
            bIsChecked.Source = this;
            bIsChecked.Path = new PropertyPath("IsChecked");
            this.uiToggleButton.SetBinding(System.Windows.Controls.Primitives.ToggleButton.IsCheckedProperty, bIsChecked);

        }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(SettingsPageToggleButtonItem), new PropertyMetadata(""));

        /// <summary>
        /// 描述
        /// </summary>
        public string Description {
            get { return (string)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }

        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register("Description", typeof(string), typeof(SettingsPageToggleButtonItem), new PropertyMetadata(""));

        /// <summary>
        /// 是否选中
        /// </summary>
        public bool? IsChecked {
            get { return (bool?)GetValue(IsCheckedProperty); }
            set { SetValue(IsCheckedProperty, value); }
        }

        public static readonly DependencyProperty IsCheckedProperty =
            DependencyProperty.Register("IsChecked", typeof(bool), typeof(SettingsPageToggleButtonItem), new PropertyMetadata(false));


    }
}
