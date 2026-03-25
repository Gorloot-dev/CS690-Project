namespace CS690_PROJECT;

class Program
{
    static void Main(string[] args)
    {
        var manager = new InventoryManager();
        

       ConsoleUI ui = new ConsoleUI(manager);
        ui.Run();
        
        
        Console.WriteLine("bye!");
    }
}