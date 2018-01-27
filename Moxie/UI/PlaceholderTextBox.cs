using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Moxie.UI
{
  public class PlaceholderTextBox : TextBox
  {
    private static readonly DependencyProperty placeholderTextProperty = DependencyProperty.Register("Placeholder", typeof(string), typeof(PlaceholderTextBox));

    public string PlaceholderText
    {
      get => (string)GetValue(placeholderTextProperty);
      set => SetValue(placeholderTextProperty, value);
    }

    public bool IsPlaceholderActive => Text == PlaceholderText && Foreground.Equals(Brushes.Gray);

    public PlaceholderTextBox()
    {
      Loaded += AddPlaceholderText;
      GotFocus += RemovePlaceholderText;
      PreviewGotKeyboardFocus += RemovePlaceholderText;
      GotKeyboardFocus += RemovePlaceholderText;
      LostFocus += AddPlaceholderText;
    }

    private void AddPlaceholderText(object sender, RoutedEventArgs e)
    {
      if (!string.IsNullOrEmpty(Text))
        return;

      Text = PlaceholderText;
      Foreground = Brushes.Gray;
    }

    private void RemovePlaceholderText(object sender, RoutedEventArgs e)
    {
      if (!IsPlaceholderActive)
        return;

      Text = "";
      Foreground = Brushes.Black;
    }
  }
}