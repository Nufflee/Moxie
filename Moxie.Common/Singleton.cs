namespace Moxie.Common
{
  public abstract class Singleton<T>
    where T : new()
  {
    public static T Instance
    {
      get
      {
        // It CANNOT!
        // ReSharper disable once ConvertIfStatementToNullCoalescingExpression
        if (instance == null)
        {
          instance = new T();
        }

        return instance;
      }
    }

    private static T instance;
  }
}