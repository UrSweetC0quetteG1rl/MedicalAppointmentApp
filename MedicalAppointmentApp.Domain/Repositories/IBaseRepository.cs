using MedicalAppointmentApp.Domain.Result;
using System.Linq.Expressions;

namespace MedicalAppointmentApp.Domain.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        //repositorio
        Task<OperationResult> Save(TEntity entity);
        Task<OperationResult> Update(TEntity entity);
        Task<OperationResult> Remove (TEntity entity);
        Task<OperationResult> GetAll();
        Task<OperationResult> GetEntityBy(int ID);
        Task<OperationResult> Exists(Expression<Func<TEntity, bool>> filter);
    }
}
