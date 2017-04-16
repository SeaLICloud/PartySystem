using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity.Utility;
using TomorrowSoft.Framework.Domain.Bases;

namespace TomorrowSoft.Framework.Domain.CustomType
{
    public class PairDictionary<TKey, TFirst, TSecond> : IEnumerable<KeyValueValuePair<TKey, TFirst, TSecond>> 
    {
        private IList<KeyValueValuePair<TKey, TFirst, TSecond>> pairs; 

        public PairDictionary(
            Func<TFirst, TKey> firstFunc, 
            IEnumerable<TFirst> firstClassItems, 
            Func<TSecond, TKey> secondFunc,
            IEnumerable<TSecond> secondClassItems)
        {
            pairs = new List<KeyValueValuePair<TKey, TFirst, TSecond>>();
            var keys = new List<TKey>();
            keys.AddRange(firstClassItems.Select(firstFunc));
            keys.AddRange(secondClassItems.Select(secondFunc));
            foreach (var key in keys.Distinct())
            {
                var pair = new KeyValueValuePair<TKey, TFirst, TSecond>(key,
                    firstClassItems.FirstOrDefault(x => firstFunc.Invoke(x).Equals(key)),
                    secondClassItems.FirstOrDefault(x => secondFunc.Invoke(x).Equals(key)));
                pairs.Add(pair);
            }
        }
        
        public KeyValueValuePair<TKey, TFirst, TSecond> this[TKey key]
        {
            get { return pairs.FirstOrDefault(x => x.Key.Equals(key)); }
        }

        public IEnumerator<KeyValueValuePair<TKey, TFirst, TSecond>> GetEnumerator()
        {
            return pairs.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}