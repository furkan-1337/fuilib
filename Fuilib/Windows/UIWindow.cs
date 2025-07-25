using Fuilib.Core;
using System.ComponentModel;

namespace Fuilib.Windows;

public class UIWindow : Form
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
    [DisplayName("2) Drop Shadow")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool DropShadow { get; set; }
    
    [Category("Appearance")]
    [DisplayName("3) Rounded Corners (Win11+)")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool RoundedCorners { get; set; }
    
    public UIWindow()
    {
        this.StartPosition = FormStartPosition.CenterScreen;
        this.FormBorderStyle = FormBorderStyle.None;
        this.BackColor = Theme.GetColor(Mode, Theme.Element.Window_Background);
    }

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
}