using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using Moxie.Common;
using Moxie.Server;

namespace Moxie
{
  public class Client
  {
    string name;
    IP4 ip;
    ClientWindow window;

    UdpClient client;

    Thread send;

    public Client(string name, IP4 ip, ClientWindow window)
    {
      this.name = name;
      this.ip = ip;
      this.window = window;

      if (!OpenConnection())
      {
        Print("Connection failed!");
        return;
      }

      Print("Attempting a connection to " + ip.Address + ":" + ip.Port + ", user: " + name);
    }

    public void SendMessage(string message)
    {
      if (string.IsNullOrWhiteSpace(message))
        return;

      message = message.TrimStart(' ');
      message = message.TrimEnd(' ');
      string fullMessage = $"{DateTime.Now.ToShortTimeString()} {name}: {message}";

      window.ShowMessage(fullMessage);

      Send(new MessagePacket(name, message));
    }

    bool OpenConnection()
    {
      client = new UdpClient();

      Send(new ConnectionPacket(name, ip));

      return true;
    }

    string Recieve()
    {
      IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, ip.Port);
      byte[] data = client.Receive(ref endPoint);

      return data.GetString();
    }

    void Send(IPacket packet)
    {
      send = new Thread(() =>
      {
        BinaryFormatter formatter = new BinaryFormatter();

        byte[] data;

        using (MemoryStream stream = new MemoryStream())
        {
          formatter.Serialize(stream, packet);
          data = stream.ToArray();
        }

        client.Send(data, data.Length, ip);
      });

      send.Start();
    }

    void Send(string text)
    {
      send = new Thread(() =>
      {
        byte[] data = Encoding.ASCII.GetBytes(text);

        client.Send(data, data.Length, ip);
      });

      send.Start();
    }

    void Print(string message) => window.Print(message);
  }
}