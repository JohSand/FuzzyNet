using System;
using System.Linq;

namespace FuzzyNet
{
  public class TokenSort : RatioAlgorithm {


    public override int apply(String s1, String s2, Ratio ratio, StringProcessor stringProcessor) {

      String sorted1 = processAndSort(s1, stringProcessor);
      String sorted2 = processAndSort(s2, stringProcessor);

      return ratio.apply(sorted1, sorted2);

    }

    private static String processAndSort(String @in, StringProcessor stringProcessor) {

      @in = stringProcessor.process(@in);
      String[] wordsArray = @in.Split(new string[] {"\\s+"}, StringSplitOptions.None);
      String joined = string.Join( " ", wordsArray.OrderBy(e => e));

      return joined.Trim();

    }

  }
}