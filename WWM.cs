using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WindowsWindowManagerV1
{
    public class WWM
    {
        /// <summary>
        /// Rectangle Struct of a window defined here: https://www.pinvoke.net/default.aspx/user32.getwindowrect
        /// </summary>
        public struct Rect
        {
            /// <summary>
            /// X position of upper-left corner
            /// </summary>
            public int Left { get; set; }
            /// <summary>
            /// Y position of upper-left corner
            /// </summary>
            public int Top { get; set; }
            /// <summary>
            /// X position of lower-right corner
            /// </summary>
            public int Right { get; set; }
            /// <summary>
            /// Y position of lower-right corner
            /// </summary>
            public int Bottom { get; set; }
        }

        public static void printRect(Rect rect)
        {
            Console.WriteLine("Left: " + rect.Left + "\tRight: " + rect.Right);
            Console.WriteLine("Top: " + rect.Top + "\tBottom: " + rect.Bottom);
        }



        [DllImport("user32.dll")]
		private static extern bool ShowWindowAsync(int hWnd, int nCmdShow);

        // An enumeration containing all the possible SW values.
        private enum SW : int
        {
             HIDE = 0,
             SHOWNORMAL = 1,
             SHOWMINIMIZED = 2,
             SHOWMAXIMIZED = 3,
             SHOWNOACTIVATE = 4,
             SHOW = 5,
             MINIMIZE = 6,
             SHOWMINNOACTIVE = 7,
             SHOWNA = 8,
             RESTORE = 9,
             SHOWDEFAULT = 10
        }

        public static bool showWindowMaximized(Process proc) {
            return ShowWindowAsync((int)proc.MainWindowHandle, (int)SW.SHOWMAXIMIZED);
        }
        public static bool showWindowMaximized(IntPtr handle) {
            return ShowWindowAsync((int)handle, (int)SW.SHOWMAXIMIZED);
        }
        public static bool showWindowMinimized(Process proc) {
            return ShowWindowAsync((int)proc.MainWindowHandle, (int)SW.MINIMIZE);
        }
        public static bool showWindowMinimized(IntPtr handle) {
            return ShowWindowAsync((int)handle, (int)SW.MINIMIZE);
        }
        public static bool hideWindow(Process proc) {
            return ShowWindowAsync((int)proc.MainWindowHandle, (int)SW.HIDE);
        }
        public static bool showWindow(Process proc) {
            return ShowWindowAsync((int)proc.MainWindowHandle, (int)SW.SHOW);
        }
        public static bool showWindowNormal(Process proc) {
            return ShowWindowAsync((int)proc.MainWindowHandle, (int)SW.SHOWNORMAL);
        }
        public static bool showWindowNormal(IntPtr handle) {
            return ShowWindowAsync((int)handle, (int)SW.SHOWNORMAL);
        }
        public static bool hideWindow(IntPtr handle) {
            return ShowWindowAsync((int)handle, (int)SW.HIDE);
        }
        public static bool showWindow(IntPtr handle) {
            return ShowWindowAsync((int)handle, (int)SW.SHOW);
        }



        /// <summary>
        /// Given a handlle and a struct reference, this will save the window's rect values into the passed rect
        /// https://www.pinvoke.net/default.aspx/user32.getwindowrect
        /// </summary>
        /// <param name="hwnd">Window handle</param>
        /// <param name="rectangle">Rectangle struct reference</param>
        /// <returns>Success status</returns>
        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hwnd, ref Rect rectangle);

        /// <summary>
        /// Gets the rect object from the given process
        /// </summary>
        /// <param name="proc">Process you want the window rect for</param>
        /// <returns>Rect object of the process's window</returns>
        public static Rect getWindowRect(Process proc)
        {
            Rect output = new Rect();
            GetWindowRect(proc.MainWindowHandle, ref output);
            return output;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        public static Rect getWindowRect(IntPtr handle)
        {
            Rect output = new Rect();
            GetWindowRect(handle, ref output);
            return output;
        }



        /// <summary>
        ///     Retrieves a handle to the foreground window (the window with which the user is currently working). The system
        ///     assigns a slightly higher priority to the thread that creates the foreground window than it does to other threads.
        ///     <para>See https://msdn.microsoft.com/en-us/library/windows/desktop/ms633505%28v=vs.85%29.aspx for more information.</para>
        /// </summary>
        /// <returns>
        ///     C++ ( Type: Type: HWND )<br /> The return value is a handle to the foreground window. The foreground window
        ///     can be NULL in certain circumstances, such as when a window is losing activation.
        /// </returns>
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();
        public static IntPtr getForegroundWindow() {
            return GetForegroundWindow();
        }



        /// <summary>
        /// Waits for the window of the process passed to be visible, specifically when the window has width and height > 0
        /// </summary>
        /// <param name="proc">Process of the window you are waiting for.</param>
        /// <param name="interval">Time in milliseconds between checking to see if window is visible. The default is 10.</param>
        /// <param name="timeout">Time until timeout exception in milliseconds. 0 means no timeout and is the default value.</param>
        /// <exception cref="TimeoutException">Timeout Exception thrown after timeout time exceeded.</exception>
        public static void waitForWindow(Process proc, int interval = 10, int timeout = 0)
        {
            int i = 0;
            Rect WindowRect = new Rect();
            do
            {
                if ((i++) * interval > timeout && timeout > 0)
                {
                    throw new TimeoutException();
                }
                System.Threading.Thread.Sleep(interval);
                GetWindowRect(proc.MainWindowHandle, ref WindowRect);
            } while (WindowRect.Left == WindowRect.Right || WindowRect.Top == WindowRect.Bottom);
        }



        [DllImport("user32.dll")]
        private static extern bool MoveWindow(IntPtr handle, int x, int y, int width, int height, bool redraw);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="proc"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public static void moveWindowTo(Process proc, int x, int y)
        {
            Rect windowRect = getWindowRect(proc);
            MoveWindow(proc.MainWindowHandle, x, y, windowRect.Right - windowRect.Left, windowRect.Bottom - windowRect.Top, true);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public static void moveWindowTo(IntPtr handle, int x, int y)
        {
            Rect windowRect = getWindowRect(handle);
            MoveWindow(handle, x, y, windowRect.Right - windowRect.Left, windowRect.Bottom - windowRect.Top, true);
        }

        public static void resizeWindow(Process proc ,int height,int width) { 
            //TODO
        }
        public static void resizeWindow(IntPtr handle ,int height,int width) { 
            //TODO
        }

    }
}
