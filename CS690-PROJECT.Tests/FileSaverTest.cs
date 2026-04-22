using System;
using System.Collections.Generic;
using System.IO;
using Xunit;
using CS690_PROJECT;



namespace CS690_PROJECT.Tests;

public class FileSaverTests
{
    [Fact]
     public void FileSaver_SavesAndLoadsData()
        {
            // 1. Setup
            string path = "test.txt";
            var saver = new FileSaver(path);
            var items = new List<Item> { new Item(1, "Laptop", "Tech", "Store", "Home", System.DateTime.Now, System.DateTime.Now, true) };

            // 2. Action
            saver.SaveItems(items);       // Save to file
            var loaded = saver.Load();    // Read from file

            // 3. Verify
            Assert.Equal("Laptop", loaded[0].Name);
        }

}