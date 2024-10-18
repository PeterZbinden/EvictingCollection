namespace EvictingCollection;

public class CountOnAddEvictionPolicy : IOnAddEvictionPolicy
{
    private readonly int _checkAfter;
    private int _itemsAddedSinceCheck;
    public CountOnAddEvictionPolicy(int checkAfterAdds)
    {
        if (checkAfterAdds < 0)
        {
            throw new ArgumentException($"{nameof(checkAfterAdds)} must not be negative");
        }

        _checkAfter = checkAfterAdds;
    }

    public bool ShouldCheckAllEvictionsOnAdd()
    {
        if (_itemsAddedSinceCheck >= _checkAfter)
        {
            return true;
        }

        return false;
    }

    public void OnItemAdded()
    {
        _itemsAddedSinceCheck++;
    }

    public void OnAllEvictionsChecked()
    {
        _itemsAddedSinceCheck = 0;
    }
}