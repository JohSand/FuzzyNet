using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using FuzzyNet;

public class Extractor {

    private int cutoff;

    public Extractor() {
        this.cutoff = 0;
    }

    public Extractor(int cutoff) {
        this.cutoff = cutoff;
    }

    public Extractor with(int cutoff) {
        this.setCutoff(cutoff);
        return this;
    }

    /**
     * Returns the list of choices with their associated scores of similarity in a list
     * of {@link ExtractedResult}
     *
     * @param query The query string
     * @param choices The list of choices
     * @param func The function to apply
     * @return The list of results
     */
    public List<ExtractedResult> extractWithoutOrder(String query, Collection<String> choices, Applicable func) {

        List<ExtractedResult> yields = new List<ExtractedResult> ();

        foreach (String s in choices) {

            int score = func.apply(query, s);

            if (score >= cutoff) {
                yields.Add(new ExtractedResult(s, score));
            }

        }

        return yields;

    }

    /**
     * Find the single best match above a score in a list of choices.
     *
     * @param query  A string to match against
     * @param choice A list of choices
     * @param func   Scoring function
     * @return An object containing the best match and it's score
     */
    public ExtractedResult extractOne(String query, Collection<String> choice, Applicable func) {

        List<ExtractedResult> extracted = extractWithoutOrder(query, choice, func);

        return extracted.Max();

    }

    /**
     * Creates a <b>sorted</b> list of {@link ExtractedResult}  which contain the
     * top @param limit most similar choices
     *
     * @param query   The query string
     * @param choices A list of choices
     * @param func    The scoring function
     * @return A list of the results
     */
    public List<ExtractedResult> extractTop(String query, Collection<String> choices, Applicable func) {

        List<ExtractedResult> best = extractWithoutOrder(query, choices, func);
        return best.OrderByDescending(e => e).ToList();
    }

    /**
     * Creates a <b>sorted</b> list of {@link ExtractedResult} which contain the
     * top @param limit most similar choices
     *
     * @param query   The query string
     * @param choices A list of choices
     * @param limit   Limits the number of results and speeds up
     *                the search (k-top heap sort) is used
     * @return A list of the results
     */
    public List<ExtractedResult> extractTop(String query, Collection<String> choices, Applicable func, int limit) {

        List<ExtractedResult> best = extractWithoutOrder(query, choices, func);

        List<ExtractedResult> results = findTopKHeap(best, limit);
      results.Reverse();

        return results;
    }

    public int getCutoff() {
        return cutoff;
    }

    public void setCutoff(int cutoff) {
        this.cutoff = cutoff;
    }
  public static List<T> findTopKHeap<T>(List<T> arr, int k) where T : IComparable<T> {
    Queue<T> pq = new Queue<T>();

    foreach (T x in arr) {
      if (pq.Count < k) pq.Enqueue(x);
      else if (x.CompareTo(pq.Peek()) > 0) {
        pq.Dequeue();
        pq.Enqueue(x);
      }
    }
    List<T> res = new List<T>();
    for (int i =k; i > 0; i--) {
      T polled = pq.Dequeue();
      if (polled != null) {
        res.Add(polled);
      }
    }
    return res;

  }
}
