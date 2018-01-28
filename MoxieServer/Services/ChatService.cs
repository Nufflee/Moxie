using System.Collections.Generic;
using Moxie.Server.Packets;

namespace Moxie.Server.Services
{
  public class ChatService : Service<ChatService>
  {
    private List<TextPacket> messages = new List<TextPacket>();

    public bool AddMessage(TextPacket packet)
    {
      // Here check if message is not from a banned/muted user. Kicked is fine.

      messages.Add(packet);

      Server.SendToAll(packet, false);

      return true;
    }

    public List<TextPacket> GetMessages()
    {
      if (messages.Count >= 100)
      {
        messages.RemoveRange(100, messages.Count - 100);
      }

      return messages;
    }
  }
}