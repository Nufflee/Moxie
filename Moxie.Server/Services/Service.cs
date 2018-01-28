using Moxie.Common;

namespace Moxie.Server.Services
{
  public class Service<T> : Singleton<T>
    where T : new()
  {
    protected Server Server => server ?? (server = Server.Instance);

    private Server server;
  }
}