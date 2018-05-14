using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.InteropServices;
using bus_rode.Tools;
using System.Windows.Interop;
using System.Diagnostics;

namespace bus_rode {
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window {

        public MainWindow() {
            InitializeComponent();

            //绑定事件
            Application.Current.Exit += fxApplication_Exit;

            //应用名显示
            uiTitle.Text = Kernel.Tools.ApplicationInformation.AppTitleName;
            uiDebugVersionText.Text = "yyc12345 Copyright 2012-2018" + Environment.NewLine +
            "This version should not be disseminated or released to the outside world. It is only for internal testing and debugging. The source code, interface design and other types of screenshots of this version of the program, copy of the program without authorization, will be cancelled after the publisher is identified." + Environment.NewLine +
            Kernel.Tools.ApplicationInformation.AppName + " " + Kernel.Tools.ApplicationInformation.AppBuild;
        }

        /// <summary>
        /// 应用结束时的操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fxApplication_Exit(object sender, ExitEventArgs e) {
            //todo:结束所有线程和取消任务栏图标显示

        }

        /// <summary>
        /// 窗口移动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fxTitleBar_MouseDown(object sender, MouseButtonEventArgs e) {
            Win32.SendMessage(new System.Windows.Interop.WindowInteropHelper(this).Handle, Win32.WM_NCLBUTTONDOWN, (int)Win32.HitTest.HTCAPTION, 0);
        }

        /// <summary>
        /// 监听窗口大小变化
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="msg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <param name="handled"></param>
        /// <returns></returns>
        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled) {

            //当鼠标输入且窗口大小正常时接受大小改变
            if (msg == Win32.WM_NCHITTEST && this.WindowState == WindowState.Normal) {
                return WmNCHitTest(lParam, ref handled);
            }

            return IntPtr.Zero;
        }

        /// <summary>
        /// 处理窗口大小变化识别函数
        /// </summary>
        /// <param name="lParam"></param>
        /// <param name="handled"></param>
        /// <returns></returns>
        private IntPtr WmNCHitTest(IntPtr lParam, ref bool handled) {

            Point mousePoint = new Point();
            //边角识别宽度
            int cornerWidth = 8;
            //边框宽度
            int customBorderThickness = 5;
            // Update cursor point  
            // The low-order word specifies the x-coordinate of the cursor.  
            // #define GET_X_LPARAM(lp) ((int)(short)LOWORD(lp))  
            mousePoint.X = (int)(short)(lParam.ToInt32() & 0xFFFF);
            // The high-order word specifies the y-coordinate of the cursor.  
            // #define GET_Y_LPARAM(lp) ((int)(short)HIWORD(lp))  
            mousePoint.Y = (int)(short)(lParam.ToInt32() >> 16);

            // Do hit test  
            handled = true;
            if (Math.Abs(mousePoint.Y - Top) <= cornerWidth
                && Math.Abs(mousePoint.X - Left) <= cornerWidth) { // Top-Left  
                handled = false;
                return IntPtr.Zero;
                //return new IntPtr((int)Win32.HitTest.HTTOPLEFT);
            } else if (Math.Abs(ActualHeight + Top - mousePoint.Y) <= cornerWidth
                  && Math.Abs(mousePoint.X - Left) <= cornerWidth) { // Bottom-Left  
                return new IntPtr((int)Win32.HitTest.HTBOTTOMLEFT);
            } else if (Math.Abs(mousePoint.Y - Top) <= cornerWidth
                  && Math.Abs(ActualWidth + Left - mousePoint.X) <= cornerWidth) { // Top-Right  
                handled = false;
                return IntPtr.Zero;
                //return new IntPtr((int)Win32.HitTest.HTTOPRIGHT);
            } else if (Math.Abs(ActualWidth + Left - mousePoint.X) <= cornerWidth
                  && Math.Abs(ActualHeight + Top - mousePoint.Y) <= cornerWidth) { // Bottom-Right  
                return new IntPtr((int)Win32.HitTest.HTBOTTOMRIGHT);
            } else if (Math.Abs(mousePoint.X - Left) <= customBorderThickness) { // Left  
                return new IntPtr((int)Win32.HitTest.HTLEFT);
            } else if (Math.Abs(ActualWidth + Left - mousePoint.X) <= customBorderThickness) { // Right  
                return new IntPtr((int)Win32.HitTest.HTRIGHT);
            } else if (Math.Abs(mousePoint.Y - Top) <= customBorderThickness) { // Top  
                handled = false;
                return IntPtr.Zero;
                //return new IntPtr((int)Win32.HitTest.HTTOP);
            } else if (Math.Abs(ActualHeight + Top - mousePoint.Y) <= customBorderThickness) { // Bottom  
                return new IntPtr((int)Win32.HitTest.HTBOTTOM);
            } else {
                handled = false;
                return IntPtr.Zero;
            }
        }

        /// <summary>
        /// 窗口大小注册
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fxMainWindow_SourceInitialized(object sender, EventArgs e) {
            HwndSource source = HwndSource.FromHwnd(new WindowInteropHelper(this).Handle);
            if (source == null)
                // Should never be null  
                throw new Exception("Cannot get HwndSource instance.");

            source.AddHook(new HwndSourceHook(this.WndProc));
        }

        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fxTitleClose_Click(object sender, RoutedEventArgs e) {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// 改变窗口大小
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fxTitleWindowChange_Click(object sender, RoutedEventArgs e) {
            this.WindowState = this.WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
        }

        /// <summary>
        /// 窗口大小变化侦测
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fxMainWindow_StateChanged(object sender, EventArgs e) {

            //修改按钮
            switch (this.WindowState) {
                case WindowState.Normal:
                    uiTitleWindowIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.WindowMaximize;
                    this.BorderThickness = new Thickness(10);
                    //uiMainWindowShadow.Opacity = 0;
                    break;
                case WindowState.Minimized:
                    break;
                case WindowState.Maximized:
                    uiTitleWindowIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.WindowRestore;
                    this.BorderThickness = new Thickness(0);
                    //uiMainWindowShadow.Opacity = 0;
                    break;
            }

        }

        /// <summary>
        /// 最小化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fxTitleMinimize_Click(object sender, RoutedEventArgs e) {
            this.WindowState = WindowState.Minimized;
        }

    }
}
