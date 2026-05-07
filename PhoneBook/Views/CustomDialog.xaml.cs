using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PhoneBook.Views
{
    public partial class CustomDialog : Window
    {
        public CustomDialog(string message, string title, DialogType type, bool showCancel = false)
        {
            InitializeComponent();

            TitleText.Text = title;
            MessageText.Text = message;

            // Настройка внешнего вида в РОЗОВОЙ гамме
            switch (type)
            {
                case DialogType.Info:
                    SetAppearance("#FF69B4", "i", "#FF69B4"); // HotPink
                    break;
                case DialogType.Warning:
                    SetAppearance("#FF1493", "!", "#FF1493"); // DeepPink
                    break;
                case DialogType.Error:
                    SetAppearance("#C71585", "✕", "#C71585"); // MediumVioletRed (ошибка чуть строже)
                    break;
                case DialogType.Confirmation:
                    SetAppearance("#FF1493", "?", "#FF1493"); // DeepPink
                    break;
            }

            CancelButton.Visibility = showCancel ? Visibility.Visible : Visibility.Collapsed;

            OkButton.Click += (s, e) => DialogResult = true;
            CancelButton.Click += (s, e) => DialogResult = false;
        }

        private void SetAppearance(string colorHex, string icon, string iconColor)
        {
            var brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(colorHex));
            IconBorder.Background = brush;
            IconText.Text = icon;
            IconText.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Cornsilk"));
            OkButton.Background = brush;
            TitleText.Foreground = brush;
        }

        public enum DialogType { Info, Warning, Error, Confirmation }
    }
}