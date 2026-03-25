using System;
using System.Collections.Generic;
using System.IO;
using CS690_PROJECT;

public class FileSaver
{
    private string path;

    public FileSaver(string filePath)
    {
        this.path = filePath;
        if (!File.Exists(path)) File.Create(path).Close();
    }

    // Save one item
    public void Save(Item item)
    {
        string line = $"{item.Id},{item.Name},{item.Type},{item.Location},{item.PurchaseDate},{item.WarrantyEnd},{item.IsImportant}";
        File.AppendAllText(path, line + Environment.NewLine);
    }

    // Load all items
    public List<Item> Load()
    {
        List<Item> items = new List<Item>();
        
        // Read all lines at once
        string[] lines = File.ReadAllLines(path);

        foreach (string line in lines)
        {
            if (line == "") continue; // Skip empty lines

            string[] parts = line.Split(',');
            
            // Create item directly from parts
            items.Add(new Item
            {
                Id = int.Parse(parts[0]),
                Name = parts[1],
                Type = parts[2],
                Location = parts[3],
                PurchaseDate = DateTime.Parse(parts[4]),
                WarrantyEnd = DateTime.Parse(parts[5]),
                IsImportant = bool.Parse(parts[6])
            });
        }
        return items;
    }
}