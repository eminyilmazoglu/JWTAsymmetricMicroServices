using PaginationLibrary;
using ProductUI.API.Dto;
using ProductUI.API.Models;

namespace ProductUI.API.Services
{
    public interface IProductService
    {
        Task<AddProductResponseDto> AddProduct(AddProductRequestDto product);
        Task<UpdateProductResponseDto> UpdateProduct(UpdateProductRequestDto product);
        Task<ResponseDto> DeleteProduct(DeleteProductRequestDto product);
        Task<PaginationListDto> ListProductPagination(SearchParams searchParam);
    }
}
