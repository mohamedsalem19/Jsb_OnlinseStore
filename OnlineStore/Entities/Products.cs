
namespace OnlineStore.Entities
{
    public class Products
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Description is Required")]
        public string Description { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Price must be non-negative")]
        public decimal Price { get; set; }


        [Range(0, int.MaxValue, ErrorMessage = "Stock must be non-negative")]
        public int Stock { get; set; }

        public ICollection<OrdersProducts> OrdersProducts { get; set; } = new List<OrdersProducts>();

    }
}
