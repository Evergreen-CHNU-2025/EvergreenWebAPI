namespace EvergreenWebAPI.Repositories.Abstractions;

public interface IUnitOfWork
{
    IFlowerRepository FlowerRepository { get; }
    IFlowersHexColorRepository FlowersHexColorRepository { get; }
    IUserRepository UserRepository { get; }

    Task SaveChangesAsync();
}