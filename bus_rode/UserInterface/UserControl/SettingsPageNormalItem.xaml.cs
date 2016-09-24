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

        }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(SettingsPageNormalItem), new PropertyMetadata(""));

        /// <summary>
        /// 描述
        /// </summary>
        public string Description {
            get { return (string)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }

        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register("Description", typeof(string), typeof(SettingsPageNormalItem), new PropertyMetadata(""));


    }
}
