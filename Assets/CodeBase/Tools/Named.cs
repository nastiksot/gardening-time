using System;

namespace CodeBase.Tools
{
    [Serializable]
    public class Named<TKey, TValue>
    {
        public TKey key;
        public TValue value;
    }

    [Serializable]
    public class Named<TName, TKey, TValue>
    {
        public TName name;
        public TKey key;
        public TValue value;
    }
}