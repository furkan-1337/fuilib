namespace Fuilib.Core;

public class Theme
{
    public static Dictionary<ThemeMode, Dictionary<Element, Color>> Colors { get; set; }

    static Theme()
    {
        Colors = new Dictionary<ThemeMode, Dictionary<Element, Color>>();
        Colors.Add(ThemeMode.Dark, new Dictionary<Element, Color>()
        {
            {Element.Window_Background, Color.FromArgb(10, 10, 10)}
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
        Window_Background
    }
}