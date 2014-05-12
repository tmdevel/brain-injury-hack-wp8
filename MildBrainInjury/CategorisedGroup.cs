using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Phone.Globalization;

namespace ViewModels {
  public class CategorisedGroup<T> : List<T>
  {
    public delegate string GetKeyDelegate(T item);

    /// <summary>
    /// The Key of this group.
    /// </summary>
    public string Category { get; private set; }
    public string CategoryFirst { get; private set; }
    public string CategoryRest { get; private set; }

    /// <summary>
    /// Public constructor.
    /// </summary>
    /// <param name="key">The key for this group.</param>
    public CategorisedGroup(string key)
    {
      Category = key;
      CategoryFirst = Category.Substring(0, 1);
      CategoryRest = Category.Substring(1);
    }

    /// <summary>
    /// Create a list of CategoryGroup<T> with keys set by a delegate.
    /// </summary>
    /// <param name="items">The items to place in the groups.</param>
    /// <param name="ci">The CultureInfo to group and sort by.</param>
    /// <param name="getKey">A delegate to get the key from an item.</param>
    /// <param name="sort">Will sort the data if true.</param>
    /// <returns>An items source for a LongListSelector</returns>
    public static List<CategorisedGroup<T>> CreateGroups(IEnumerable<T> items, CultureInfo ci, GetKeyDelegate getKey, bool sort)
    {
      var list = new List<CategorisedGroup<T>>();
      foreach (var item in items) {
        if (list.Any(x => x.Category == getKey(item))) {
          list.First(x => x.Category == getKey(item)).Add(item);
        }
        else {
          var newList = new CategorisedGroup<T>(getKey(item));
          newList.Add(item);
          list.Add(newList);
        }
      }

      if (sort)
      {
        foreach (CategorisedGroup<T> group in list)
        {
          group.Sort((c0, c1) => { return ci.CompareInfo.Compare(getKey(c0), getKey(c1)); });
        }
      }

      return list;
    }

  }
}