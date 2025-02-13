using Repository.Repositories;

namespace Repository.Common;

public interface IUnitOfWork
{
    ICategoryRepository CategoryRepository { get; }
}
