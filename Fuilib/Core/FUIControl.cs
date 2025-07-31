using System.Drawing.Drawing2D;

namespace Fuilib.Core;

public class FUIControl : Control
{
    public FUIControl()
    {
        this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        this.DoubleBuffered = true;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
        e.Graphics.PixelOffsetMode = PixelOffsetMode.None;
        base.OnPaint(e);
    }
}