using System;
using System.Collections.Generic;

namespace Nixill.Utils
{
  public class GeneratorDictionary<K, V> : Dictionary<K, V>
  {
    public GeneratorFunc<K, V> Generator { get; protected set; }

    public GeneratorDictionary(GeneratorFunc<K, V> func)
    {
      Generator = func;
    }

    public GeneratorDictionary(IDictionary<K, V> dictionary, GeneratorFunc<K, V> func) : base(dictionary)
    {
      Generator = func;
    }

    public GeneratorDictionary(IEqualityComparer<K> comparer, GeneratorFunc<K, V> func) : base(comparer)
    {
      Generator = func;
    }

    public GeneratorDictionary(int capacity, GeneratorFunc<K, V> func) : base(capacity)
    {
      Generator = func;
    }

    public GeneratorDictionary(IDictionary<K, V> dictionary, IEqualityComparer<K> comparer, GeneratorFunc<K, V> func) : base(dictionary, comparer)
    {
      Generator = func;
    }

    public GeneratorDictionary(int capacity, IEqualityComparer<K> comparer, GeneratorFunc<K, V> func) : base(capacity, comparer)
    {
      Generator = func;
    }

    public new V this[K key]
    {
      get
      {
        try
        {
          return base[key];
        }
        catch (KeyNotFoundException)
        {
          return Add(key);
        }
      }
      set => base[key] = value;
    }

    public V Add(K key)
    {
      if (key == null)
      {
        throw new ArgumentNullException("Null keys cannot be added to GeneratorDictionaries.");
      }
      else if (ContainsKey(key))
      {
        throw new ArgumentException("Key " + key.ToString() + " already exists in map.");
      }
      else
      {
        V val = Generator.Generate(key);
        base[key] = val;
        return val;
      }
    }

    public bool? CanGenerateForKey(K key) => Generator.CanGenerateFrom(key);
    public bool? CanGenerateValue(V val) => Generator.CanGenerate(val);
  }

  public class GeneratorFunc<K, V>
  {
    private Func<K, V> Generator;
    private Func<K, bool?> KeyChecker;
    private Func<V, bool?> ValChecker;

    public GeneratorFunc(Func<K, V> generator, Func<K, bool?> keyChecker, Func<V, bool?> valChecker)
    {
      Generator = generator;
      KeyChecker = keyChecker;
      ValChecker = valChecker;
    }

    public GeneratorFunc(Func<K, V> generator)
      : this(generator, (key) => null, (val) => null) { }
    public GeneratorFunc(Func<K, V> generator, Func<K, bool?> keyChecker)
      : this(generator, keyChecker, (val) => null) { }

    public V Generate(K key) => Generator.Invoke(key);
    public bool? CanGenerateFrom(K key) => KeyChecker.Invoke(key);
    public bool? CanGenerate(V val) => ValChecker.Invoke(val);

    public static implicit operator GeneratorFunc<K, V>(Func<K, V> func) => new GeneratorFunc<K, V>(func);
    public static explicit operator Func<K, V>(GeneratorFunc<K, V> gen) => gen.Generator;
  }

  public sealed class DefaultGeneratorFuncs
  {

  }
}