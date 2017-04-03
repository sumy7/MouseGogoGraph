using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class MouseHook
{
    public int hHook = 0;
    public Win32Api.HookProc hookProcedure;

    private int mouseX;
    private int mouseY;

    private bool mouseHooked = false;

    public bool MouseHooked
    {
        get
        {
            return mouseHooked;
        }
    }

    public MouseHook() { }

    public void hook()
    {
        if (this.hHook != 0) return;
        Debug.Log("mouse hook");
        this.hookProcedure = new Win32Api.HookProc(this.MouseHookProc);
        this.hHook = Win32Api.SetWindowsHookEx(Win32Api.WH_MOUSE_LL, hookProcedure, Win32Api.MAR, 0);
        if (this.hHook == 0)
        {
            Debug.Log("Hook failed. ErrorCode: " + Marshal.GetLastWin32Error());
            return;
        }
        mouseHooked = true;
    }

    public void unhook()
    {
        mouseHooked = false;
        bool ret = Win32Api.UnhookWindowsHookEx(hHook);
        if (!ret)
        {
            Debug.Log("UnHook failed.");
            return;
        }
        hHook = 0;
    }

    public int MouseHookProc(int nCode, IntPtr wParam, IntPtr lParam)
    {
        Win32Api.MouseHookStruct MyMouseHookStruct = (Win32Api.MouseHookStruct)Marshal.PtrToStructure(lParam, typeof(Win32Api.MouseHookStruct));
        if (nCode >= 0)
        {
            this.mouseX = MyMouseHookStruct.pt.x;
            this.mouseY = MyMouseHookStruct.pt.y;
        }
        return Win32Api.CallNextHookEx(hHook, nCode, wParam, lParam);
    }

    public int getPointX()
    {
        return this.mouseX;
    }

    public int getPointY()
    {
        return this.mouseY;
    }
}