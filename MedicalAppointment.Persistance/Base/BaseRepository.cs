using MedicalAppointment.Persistance.Context;
using MedicalAppointmentApp.Domain.Repositories;
using MedicalAppointmentApp.Domain.Result;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MedicalAppointment.Persistance.Base
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly MedicalAppointmentContext _MedicalAppointmentContext;
        private DbSet<TEntity> entities;

        //Se inyecta el contexto para evitar dependencia
        public BaseRepository(MedicalAppointmentContext medicalAppointmentContext) { //En caso de aceptar otro tipo de contextos se puede ponder DbContext porque hereda de esta
            _MedicalAppointmentContext = medicalAppointmentContext;
            this.entities = _MedicalAppointmentContext.Set<TEntity>();
        }

        public virtual async Task<bool> Exists(Expression<Func<TEntity, bool>> filter)
        {
            
            try
            {
                var exists = await this.entities.AnyAsync(filter);
                return exists;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocurrio el siguiente error: {ex.Message} verificando que existe el registro");
                return false;
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
        public virtual async Task<OperationResult> RemoveById(int id)
        {
            OperationResult result = new OperationResult();
            try
            {
                var entity = await this.entities.FindAsync(id);
                if (entity == null)
                {
                    result.Success = false;
                    result.Message = $"No se encontró la entidad con ID {id}.";
                    return result;
                }

                entities.Remove(entity);
                await _MedicalAppointmentContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Ocurrió un error {ex.Message} removiendo la entidad.";
            }
            return result;
        }
    }
}
