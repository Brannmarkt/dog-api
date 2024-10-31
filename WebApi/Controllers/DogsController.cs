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
        return result.Result switch
        {
            DogServiceResult.NotFound => NotFound("No dogs were found"),
            DogServiceResult.InvalidData => BadRequest("Page size and number should be positive numbers"),
            DogServiceResult.Success => Ok(result.DogsDto),
            _ => StatusCode(500, "An unexpected error occurred")
        };
    }

    [HttpGet("{name}")]
    public async Task<IActionResult> GetDogByName(string name)
    {
        var result = await dogService.GetDogByNameAsync(name);
        return result.Result switch
        {
            DogServiceResult.NotFound => NotFound($"There is no dog named '{name}'"),
            DogServiceResult.Success => Ok(result.DogDto), 
            _ => StatusCode(500, "An unexpected error occurred")
        };
    }

    [HttpPost]
    public async Task<IActionResult> CreateDog([FromBody] DogDto dogDto)
    {
        var result = await dogService.CreateDogAsync(dogDto);
        return result switch
        {
            DogServiceResult.InvalidData => BadRequest("Weight and tail length should be positive numbers"),
            DogServiceResult.Conflict => BadRequest($"A dog named {dogDto.Name} already exists"),
            DogServiceResult.Success => Ok(result.ToString()),
            _ => StatusCode(500, "An unexpected error occurred")
        };
    }
}
