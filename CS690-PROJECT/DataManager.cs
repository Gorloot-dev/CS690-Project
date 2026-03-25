using System;
using System.Collections.Generic;

namespace CS690_PROJECT
{
    public class InventoryManager
    {
        public List<Item> Items { get; set; }
        public FileSaver saver;

        public InventoryManager()
        {
            Items = new List<Item>();
            saver = new FileSaver("data.txt");
             // Auto-load on start
            Items = saver.Load();           
        }


        public void AddItem(Item item)
        {
            // If ID is 0 or negative, assign a new one automatically
            if (item.Id <= 0)
            {
                item.Id = GetNextId();
            }
            Items.Add(item);
        }

    

        public int GetNextId()
        {
            if (Items.Count == 0)
            {
                return 1;
            }

            int maxId = 0;
            foreach (var item in Items)
            {
                if (item.Id > maxId)
                {
                    maxId = item.Id;
                }
            }
            return maxId + 1;
        }

    
        public List<Item> GetDuplicates()
        {
            List<Item> duplicates = new List<Item>();

            // Loop 1: Pick an item to check
            foreach (var currentItem in Items)
            {
                bool isDuplicate = false;

                // Loop 2: Compare against every other item
                foreach (var otherItem in Items)
                {
                    // Skip comparing the item to itself
                    if (currentItem == otherItem)
                    {
                        continue;
                    }

                    // Check if Names match (Case Insensitive)
                    if (string.Equals(currentItem.Name, otherItem.Name, StringComparison.OrdinalIgnoreCase))
                    {
                        isDuplicate = true;
                        break; // Stop checking, we found a match
                    }
                }

                // If a match was found, add it to our list
                if (isDuplicate)
                {
                    // Avoid adding the same item twice to the result list
                    bool alreadyAdded = false;
                    foreach (var existing in duplicates)
                    {
                        if (existing == currentItem)
                        {
                            alreadyAdded = true;
                            break;
                        }
                    }

                    if (!alreadyAdded)
                    {
                        duplicates.Add(currentItem);
                    }
                }
            }

            return duplicates;
        }
    }
}