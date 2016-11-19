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
using bus_rode.UserInterface.Converter;

namespace bus_rode.UserInterface.UserControl {
    /// <summary>
    /// SettingsPageSliderItem.xaml 的交互逻辑
    /// </summary>
    [System.Windows.Markup.ContentProperty("Title")]
    public partial class SettingsPageSliderItem : System.Windows.Controls.UserControl {
        public SettingsPageSliderItem() {
            InitializeComponent();

            //绑定 数值文本和滑动条
            converterParam = "{0}";
            Binding bShowValue = new Binding();
            bShowValue.Mode = BindingMode.OneWay;
            bShowValue.Converter = new SliderValueAddWordsConverter();
            bShowValue.Source = this.uiSlider;
            bShowValue.Path = new PropertyPath("Value");
            bShowValue.ConverterParameter = converterParam;
            this.uiSliderValueText.SetBinding(TextBlock.TextProperty, bShowValue);

        }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(SettingsPageSliderItem),
                        new PropertyMetadata("", new PropertyChangedCallback((DependencyObject d, DependencyPropertyChangedEventArgs e) => {
                            SettingsPageSliderItem v = d as SettingsPageSliderItem;
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
            DependencyProperty.Register("Description", typeof(string), typeof(SettingsPageSliderItem),
                        new PropertyMetadata("", new PropertyChangedCallback((DependencyObject d, DependencyPropertyChangedEventArgs e) => {
                            SettingsPageSliderItem v = d as SettingsPageSliderItem;
                            v.uiDescription.Text = e.NewValue.ToString();
                        })));


        /// <summary>
        /// 最大值
        /// </summary>
        public double Maximum {
            get { return uiSlider.Maximum; }
            set { uiSlider.Maximum = value; }
        }

        /// <summary>
        /// 最小值
        /// </summary>
        public double Minimum {
            get { return uiSlider.Minimum; }
            set { uiSlider.Minimum = value; }
        }

        /// <summary>
        /// 当前值
        /// </summary>
        public double Value {
            get { return uiSlider.Value; }
            set {
                FlagOfForcingToChange = true;
                uiSlider.Value = value;
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

        private void fx_DataChange(object sender, RoutedPropertyChangedEventArgs<double> e) {
            //judge flag
            if (FlagOfForcingToChange == true) {
                FlagOfForcingToChange = false;
                return;

            } else OnDataChanged();
        }

        #region 滚动条到文本框的转换
        /// <summary>
        /// 转换器参数
        /// </summary>
        private string converterParam;
        /// <summary>
        /// 转换器参数
        /// </summary>
        public string ConverterParam {
            get { return converterParam; }
            set {
                converterParam = value;

                //设置绑定
                Binding bShowValue = new Binding();
                bShowValue.Mode = BindingMode.OneWay;
                bShowValue.Converter = new SliderValueAddWordsConverter();
                bShowValue.Source = this.uiSlider;
                bShowValue.Path = new PropertyPath("Value");
                bShowValue.ConverterParameter = converterParam;
                this.uiSliderValueText.SetBinding(TextBlock.TextProperty, bShowValue);

            }
        }


        #endregion


    }
}
