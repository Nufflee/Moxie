using System.Collections.Generic;
using System.Net;
using Moxie.Common;
using Moxie.Server.Packets;

namespace Moxie.Server.Services
{
  public class UserService : Service<UserService>
  {
    private List<User> users = new List<User>();

    public void ConnectUser(User user)
    {
      Server.Send(new ListPacket<TextPacket>(ChatService.Instance.GetMessages(), (IPEndPoint)Server.Instance.Ip), user.Ip);
      Server.SendToAll(new TextPacket($"{user.Name} joined! Say hello.", user.Ip), false);

      users.Add(user);
    }

    public bool ValidateUser(User user)
    {
      return true;
    }

    public List<User> GetUsers()
    {
      return users;
    }
  }
}