using System;

namespace Moxie.Common
{
  [Serializable]
  public class User
  {
    public readonly string name;
    public readonly Guid id;
    public readonly IP4 ip;

    public User(string name, Guid id, IP4 ip)
    {
      this.name = name;
      this.id = id;
      this.ip = ip;
    }

    public static bool operator ==(User left, User right)
    {
      if ((object)left == null || (object)right == null)
        throw new ArgumentNullException();

      return left.id == right.id;
    }

    public static bool operator !=(User left, User right)
    {
      return !(left == right);
    }

    protected bool Equals(User other)
    {
      return string.Equals(name, other.name) && id.Equals(other.id) && ip.Equals(other.ip);
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      return obj.GetType() == GetType() && Equals((User)obj);
    }

    public override int GetHashCode()
    {
      unchecked
      {
        int hashCode = (name != null ? name.GetHashCode() : 0);
        hashCode = (hashCode * 397) ^ id.GetHashCode();
        hashCode = (hashCode * 397) ^ ip.GetHashCode();
        return hashCode;
      }
    }
  }
}