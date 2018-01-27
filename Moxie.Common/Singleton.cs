using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moxie.Common
{
  public abstract class Singleton<T>
    where T : new()
  {
    public T Instance
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

    private T instance;
  }
}