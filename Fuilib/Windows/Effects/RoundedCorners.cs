using Fuilib.Utils;

namespace Fuilib.Windows.Effects;

public class RoundedCorners
{
    public static void Apply(FUIWindow window)
    {
        if (Environment.OSVersion.Version.Build >= 22000)
        {
            if(window.DropShadow)
                throw new NotSupportedException("This effect does not support drop shadow.");
            
            var preference = Win32.DwmWindowCornerPreference.Round;
            Win32.DwmSetWindowAttribute(window.Handle, Win32.DwmWindowAttribute.DWMWA_WINDOW_CORNER_PREFERENCE,
                ref preference, sizeof(int));
        }
        else
            throw new NotSupportedException("This effect requires Windows 11 or later.");
    }
}