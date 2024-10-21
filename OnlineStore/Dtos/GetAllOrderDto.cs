namespace OnlineStore.Dtos
{
    public class GetAllOrderDto
    {
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public List<OrderProductToDto> OrderProducts { get; set; } = new List<OrderProductToDto>();
    }
}
