using BenchmarkDotNet.Attributes;

namespace EvictingCollection.Benchmark;

public class ReadBenchmark
{
    [Params(10, 100, 1000, 10_000)]
    public int ItemsToAdd { get; set; }

    private AutomaticallyEvictingEnumerable<int> _automaticallyEvicting;
    private ManuallyEvictingEnumerable<int> _manuallyEvicting;
    private ManuallyEvictingEnumerable<int> _list;

    [GlobalSetup]
    public void Setup()
    {
        for (int i = 0; i < ItemsToAdd; i++)
        {
            _automaticallyEvicting.Add(i);
            _manuallyEvicting.Add(i);
            _list.Add(i);
        }
    }

    [Benchmark]
    public int AutomaticallyEvicting()
    {
        var x = 0;
        foreach (var item in _automaticallyEvicting)
        {
            x += item;
        }
        return x;
    }

    [Benchmark]
    public int ManuallyEvicting()
    {
        _manuallyEvicting.Evict();
        var x = 0;
        foreach (var item in _manuallyEvicting)
        {
            x += item;
        }
        return x;
    }

    [Benchmark]
    public int List()
    {
        var x = 0;
        foreach (var item in _list)
        {
            if (EvictionPolicy(item))
            {
                continue;
            }
            x += item;
        }

        return x;
    }

    // Simple logic to keep the focus on the differences of collection implementations
    private bool EvictionPolicy(int value)
    {
        return value > 50;
    }
}