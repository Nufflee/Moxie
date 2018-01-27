using System;
using System.Windows;
using System.Windows.Input;
using Moxie.Common;

namespace Moxie.Client.UI
{
  /// <summary>
  /// Interaction logic for ClientWindow.xaml
  /// </summary>
  public partial class ClientWindow : Window
  {
    private Client client;

    public ClientWindow(string name, IP4 address)
    {
      InitializeComponent();

      client = new Client(name, address, this);
    }

    private void OnLoaded_TextMessage(object sender, RoutedEventArgs e)
    {
      TextMessage.Focus();
    }

    private void OnKeyDown_TextMessage(object sender, KeyEventArgs e)
    {
      if (e.Key == Key.Enter)
      {
        client.SendMessage(TextMessage.Text);
      }
    }

    private void OnClick_ButtonSend(object sender, RoutedEventArgs e)
    {
      client.SendMessage(TextMessage.Text);
    }

    public void Print(string message)
    {
      TextHistory.Dispatcher.Invoke(() => TextHistory.AppendText(message + Environment.NewLine));

      ScrollHistory.Dispatcher.Invoke(() => ScrollHistory.ScrollToBottom());
    }

    public void ShowMessage(string message)
    {
      Print(message);
      TextMessage.Dispatcher.Invoke(() => TextMessage.Clear());
      TextMessage.Dispatcher.Invoke(() => TextMessage.Focus());
    }
  }
}