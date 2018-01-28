using System.Collections.Generic;
using Moxie.Server.Commands;
using Moxie.Server.Packets;

namespace Moxie.Server.Services
{
  public class CommandService : Service<CommandService>
  {
    private Dictionary<string, Command> commands;

    public CommandService()
    {
    }

    public void Process(TextPacket packet)
    {
    }

    public Dictionary<string, Command> GetCommands()
    {
      return commands;
    }
  }
}