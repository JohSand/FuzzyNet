using System;

namespace FuzzyNet.Levenshtein {
  public struct OpCode
  {
    public EditType type;
    public int sbeg;
    public int send;
    public int dbeg;
    public int dend;

    public String toString() {
      return type + "(" + sbeg + "," + send + ","
             + dbeg + "," + dend + ")";
    }
  }
}