using System;
using System.Linq;
using static FuzzyNet.FuzzySearch;
namespace FuzzyNet
{
  public class WeightedRatio : BasicAlgorithm {

    public static readonly double UNBASE_SCALE = .95;
    public static readonly double PARTIAL_SCALE = .90;
    public static readonly bool TRY_PARTIALS = true;

    public override int apply(String s1, String s2, StringProcessor stringProcessor) {

      s1 = stringProcessor.process(s1);
      s2 = stringProcessor.process(s2);

      int len1 = s1.Length;
      int len2 = s2.Length;

      if (len1 == 0 || len2 == 0) { return 0; }

      bool tryPartials = TRY_PARTIALS;
      double unbaseScale = UNBASE_SCALE;
      double partialScale = PARTIAL_SCALE;

      int @base = Ratio(s1, s2);
      double lenRatio = ((double) Math.Max(len1, len2)) / Math.Min(len1, len2);

      // if strings are similar length don't use partials
      if (lenRatio < 1.5) tryPartials = false;

      // if one string is much shorter than the other
      if (lenRatio > 8) partialScale = .6;

      if (tryPartials) {

        double partial = PartialRatio(s1, s2) * partialScale;
        double partialSor = TokenSortPartialRatio(s1, s2) * unbaseScale * partialScale;
        double partialSet = TokenSetPartialRatio(s1, s2) * unbaseScale * partialScale;

        return (int) Math.Round(Max(@base, partial, partialSor, partialSet));

      }
      else {

        double tokenSort = TokenSortRatio(s1, s2) * unbaseScale;
        double tokenSet = TokenSetRatio(s1, s2) * unbaseScale;

        return (int) Math.Round(Max(@base, tokenSort, tokenSet));

      }

    }

    public static T Max<T>(params T[] args) => args.Max();
  }
}