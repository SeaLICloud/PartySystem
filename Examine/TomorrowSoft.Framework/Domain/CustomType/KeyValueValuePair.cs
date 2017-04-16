using System;

namespace TomorrowSoft.Framework.Domain.CustomType
{
    [Serializable]
    public class KeyValueValuePair<TKey, TFirst, TSecond>
    {
        public KeyValueValuePair(TKey key, TFirst first, TSecond second)
        {
            Key = key;
            First = first;
            Second = second;
        }

        public TKey Key { get; set; }
        public TFirst First { get; set; }
        public TSecond Second { get; set; }
    }
}