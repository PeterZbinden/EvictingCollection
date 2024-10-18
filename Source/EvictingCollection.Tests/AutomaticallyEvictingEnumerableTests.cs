using FluentAssertions;

namespace EvictingCollection.Tests;

public class AutomaticallyEvictingEnumerableTests
{
    [Fact]
    public async Task Ensure_AutomaticEvictedItems_AreRemoved()
    {
        // Arrange
        bool ShouldEvict(int i) => i > 2;
        var collection = new AutomaticallyEvictingEnumerable<int>(ShouldEvict);

        // Act
        collection.Add(1);
        collection.Add(2);
        collection.Add(3);
        collection.Add(4);

        // Assert
        collection.Should().BeEquivalentTo([1, 2]);
    }
}