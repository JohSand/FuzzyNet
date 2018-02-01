using System;
using System.Collections.Generic;
using System.Linq;

namespace FuzzyNet
{
  public class TokenSet : RatioAlgorithm {

    public override int apply(String s1, String s2, Ratio ratio, StringProcessor stringProcessor) {

      s1 = stringProcessor.process(s1);
      s2 = stringProcessor.process(s2);

      var tokens1 = tokenizeSet(s1);
      var tokens2 = tokenizeSet(s2);

      var intersection = tokens1.Intersect(tokens2);
     var diff1to2 = tokens1.Except(tokens2);
      var diff2to1 = tokens2.Except(tokens1);

      String sortedInter = string.Join(" ", intersection.OrderBy(e => e)).Trim();
      String sorted1to2 = (sortedInter + " " + string.Join(" ", diff1to2.OrderBy(e => e))).Trim();
      String sorted2to1 = (sortedInter + " " + string.Join(" ", diff2to1.OrderBy(e => e))).Trim();

      var results = new List<int>();

      results.Add(ratio.apply(sortedInter, sorted1to2));
      results.Add(ratio.apply(sortedInter, sorted2to1));
      results.Add(ratio.apply(sorted1to2, sorted2to1));

      return results.Max();

    }
    static HashSet<String> tokenizeSet(String @in){

      return new HashSet<String>(tokenize(@in));

    }
    static String[] tokenize(String @in){

      return @in.Split(new []{"\\s+"}, StringSplitOptions.None);

    }
  }
}