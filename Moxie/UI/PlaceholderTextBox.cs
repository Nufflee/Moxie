using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace Moxie
{
  public class PlaceholderTextBox : TextBox
  {
    public static readonly DependencyProperty placeholderTextProperty = DependencyProperty.Register("Placeholder", typeof(string), typeof(PlaceholderTextBox));

    public string PlaceholderText
    {
      get { return (string)GetValue(placeholderTextProperty); }
      set { SetValue(placeholderTextProperty, value); }
    }

    public bool IsPlaceholderActive
    {
      get { return Text == PlaceholderText && Foreground.Equals(Brushes.Gray); }
    }

    public PlaceholderTextBox()
    {
      Loaded += AddPlaceholderText;
      GotFocus += RemovePlaceholderText;
      PreviewGotKeyboardFocus += RemovePlaceholderText;
      GotKeyboardFocus += RemovePlaceholderText;
      LostFocus += AddPlaceholderText;
    }

    void AddPlaceholderText(object sender, RoutedEventArgs e)
    {
      if (!string.IsNullOrEmpty(Text))
        return;

      Text = PlaceholderText;
      Foreground = Brushes.Gray;
    }

    void RemovePlaceholderText(object sender, RoutedEventArgs e)
    {
      if (!IsPlaceholderActive)
        return;

      Text = "";
      Foreground = Brushes.Black;
    }
  }
}