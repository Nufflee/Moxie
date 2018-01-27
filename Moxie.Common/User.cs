using System;

namespace Moxie.Common
{
  [Serializable]
  public class User
  {
    public string Name { get; }
    public Guid Id { get; }
    public IP4 Ip { get; }

    public User(string name, Guid id, IP4 ip)
    {
      Name = name;
      Id = id;
      Ip = ip;
    }

    public static bool operator ==(User left, User right)
    {
      if ((object)left == null || (object)right == null)
        throw new ArgumentNullException();

      return left.Id == right.Id;
    }

    public static bool operator !=(User left, User right)
    {
      return !(left == right);
    }

    protected bool Equals(User other)
    {
      return string.Equals(Name, other.Name) && Id.Equals(other.Id) && Ip.Equals(other.Ip);
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
        int hashCode = (Name != null ? Name.GetHashCode() : 0);
        hashCode = (hashCode * 397) ^ Id.GetHashCode();
        hashCode = (hashCode * 397) ^ Ip.GetHashCode();
        return hashCode;
      }
    }
  }
}