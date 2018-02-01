using System;

namespace FuzzyNet.Levenshtein
{
  public struct MatchingBlock {
    public int spos;
    public int dpos;
    public int length;

    public String toString() {
      return "(" + spos + "," + dpos + "," + length + ")";
    }
  }
}