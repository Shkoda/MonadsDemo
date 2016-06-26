using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
   public static class WhereExt
    {
        static IEnumerable<T> WhereHelper<T>(T item, Func<T, bool> predicate)
        {
            if (predicate(item)) yield return item;
        }
       public static IEnumerable<T> Where1<T>(this IEnumerable<T> items, Func<T, bool> predicate)
        {
            return items.SelectMany(item => WhereHelper(item, predicate));
        }

    }
}
