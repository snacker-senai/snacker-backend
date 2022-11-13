using Snacker.Domain.Entities;

namespace Snacker.Domain.DTOs
{
    public class ProductAndQuantityDTO
    {
        public ProductAndQuantityDTO() { }
        public ProductAndQuantityDTO(OrderHasProduct orderHasProduct)
        {
            ProductId = orderHasProduct.ProductId;
            Quantity = orderHasProduct.Quantity;
            ProductName = orderHasProduct.Product.Name;
            Price = orderHasProduct.Product.Price;
            Details = orderHasProduct.Details;
            PreReady = orderHasProduct.Product.PreReady;
            IndividualOrderStatus = new OrderStatusDTO
            {
                Id = orderHasProduct.OrderStatus.Id,
                Name = orderHasProduct.OrderStatus.Name
            };
        }

        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Details { get; set; }
        public bool PreReady { get; set; }
        public OrderStatusDTO IndividualOrderStatus { get; set; }
    }
}
