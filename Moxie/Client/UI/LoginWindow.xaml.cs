using System;
using System.Net;
using System.Windows;
using Moxie.Common;

namespace Moxie
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

    void OnClick_ButtonLogin(object sender, RoutedEventArgs e)
    {
      LogIn(TextUsername.Text, new IP4(TextIPAddress.Text, int.Parse(TextPort.Text)));
    }

    void LogIn(string name, IP4 address)
    {
      ClientWindow client = new ClientWindow(name, address);
      client.Show();

      Close();
    }
  }
}