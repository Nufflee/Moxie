namespace Moxie.Server
{
  public class Program
  {
    private Server server;

    public Program(int port)
    {
      server = new Server(port);
    }

    private static void Main(string[] args)
    {
      int port;

      if (args.Length == 0)
      {
        port = 25565;
      }
      else
      {
        int.TryParse(args[0], out port);
      }

      new Program(port);
    }
  }
}