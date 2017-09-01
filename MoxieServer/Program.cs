using System;

namespace MoxieServer
{
  public class Program
  {
    static int port;

    static void Main(string[] args)
    {
      if (args.Length == 0)
      {
        port = 25565;
        return;
      }

      int.TryParse(args[0], out port);
    }
  }
}