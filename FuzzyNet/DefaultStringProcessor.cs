using System;
using System.Text.RegularExpressions;

namespace FuzzyNet {
  public class DefaultStringProcessor : StringProcessor {

    private static String pattern = "[^\\p{Alnum}]";
    private static Regex r = compilePattern();


    /**
     * Substitute non alphanumeric characters.
     *
     * @param in The input string
     * @param sub The string to substitute with
     * @return The replaced string
     */
    public static String subNonAlphaNumeric(String @in, String sub) {
      return r.Replace(@in, sub);
    }

    /**
     * Performs the default string processing on the input string
     *
     * @param in Input string
     * @return The processed string
     */

    public String process(String @in) {
      return subNonAlphaNumeric(@in, " ").ToLower().Trim();
    }

    private static Regex compilePattern() {

      return new Regex(pattern,
        RegexOptions.Singleline | RegexOptions.Compiled);
      //try{
      //  p = p.compile(pattern, Pattern.UNICODE_CHARACTER_CLASS);
      //} catch (IllegalArgumentException e) {
      //  // Even though Android supports the unicode pattern class
      //  // for some reason it throws an IllegalArgumentException
      //  // if we pass the flag like on standard Java runtime
      //  //
      //  // We catch this and recompile without the flag (unicode should still work)
      //  p = Pattern.compile(pattern);
      //}

      //return p;

    }

  }
}