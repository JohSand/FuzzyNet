using System;

namespace FuzzyNet.Levenshtein {
  public struct EditOp {

    public EditOp(EditType type, int spos, int dpos) {
      this.type = type;
      this.spos = spos;
      this.dpos = dpos;
    }

    public EditType type { get; }
    public int spos { get; } // source block pos
    public int dpos { get; } // destination block pos


    public String toString() {
      return type + "(" + spos + "," + dpos + ")";
    }

  }
}