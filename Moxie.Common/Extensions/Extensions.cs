using System.Text;

namespace Moxie.Common.Extensions
{
  public static class Extensions
  {
    public static byte[] GetBytes(this string text)
    {
      return Encoding.ASCII.GetBytes(text);
    }

    public static string GetString(this byte[] bytes)
    {
      return Encoding.ASCII.GetString(bytes);
    }
  }
}