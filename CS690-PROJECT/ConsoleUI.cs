using System.Runtime.CompilerServices;
using Spectre.Console;

namespace CS690_PROJECT
{
    public class ConsoleUI
    {
        public InventoryManager manager;
        string fileName = "data.txt";
        
        FileSaver fileSaver = new FileSaver("data.txt");
         

        public ConsoleUI(InventoryManager mgr)
        {
            manager = mgr;
            fileSaver = new FileSaver(fileName);
             // Auto-load on start
        }

        public void Run()
        {
            while (true)
            {
                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[bold blue]=== Inventory System ===[/]")
                        .AddChoices(new[] {
                            "1. Show All",
                            "2. Add Item",
                            "3. Edit Item",
                            "4. Show Duplicates",
                            "5. Exit"
                        })
                );

                if (choice == "1. Show All") ShowAll();
                else if (choice == "2. Add Item") AddItem();
                else if (choice == "3. Edit Item") EditItem();
                else if (choice == "4. Show Duplicates") ShowDuplicates(manager);
                else if (choice == "5. Exit") break;
            }
            
        }

        // --- INTERNAL SAVE HELPER ---
        private void SaveData()
        {
            var lines = new List<string>();
            foreach (var item in manager.Items)
            {
                lines.Add($"{item.Id},{item.Name},{item.Type},{item.Location},{item.PurchaseDate},{item.WarrantyEnd},{item.IsImportant}");
            }
            
            File.WriteAllLines(fileName, lines);
            // Optional: Small notification if you want, otherwise keep it silent
            // AnsiConsole.WriteLine("[dim]Data saved.[/]"); 
        }

        // --- INTERNAL LOAD HELPER ---
        public void LoadData()
        {
            if (!File.Exists(fileName)) return;

            var lines = File.ReadAllLines(fileName);

            foreach (var line in lines)
            {
                var parts = line.Split(',');
                if (parts.Length == 7  )
                {
                    manager.Items.Add(new Item
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
            }
            if (manager.Items.Count > 0)
                AnsiConsole.WriteLine($"[green]Loaded {manager.Items.Count} items.[/]");
        }

        // --- ACTIONS ---

        public void ShowAll()
        {
            if (manager.Items.Count == 0) { AnsiConsole.WriteLine("No items."); return; }
            var table = new Table();
            table.AddColumn("ID"); table.AddColumn("Name"); table.AddColumn("Type"); 
            table.AddColumn("Location"); table.AddColumn("Purchase Date"); table.AddColumn("Warranty End"); table.AddColumn("Important");
            foreach (var i in manager.Items)
                table.AddRow(i.Id.ToString() ?? "", i.Name ?? "", i.Type ?? "", i.Location ?? "",  i.PurchaseDate.ToShortDateString(), i.WarrantyEnd.ToShortDateString(), i.IsImportant ? "Yes" : "No");
            AnsiConsole.Write(table);
        }

        public void AddItem()
        {
            var newItem = new Item();
            newItem.Id = manager.GetNextId();
            newItem.Name = AnsiConsole.Ask<string>("Name:");
            newItem.Type = AnsiConsole.Ask<string>("Type:");
            newItem.Location = AnsiConsole.Ask<string>("Location:");
            newItem.PurchaseDate = AnsiConsole.Ask<DateTime>("Purchase Date (YYYY-MM-DD):");
            newItem.WarrantyEnd = AnsiConsole.Ask<DateTime>("Warranty End (YYYY-MM-DD):");
            newItem.IsImportant = AnsiConsole.Confirm("Is this item important?");
            
            manager.AddItem(newItem);
            SaveData();
            AnsiConsole.WriteLine("Added & Saved!");
        }

        public void EditItem()
        {
            string name = AnsiConsole.Ask<string>("Enter name to find:");
            var found = manager.Items.FirstOrDefault(i => i.Name != null && i.Name.ToLower() == name.ToLower());
            
            if (found == null) { 
                AnsiConsole.WriteLine("Not found."); 
                return; 
            }
            
            found.Name = AnsiConsole.Ask<string>($"Name [{found.Name}]:", found.Name ?? "");
            found.Type = AnsiConsole.Ask<string>($"Type [{found.Type}]:", found.Type ?? "");
            found.Location = AnsiConsole.Ask<string>($"Location [{found.Location}]:", found.Location ?? "");
            found.PurchaseDate = AnsiConsole.Ask<DateTime>($"Purchase Date [{found.PurchaseDate.ToShortDateString()}]:", found.PurchaseDate);
            found.WarrantyEnd = AnsiConsole.Ask<DateTime>($"Warranty End [{found.WarrantyEnd.ToShortDateString()}]:", found.WarrantyEnd);
            found.IsImportant = AnsiConsole.Confirm($"Is Important [{found.IsImportant}]:", found.IsImportant);
            SaveData(); 
            AnsiConsole.WriteLine("Updated & Saved!");
        }

    public void ShowDuplicates(InventoryManager manager)
    {
            Console.WriteLine("\n=== Duplicate Items (By Name) ===");
            
            List<Item> duplicates = manager.GetDuplicates();

            if (duplicates.Count == 0)
            {
                Console.WriteLine("No duplicates found based on Name.");
            }
            else
            {
            var table = new Table();
            table.AddColumn("ID"); table.AddColumn("Name"); table.AddColumn("Type"); 
            table.AddColumn("Location"); table.AddColumn("Purchase Date"); table.AddColumn("Warranty End"); table.AddColumn("Important");
                
                foreach (var item in duplicates)
                {
                    table.AddRow(item.Id.ToString() ?? "", item.Name ?? "", item.Type ?? "", item.Location ?? "", item.PurchaseDate.ToShortDateString(), item.WarrantyEnd.ToShortDateString(), item.IsImportant ? "Yes" : "No");
                }
                AnsiConsole.Write(table);
                Console.WriteLine($"Total duplicates found: {duplicates.Count}");
            }
        }
    }
}