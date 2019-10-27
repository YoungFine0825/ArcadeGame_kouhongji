using UnityEngine;
using System.Collections;

namespace JW.Framework.Network
{
    public class ObjectBase
    {
        public static implicit operator bool(ObjectBase obj)
        {
            return !object.ReferenceEquals(obj, null);
        }
        public static bool operator ==(ObjectBase obja, ObjectBase objb)
        {
            return object.ReferenceEquals(obja, objb);
        }
        public static bool operator !=(ObjectBase obja, ObjectBase objb)
        {
            return !object.ReferenceEquals(obja, objb);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        protected ObjectBase()
        {

        }

    }
}

