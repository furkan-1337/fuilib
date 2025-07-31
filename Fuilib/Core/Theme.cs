namespace Fuilib.Core;

public class Theme
{
    public static Dictionary<ThemeMode, Dictionary<Element, Color>> Colors { get; set; }

    static Theme()
    {
        Colors = new Dictionary<ThemeMode, Dictionary<Element, Color>>();
        Colors.Add(ThemeMode.Dark, new Dictionary<Element, Color>()
        {
            {Element.Window_Background, Color.FromArgb(14, 14, 14)},
            {Element.Controlbar_Background, Color.FromArgb(254,12, 12, 12)},
            {Element.Button_Background, Color.FromArgb(254,24, 24, 24)},
            {Element.Button_Border, Color.FromArgb(254,68, 68, 68)},
        });
    }

    public static Color GetColor(ThemeMode themeType, Element element)
    {
        return Colors[themeType][element];   
    }
    
    public enum ThemeMode
    {
        Light,
        Dark,
    }

    public enum Element
    {
        Window_Background,
        Controlbar_Background,
        Button_Background,
        Button_Border,
    }
}