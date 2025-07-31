using Fuilib.Utils;

namespace Fuilib.Windows.Effects;

public class Blur
{
    public static void Apply(FUIWindow window)
    {
        Win32.SetWindowComposition(window.Handle, new Win32.AccentPolicy() { AccentState = Win32.AccentState.ACCENT_ENABLE_BLURBEHIND }, window.DropShadow);
    }
}