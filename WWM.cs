using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WindowsWindowManager
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

        /// <summary>
        /// Prints the attributes of a rectangle to the console
        /// </summary>
        /// <param name="rect">Rect object to be print</param>
        public static void printRect(Rect rect)
        {
            Console.WriteLine("Left: " + rect.Left + "\tRight: " + rect.Right);
            Console.WriteLine("Top: " + rect.Top + "\tBottom: " + rect.Bottom);
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

        [DllImport("user32.dll")]
        private static extern bool MoveWindow(IntPtr handle, int x, int y, int width, int height, bool redraw);

        [DllImport("user32.dll")]
        private static extern bool ShowWindowAsync(int hWnd, int nCmdShow);

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
        public static IntPtr getForegroundWindow()
        {
            return GetForegroundWindow();
        }


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

        /// <summary>
        /// Maximizes Window of given Process
        /// </summary>
        /// <param name="proc">Process to have window be maximized</param>
        /// <returns>Success status</returns>
        public static bool showWindowMaximized(Process proc)
        {
            return ShowWindowAsync((int)proc.MainWindowHandle, (int)SW.SHOWMAXIMIZED);
        }

        /// <summary>
        /// Maximizes Window of gvien Handle
        /// </summary>
        /// <param name="handle">handle of window to be maximized</param>
        /// <returns>Success status</returns>
        public static bool showWindowMaximized(IntPtr handle)
        {
            return ShowWindowAsync((int)handle, (int)SW.SHOWMAXIMIZED);
        }

        /// <summary>
        /// Minimizes Window of given Process
        /// </summary>
        /// <param name="proc">Process to have window be minimized</param>
        /// <returns>Success status</returns>
        public static bool showWindowMinimized(Process proc)
        {
            return ShowWindowAsync((int)proc.MainWindowHandle, (int)SW.MINIMIZE);
        }

        /// <summary>
        /// Minimizes Window of gvien Handle
        /// </summary>
        /// <param name="handle">handle of window to be minimized</param>
        /// <returns>Success status</returns>
        public static bool showWindowMinimized(IntPtr handle)
        {
            return ShowWindowAsync((int)handle, (int)SW.MINIMIZE);
        }
        
        /// <summary>
        /// Hides Window of gvien Process 
        /// </summary>
        /// <param name="proc">handle of window to be hidden</param>
        /// <returns>Success status</returns>
        public static bool hideWindow(Process proc)
        {
            return ShowWindowAsync((int)proc.MainWindowHandle, (int)SW.HIDE);
        }
        
        /// <summary>
        /// Hides Window of gvien Handle
        /// </summary>
        /// <param name="handle">handle of window to be hidden</param>
        /// <returns>Success status</returns>
        public static bool hideWindow(IntPtr handle)
        {
            return ShowWindowAsync((int)handle, (int)SW.HIDE);
        }
        /// <summary>
        /// Shows Window of gvien Process
        /// </summary>
        /// <param name="proc">Process of window to be shown</param>
        /// <returns>Success status</returns>
        public static bool showWindow(Process proc)
        {
            return ShowWindowAsync((int)proc.MainWindowHandle, (int)SW.SHOW);
        }

        /// <summary>
        /// Shows Window of gvien Handle
        /// </summary>
        /// <param name="handle">Handle of window to be shown</param>
        /// <returns>Success status</returns>
        public static bool showWindow(IntPtr handle)
        {
            return ShowWindowAsync((int)handle, (int)SW.SHOW);
        }

        
        /// <summary>
        /// Makes a window display windowed. Not Maximized and Not Minimized.
        /// </summary>
        /// <param name="proc">Process of window to be displayed windowed</param>
        /// <returns>Success Status</returns>
        public static bool showWindowNormal(Process proc)
        {
            return ShowWindowAsync((int)proc.MainWindowHandle, (int)SW.SHOWNORMAL);
        }

        /// <summary>
        /// Makes a window display windowed. Not Maximized and Not Minimized.
        /// </summary>
        /// <param name="handle">Handle of window to be displayed windowed</param>
        /// <returns>Success Status</returns>
        public static bool showWindowNormal(IntPtr handle)
        {
            return ShowWindowAsync((int)handle, (int)SW.SHOWNORMAL);
        }



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
        /// Gets the rect object from the given handle
        /// </summary>
        /// <param name="handle">Handle you want the window rect for</param>
        /// <returns>Rect object of the process's window</returns>
        public static Rect getWindowRect(IntPtr handle)
        {
            Rect output = new Rect();
            GetWindowRect(handle, ref output);
            return output;
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




        /// <summary>
        /// Move the window of a given process to the given coordinates.
        /// The coordinates represent the top left coordinate of the window.
        /// </summary>
        /// <param name="proc">Process object of the window intended to be moved</param>
        /// <param name="x">X coordinate of the top left corner of the window</param>
        /// <param name="y">Y coordinate of the top left corner of the window</param>
        public static void moveWindowTo(Process proc, int x, int y)
        {
            Rect windowRect = getWindowRect(proc);
            MoveWindow(proc.MainWindowHandle, x, y, windowRect.Right - windowRect.Left, windowRect.Bottom - windowRect.Top, true);
        }

        /// <summary>
        /// Move the window of a given handle to the given coordinates.
        /// The coordinates represent the top left coordinate of the window.
        /// </summary>
        /// <param name="handle">Handle of the window intended to be moved</param>
        /// <param name="x">X coordinate of the top left corner of the window</param>
        /// <param name="y">Y coordinate of the top left corner of the window</param>
        public static void moveWindowTo(IntPtr handle, int x, int y)
        {
            Rect windowRect = getWindowRect(handle);
            MoveWindow(handle, x, y, windowRect.Right - windowRect.Left, windowRect.Bottom - windowRect.Top, true);
        }

        /// <summary>
        /// Resize window of the given handle by given height and width.
        /// </summary>
        /// <param name="proc">Handle of the window to be resized</param>
        /// <param name="height">New window height</param>
        /// <param name="width">New window width</param>
        public static void resizeWindow(Process proc, int height, int width)
        {
            resizeWindow(proc.MainWindowHandle, height, width);
        }

        /// <summary>
        /// Resize window of the given handle by given height and width.
        /// </summary>
        /// <param name="handle">Handle of the window to be resized</param>
        /// <param name="height">New window height</param>
        /// <param name="width">New window width</param>
        public static void resizeWindow(IntPtr handle, int height, int width)
        {
            if (height < 0) height = 0;
            if (width < 0) width = 0;
            Rect windowRect = getWindowRect(handle);
            MoveWindow(handle, windowRect.Left, windowRect.Top, height, width, true);
        }

    }
}
