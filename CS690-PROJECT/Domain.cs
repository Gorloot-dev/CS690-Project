namespace CS690_PROJECT
{
    public class Item
    {
        public int Id { get; set;}
        public string? Name { get;set; }
        public string? Type { get; set; }
        public string? Location { get; set; }


        public DateTime PurchaseDate;
        public DateTime WarrantyEnd;
        public bool IsImportant;
    }
}