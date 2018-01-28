using Moxie.Common;

namespace Moxie.Server.Commands
{
  public abstract class Command
  {
    public virtual string Description { get; }
    public virtual string[] Aliases { get; }
    public virtual char Prefix { get; } = '!';

    public void Run(string command, User user, string[] args)
    {
      if (command.StartsWith(Prefix.ToString()))
        Process(user, args);
    }

    protected abstract void Process(User user, string[] args);
  }
}