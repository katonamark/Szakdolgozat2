using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.InteropServices;


namespace AgentUI;

internal static class RemoteInputService
{
    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool SetCursorPos(int x, int y);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);

    private const uint INPUT_MOUSE = 0;
    private const uint INPUT_KEYBOARD = 1;

    private const uint KEYEVENTF_KEYUP = 0x0002;

    private const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
    private const uint MOUSEEVENTF_LEFTUP = 0x0004;
    private const uint MOUSEEVENTF_RIGHTDOWN = 0x0008;
    private const uint MOUSEEVENTF_RIGHTUP = 0x0010;
    private const uint MOUSEEVENTF_WHEEL = 0x0800;

    public static void ExecuteMouseAction(string action, int x, int y, int delta)
    {
        SetCursorPos(x, y);

        switch (action.ToLowerInvariant())
        {
            case "leftdown":
                SendMouse(MOUSEEVENTF_LEFTDOWN);
                break;

            case "leftup":
                SendMouse(MOUSEEVENTF_LEFTUP);
                break;

            case "leftclick":
                SendMouse(MOUSEEVENTF_LEFTDOWN);
                SendMouse(MOUSEEVENTF_LEFTUP);
                break;

            case "doubleclick":
                SendMouse(MOUSEEVENTF_LEFTDOWN);
                SendMouse(MOUSEEVENTF_LEFTUP);
                Thread.Sleep(60);
                SendMouse(MOUSEEVENTF_LEFTDOWN);
                SendMouse(MOUSEEVENTF_LEFTUP);
                break;

            case "rightclick":
                SendMouse(MOUSEEVENTF_RIGHTDOWN);
                SendMouse(MOUSEEVENTF_RIGHTUP);
                break;

            case "wheel":
                SendMouse(MOUSEEVENTF_WHEEL, delta);
                break;

            case "move":
            default:
                break;
        }
    }

    public static void ExecuteKeyEvent(int keyCode, bool keyDown, bool ctrl, bool alt, bool shift)
    {
        var modifiers = new List<ushort>();

        if (ctrl) modifiers.Add(0x11);   // VK_CONTROL
        if (alt) modifiers.Add(0x12);    // VK_MENU
        if (shift) modifiers.Add(0x10);  // VK_SHIFT

        ushort mainKey = (ushort)keyCode;

        if (keyDown)
        {
            foreach (var modifier in modifiers)
            {
                SendKey(modifier, false);
            }

            SendKey(mainKey, false);
        }
        else
        {
            SendKey(mainKey, true);

            for (int i = modifiers.Count - 1; i >= 0; i--)
            {
                SendKey(modifiers[i], true);
            }
        }
    }

    private static void SendMouse(uint flags, int mouseData = 0)
    {
        var inputs = new[]
        {
            new INPUT
            {
                type = INPUT_MOUSE,
                U = new InputUnion
                {
                    mi = new MOUSEINPUT
                    {
                        dx = 0,
                        dy = 0,
                        mouseData = mouseData,
                        dwFlags = flags,
                        time = 0,
                        dwExtraInfo = IntPtr.Zero
                    }
                }
            }
        };

        SendInput((uint)inputs.Length, inputs, Marshal.SizeOf<INPUT>());
    }

    private static void SendKey(ushort virtualKey, bool keyUp)
    {
        var inputs = new[]
        {
            new INPUT
            {
                type = INPUT_KEYBOARD,
                U = new InputUnion
                {
                    ki = new KEYBDINPUT
                    {
                        wVk = virtualKey,
                        wScan = 0,
                        dwFlags = keyUp ? KEYEVENTF_KEYUP : 0,
                        time = 0,
                        dwExtraInfo = IntPtr.Zero
                    }
                }
            }
        };

        SendInput((uint)inputs.Length, inputs, Marshal.SizeOf<INPUT>());
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct INPUT
    {
        public uint type;
        public InputUnion U;
    }

    [StructLayout(LayoutKind.Explicit)]
    private struct InputUnion
    {
        [FieldOffset(0)]
        public MOUSEINPUT mi;

        [FieldOffset(0)]
        public KEYBDINPUT ki;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct MOUSEINPUT
    {
        public int dx;
        public int dy;
        public int mouseData;
        public uint dwFlags;
        public uint time;
        public IntPtr dwExtraInfo;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct KEYBDINPUT
    {
        public ushort wVk;
        public ushort wScan;
        public uint dwFlags;
        public uint time;
        public IntPtr dwExtraInfo;
    }
}