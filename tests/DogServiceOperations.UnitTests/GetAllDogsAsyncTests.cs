using Application.Helpers;
using Application.Interfaces;
using FluentAssertions;
using Moq;

namespace DogServiceOperations.UnitTests;

public class GetAllDogsAsyncTests : TestFixtureBase
{
    [Fact]
    public async Task ShouldReturnInvalidData_WhenOptionsAreInvalid()
    {
        var options = new DogsQueryingOptions(
            SortingOption: SortingOption.Ascending,
            SortingProperty: DogSortingProperty.Weight,
            PageNumber: -2,
            PageSize: 0);

        var result = await dogService.GetAllDogsAsync(options);
        result.operationResult.Should().Be(DogServiceResultStatus.InvalidData);
        result.dataResult.Should().BeNull();
    }

    [Fact]
    public async Task ShouldReturnNotFound_WhenNoDogsExist()
    {
        var options = new DogsQueryingOptions(
            SortingOption: SortingOption.Ascending,
            SortingProperty: DogSortingProperty.Weight,
            PageNumber: 1,
            PageSize: 10);

        var result = await dogService.GetAllDogsAsync(options);
        result.operationResult.Should().Be(DogServiceResultStatus.NotFound);
        result.dataResult.Should().BeNull();
    }

    [Fact]
    public async Task ShouldReturnSuccessWithDogsDto_WhenDogsExist()
    {
        var options = new DogsQueryingOptions(
            SortingOption: SortingOption.Ascending, 
            SortingProperty: DogSortingProperty.Weight, 
            PageNumber: 1, 
            PageSize: 10);

        dogRepositoryMock.Setup(repo => repo.GetAllAsync(options, d => d.Weight)).ReturnsAsync(Dogs); 
        var result = await dogService.GetAllDogsAsync(options);
        result.operationResult.Should().Be(DogServiceResultStatus.Success);
        result.dataResult.Should().BeEquivalentTo(DogDtos);
    }
}
