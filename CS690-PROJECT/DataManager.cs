
namespace CS690_PROJECT
{
    public class DataManager
    {
        public List<Item> Items { get; }
        public FileSaver saver {get;}

    // New method to reload data
         public DataManager()
        {
            Items = new List<Item>();
            saver = new FileSaver("data.txt");
             // Auto-load on start
            Items = saver.Load();    

        }

        public void AddItem(Item item)
        {
            // assign an new ID automatically
            if (item.Id == 0)
            {
                item.Id = GetNextId();
            }
            Items.Add(item);
            Save();
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
            //IDs start at 1, so add 1 to the max ID found
            return maxId + 1;
        }

    public void Save()
    {
        // This tells the FileSaver to overwrite the file with the current list
        saver.SaveItems(Items);
    }

    public List<Item> GetDuplicates()
    {   // This method checks for items with the same name (case-insensitive) and returns a list of duplicates
        List<Item> duplicates = new List<Item>();
        foreach (var currentItem in Items)
        {
            if (currentItem.Name == null) continue;

            foreach (var otherItem in Items)
            {
                if (currentItem == otherItem) continue;
                if (string.Equals(currentItem.Name, otherItem.Name, StringComparison.OrdinalIgnoreCase))
                {
                    // Check if already added
                    if (!duplicates.Contains(currentItem))
                    {
                        duplicates.Add(currentItem);
                    }
                    break;
                }
            }
        }
        return duplicates;
        }
    }
}