using Nixill.Utils;

namespace Nixill.Test
{
  public class GenDictTest
  {
    static void Main(string[] args)
    {
      GeneratorFunc<string, int> func =
        new GeneratorFunc<string, int>((key) => 0, (key) => true, (val) => val == 0);

      GeneratorDictionary<object, int> ints = new GeneratorDictionary<string, int>(func);
    }
  }
}