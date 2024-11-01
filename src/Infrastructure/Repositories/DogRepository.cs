using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Context;

namespace Infrastructure.Repositories;

public class DogRepository(ApiDbContext context) : GenericRepository<DogEntity>(context), IDogRepository
{
    
}
