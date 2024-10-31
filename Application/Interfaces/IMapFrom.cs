using AutoMapper;

namespace Infrastructure.Helpers.Mapper;

public interface IMapFrom<T>
{
    void Mapping(Profile profile)
    {
        profile.CreateMap(typeof(T), GetType());
    }
}
