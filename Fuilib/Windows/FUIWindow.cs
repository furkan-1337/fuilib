using Fuilib.Core;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using Fuilib.Utils;

namespace Fuilib.Windows;

public class FUIWindow : Form
{
    [Category("Appearance")]
    [DisplayName("1) Mode")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Theme.ThemeMode Mode { get; set; } = Theme.ThemeMode.Dark;
    
    [Category("Appearance")]
    [DisplayName("2) Blur Behind")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool Blur { get; set; }
    
    [Category("Appearance")]
    [DisplayName("3) Drop Shadow")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool DropShadow { get; set; }
    
    [Category("Appearance")]
    [DisplayName("4) Rounded Corners (Win11+)")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool RoundedCorners { get; set; }

    [Category("Controlbar")]
    [DisplayName("1) Title")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public string Title
    {
        get => _title;
        set
        {
            _title = value;
            this.Text = value;
            this.Invalidate();
        }
    }
    private string _title = "fuilib";
    
    [Category("Controlbar")]
    [DisplayName("2) Shadow Color")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]

    public Color Control_DropShadow
    {
        get => _shadow;
        set
        {
            _shadow = value;
            this.Invalidate();
        }
    }
    private Color _shadow = Color.Black;
    
    public FUIWindow()
    {
        this.StartPosition = FormStartPosition.CenterScreen;
        this.FormBorderStyle = FormBorderStyle.None;
        this.BackColor = Theme.GetColor(Mode, Theme.Element.Window_Background);
    }
    private Font _font = new Font("Yu Gothic UI", 12);
    protected override void OnHandleCreated(EventArgs e)
    {
        base.OnHandleCreated(e);
        if(DropShadow)
            Effects.Shadow.Apply(this);
        if(Blur)
            Effects.Blur.Apply(this);
        if(RoundedCorners)
            Effects.RoundedCorners.Apply(this);
    }
    
    protected override void OnPaint(PaintEventArgs e)
    {
        e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
        e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
        e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
        
        Rectangle controlBar = new Rectangle(0, 0, this.Width, 35);
        Rectangle redLight = new Rectangle(0, 34, this.Width, 15);
        
        var bgColor = Theme.GetColor(Mode, Theme.Element.Window_Background);
        var controlColor = Theme.GetColor(Mode, Theme.Element.Controlbar_Background);
        
        e.Graphics.FillRectangle(new SolidBrush(controlColor), controlBar);
        using (LinearGradientBrush brush = new LinearGradientBrush(redLight, Control_DropShadow.Lerp(bgColor, 0.7f), Color.Transparent, LinearGradientMode.Vertical)) {
            e.Graphics.FillRectangle(brush, redLight);
        }
        
        var textSize = e.Graphics.MeasureString(Title, _font);
        e.Graphics.DrawString(Title, _font, Brushes.White, this.Width / 2 - textSize.Width / 2, controlBar.Height / 2 - textSize.Height / 2);
        base.OnPaint(e);
    }
}