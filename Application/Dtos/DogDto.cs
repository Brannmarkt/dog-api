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

    public DogDto()
    {
        Name = string.Empty;
        Color = string.Empty;
        TailLength = 0;
        Weight = 0;
    }

    public DogDto(string name, string color, int tailLength, int weight)
    {
        Name = name;
        Color = color;
        TailLength = tailLength;
        Weight = weight;
    }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<DogEntity, DogDto>().ReverseMap();
    }
}
