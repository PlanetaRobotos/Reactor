using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class ListExtensions
{
    /// <summary>
    /// Shuffles the element order of the specified list.
    /// </summary>
    public static void Shuffle<T>(this IList<T> ts)
    {
        var count = ts.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
        }
    }

    public static T GetRandomElement<T>(this T[] array) =>
        array[UnityEngine.Random.Range(0, array.Length)];

    public static object GetFirstValue(this Hashtable hashtable) =>
        (from DictionaryEntry de in hashtable select de.Value).FirstOrDefault();

    public static void SetNextIndex(this ref int index, int maxIndex) => index = (index + 1) % maxIndex;

    public static void SetPreviousIndex(this ref int index, int maxIndex)
    {
        if (index <= 0)
            index = maxIndex - 1;
        else
            index--;
    }
    
    public static void Move<T>(this List<T> list, T item, int newIndex)
            {
                if (item != null)
                {
                    var oldIndex = list.IndexOf(item);
                    if (oldIndex > -1)
                    {
                        list.RemoveAt(oldIndex);
    
                        if (newIndex > oldIndex) newIndex--;
                        // the actual index could have shifted due to the removal
    
                        list.Insert(newIndex, item);
                    }
                }
    
            }
            
            public static void Move<T>(this List<T> list, int oldIndex, int newIndex)
            {
                // exit if positions are equal or outside array
                if ((oldIndex == newIndex) || (0 > oldIndex) || (oldIndex >= list.Count) || (0 > newIndex) ||
                    (newIndex >= list.Count)) return;
                // local variables
                var i = 0;
                T tmp = list[oldIndex];
                // move element down and shift other elements up
                if (oldIndex < newIndex)
                {
                    for (i = oldIndex; i < newIndex; i++)
                    {
                        list[i] = list[i + 1];
                    }
                }
                // move element up and shift other elements down
                else
                {
                    for (i = oldIndex; i > newIndex; i--)
                    {
                        list[i] = list[i - 1];
                    }
                }
                // put element from position 1 to destination
                list[newIndex] = tmp;
            }
}