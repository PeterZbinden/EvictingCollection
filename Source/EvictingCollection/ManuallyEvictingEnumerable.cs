namespace EvictingCollection;

public class ManuallyEvictingEnumerable<T> : EvictingEnumerableBase<T>
{
    public ManuallyEvictingEnumerable(Func<T, bool> shouldEvict) : base(shouldEvict)
    {
    }

    public override void Add(T item)
    {
        Items.Add(item);
    }

    public override IEnumerator<T> GetEnumerator()
    {
        foreach (var item in Items)
        {
            yield return item;
        }
    }

    public void Evict()
    {
        EvictItems();
    }
}