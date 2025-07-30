using Ambev.DeveloperEvaluation.Application.DTOs;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Mappers
{
    public class SaleMappingProfile : Profile
    {
        public SaleMappingProfile()
        {
            CreateMap<Sale, SaleDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

            CreateMap<SaleItem, SaleDto.SaleItemDto>();
        }
    }
}
