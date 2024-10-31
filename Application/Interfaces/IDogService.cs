using Application.DTOs;
using Application.Helpers;

namespace Application.Interfaces;

public record DogServiceResult<T>
(
    DogServiceResultStatus operationResult,
    T? dataResult
);

public enum DogServiceResultStatus
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
    Task<DogServiceResult<IEnumerable<DogDto>>> GetAllDogsAsync(DogsQueryingOptions options);
    Task<DogServiceResult<DogDto>> GetDogByNameAsync(string name);
    Task<DogServiceResult<DogDto?>> CreateDogAsync(DogDto dto);
}
