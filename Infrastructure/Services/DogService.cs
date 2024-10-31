using Application.DTOs;
using Application.Helpers;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;

namespace Infrastructure.Services;

public class DogService(IDogRepository dogRepository, IMapper mapper) : IDogService
{
    public async Task<(DogServiceResult, IEnumerable<DogDto>?)> GetAllDogsAsync(DogsQueryingOptions options)
    {
        if (options == null || options.PageNumber <= 0 || options.PageSize <= 0)
        {
            return (DogServiceResult.InvalidData, null);
        }
        IEnumerable<DogEntity> dogs = options.SortingProperty switch
        {
            DogSortingProperty.Weight => await dogRepository.GetAllAsync(options, d => d.Weight),
            DogSortingProperty.TailLength => await dogRepository.GetAllAsync(options, d => d.TailLength),
            _ => await dogRepository.GetAllAsync(options)
        };
        
        if (!dogs.Any()) 
        {
            return (DogServiceResult.NotFound, null);
        }
        var dogsDto = dogs.Select(d => mapper.Map<DogDto>(d));
        return (DogServiceResult.Success, dogsDto);
    }

    public async Task<(DogServiceResult, DogDto?)> GetDogByNameAsync(string name)
    {
        var dog = await dogRepository.GetAsync(u => u.Name == name);
        if (dog == null)
        {
            return (DogServiceResult.NotFound, null);
        }
        var dogDto = mapper.Map<DogDto>(dog);
        return (DogServiceResult.Success, dogDto);
    }

    public async Task<DogServiceResult> CreateDogAsync(DogDto dogDto)
    {
        if (await dogRepository.GetAsync(u => u.Name == dogDto.Name) != null)
        {
            return DogServiceResult.Conflict;
        }

        if(dogDto.Weight <= 0 || dogDto.Weight <= 0)
        {
            return DogServiceResult.InvalidData;
        }
        var dog = mapper.Map<DogEntity>(dogDto);
        await dogRepository.AddAsync(dog);
        return DogServiceResult.Success;
    }
}
