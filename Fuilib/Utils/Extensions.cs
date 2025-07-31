using System.Drawing.Drawing2D;

namespace Fuilib.Utils;

public static class Extensions
{
    public static Color Lerp(this Color color1, Color color2, float t)
    {
        t = Math.Clamp(t, 0f, 1f);
        int r = (int)(color1.R + (color2.R - color1.R) * t);
        int g = (int)(color1.G + (color2.G - color1.G) * t);
        int b = (int)(color1.B + (color2.B - color1.B) * t);
        int a = (int)(color1.A + (color2.A - color1.A) * t);
        return Color.FromArgb(a, r, g, b);
    }
    
    public static GraphicsPath Round(this Rectangle rect, int radius)
    {
        GraphicsPath path = new GraphicsPath();
        int diameter = radius * 2;

        // Üst sol köşe
        path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
        // Üst kenar
        path.AddLine(rect.X + radius, rect.Y, rect.Right - radius, rect.Y);
        // Üst sağ köşe
        path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
        // Sağ kenar
        path.AddLine(rect.Right, rect.Y + radius, rect.Right, rect.Bottom - radius);
        // Alt sağ köşe
        path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
        // Alt kenar
        path.AddLine(rect.Right - radius, rect.Bottom, rect.X + radius, rect.Bottom);
        // Alt sol köşe
        path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
        // Sol kenar
        path.AddLine(rect.X, rect.Bottom - radius, rect.X, rect.Y + radius);

        path.CloseFigure();
        return path;
    }
}