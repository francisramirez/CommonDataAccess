
namespace MTK.DataAccess.Contracts
{
    using System.Threading.Tasks;

    public interface IUnitOfWork
    {
        Task<bool> Commit();
        Task<bool> RollBack();
    }
}
