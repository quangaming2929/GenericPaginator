using System.Collections.Generic;

namespace ImagiSekaiTechnologies.GenericPaginator.Extensions
{
    public static class IEnumerableExtenstions
    {
        public static IEnumerable<T> Insert<T>(this IEnumerable<T> ts, T item, int index)
        {
            int i = 0;
            foreach (var enumItem in ts)
            {
                if (i == index)
                {
                    yield return item;
                    i++;
                }

                yield return enumItem;
                i++;
            }

            if (i >= index) yield return item;
        }

        public static IEnumerable<T> RemoveAt<T>(this IEnumerable<T> ts, int index)
        {
            int i = 0;
            foreach (var item in ts)
            {
                if (i != index)
                {
                    yield return item;
                }

                i++;
            }
        }

        public static IEnumerable<T> MoveOffset<T>(this IEnumerable<T> ts, T item, int offset) where T : class
        {
            if (offset == 0)
            {
                foreach (var enumItem in ts)
                {
                    yield return enumItem;
                }
            }
            if (offset > 0)
            {
                int i = -1;
                T mvItem = default;
                foreach (var enumItem in ts)
                {
                    if (enumItem.Equals(item))
                    {
                        mvItem = item;
                        i = offset;
                        continue;
                    }

                    if (i == 0)
                        yield return mvItem;
                    yield return enumItem;
                    i--;
                }

                if (i >= 0) yield return mvItem;
            }
            else
            {
                Queue<T> tQueue = new Queue<T>();
                foreach (var enumItem in ts)
                {
                    if (item.Equals(enumItem))
                    {
                        yield return enumItem;
                        while (tQueue.Count > 0)
                            yield return tQueue.Dequeue();
                    }
                    else
                    {
                        tQueue.Enqueue(enumItem);
                        if (tQueue.Count > -offset)
                            yield return tQueue.Dequeue();
                    }
                }
                while (tQueue.Count > 0)
                    yield return tQueue.Dequeue();
            }
        }

        public static IEnumerable<T> Move<T>(this IEnumerable<T> ts, T item, int index)
        {
            int i = 0;
            bool meetOld = false;
            foreach (var enumitem in ts)
            {
                if (i == index)
                    yield return item;
                if (enumitem.Equals(item) && !meetOld)
                {
                    meetOld = true;
                    continue;
                }
                else
                {
                    yield return enumitem;
                }

                i++;
            }
        }

        public static IEnumerable<T> MoveToFirst<T>(this IEnumerable<T> ts, T item) where T : class
        {
            yield return item;
            foreach (var enumItem in ts)
            {
                if (!enumItem.Equals(item))
                    yield return enumItem;
            }
        }

        public static IEnumerable<T> MoveToLast<T>(this IEnumerable<T> ts, T item) where T : class
        {
            foreach (var enumItem in ts)
            {
                if (!enumItem.Equals(item))
                    yield return enumItem;
            }
            yield return item;
        }

        public static int FindIndex<T>(this IEnumerable<T> ts, T item) where T : class
        {
            int i = 0;
            foreach (var enumItem in ts)
            {
                if (enumItem.Equals(item))
                    return i;
                i++;
            }

            return -1;
        }
    }

}
