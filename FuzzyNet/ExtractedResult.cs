using System;

namespace FuzzyNet {
  public class ExtractedResult : IComparable<ExtractedResult> {

    private String s;
    private int score;

    public ExtractedResult(String s, int score) {
      this.s = s;
      this.score = score;
    }


    public int CompareTo(ExtractedResult o) {
      return getScore().CompareTo(o.getScore());
    }

    public String getString() {
      return s;
    }

    public void setString(String s) {
      this.s = s;
    }

    public int getScore() {
      return score;
    }


    public String toString() {
      return "(string: " + s + ", score:" + score + ")";
    }
  }
}