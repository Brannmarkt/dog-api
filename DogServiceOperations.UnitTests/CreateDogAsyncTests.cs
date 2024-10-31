using Application.DTOs;
using Application.Interfaces;
using FluentAssertions;
using Moq;

namespace DogServiceOperations.UnitTests;

public class CreateDogAsyncTests : TestFixtureBase
{
    [Fact]
    public async Task ShouldReturnConflict_WhenDogAlreadyExists()
    {
        var dogDto = DogDtos[0];
        dogRepositoryMock.Setup(repo => repo.GetAsync(d => d.Name == dogDto.Name)).ReturnsAsync(Dogs[0]);
        var result = await dogService.CreateDogAsync(dogDto);
        result.operationResult.Should().Be(DogServiceResultStatus.Conflict);
        result.dataResult.Should().BeNull();
    }

    [Fact]
    public async Task ShouldReturnInvalidData_WhenDogDataIsInvalid()
    {
        var invalidDogDto = new DogDto { Name = "Neo", Color = "black", Weight = -1, TailLength = 0 };
        var result = await dogService.CreateDogAsync(invalidDogDto);
        result.operationResult.Should().Be(DogServiceResultStatus.InvalidData);
        result.dataResult.Should().BeNull();
    }

    [Fact]
    public async Task ShouldReturnSuccess_WhenDogIsCreatedSuccessfully()
    {
        var dogDto = DogDtos[0];
        dogRepositoryMock.Setup(repo => repo.GetAsync(d => d.Name == dogDto.Name)).ReturnsAsync((Domain.Entities.DogEntity)null);
        var result = await dogService.CreateDogAsync(dogDto);
        result.operationResult.Should().Be(DogServiceResultStatus.Success);
        result.dataResult.Should().BeEquivalentTo(dogDto);
    }
}
