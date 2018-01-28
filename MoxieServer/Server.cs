using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Moxie.Common;
using Moxie.Server.Packets;
using Moxie.Server.Services;

namespace Moxie.Server
{
  public class Server : Singleton<Server>
  {
    public IP4 Ip { get; private set; }
    public int Port { get; private set; }

    private UdpClient udpClient;

    private Thread run, manage, send, recieve;

    private bool isRunning;

    public void Start(int port)
    {
      Port = port;
      udpClient = new UdpClient(port);
      Ip = (IP4)udpClient.Client.LocalEndPoint;

      run = new Thread(Run);
      run.Start();
    }

    private void Run()
    {
      isRunning = true;

      Console.WriteLine($"Server started on port: {Port}");

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
          UserService.Instance.ConnectUser(packet.User);
          Console.WriteLine($"{packet.User.Name} connected to the server with id: {packet.User.Id}");

          break;

        case TextPacket packet:
          ChatService.Instance.AddMessage(packet);
          Console.WriteLine(packet.FormattedText);

          break;
      }
    }

    public void SendToAll(Packet packet, bool sendToSender = true)
    {
      foreach (User user in UserService.Instance.GetUsers())
      {
        if (!sendToSender)
        {
          if (user.Ip != packet.Sender)
          {
            Send(packet, user.Ip);
          }
        }
        else
        {
          Send(packet, user.Ip);
        }
      }
    }

    public void Send(Packet packet, IP4 ip)
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