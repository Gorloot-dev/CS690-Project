using CS690_PROJECT;


public class FileSaver
{
    private string path;

    public FileSaver(string filePath)
    {
        this.path = filePath;
        if (!File.Exists(path)) File.Create(path).Close();
    }

    // Save a list of items
    public void SaveItems(List<Item> items)
    {
        var lines = new List<string>();
        foreach (var item in items)
        {
            string line = $"{item.Id},{item.Name},{item.Type},{item.LocationPurchase},{item.LocationHome},{item.PurchaseDate:yyyy-MM-dd},{item.WarrantyEnd:yyyy-MM-dd},{item.IsImportant}";
            lines.Add(line); 
        }
        File.WriteAllLines(path, lines);
        
    }

    // Load all items
    public List<Item> Load()
    {
        List<Item> items = new List<Item>();
        
        // Read all lines at once
        string[] lines = File.ReadAllLines(path);

        foreach (string line in lines)
        {
            if (string.IsNullOrEmpty(line)) continue; // Skip empty lines

            string[] parts = line.Split(',');
            
            // Create item directly from parts
            items.Add(new Item
            {
                Id = int.Parse(parts[0]),
                Name = parts[1],
                Type = parts[2],
                LocationPurchase = parts[3],
                LocationHome = parts[4],
                PurchaseDate = DateTime.Parse(parts[5]),
                WarrantyEnd = DateTime.Parse(parts[6]),
                IsImportant = bool.Parse(parts[7])
            });
        }
        return items;
    }
}
