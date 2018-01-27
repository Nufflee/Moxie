using System;
using Moxie.Common;

namespace Moxie.Server.Packets
{
  [Serializable]
  public class TextPacket : Packet
  {
    public override IP4 Sender { get; }
    public string Text { get; }
    public string FormattedText { get; protected set; }
    public DateTime SendTime { get; }

    public TextPacket(string text, IP4 sender)
    {
      Text = text;
      SendTime = DateTime.Now;
      FormattedText = $"{SendTime.ToShortTimeString()} {Text}";
      Sender = sender;
    }
  }
}