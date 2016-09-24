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
            Binding bMax = new Binding(), bMin = new Binding(), bValue = new Binding();
            bMax.Mode = BindingMode.TwoWay;
            bMin.Mode = BindingMode.TwoWay;
            bValue.Mode = BindingMode.TwoWay;

            bMax.Source = this;
            bMax.Path = new PropertyPath("Maximum");
            bMin.Source = this;
            bMin.Path = new PropertyPath("Minimum");
            bValue.Source = this;
            bValue.Path = new PropertyPath("Value");

            this.uiSlider.SetBinding(Slider.MaximumProperty, bMax);
            this.uiSlider.SetBinding(Slider.MinimumProperty, bMin);
            this.uiSlider.SetBinding(Slider.ValueProperty, bValue);

            //设置绑定
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
            DependencyProperty.Register("Title", typeof(string), typeof(SettingsPageSliderItem), new PropertyMetadata(""));

        /// <summary>
        /// 描述
        /// </summary>
        public string Description {
            get { return (string)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }

        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register("Description", typeof(string), typeof(SettingsPageSliderItem), new PropertyMetadata(""));

        /// <summary>
        /// 最大值
        /// </summary>
        public int Maximum {
            get { return (int)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }

        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register("Maximum", typeof(int), typeof(SettingsPageSliderItem), new PropertyMetadata(1));

        /// <summary>
        /// 最小值
        /// </summary>
        public int Minimum {
            get { return (int)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }

        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register("Minimum", typeof(int), typeof(SettingsPageSliderItem), new PropertyMetadata(0));

        /// <summary>
        /// 当前值
        /// </summary>
        public int Value {
            get { return (int)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(int), typeof(SettingsPageSliderItem), new PropertyMetadata(0));


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



    }
}
