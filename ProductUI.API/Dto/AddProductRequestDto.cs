using ProductUI.API.Models;

namespace ProductUI.API.Dto
{
    public class AddProductRequestDto
    {
        public string Name { get; set; }
        public int StockCount{ get; set; }
        public double Price { get; set; }
    }
}
