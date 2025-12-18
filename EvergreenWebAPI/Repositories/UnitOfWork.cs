using EvergreenWebAPI.Repositories.Abstractions;

namespace EvergreenWebAPI.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContex;

    public UnitOfWork(ApplicationDbContext dbContex, IFlowerRepository flowerRepository,
        IFlowersHexColorRepository flowersHexColorRepository, IUserRepository userRepository)
    {
        _dbContex = dbContex;
        FlowerRepository = flowerRepository;
        FlowersHexColorRepository = flowersHexColorRepository;
        UserRepository = userRepository;
    }

    public IFlowerRepository FlowerRepository { get; }

    public IFlowersHexColorRepository FlowersHexColorRepository { get; }

    public IUserRepository UserRepository { get; }

    public async Task SaveChangesAsync()
        => await _dbContex.SaveChangesAsync();
}