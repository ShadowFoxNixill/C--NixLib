using System;
using System.Collections.Generic;

namespace Nixill.Utils
{
  public class CacheMap<K, V>
  {
    private Dictionary<K, V> Backing;
    private Func<K, V> Builder;

    public CacheMap(Func<K, V> inBuild)
    {
      Builder = inBuild;
      Backing = new Dictionary<K, V>();
    }

    public int Count { get => Backing.Count; }

    public V this[K key]
    {
      get => Get(key);
      set => Set(key, value);
    }

    public V Get(K key)
    {
      V ret;
      if (Backing.TryGetValue(key, out ret))
      {
        return ret;
      }
      else
      {
        ret = Builder.Invoke(key);
        Backing[key] = ret;
        return ret;
      }
    }

    public void Set(K key, V value) => Backing[key] = value;
    public void Clear() => Backing.Clear();
    public bool ContainsKey(K key) => Backing.ContainsKey(key);
    public bool ContainsValue(V value) => Backing.ContainsValue(value);
  }
}