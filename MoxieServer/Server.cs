using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Moxie.Common;
using Moxie.Server.Packets;

namespace Moxie.Server
{
  public class Server
  {
    private int port;
    private UdpClient udpClient;

    private Thread run, manage, send, recieve;

    private List<User> users = new List<User>();
    private List<MessagePacket> messages = new List<MessagePacket>();

    private bool isRunning;

    public Server(int port)
    {
      this.port = port;
      udpClient = new UdpClient(port);
      run = new Thread(Run);

      run.Start();
    }

    private void Run()
    {
      isRunning = true;

      Console.WriteLine($"Server started on port: {port}");

      ManageClients();
      Recieve();
    }

    private void ManageClients()
    {
      manage = new Thread(() =>
      {
        while (isRunning)
        {
        }
      });

      manage.Start();
    }

    private void Recieve()
    {
      recieve = new Thread(() =>
      {
        while (isRunning)
        {
          IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 0);

          byte[] data = udpClient.Receive(ref endPoint);

          Process(data);
        }
      });

      recieve.Start();
    }

    private void Process(byte[] data)
    {
      object packetData = null;

      try
      {
        packetData = Packet.Deserialize(data);
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
      }

      switch (packetData)
      {
        case ConnectionPacket packet:
          users.Add(packet.User);
          Send(new ListPacket<MessagePacket>(messages), packet.Ip);
          SendToAll(new MessagePacket(packet.User, $"{packet.User.Name} joined! Say hello."));

          Console.WriteLine($"{packet.User.Name} connected to the server with id: {packet.User.Id}");
          break;

        case MessagePacket packet:
          SendToAll(packet);
          messages.Add(packet);

          if (messages.Count >= 100)
          {
            messages.RemoveRange(100, messages.Count - 100);
          }

          Console.WriteLine(packet.ToString());
          break;
      }
    }

    private void SendToAll(Packet packet, bool sendToSender = false)
    {
      foreach (User user in users)
      {
        Send(packet, user.Ip);
      }
    }

    private void Send(Packet packet, IP4 ip)
    {
      send = new Thread(() =>
      {
        byte[] data = packet.Serialize();

        udpClient.Send(data, data.Length, ip);
      });

      send.Start();
    }
  }
}