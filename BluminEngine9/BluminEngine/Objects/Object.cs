using BluminEngine9.BluminEngine;
using System.Xml.Linq;

namespace BluminEngine9.BluminEngine.Objects
{
    public abstract class Object : Instancable, IEquatable<Object>
    {
        
        private int hashcode;

        protected Object()
        {
            HashCode hc = new HashCode();
            hc.Add(this);
            hc.Add(GetInstanceId());
            hashcode = hc.ToHashCode();
        }

        public bool Equals(Object? other)
        {
            if(other.hashcode == hashcode && other.GetInstanceId() == GetInstanceId())
            {
                return true;
            }
            return false;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Object);
        }

        public override int GetHashCode()
        {
            return hashcode;
        }


        public override string? ToString()
        {
            return "Object: " + GetInstanceId();
        }

        public static bool operator ==(Object? left, Object? right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Object? left, Object? right)
        {
            if (left.Equals(right))
            {
                return false;
            }
            return true;
        }
    }

    public abstract class Instancable
    {
        private Guid InstanceId { get; }

        protected Instancable()
        {
            InstanceId = Guid.NewGuid();
        }

        public Guid GetInstanceId()
        {
            return InstanceId;
        }
    }
}
