namespace Moxie.Server
{
  public class Program
  {
    public Program(int port)
    {
      Server.Instance.Start(port);
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