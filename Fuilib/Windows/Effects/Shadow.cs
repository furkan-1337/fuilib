using Fuilib.Utils;

namespace Fuilib.Windows.Effects;

public class Shadow
{
    public static void Apply(UIWindow window)
    {
        Win32.SetWindowComposition(window.Handle, new Win32.AccentPolicy() { AccentState = window.Blur ? Win32.AccentState.ACCENT_ENABLE_BLURBEHIND : Win32.AccentState.ACCENT_ENABLE_GRADIENT }, true);
    }
}