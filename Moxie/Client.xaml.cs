using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Moxie
{
  /// <summary>
  /// Interaction logic for Client.xaml
  /// </summary>
  public partial class Client : Window
  {
    string name;
    string address;
    int port;

    public Client(string name, string address, int port)
    {
      this.name = name;
      this.address = address;
      this.port = port;

      InitializeComponent();

      if (!OpenConnection(address, port))
      {
        Print("Connection failed!");
        return;
      }

      Print("Attempting a connection to " + address + ":" + port + ", user: " + name);
    }

    public void Print(string message)
    {
      TextHistory.AppendText(message + Environment.NewLine);
      ScrollHistory.ScrollToBottom();
    }

    void Send(string message)
    {
      if (string.IsNullOrWhiteSpace(message))
        return;

      message = message.TrimStart(' ');
      message = message.TrimEnd(' ');
      message = $"{DateTime.Now.Hour}:{DateTime.Now.Minute} {name}: {message}";

      Print(message);
      TextMessage.Clear();
      TextMessage.Focus();
    }

    bool OpenConnection(string address, int port)
    {
    }

    void OnLoaded_TextMessage(object sender, RoutedEventArgs e)
    {
      TextMessage.Focus();
    }

    void OnKeyDown_TextMessage(object sender, KeyEventArgs e)
    {
      if (e.Key == Key.Enter)
      {
        Send(TextMessage.Text);
      }
    }

    void OnClick_ButtonSend(object sender, RoutedEventArgs e)
    {
      Send(TextMessage.Text);
    }
  }
}