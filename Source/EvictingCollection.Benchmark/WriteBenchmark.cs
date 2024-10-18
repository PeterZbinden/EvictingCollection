using BenchmarkDotNet.Attributes;

namespace EvictingCollection.Benchmark;

public class WriteBenchmark
{
    [Params(10, 100, 1000, 10_000)]
    public int ItemsToAdd { get; set; }

    [Benchmark]
    public void AutomaticallyEvicting()
    {
        var collection = new AutomaticallyEvictingEnumerable<int>(EvictionPolicy);
        for (int i = 0; i < ItemsToAdd; i++)
        {
            collection.Add(i);
        }
    }

    [Benchmark]
    public void ManuallyEvicting_10()
    {
        var collection = new ManuallyEvictingEnumerable<int>(EvictionPolicy);
        for (int i = 0; i < ItemsToAdd; i++)
        {
            collection.Add(i);
            if (i % 10 == 0)
            {
                collection.Evict();
            }
        }
    }

    [Benchmark]
    public void ManuallyEvicting_100()
    {
        var collection = new ManuallyEvictingEnumerable<int>(EvictionPolicy);
        for (int i = 0; i < ItemsToAdd; i++)
        {
            collection.Add(i);
            if (i % 100 == 0)
            {
                collection.Evict();
            }
        }
    }

    [Benchmark]
    public void ManuallyEvicting_1000()
    {
        var collection = new ManuallyEvictingEnumerable<int>(EvictionPolicy);
        for (int i = 0; i < ItemsToAdd; i++)
        {
            collection.Add(i);
            if (i % 1000 == 0)
            {
                collection.Evict();
            }
        }
    }

    [Benchmark]
    public void List()
    {
        var collection = new List<int>();
        for (int i = 0; i < ItemsToAdd; i++)
        {
            if (!EvictionPolicy(i))
            {
                collection.Add(i);
            }
        }
    }

    // Simple logic to keep the focus on the differences of collection implementations
    private bool EvictionPolicy(int value)
    {
        return value > 50;
    }
}