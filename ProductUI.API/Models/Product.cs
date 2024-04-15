using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ProductUI.API.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string StockCode { get; set; }
        public int StockCount { get; set; }
        public double Price { get; set; }
    }
}
