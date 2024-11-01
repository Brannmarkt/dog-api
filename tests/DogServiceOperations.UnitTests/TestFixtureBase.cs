using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Services;
using Moq;

namespace DogServiceOperations.UnitTests;

public abstract class TestFixtureBase
{
    protected List<DogEntity> Dogs { get; }
    protected List<DogDto> DogDtos { get; }
    protected Mock<IDogRepository> dogRepositoryMock { get; }
    protected Mock<IMapper> mapperMock { get; }
    protected DogService dogService { get; }

    protected TestFixtureBase()
    {
        dogRepositoryMock = new Mock<IDogRepository>();
        mapperMock = new Mock<IMapper>();
        dogService = new DogService(dogRepositoryMock.Object, mapperMock.Object);

        Dogs = new List<DogEntity>
        {
            new DogEntity { Name = "Neo", Color = "white", Weight = 20, TailLength = 15 },
            new DogEntity { Name = "Jessy", Color = "amber", Weight = 25, TailLength = 8 },
            new DogEntity { Name = "Max", Color = "black", Weight = 30, TailLength = 10 }
        };

        DogDtos = new List<DogDto>
        {
            new DogDto { Name = "Neo", Color = "white", Weight = 20, TailLength = 15 },
            new DogDto { Name = "Jessy", Color = "amber", Weight = 25, TailLength = 8 },
            new DogDto { Name = "Max", Color = "black", Weight = 30, TailLength = 10 }
        };

        for (int i = 0; i < Dogs.Count; i++)
        {
            mapperMock.Setup(m => m.Map<DogDto>(Dogs[i])).Returns(DogDtos[i]);
            mapperMock.Setup(m => m.Map<DogEntity>(DogDtos[i])).Returns(Dogs[i]);
        }
        mapperMock.Setup(m => m.Map<IEnumerable<DogDto>>(Dogs)).Returns(DogDtos);
        mapperMock.Setup(m => m.Map<IEnumerable<DogDto>>(It.IsAny<IEnumerable<DogEntity>>())).Returns(DogDtos);
    }
}
