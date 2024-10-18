namespace EvictingCollection.Time;

public class SystemTimeProvider : ITimeProvider
{
    public DateTime GetUtcNow()
    {
        return DateTime.UtcNow;
    }
}