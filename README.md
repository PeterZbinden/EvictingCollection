[NuGet.org](https://www.nuget.org/packages/EvictingCollection)

# Evicting Collection
A library that simplifies the handling of changing sets of data that have to be processed together based on some criteria.
Useful when dealing with infinite Streams of Data that constantly have to be processed.

Example:
- Continuous measurements, where we have to calculate the moving Average of the last 15 minutes.

### Add Namespace
```csharp
using EvictingCollection;
```

### Usage of AutomaticallyEvictingEnumerable
```csharp
bool ShouldEvict(int i) => i > 2;
var collection = new AutomaticallyEvictingEnumerable<int>(ShouldEvict);

collection.Add(1);
collection.Add(2);
collection.Add(3);
collection.Add(4);

foreach (var i in collection)
{
    Console.WriteLine(i);
}

// Output:
// 1
// 2
```

### Usage of ManuallyEvictingEnumerable
```csharp
bool ShouldEvict(int i) => i > 2;
var collection = new ManuallyEvictingEnumerable<int>(ShouldEvict);

collection.Add(1);
collection.Add(2);
collection.Add(3);
collection.Add(4);

foreach (var i in collection)
{
    Console.WriteLine(i);
}

// Output:
// 1
// 2
// 3
// 4

// Items are only evicted when Evict(); is called
collection.Evict();

foreach (var i in collection)
{
    Console.WriteLine(i);
}

// Output:
// 1
// 2
```