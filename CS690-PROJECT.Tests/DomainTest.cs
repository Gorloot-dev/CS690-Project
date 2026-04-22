using System;
using System.Collections.Generic;
using System.IO;
using Xunit;
using CS690_PROJECT;



namespace CS690_PROJECT.Tests;

public class DomainTests
{
    [Fact]
    public void TestItemCreation()
    {
        var item = new Item(1, "Laptop", "Electronics", "Best Buy", "Home Office", new DateTime(2022, 1, 15), new DateTime(2024, 1, 15), true);
        
        Assert.Equal(1, item.Id);
        Assert.Equal("Laptop", item.Name);
        Assert.Equal("Electronics", item.Type);
        Assert.Equal("Best Buy", item.LocationPurchase);
        Assert.Equal("Home Office", item.LocationHome);
        Assert.Equal(new DateTime(2022, 1, 15), item.PurchaseDate);
        Assert.Equal(new DateTime(2024, 1, 15), item.WarrantyEnd);
        Assert.True(item.IsImportant);
    }
}
