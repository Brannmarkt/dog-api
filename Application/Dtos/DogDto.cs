using AutoMapper;
using Domain.Entities;
using Infrastructure.Helpers.Mapper;

namespace Application.DTOs;

public class DogDto : IMapFrom<DogEntity>
{
    public required string Name { get; set; }
    public required string Color { get; set; }
    public int TailLength { get; set; }
    public int Weight { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<DogEntity, DogDto>().ReverseMap();
    }
}
