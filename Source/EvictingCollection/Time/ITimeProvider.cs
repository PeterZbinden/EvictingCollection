namespace EvictingCollection.Time;

public interface ITimeProvider
{
    DateTime GetUtcNow();
}