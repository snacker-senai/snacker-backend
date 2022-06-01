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
        }

        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
    }
}
