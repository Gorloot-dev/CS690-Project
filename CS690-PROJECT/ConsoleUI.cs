using Spectre.Console;

using CS690_PROJECT;

namespace CS690_PROJECT
{
    public class ConsoleUI
    {
        public DataManager manager;
        //string fileName = "data.txt";
        //FileSaver fileSaver = new FileSaver("data.txt");
         
        public ConsoleUI(DataManager mgr)
        {
            manager = mgr;
            // fileSaver = new FileSaver(fileName);
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

                switch (choice)
                {
                    case "1. Show All":
                        ShowAll();
                        break;
                    case "2. Add Item":
                        AddItem();
                        break;
                    case "3. Edit Item":
                        EditItem();
                        break;
                    case "4. Show Duplicates":
                        ShowDuplicates();
                        break;
                    case "5. Exit":
                        return; // Exits the loop and the program

                }
            }
            
        }

        // --- ACTIONS ---

        public void ShowAll()
       { // This method displays all items in a nice table format using Spectre.Console
            AnsiConsole.MarkupLine("[bold]Current Inventory:[/]");
            var table = new Table();
            table.AddColumn("ID");
            table.AddColumn("Name");
            table.AddColumn("Type");
            table.AddColumn("Purchase Location");
            table.AddColumn("Home Location");
            table.AddColumn("Purchase Date");
            table.AddColumn("Warranty End");
            table.AddColumn("Important");

            foreach (var i in manager.Items)
            {
                table.AddRow(
                    i.Id.ToString(),
                    i.Name ?? "-",
                    i.Type ?? "-",
                    i.LocationPurchase ?? "-",
                    i.LocationHome ?? "-",
                    i.PurchaseDate.ToShortDateString(),
                    i.WarrantyEnd.ToShortDateString(),
                    i.IsImportant ? "Yes" : "No"
                );
            }
            AnsiConsole.Write(table);
        }



        public void AddItem()
        {
            //var newItem = new Item();
            //var Id = manager.GetNextId();
            var Name = AnsiConsole.Ask<string>("Name:");
            var Type = AnsiConsole.Ask<string>("Type:");
            var LocationPurchase = AnsiConsole.Ask<string>("Purchase Location:");
            var LocationHome = AnsiConsole.Ask<string>("Home Location:");
            var PurchaseDate = AnsiConsole.Prompt(new TextPrompt<DateTime>("Purchase Date (YYYY-MM-DD):").ValidationErrorMessage("[red]Invalid date format[/]"));
            var WarrantyEnd = AnsiConsole.Prompt(new TextPrompt<DateTime>("Warranty End (YYYY-MM-DD):").ValidationErrorMessage("[red]Invalid date format[/]"));
            var IsImportant = AnsiConsole.Confirm("Is this item important?");
            
            var newItem = new Item(0, Name, Type, LocationPurchase, LocationHome, PurchaseDate, WarrantyEnd, IsImportant);
            manager.AddItem(newItem); // Save happens inside AddItem
            AnsiConsole.MarkupLine("[green]Item added successfully![/]");
        }

        public void EditItem()
        {   //1. Check if there are items to edit first
            if (manager.Items.Count == 0)
            {
                AnsiConsole.WriteLine("[yellow]No items to edit.[/]");
                return;
            }

            // 2. Show a table of items so you can see the IDs
            AnsiConsole.MarkupLine("[bold]Current Inventory:[/]");
            var table = new Table();
            table.AddColumn("ID");
            table.AddColumn("Name");
            table.AddColumn("Type");
            table.AddColumn("Purchase Location");
            table.AddColumn("Home Location");
            
            foreach (var i in manager.Items)
            {
                table.AddRow(i.Id.ToString(), i.Name ?? "No Name", i.Type ?? "-", i.LocationPurchase ?? "-", i.LocationHome ?? "-");
            }
            AnsiConsole.Write(table);

            // 3. Ask for the ID
            int idToEdit = AnsiConsole.Ask<int>("Enter the [green]ID[/] of the item to edit:");

    
            // 4. Find the item by ID (Unique match)
            Item? found = null;
            foreach (var i in manager.Items)
            {
                if (i.Id == idToEdit)
                {
                    found = i;
                    break;
                }
            }

            if (found == null)
            {
                AnsiConsole.WriteLine("Item not found");
                return;
            }

            AnsiConsole.WriteLine($"[bold]Editing: {found.Name}[/]");
            
            found.Name = AnsiConsole.Ask<string>("New Name:", found.Name ?? "");
            found.Type = AnsiConsole.Ask<string>("New Type:", found.Type ?? "");
            found.LocationPurchase = AnsiConsole.Ask<string>("New Purchase Location:", found.LocationPurchase ?? "");
            found.LocationHome = AnsiConsole.Ask<string>("New Home Location:", found.LocationHome ?? "");

            found.PurchaseDate = AnsiConsole.Ask<DateTime>("New Purchase Date:", found.PurchaseDate);
            found.WarrantyEnd = AnsiConsole.Ask<DateTime>("New Warranty End:", found.WarrantyEnd);
            
            found.IsImportant = AnsiConsole.Confirm("Is Important?", found.IsImportant);

            manager.Save();
            AnsiConsole.WriteLine("[green]Item updated![/]");
        }
    public void ShowDuplicates()
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
            table.AddColumn("ID"); 
            table.AddColumn("Name"); 
            table.AddColumn("Type"); 
            table.AddColumn("Purchase Location"); 
            table.AddColumn("Home Location");
            table.AddColumn("Purchase Date"); 
            table.AddColumn("Warranty End"); table.AddColumn("Important");
                
                foreach (var item in duplicates)
                {
                    table.AddRow(item.Id.ToString(), item.Name ?? "-", item.Type ?? "-", item.LocationPurchase ?? "-", item.LocationHome ?? "-", item.PurchaseDate.ToShortDateString(), item.WarrantyEnd.ToShortDateString(), item.IsImportant ? "Yes" : "No");
                }
                AnsiConsole.Write(table);
                Console.WriteLine($"Total duplicates found: {duplicates.Count}");
            }
        }
    }
}
