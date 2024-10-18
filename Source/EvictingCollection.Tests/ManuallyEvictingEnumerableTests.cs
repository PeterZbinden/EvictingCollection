using FluentAssertions;

namespace EvictingCollection.Tests;

public class ManuallyEvictingEnumerableTests
{
    [Fact]
    public async Task Ensure_ManuallyEvictedItems_AreNotRemoved_WithoutCallToEvict()
    {
        // Arrange
        bool EvictionPolicy(int i) => i > 2;
        var collection = new ManuallyEvictingEnumerable<int>(EvictionPolicy);

        // Act
        collection.Add(1);
        collection.Add(2);
        collection.Add(3);
        collection.Add(4);

        // Assert
        collection.Should().BeEquivalentTo([1, 2, 3, 4]);
    }

    [Fact]
    public async Task Ensure_ManuallyEvictedItems_AreRemoved_AfterCallToEvict()
    {
        // Arrange
        bool EvictionPolicy(int i) => i > 2;
        var collection = new ManuallyEvictingEnumerable<int>(EvictionPolicy);

        // Act
        collection.Add(1);
        collection.Add(2);
        collection.Add(3);
        collection.Add(4);

        collection.Evict();

        // Assert
        collection.Should().BeEquivalentTo([1, 2]);
    }
}