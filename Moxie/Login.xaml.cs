using System;
using System.Windows;

namespace Moxie
{
  /// <summary>
  /// Interaction logic for Login.xaml
  /// </summary>
  public partial class Login : Window
  {
    public Login()
    {
      LogIn("Nuff", "localhost", 25556);
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
      Client client = new Client(name, address, port);
      client.Show();

      Close();
    }
  }
}