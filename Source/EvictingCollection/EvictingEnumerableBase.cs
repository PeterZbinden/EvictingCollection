using System.Collections;

namespace EvictingCollection;

public abstract class EvictingEnumerableBase<T> : IEnumerable<T>
{
    protected readonly Func<T, bool> ShouldEvict;
    protected readonly IList<T> Items = new List<T>();

    public EvictingEnumerableBase(Func<T, bool> shouldEvict)
    {
        ShouldEvict = shouldEvict;
    }

    protected void EvictItems()
    {
        for (int i = Items.Count - 1; i > -1; i--)
        {
            var existingItem = Items[i];
            if (ShouldEvict(existingItem))
            {
                Items.RemoveAt(i);
            }
        }
    }

    public abstract void Add(T item);

    public void Clear()
    {
        Items.Clear();
    }

    public abstract IEnumerator<T> GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}