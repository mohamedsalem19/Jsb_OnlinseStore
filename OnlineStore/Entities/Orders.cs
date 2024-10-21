namespace OnlineStore.Entities
{
    public class Orders
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal TotalAmount { get; set; }

        public ICollection<OrdersProducts> OrdersProducts { get; set; } = new List<OrdersProducts>();
    }
}
