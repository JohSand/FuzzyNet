using System;

namespace FuzzyNet
{
  public interface StringProcessor
  {
    /**
 * Transforms the input string
 *
 * @param in Input string
 * @return The processed string
 */
    string process(string @in);
  }
}