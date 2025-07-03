using Microsoft.Win32;
using System.Windows.Media;

namespace WebViewLib
{
    public static class ThemeManager
    { 
        public static ThemeModel Theme { get; } = new ThemeModel();

        public static string ToRgbString(SolidColorBrush brush)
        {
            Color color = brush.Color;
            return $"rgb({color.R}, {color.G}, {color.B})";
        }

        public static string ToRgbString(Color color) =>
             $"rgb({color.R}, {color.G}, {color.B})";
    }

    public class ThemeModel : ViewModelBase
    {
        private bool _doNotChangeDocumentColors;
        private SolidColorBrush _background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));
        private SolidColorBrush _foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#000000"));

        public  string LightForegroundColorString => "#000000";
        public  string LightBackgroundColorString => "#FFFFFF";
        public  string DarkForegroundColorString => "#FFFFFF";
        public  string DarkBackgroundColorString => "#1E1E1E";

        public  SolidColorBrush Background {   get => _background;  set => SetProperty(ref _background, value);}
        public  SolidColorBrush Foreground {   get => _foreground;  set => SetProperty(ref _foreground, value); }
        public  bool DoNotChangeDocumentColors {   get => _doNotChangeDocumentColors;  set => SetProperty(ref _doNotChangeDocumentColors, value);}

        public ThemeModel()
        {
            //DetectSystemTheme();
        }

        public void ToggleDarkMode(bool isDarkMode)
        {
            if (isDarkMode)
            {
                Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(DarkBackgroundColorString));
                Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(DarkForegroundColorString));
            }
            else
            {
                Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(LightBackgroundColorString));
                Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(LightForegroundColorString));
            }
        }

        public void DetectSystemTheme()
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize"))
                {
                    if (key != null)
                    {
                        object value = key.GetValue("AppsUseLightTheme");
                        if (value is int reg)
                        {
                            bool isLight = reg != 0;
                            Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(isLight ? LightBackgroundColorString : DarkBackgroundColorString));
                            Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(isLight ? LightForegroundColorString : DarkForegroundColorString));
                        }
                    }
                }
            }
            catch
            {
                Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(LightBackgroundColorString));
                Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(LightForegroundColorString));
            }
        }
    }
}
