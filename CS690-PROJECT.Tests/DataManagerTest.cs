using System;
using System.Collections.Generic;
using System.IO;
using Xunit;
using CS690_PROJECT;



namespace CS690_PROJECT.Tests;

public class DataManagerTests
{
    [Fact]
        public void DataManager_AssignsId_AndFindsDuplicates()
        {
            // 1. Setup
            var manager = new DataManager();
            manager.Items.Clear(); // Start fresh

            // 2. Action: Add items
            manager.AddItem(new Item(0, "TV", "Elec", "A", "B", DateTime.Now, DateTime.Now, false)); // Should get ID 1
            manager.AddItem(new Item(0, "tv", "Elec", "A", "B", DateTime.Now, DateTime.Now, false)); // Should get ID 2 (Duplicate)

            // 3. Verify
            Assert.Equal(1, manager.Items[0].Id);      // Check ID assignment
            Assert.Equal(2, manager.GetDuplicates().Count); // Check duplicate detection
    }
}