using EvictingCollection.Time;

namespace EvictingCollection;

public class TimeOnAddEvictionPolicy : IOnAddEvictionPolicy
{
    private readonly TimeSpan _checkAfter;
    private readonly ITimeProvider _timeProvider;

    private DateTime _nextCheck;
    public TimeOnAddEvictionPolicy(TimeSpan checkAfter, ITimeProvider? timeProvider = null)
    {
        _checkAfter = checkAfter;
        _timeProvider = timeProvider ?? new SystemTimeProvider();
        _nextCheck = _timeProvider.GetUtcNow().Add(_checkAfter);
    }

    public bool ShouldCheckAllEvictionsOnAdd()
    {
        var now = _timeProvider.GetUtcNow();
        return now >= _nextCheck;
    }

    public void OnItemAdded()
    {
    }

    public void OnAllEvictionsChecked()
    {
        _nextCheck = _timeProvider.GetUtcNow().Add(_checkAfter);
    }
}