namespace FirstASPDotNetCoreWebAPIProject.Models
{
    public class Product
    {
        public  int Id { get; set; }
        public string? Name { get; set; }
        public required int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
