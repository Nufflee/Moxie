using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using Moxie.Common;

namespace Moxie.Server
{
  public class Server
  {
    int port;

    UdpClient udpClient;
    Thread run, manage, send, recieve;

    List<ServerClient> clients = new List<ServerClient>();

    bool isRunning;

    public Server(int port)
    {
      this.port = port;
      udpClient = new UdpClient(port);
      run = new Thread(Run);

      run.Start();
    }

    void Run()
    {
      isRunning = true;

      Console.WriteLine($"Server started on port: {port}");

      ManageClients();
      Recieve();
    }

    void ManageClients()
    {
      manage = new Thread(() =>
      {
        while (isRunning)
        {
        }
      });

      manage.Start();
    }

    void Recieve()
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

    void Process(byte[] data)
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

      if (packetData is ConnectionPacket)
      {
        ConnectionPacket packet = (ConnectionPacket)packetData;

        Guid id = Guid.NewGuid();

        clients.Add(new ServerClient(packet.name, packet.ip, id.ToString()));
        Console.WriteLine($"{packet.name} connected to the server with id: {id}");
      }
      else if (packetData is MessagePacket)
      {
        MessagePacket packet = (MessagePacket)packetData;

        SendToAll(packet);

        Console.WriteLine(packet.ToString());
      }
    }

    void SendToAll(Packet packet)
    {
      foreach (ServerClient client in clients)
      {
        Send(packet, client.ip);
      }
    }

    void Send(Packet packet, IP4 ip)
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