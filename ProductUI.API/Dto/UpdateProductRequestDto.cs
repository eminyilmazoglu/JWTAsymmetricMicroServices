namespace ProductUI.API.Dto
{
    public class UpdateProductRequestDto : AddProductRequestDto
    {
        public string StockCode { get; set; }
    }
}
