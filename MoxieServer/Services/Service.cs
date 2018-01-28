using Moxie.Common;

namespace Moxie.Server.Services
{
  public class Service<T> : Singleton<T>
    where T : new()
  {
    public Server Server => server ?? (server = Server.Instance);

    private Server server;
  }
}