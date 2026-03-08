using AutoMapper;
using Catolog.DTOs.CategoryDTOs;
using Catolog.DTOs.FeatureSliderDto;
using Catolog.DTOs.ProductDetailDTOs;
using Catolog.DTOs.ProductDTOs;
using Catolog.DTOs.ProductImagesDTOs;
using Catolog.Entities;

namespace Catolog.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            // Category Mappings
            CreateMap<Category, ResultCategoryDTOs>().ReverseMap();
            CreateMap<Category, CreateCategoryDTOs>().ReverseMap();
            CreateMap<Category, UpdateCategoryDTOs>().ReverseMap();
            CreateMap<Category, GetByIdCategoryDTOs>().ReverseMap();

            // Product Mappings
            CreateMap<Product, ResultProductDTOs>().ReverseMap();
            CreateMap<Product, CreateProductDTOs>().ReverseMap();
            CreateMap<Product, UpdateProductDTOs>().ReverseMap();
            CreateMap<Product, GetByIdProductDTOs>().ReverseMap();

            // ProductWithCategory -> ResultProductDTOs (Kritik Alan)
            CreateMap<ProductWithCategory, ResultProductDTOs>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src =>
                    src.Categories != null && src.Categories.Any() ? src.Categories.FirstOrDefault() : null))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src =>
                    src.Categories != null && src.Categories.Any() ? src.Categories.FirstOrDefault().CategoryName : "Kategori Yok"));

            // ProductDetail Mappings
            CreateMap<ProductDetail, ResultProductDetailDTOs>().ReverseMap();
            CreateMap<ProductDetail, CreateProductDetailDTOs>().ReverseMap();
            CreateMap<ProductDetail, UpdateProductDetailDTOs>().ReverseMap();
            CreateMap<ProductDetail, GetByIdProductDetailDTOs>().ReverseMap();

            // ProductImages Mappings
            CreateMap<ProductImages, ResultProductImagesDTOs>().ReverseMap();
            CreateMap<ProductImages, CreateProductImagesDTOs>().ReverseMap();
            CreateMap<ProductImages, UpdateProductImagesDTOs>().ReverseMap();
            CreateMap<ProductImages, GetByIdProductImagesDTOs>().ReverseMap();

            CreateMap<FeatureSlider, ResultFeatureSliderDto>().ReverseMap();
            CreateMap<FeatureSlider, CreateFeatureSliderDto>().ReverseMap();
            CreateMap<FeatureSlider, UpdateFeatureSliderDto>().ReverseMap();
            CreateMap<FeatureSlider, GetByIdFeatureSliderDto>().ReverseMap();
        }
    }
}
