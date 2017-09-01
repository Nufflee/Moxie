using System;
using System.Windows;

namespace Moxie
{
  /// <summary>
  /// Interaction logic for LoginWindow.xaml
  /// </summary>
  public partial class LoginWindow : Window
  {
    public LoginWindow()
    {
      LogIn("Nuff", "127.0.0.1", 25565);
      InitializeComponent();
    }

    void OnClick_ButtonLogin(object sender, RoutedEventArgs e)
    {
      if (TextUsername.Text == "Username")
      {
        LogIn("Nuff", "localhost", 25556);
        return;
      }

      LogIn(TextUsername.Text, TextIPAddress.Text, int.Parse(TextPort.Text));
    }

    void LogIn(string name, string address, int port)
    {
      ClientWindow client = new ClientWindow(name, address, port);
      client.Show();

      Close();
    }
  }
}