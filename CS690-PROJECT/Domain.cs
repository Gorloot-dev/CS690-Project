namespace CS690_PROJECT
{
    public class Item
    {
        public int Id { get; set;}
        public string? Name { get;set; }
        public string? Type { get; set; }

        public string? LocationHome { get; set; }
        
        public string? LocationPurchase { get; set; }

        public DateTime PurchaseDate { get; set; }
        public DateTime WarrantyEnd { get; set; }
        public bool IsImportant { get; set; }


    public Item(int id, string name, string type, string locationPurchase, string locationHome, DateTime purchaseDate, DateTime warrantyEnd, bool isImportant)
        {
            Id = id;
            Name = name;
            Type = type;
            LocationPurchase = locationPurchase;
            LocationHome = locationHome;
            PurchaseDate = purchaseDate;
            WarrantyEnd = warrantyEnd;
            IsImportant = isImportant;
        }

        // Default constructor for object initializers
        public Item() { }

    }
}