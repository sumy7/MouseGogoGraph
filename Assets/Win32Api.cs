﻿using System;
using System.Runtime.InteropServices;

public class Win32Api
{
    public const int WH_MOUSE_LL = 14;

    public static IntPtr MAR = LoadLibrary("user32.dll");

    [StructLayout(LayoutKind.Sequential)]
    public class POINT
    {
        public int x;
        public int y;
    }
    [StructLayout(LayoutKind.Sequential)]
    public class MouseHookStruct
    {
        public POINT pt;
        public int hwnd;
        public int wHitTestCode;
        public int dwExtraInfo;
    }
    public delegate int HookProc(int nCode, IntPtr wParam, IntPtr lParam);

    [DllImport("user32.dll", SetLastError = true)]
    public static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, int threadId);
    [DllImport("user32.dll")]
    public static extern bool UnhookWindowsHookEx(int idHook);
    [DllImport("user32.dll")]
    public static extern int CallNextHookEx(int idHook, int nCode, IntPtr wParam, IntPtr lParam);

    [DllImport("kernel32.dll")]
    public static extern IntPtr LoadLibrary(string dllToLoad);


    //[DllImport("user32.dll")]
    //[return: MarshalAs(UnmanagedType.Bool)]
    //public static extern bool GetCursorPos(out POINT pt);
}