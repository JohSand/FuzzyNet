using System;

namespace FuzzyNet
{
  public abstract class RatioAlgorithm : BasicAlgorithm {

    private Ratio ratio;

    public RatioAlgorithm() : base() {
      this.ratio = new SimpleRatio();
    }

    public RatioAlgorithm(StringProcessor stringProcessor) : base(stringProcessor)  {
    }

    public RatioAlgorithm(Ratio ratio) : base() {
      this.ratio = ratio;
    }


    public RatioAlgorithm(StringProcessor stringProcessor, Ratio ratio): base(stringProcessor)  {
      this.ratio = ratio;
    }

    public abstract int apply(String s1, String s2, Ratio ratio, StringProcessor stringProcessor);

    public RatioAlgorithm with(Ratio ratio) {
      setRatio(ratio);
      return this;
    }

    public int apply(String s1, String s2, Ratio ratio) {
      return apply(s1, s2, ratio, getStringProcessor());
    }


    public override int apply(String s1, String s2, StringProcessor stringProcessor) {
      return apply(s1, s2, getRatio(), stringProcessor);
    }

    public void setRatio(Ratio ratio) {
      this.ratio = ratio;
    }

    public Ratio getRatio() {
      return ratio;
    }
  }
}