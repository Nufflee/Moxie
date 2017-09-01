using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
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

          string text = data.GetString();

          Process(text);

          Console.WriteLine($"{clients[0].ip}:{clients[0].port}");
          Console.WriteLine(text);
        }
      });

      recieve.Start();
    }

    void Process(byte[] packet)
    {
      if ()
    }
  }
}