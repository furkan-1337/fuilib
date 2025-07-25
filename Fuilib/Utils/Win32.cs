using System.Runtime.InteropServices;

namespace Fuilib.Utils;

internal class Win32
{
    [DllImport("user32.dll")]
    public static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);
    
    [DllImport("dwmapi.dll", PreserveSig = true)]
    public static extern int DwmSetWindowAttribute(IntPtr hwnd, DwmWindowAttribute attribute, ref DwmWindowCornerPreference pvAttribute, int cbAttribute);
    
    [DllImport("dwmapi.dll")]
    public static extern int DwmSetWindowAttribute(IntPtr hwnd, int dwAttribute, ref int pvAttribute, int cbAttribute);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern int SetClassLong(IntPtr hwnd, int nIndex, int dwNewLong);
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern int GetClassLong(IntPtr hwnd, int nIndex);
    
    [DllImport("user32.dll")]
    public static extern bool SetWindowDisplayAffinity(IntPtr hwnd, DisplayAffinity affinity);
    
    public static void SetWindowComposition(IntPtr HWnd, Win32.AccentPolicy accent, bool hasFrame = true)
    {
        if (Environment.OSVersion.Version.Major >= 6)
        {
            if (hasFrame)
                accent.AccentFlags = 0x20 | 0x40 | 0x80 | 0x100;

            int accentStructSize = Marshal.SizeOf(accent);

            IntPtr accentPtr = Marshal.AllocHGlobal(accentStructSize);
            Marshal.StructureToPtr(accent, accentPtr, false);

            Win32.WindowCompositionAttributeData data = new Win32.WindowCompositionAttributeData();
            data.Attribute = Win32.WindowCompositionAttribute.WCA_ACCENT_POLICY;
            data.SizeOfData = accentStructSize;
            data.Data = accentPtr;

            Win32.SetWindowCompositionAttribute(HWnd, ref data);

            Marshal.FreeHGlobal(accentPtr);
        }
        else
            throw new NotSupportedException("This feature requires Windows Vista or later.");
    }
    
    public enum WindowCompositionAttribute
    {
        WCA_UNDEFINED = 0,
        WCA_NCRENDERING_ENABLED = 1,
        WCA_NCRENDERING_POLICY = 2,
        WCA_TRANSITIONS_FORCEDISABLED = 3,
        WCA_ALLOW_NCPAINT = 4,
        WCA_CAPTION_BUTTON_BOUNDS = 5,
        WCA_NONCLIENT_RTL_LAYOUT = 6,
        WCA_FORCE_ICONIC_REPRESENTATION = 7,
        WCA_EXTENDED_FRAME_BOUNDS = 8,
        WCA_HAS_ICONIC_BITMAP = 9,
        WCA_THEME_ATTRIBUTES = 10,
        WCA_NCRENDERING_EXILED = 11,
        WCA_NCADORNMENTINFO = 12,
        WCA_EXCLUDED_FROM_LIVEPREVIEW = 13,
        WCA_VIDEO_OVERLAY_ACTIVE = 14,
        WCA_FORCE_ACTIVEWINDOW_APPEARANCE = 15,
        WCA_DISALLOW_PEEK = 16,
        WCA_CLOAK = 17,
        WCA_CLOAKED = 18,
        WCA_ACCENT_POLICY = 19,
        WCA_FREEZE_REPRESENTATION = 20,
        WCA_EVER_UNCLOAKED = 21,
        WCA_VISUAL_OWNER = 22,
        WCA_HOLOGRAPHIC = 23,
        WCA_EXCLUDED_FROM_DDA = 24,
        WCA_PASSIVEUPDATEMODE = 25,
        WCA_LAST = 26
    }

    public enum AccentState
    {
        ACCENT_DISABLED = 0,
        ACCENT_ENABLE_GRADIENT = 1,
        ACCENT_ENABLE_TRANSPARENTGRADIENT = 2,
        ACCENT_ENABLE_BLURBEHIND = 3,
        ACCENT_ENABLE_ACRYLICBLURBEHIND = 4, // RS4 1803
        ACCENT_ENABLE_HOSTBACKDROP = 5, // RS5 1809
        ACCENT_INVALID_STATE = 6
    }

    public enum DisplayAffinity : uint
    {
        None = 0,
        Monitor = 1
    }
    
    public enum DwmWindowAttribute
    {
        DWMWA_WINDOW_CORNER_PREFERENCE = 33,
        DWMWA_SYSTEMBACKDROP_TYPE = 38
    }

    public enum DwmWindowCornerPreference
    {
        Default = 0,
        DoNotRound = 1,
        Round = 2,
        RoundSmall = 3
    }
    
    [StructLayout(LayoutKind.Sequential)]
    public struct WindowCompositionAttributeData
    {
        public WindowCompositionAttribute Attribute;
        public IntPtr Data;
        public int SizeOfData;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct AccentPolicy
    {
        public AccentState AccentState;
        public int AccentFlags;
        public int GradientColor;
        public int AnimationId;
    }
}