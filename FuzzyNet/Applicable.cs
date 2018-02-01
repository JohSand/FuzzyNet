using System;
using System.Collections.Generic;
using System.Linq;
using FuzzyNet.Levenshtein;

namespace FuzzyNet {
  public interface Applicable {
    /**
 * Apply the ratio/algorithm to the input strings
 *
 * @param s1 Input string
 * @param s2 Input string
 * @return The score of similarity
 */
    int apply(String s1, String s2);
  }

  public interface Ratio : Applicable {
    /**
     * Applies the ratio between the two strings
     *
     * @param s1 Input string
     * @param s2 Input string
     * @param sp String processor to pre-process strings before calculating the ratio
     * @return Integer representing ratio of similarity
     */
    int apply(string s1, string s2, StringProcessor sp);
  }

  public class PartialRatio : Ratio {

    /**
     * Computes a partial ratio between the strings
     *
     * @param s1 Input string
     * @param s2 Input string
     * @return The partial ratio
     */
    public int apply(String s1, String s2) {

      String shorter;
      String longer;

      if (s1.Length < s2.Length) {

        shorter = s1;
        longer = s2;

      }
      else {

        shorter = s2;
        longer = s1;

      }

      MatchingBlock[] matchingBlocks = DiffUtils.getMatchingBlocks(shorter, longer);

      var scores = new List<double>();

      foreach (var mb in matchingBlocks) {

        int dist = mb.dpos - mb.spos;

        int long_start = dist > 0 ? dist : 0;
        int long_end = long_start + shorter.Length;

        if (long_end > longer.Length) long_end = longer.Length;

        String long_substr = longer.Substring(long_start, long_end);

        double ratio = DiffUtils.getRatio(shorter, long_substr);

        if (ratio > .995) {
          return 100;
        }
        else {
          scores.Add(ratio);
        }

      }

      return (int) Math.Round(100 * scores.Max());

    }

    public int apply(String s1, String s2, StringProcessor sp) {
      return apply(sp.process(s1), sp.process(s2));
    }


  }

  public class SimpleRatio : Ratio {

    /**
     * Computes a simple Levenshtein distance ratio between the strings
     *
     * @param s1 Input string
     * @param s2 Input string
     * @return The resulting ratio of similarity
     */

    public int apply(String s1, String s2) {

      return (int) Math.Round(100 * DiffUtils.getRatio(s1, s2));

    }


    public int apply(String s1, String s2, StringProcessor sp) {
      return apply(sp.process(s1), sp.process(s2));
    }
  }
}