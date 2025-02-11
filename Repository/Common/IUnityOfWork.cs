using Repository.Repositories;

namespace Repository.Common
{
    public interface IUnityOfWork
    {
        public ICategoryRepository CategoryRepository { get; }
    }
}
