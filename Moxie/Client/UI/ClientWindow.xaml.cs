using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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
  /// Interaction logic for ClientWindow.xaml
  /// </summary>
  public partial class ClientWindow : Window
  {
    Client client;

    public ClientWindow(string name, string address, int port)
    {
      InitializeComponent();

      client = new Client(name, address, port, this);
    }

    void OnLoaded_TextMessage(object sender, RoutedEventArgs e)
    {
      TextMessage.Focus();
    }

    void OnKeyDown_TextMessage(object sender, KeyEventArgs e)
    {
      if (e.Key == Key.Enter)
      {
        client.SendMessage(TextMessage.Text);
      }
    }

    void OnClick_ButtonSend(object sender, RoutedEventArgs e)
    {
      client.SendMessage(TextMessage.Text);
    }

    public void Print(string message)
    {
      TextHistory.AppendText(message + Environment.NewLine);
      ScrollHistory.ScrollToBottom();
    }

    public void ShowMessage(string message)
    {
      Print(message);
      TextMessage.Clear();
      TextMessage.Focus();
    }
  }
}