namespace EvictingCollection;

public class AutomaticallyEvictingEnumerable<T> : EvictingEnumerableBase<T>
{
    private readonly IOnAddEvictionPolicy _onAddEvictionPolicy;

    public AutomaticallyEvictingEnumerable(Func<T, bool> shouldEvict, IOnAddEvictionPolicy? onAddEvictionPolicy = null) : base(shouldEvict)
    {
        _onAddEvictionPolicy = onAddEvictionPolicy ?? new CountOnAddEvictionPolicy(100);
    }

    public override void Add(T item)
    {
        if (_onAddEvictionPolicy.ShouldCheckAllEvictionsOnAdd())
        {
            EvictItems();
            _onAddEvictionPolicy.OnAllEvictionsChecked();
        }

        if (ShouldEvict(item))
        {
            return;
        }

        Items.Add(item);
        _onAddEvictionPolicy.OnItemAdded();
    }

    public override IEnumerator<T> GetEnumerator()
    {
        for (int i = Items.Count - 1; i > -1; i--)
        {
            var item = Items[i];
            if (ShouldEvict(item))
            {
                Items.RemoveAt(i);
                continue;
            }

            yield return item;
        }
    }
}