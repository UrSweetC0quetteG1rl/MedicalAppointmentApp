using MedicalAppointment.Persistance.Context;
using MedicalAppointment.Persistance.Exceptions;
using MedicalAppointmentApp.Domain.Repositories;
using MedicalAppointmentApp.Domain.Result;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace MedicalAppointment.Persistance.Base
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly MedicalAppointmentContext _MedicalAppointmentContext;
        private DbSet<TEntity> entities;
        private readonly ILogger<BaseRepository<TEntity>> _logger;

        //Se inyecta el contexto para evitar dependencia
        public BaseRepository(MedicalAppointmentContext medicalAppointmentContext, ILogger<BaseRepository<TEntity>> logger) { //En caso de aceptar otro tipo de contextos se puede ponder DbContext porque hereda de esta
            _MedicalAppointmentContext = medicalAppointmentContext;
            _logger = logger;
            this.entities = _MedicalAppointmentContext.Set<TEntity>();
        }

        protected async Task<OperationResult> ExecuteOperationWithLogging(Func<Task<OperationResult>> operation, string errorMessage)
        {
            var operationResult = new OperationResult();
            try
            {
                return await operation();
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = errorMessage;
                _logger.LogError(ex, errorMessage);
                return operationResult;
            }
        }

        public virtual async Task<bool> Exists(Expression<Func<TEntity, bool>> filter)
        {
            try
            {
                return await this.entities.AnyAsync(filter);
            }
            catch (Exception ex)
            {
                throw new EntityExistsException("Esta entidad ya existe.", ex);
            }
        }
        public virtual async Task<OperationResult> GetAll()
        {
            OperationResult result = new OperationResult();

            try
            {
                var datos = await this.entities.ToListAsync();
                result.Data = datos;
            }
            catch (Exception ex) 
            {
                result.Success = false;
                result.Message = $"Ocurrio un error {ex.Message} obteniendo los datos. ";
            }
            return result;
        }
        public virtual async Task<OperationResult> GetEntityBy(int ID)
        {
            OperationResult result = new OperationResult();
            try
            {
                var entity = await this.entities.FindAsync(ID);
                result.Data = entity;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Ocurrio un error {ex.Message} obteniendo la entidad.";
            }
            return result;

        }
        public virtual async Task<OperationResult> Remove(TEntity entity)
        {
            OperationResult result = new OperationResult();

            try
            {
                entities.Remove(entity);
                await _MedicalAppointmentContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Ocurrio un error {ex.Message} removiendo la entidad.";
            }
            return result;
        }
        public virtual async Task<OperationResult> Save(TEntity entity)
        {
            OperationResult result = new OperationResult();
            try
            {
                entities.Add(entity);
                await _MedicalAppointmentContext.SaveChangesAsync();
            }
            catch (DbUpdateException dbEx)
            {
                result.Success = false;
                result.Message = $"Error al guardar la entidad: {dbEx.Message}. Detalles: {dbEx.InnerException?.Message}";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Ocurrio un error {ex.Message} guardando la entidad.";
            }
            return result;
        }
        public virtual async Task<OperationResult> Update(TEntity entity)
        {
            OperationResult result = new OperationResult();
            try
            {
                entities.Update(entity);
                await _MedicalAppointmentContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message =  $"Ocurrio un error {ex.Message} actualizando la entidad.";
            }
            return result;
        }
    }
}
