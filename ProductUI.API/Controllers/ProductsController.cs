using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaginationLibrary;
using ProductUI.API.Dto;
using ProductUI.API.Services;

namespace ProductUI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("ProductAdd")]
        [Authorize(Roles = "PRODUCTADD")]
        public async Task<IActionResult> ProductAdd(AddProductRequestDto addProductRequestDto)
        {
            if (addProductRequestDto != null)
            {
                AddProductResponseDto addProductResponseDto = await _productService.AddProduct(addProductRequestDto);
                return Ok(addProductResponseDto);
            }
            else
                return BadRequest();
        }

        [HttpPut("ProductUpdate")]
        [Authorize(Roles = "PRODUCTUPDATE")]
        public async Task<IActionResult> ProductUpdate(UpdateProductRequestDto updateProductRequestDto)
        {
            if (updateProductRequestDto != null)
            {
                // ex: [{"id":"name","value":"do"}]
                // search: stockCode ile yapilir...
                UpdateProductResponseDto updateProductResponseDto = await _productService.UpdateProduct(updateProductRequestDto);
                return Ok(updateProductResponseDto);
            }
            else
                return BadRequest();
        }

        [HttpDelete("ProductDelete")]
        [Authorize(Roles = "PRODUCTDELETE")]
        public async Task<IActionResult> ProductDelete(DeleteProductRequestDto deleteProductRequestDto)
        {

            if (deleteProductRequestDto != null)
            {
                ResponseDto responseDto = await _productService.DeleteProduct(deleteProductRequestDto);
                return Ok(responseDto);
            }
            else
                return BadRequest();
        }

        [HttpGet("ProductList")]
        [Authorize(Roles = "PRODUCTREAD")]
        public async Task<IActionResult> ProductListPagination([FromQuery] SearchParams searchParams)
        {
            if (searchParams != null)
            {
                PaginationListDto paginationListDto = await _productService.ListProductPagination(searchParams);
                return Ok(paginationListDto);
            }
            else
                return BadRequest();
        }
    }
}
