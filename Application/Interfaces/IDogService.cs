using Application.DTOs;
using Application.Helpers;

namespace Application.Interfaces;

public enum DogServiceResult
{
    Success,
    NotFound,
    Conflict,
    InvalidData
}

public enum DogSortingProperty
{
    Weight,
    TailLength
}

public interface IDogService
{
    Task<(DogServiceResult Result, IEnumerable<DogDto>? DogsDto)> GetAllDogsAsync(DogsQueryingOptions options);
    Task<(DogServiceResult Result, DogDto? DogDto)> GetDogByNameAsync(string name);
    Task<DogServiceResult> CreateDogAsync(DogDto dogDto);
}
