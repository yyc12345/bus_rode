﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows;
using System;

namespace bus_rode.Tools {

    /// <summary>
    /// Win32API
    /// </summary>
    public class Win32 {
        [DllImport("user32.dll")]
        internal static extern bool ClientToScreen(IntPtr hWnd, ref POINT lpPoint);

        [DllImport("user32.dll")]
        internal static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);
        public const int WM_NCHITTEST = 0x0084;
        public enum HitTest : int {
            HTERROR = -2,
            HTTRANSPARENT = -1,
            HTNOWHERE = 0,
            HTCLIENT = 1,
            HTCAPTION = 2,
            HTSYSMENU = 3,
            HTGROWBOX = 4,
            HTSIZE = 4,
            HTMENU = 5,
            HTHSCROLL = 6,
            HTVSCROLL = 7,
            HTMINBUTTON = 8,
            HTREDUCE = 8,
            HTMAXBUTTON = 9,
            HTZOOM = 9,
            HTLEFT = 10,
            HTRIGHT = 11,
            HTTOP = 12,
            HTTOPLEFT = 13,
            HTTOPRIGHT = 14,
            HTBOTTOM = 15,
            HTBOTTOMLEFT = 16,
            HTBOTTOMRIGHT = 17,
            HTBORDER = 18,
            HTCLOSE = 20,
            HTHELP = 21,
        };
        public const int WM_GETMINMAXINFO = 0x0024;
        public const int MONITOR_DEFAULTTONEAREST = 2;
        [DllImport("user32.dll")]
        public static extern IntPtr MonitorFromWindow(IntPtr hwnd, int dwFlags);
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }
        [StructLayout(LayoutKind.Sequential)]
        public class MONITORINFOEX {
            public int cbSize;
            public RECT rcMonitor; // The display monitor rectangle  
            public RECT rcWork; // The working area rectangle  
            public int dwFlags;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x20)]
            public char[] szDevice;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT {
            public int x;
            public int y;

            public POINT(int x, int y) {
                this.x = x;
                this.y = y;
            }
            public POINT(Point pt) {
                x = Convert.ToInt32(pt.X);
                y = Convert.ToInt32(pt.Y);
            }
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct MINMAXINFO {
            public POINT ptReserved;
            public POINT ptMaxSize; // The maximized size of the window  
            public POINT ptMaxPosition; // The position of the maximized window  
            public POINT ptMinTrackSize;
            public POINT ptMaxTrackSize;
        }
        [DllImport("user32.dll")]
        public static extern bool GetMonitorInfo(HandleRef hmonitor, [In, Out] MONITORINFOEX monitorInfo);
        public const int WM_NCLBUTTONDOWN = 0x00A1;
        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
    }
}
