namespace OnlineStore.Dtos
{
    public class OrderProductToDto
    {
        public int ProductId { get; set; }
        public ProductOrderDto Product { get; set; }
    }
    public class ProductOrderDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }


}
