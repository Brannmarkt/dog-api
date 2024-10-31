using Application.Interfaces;
using FluentAssertions;
using Moq;

namespace DogServiceOperations.UnitTests;

public class GetDogByNameAsyncTests : TestFixtureBase
{
    [Fact]
    public async Task ShouldReturnNotFound_WhenDogDoesNotExist()
    {
        dogRepositoryMock.Setup(repo => repo.GetAsync(d => d.Name == "Unknown")).ReturnsAsync((Domain.Entities.DogEntity)null);
        var result = await dogService.GetDogByNameAsync("Unknown");
        result.operationResult.Should().Be(DogServiceResultStatus.NotFound);
        result.dataResult.Should().BeNull();
    }

    [Fact]
    public async Task ShouldReturnSuccessWithDogDto_WhenDogExists()
    {
        dogRepositoryMock.Setup(repo => repo.GetAsync(d => d.Name == "Neo")).ReturnsAsync(Dogs[0]);
        var result = await dogService.GetDogByNameAsync("Neo");
        result.operationResult.Should().Be(DogServiceResultStatus.Success);
        result.dataResult.Should().BeEquivalentTo(DogDtos[0]);
    }
}
