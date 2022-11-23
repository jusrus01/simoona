using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Shrooms.Infrastructure.Models
{
    public class ConstantConcurrentDictionary<TKey, TValue> where TValue : ICloneable
    {
        private readonly ConcurrentDictionary<TKey, TValue> _dictionary;

        public ConstantConcurrentDictionary(Dictionary<TKey, TValue> dictionary)
        {
            _dictionary = new ConcurrentDictionary<TKey, TValue>(dictionary); // Creating a copy so that no one could modify it by holding reference to dictionary
        }

        public bool ContainsKey(TKey key)
        {
            return _dictionary.ContainsKey(key);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            if (_dictionary.TryGetValue(key, out value))
            {
                value = (TValue)value.Clone(); // Creating a copy so that no one could modify it by holding reference to value

                return true;
            }

            return false;
        }
    }
}
