using AutoMapper;
using FirstASPDotNetCoreWebAPIProject.DTOs;
using FirstASPDotNetCoreWebAPIProject.Models;

namespace FirstASPDotNetCoreWebAPIProject.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>()
             .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(scr => scr.Category.Name));

            CreateMap<CreateOrUpdateProductDTO, Product>();

        }
    }
}
