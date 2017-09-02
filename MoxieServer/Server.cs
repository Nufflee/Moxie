using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;

namespace Moxie.Server
{
  public class Server
  {
    int port;

    UdpClient client;
    Thread run, manage, send, recieve;

    List<ServerClient> clients = new List<ServerClient>();

    bool isRunning;

    public Server(int port)
    {
      this.port = port;
      client = new UdpClient(port);
      run = new Thread(Run);

      run.Start();
    }

    void Run()
    {
      isRunning = true;

      Console.WriteLine($"Server started on port: {port}");

      ManageClients();
      Recieve();

      send = new Thread(() =>
      {
      });
      send.Start();
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

          byte[] data = client.Receive(ref endPoint);

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
        BinaryFormatter formatter = new BinaryFormatter();

        using (MemoryStream stream = new MemoryStream())
        {
          stream.Write(data, 0, data.Length);
          stream.Seek(0, SeekOrigin.Begin);

          packetData = formatter.Deserialize(stream);
        }
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
      }

      if (packetData is ConnectionPacket)
      {
        ConnectionPacket packet = (ConnectionPacket)packetData;

        clients.Add(new ServerClient(packet.name, packet.ip, new Random().Next()));
      }
      else if (packetData is MessagePacket)
      {
        MessagePacket packet = (MessagePacket)packetData;

        Console.WriteLine($"{DateTime.Now.ToShortTimeString()} {packet.name}: {packet.message}");
      }
    }
  }
}