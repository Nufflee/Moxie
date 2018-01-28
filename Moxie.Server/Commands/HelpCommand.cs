using Moxie.Common;
using Moxie.Server.Services;

namespace Moxie.Server.Commands
{
  public class HelpCommand : Command
  {
    public override string Description { get; } = "Shows this list of commands.";
    public override string[] Aliases { get; } = { "commands" };

    protected override void Process(User user, string[] args)
    {
      //CommandService.Instance.
    }
  }
}