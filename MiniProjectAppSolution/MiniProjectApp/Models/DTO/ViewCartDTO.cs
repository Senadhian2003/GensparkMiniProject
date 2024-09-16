namespace MiniProjectApp.Models.DTO
{
    public class ViewCartDTO
    {

        public double Total { get; set; }
        public double discount {  get; set; }
        public List<CartItemDTO> Items { get; set; }

    }
}
