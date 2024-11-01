using Application.DTOs;
using Application.Helpers;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DogsController(IDogService dogService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllDogs([FromQuery] DogsQueryingOptions options)
    {
        var result = await dogService.GetAllDogsAsync(options);
        return result.operationResult switch
        {
            DogServiceResultStatus.NotFound => NotFound("No dogs were found"),
            DogServiceResultStatus.InvalidData => BadRequest("Page size and number should be positive numbers"),
            DogServiceResultStatus.Success => Ok(result.dataResult),
            _ => StatusCode(500, "An unexpected error occurred")
        };
    }

    [HttpGet("{name}")]
    public async Task<IActionResult> GetDogByName(string name)
    {
        var result = await dogService.GetDogByNameAsync(name);
        return result.operationResult switch
        {
            DogServiceResultStatus.NotFound => NotFound($"There is no dog named '{name}'"),
            DogServiceResultStatus.Success => Ok(result.dataResult), 
            _ => StatusCode(500, "An unexpected error occurred")
        };
    }

    [HttpPost]
    public async Task<IActionResult> CreateDog([FromBody] DogDto dogDto)
    {
        var result = await dogService.CreateDogAsync(dogDto);
        return result.operationResult switch
        {
            DogServiceResultStatus.InvalidData => BadRequest("Weight and tail length should be positive numbers"),
            DogServiceResultStatus.Conflict => BadRequest($"A dog named {dogDto.Name} already exists"),
            DogServiceResultStatus.Success => Ok(result.dataResult),
            _ => StatusCode(500, "An unexpected error occurred")
        };
    }
}
