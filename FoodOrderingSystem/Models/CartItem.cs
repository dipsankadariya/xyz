namespace FoodOrderingSystem.Models
{
    public class CartItem
    {

        public int CartId { get; set; }
        public int ItemId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Subtotal => Price * Quantity;

        public string ImageUrl { get; set; }
    }

}