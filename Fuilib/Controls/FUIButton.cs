using Fuilib.Core;
using Fuilib.Utils;
using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace Fuilib.Controls;

public class FUIButton : FUIControl
{
    [Category("Appearance")]
    [DisplayName("1) Content")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public string Content
    {
        get => _content;
        set
        {
            _content = value;
            this.Invalidate();
        }
    }
    private string _content = "Button";

    [Category("Appearance")]
    [DisplayName("2) Radius")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int Radius
    {
        get => _radius;
        set
        {
            _radius = value;
            this.Invalidate();
        }
    }
    private int _radius = 5;
    
    [Category("Appearance")]
    [DisplayName("3) Border Size")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int Border
    {
        get => _border;
        set
        {
            _border = value;
            this.Invalidate();
        }
    }
    private int _border = 1;

    private Color _currentColor;
    private Color _bgColor = Theme.GetColor(Theme.ThemeMode.Dark, Theme.Element.Button_Background);
    private Color _borderUpperColor = Theme.GetColor(Theme.ThemeMode.Dark, Theme.Element.Button_Border);
    private Color _borderLowerColor = Color.Transparent;

    public FUIButton()
    {
        _currentColor = _bgColor;
    }
    
    protected override void OnMouseDown(MouseEventArgs e)
    {
        _currentColor = _bgColor.Lerp(Color.Black, 0.03f);
        this.Invalidate();
        base.OnMouseDown(e);
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
        _currentColor = _bgColor;
        this.Invalidate();
        base.OnMouseUp(e);
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
        _currentColor = _bgColor.Lerp(Color.White, 0.03f);
        this.Invalidate();
        base.OnMouseMove(e);
    }

    protected override void OnMouseLeave(EventArgs e)
    {
        _currentColor = _bgColor;
        this.Invalidate();
        base.OnMouseLeave(e);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        Font _font = new Font("Yu Gothic UI", 9);
        var rectangle = new Rectangle(Border, Border, this.Width - Border * 2, this.Height - Border * 2);
        var path = rectangle.Round(Radius);
        e.Graphics.FillPath(new SolidBrush(_currentColor), path);

        using (LinearGradientBrush brush = new LinearGradientBrush(rectangle, _borderUpperColor.Lerp(_bgColor, 0.7f), _borderLowerColor.Lerp(_bgColor, 0.7f), LinearGradientMode.Vertical)) {
            if (Border > 0)
                e.Graphics.DrawPath(new Pen(brush, Border), path); 
        }

        var textSize = e.Graphics.MeasureString(Content, _font);
        e.Graphics.DrawString(Content, _font, Brushes.White, rectangle.Width / 2 - textSize.Width / 2 + 1, rectangle.Height / 2 - textSize.Height / 2 + 1);
    }
}