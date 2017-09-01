using System;
using System.Text;

namespace Moxie
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

    public static string Prepend(this string text, string value, Func<string, int> condition)
    {
      for (int i = 0; i < condition.Invoke(text); i++)
      {
        text = value + text;
      }

      return text;
    }
  }
}