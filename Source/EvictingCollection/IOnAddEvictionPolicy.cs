namespace EvictingCollection;

public interface IOnAddEvictionPolicy
{
    bool ShouldCheckAllEvictionsOnAdd();
    void OnItemAdded();
    void OnAllEvictionsChecked();
}