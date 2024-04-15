using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaginationLibrary;
using ProductUI.API.Contextes;
using ProductUI.API.Dto;
using ProductUI.API.Models;
using System.Linq.Expressions;
using System.Text.Json;

namespace ProductUI.API.Services
{
    public class ProductService : IProductService
    {
        private readonly ProductAPIDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductService> _logger;

        public ProductService(ProductAPIDbContext context, IMapper mapper, ILogger<ProductService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<AddProductResponseDto> AddProduct(AddProductRequestDto addProductRequestDto)
        {
            _logger.LogInformation("AddProduct fonksiyonu basladi...");
            AddProductResponseDto result;
            try
            {
                var product = _mapper.Map<Product>(addProductRequestDto);
                product.StockCode = Guid.NewGuid().ToString();
                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                result = _mapper.Map<AddProductResponseDto>(product);
                result.status = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Concat("AddProduct fonksiyonunda hata olustu: ", ex.Message));
                result = new AddProductResponseDto() { status = false, message = "Ürün eklenirken hata oluştu!" };
            }
            return result;
        }

        public async Task<ResponseDto> DeleteProduct(DeleteProductRequestDto product)
        {
            _logger.LogInformation("DeleteProduct fonksiyonu basladi...");
            try
            {
                var deleteProduct = await _context.Products.Where(x => x.StockCode == product.StockCode).ExecuteDeleteAsync();

                if (deleteProduct != null && deleteProduct > 0)
                {
                    return new ResponseDto() { status = true };
                }
                else
                    return new ResponseDto() { status = false, message = "Silme İşlemi Başarısız!" };
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Concat("DeleteProduct fonksiyonunda hata olustu: "), ex.Message);
                return new ResponseDto() { status = false, message = "Silme İşlemi Başarısız!" };
            }

        }

        public async Task<PaginationListDto> ListProductPagination(SearchParams searchParam)
        {
            _logger.LogInformation("ListProductPagination fonksiyonu basladi...");
            List<ColumnFilter> columnFilters = new List<ColumnFilter>();
            if (!String.IsNullOrEmpty(searchParam.ColumnFilters))
            {
                try
                {
                    columnFilters.AddRange(JsonSerializer.Deserialize<List<ColumnFilter>>(searchParam.ColumnFilters));
                }
                catch (Exception)
                {
                    columnFilters = new List<ColumnFilter>();
                }
            }

            List<ColumnSorting> columnSorting = new List<ColumnSorting>();
            if (!String.IsNullOrEmpty(searchParam.OrderBy))
            {
                try
                {
                    columnSorting.AddRange(JsonSerializer.Deserialize<List<ColumnSorting>>(searchParam.OrderBy));
                }
                catch (Exception)
                {
                    columnSorting = new List<ColumnSorting>();
                }
            }

            Expression<Func<Product, bool>> filters = null;
            //Öncelikle SearchTerm’imizi kontrol ediyoruz. Eğer bilgi içeriyorsa filtre oluşturuyoruz.
            var searchTerm = "";
            if (!string.IsNullOrEmpty(searchParam.SearchTerm))
            {
                searchTerm = searchParam.SearchTerm.Trim().ToLower();
                filters = x => x.StockCode.ToLower().Contains(searchTerm); // Hizli arama bu sadece id ile arama içinde kullanılabilir.
            }
            // Daha sonra, ColumnFilters'ta veri varsa, bir filtrenin üzerine yazıyoruz.
            if (columnFilters.Count > 0)
            {
                var customFilter = CustomExpressionFilter<Product>.CustomFilter(columnFilters, new Product());
                filters = customFilter;
            }

            PaginationListDto paginationListDto = new PaginationListDto(); ;
            try
            {
                var query = _context.Products.AsQueryable().CustomQuery(filters);
                var count = query.Count();
                var filteredData = query.CustomPagination(searchParam.PageNumber, searchParam.PageSize).ToList();

                var pagedList = new PagedList<Product>(filteredData, count, searchParam.PageNumber, searchParam.PageSize);

                if (pagedList != null)
                {
                    paginationListDto.DataList = pagedList;
                    paginationListDto.PaginationInfo = pagedList.MetaData;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Concat("ListProductPagination fonksiyonunda hata olustu: ", ex.Message));
                paginationListDto = new PaginationListDto();
            }

            return paginationListDto;
        }

        public async Task<UpdateProductResponseDto> UpdateProduct(UpdateProductRequestDto productStockCode)
        {
            _logger.LogInformation("UpdateProduct fonksiyonu basladi...");
            UpdateProductResponseDto updateProductResponse;
            try
            {
                var product = await _context.Products.FirstOrDefaultAsync(x => x.StockCode == productStockCode.StockCode);
                var mappingProduct = _mapper.Map<Product>(productStockCode);
                mappingProduct.StockCode = product.StockCode;
                mappingProduct.Id = product.Id;

                _context.Products.Entry(product).CurrentValues.SetValues(mappingProduct);
                await _context.SaveChangesAsync();

                updateProductResponse = new UpdateProductResponseDto() { status = true };
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Concat("UpdateProduct fonksiyonunda hata olustu: ", ex.Message));
                updateProductResponse = new UpdateProductResponseDto() { status = false, message = "Güncelleme işleminde bir hata oluştu!" };
            }

            return updateProductResponse;
        }
    }
}
