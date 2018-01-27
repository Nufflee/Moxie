using System;
using System.Windows;
using Moxie.Common;

namespace Moxie.Client.UI
{
  /// <summary>
  /// Interaction logic for LoginWindow.xaml
  /// </summary>
  public partial class LoginWindow : Window
  {
    public LoginWindow()
    {
      LogIn("Nuff", new IP4("127.0.0.1", 25565));
      InitializeComponent();
    }

    private void OnClick_ButtonLogin(object sender, RoutedEventArgs e)
    {
      LogIn(TextUsername.Text, new IP4(TextIPAddress.Text, int.Parse(TextPort.Text)));
    }

    private void LogIn(string name, IP4 address)
    {
      ClientWindow client = new ClientWindow(name, address);
      client.Show();

      Hide();
    }

    protected override void OnClosed(EventArgs e)
    {
      base.OnClosed(e);

      Application.Current.Shutdown();
    }
  }
}