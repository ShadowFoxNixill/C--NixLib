namespace Nixill.Utils {
  public class SingleValueGenerator<K, V> : GeneratorFunc<K, V> {
    public SingleValueGenerator(V val) : base((key) => val, (key) => key != null, (vTest) => val.Equals(vTest)) { }
  }

  public class EchoGenerator<T> : GeneratorFunc<T, T> {
    public EchoGenerator() : base((key) => key, (key) => key != null, (val) => val != null) { }
  }

  public class CountingGenerator<K> : GeneratorFunc<K, int> {
    private int Current = 0;

    public CountingGenerator() : base((key) => Current++, ) {

    }
  }
}