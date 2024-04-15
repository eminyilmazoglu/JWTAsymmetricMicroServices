using AutoMapper;
using ProductUI.API.Dto;
using ProductUI.API.Models;

namespace ProductUI.API.AutoMapperConfigProfile
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Product To AddProductResponseDto
            CreateMap<Product, AddProductResponseDto>()
        .ForMember(dest => dest.StockCode, opt => opt.MapFrom(src => src.StockCode));

            // AddProductRequestDto To Product
            CreateMap<AddProductRequestDto, Product>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.StockCount, opt => opt.MapFrom(src => src.StockCount))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.StockCode, opt => opt.Ignore());

            // UpdateProductRequestDto To Product
            CreateMap<UpdateProductRequestDto, Product>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.StockCount, opt => opt.MapFrom(src => src.StockCount))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.StockCode, opt => opt.Ignore());
        }
    }
}
