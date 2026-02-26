using AutoMapper;
using Catolog.DTOs.CategoryDTOs;
using Catolog.DTOs.ProductDetailDTOs;
using Catolog.DTOs.ProductDTOs;
using Catolog.DTOs.ProductImagesDTOs;
using Catolog.Entities;

namespace Catolog.Mapping
{
    public class GeneralMapping : Profile
    {
        //mapping lerin amacı: dto ile entity leri eşletirmek
        public GeneralMapping()
        {
            CreateMap<Category, ResultCategoryDTOs>().ReverseMap();//Category nesnesi ile ResultCategoryDTOs daki nesneleri eşleştirmek
            CreateMap<Category, CreateCategoryDTOs>().ReverseMap();
            CreateMap<Category, UpdateCategoryDTOs>().ReverseMap();
            CreateMap<Category, GetByIdCategoryDTOs>().ReverseMap();

            CreateMap<Product, ResultProductDTOs>().ReverseMap();
            CreateMap<Product, CreateProductDTOs>().ReverseMap();
            CreateMap<Product, UpdateProductDTOs>().ReverseMap();
            CreateMap<Product, GetByIdProductDTOs>().ReverseMap();

            CreateMap<ProductDetail, ResultProductDetailDTOs>().ReverseMap();
            CreateMap<ProductDetail, CreateProductDetailDTOs>().ReverseMap();
            CreateMap<ProductDetail, UpdateProductDetailDTOs>().ReverseMap();
            CreateMap<ProductDetail, GetByIdProductDetailDTOs>().ReverseMap();

            CreateMap<ProductImages, ResultProductImagesDTOs>().ReverseMap();
            CreateMap<ProductImages, CreateProductImagesDTOs>().ReverseMap();
            CreateMap<ProductImages, UpdateProductImagesDTOs>().ReverseMap();
            CreateMap<ProductImages, GetByIdProductImagesDTOs>().ReverseMap();

        }
    }
}
