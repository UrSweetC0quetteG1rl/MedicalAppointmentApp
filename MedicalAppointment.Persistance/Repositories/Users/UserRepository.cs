using MedicalAppointment.Persistance.Base;
using MedicalAppointment.Persistance.Context;
using MedicalAppointment.Persistance.Interfaces.Users;
using MedicalAppointment.Persistance.Models.User;
using MedicalAppointmentApp.Domain.Entities.User;
using MedicalAppointmentApp.Domain.Result;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MedicalAppointment.Persistance.Repositories.UserRepository
{
    public class UserRepository(MedicalAppointmentContext medicalAppointmentContext, ILogger<UserRepository> logger)
        : BaseRepository<User>(medicalAppointmentContext), IUserRepository
    {
        private readonly MedicalAppointmentContext _medicalAppointmentContext = medicalAppointmentContext;
        private readonly ILogger<UserRepository> _logger = logger;

        private OperationResult ValidateUserEntity(User entity)
        {
            OperationResult operationResult = new OperationResult();

            if (entity == null)
            {
                operationResult.Success = false;
                operationResult.Message = "La entidad es requerida.";
                return operationResult;
            }

            if (entity.FirstName == null)
            {
                operationResult.Success = false;
                operationResult.Message = "El campo nombre es obligatorio.";
                return operationResult;
            }

            if (entity.LastName == null)
            {
                operationResult.Success = false;
                operationResult.Message = "El campo apellido es obligatorio.";
                return operationResult;
            }

            if (entity.Email == null)
            {
                operationResult.Success = false;
                operationResult.Message = "Su correo electronico es necesario.";
                return operationResult;
            }

            if (entity.Password == null)
            {
                operationResult.Success = false;
                operationResult.Message = "La contraseña es necesaria.";
                return operationResult;
            }

            operationResult.Success = true;
            return operationResult;
        }
        

        public async override Task<OperationResult> Save(User entity)
        {
            OperationResult operationResult = ValidateUserEntity(entity);

            if (!operationResult.Success) return operationResult;

            try
            {
                operationResult = await base.Save(entity);
            }
            catch (Exception ex)
            {
                operationResult = HandleException("Ocurrió un error al guardar.", ex);
            }
            return operationResult;
        }
        public async override Task<OperationResult> Update(User entity)
        {
            OperationResult operationResult = ValidateUserEntity(entity);
            if (!operationResult.Success) return operationResult;
           
            try
            {
                var userToUpdate = await _medicalAppointmentContext.Users.FindAsync(entity.UserId);
                if (userToUpdate == null)
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "Este usuario no existe."
                    };
                }
                UpdateUserProperties(userToUpdate, entity);
                operationResult = await base.Update(userToUpdate);
            }
            catch (Exception ex)
            {
                operationResult = HandleException("Error actualizando el usuario.", ex);
            }
            return operationResult;

        }
        public async override Task<OperationResult> Remove(User entity)
        {
            OperationResult operationResult = new OperationResult();

            try
            {
                User? userToRemove = await _medicalAppointmentContext.Users.FindAsync(entity.UserId);

                if (userToRemove == null)
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "El usuario no existe."
                    };
                }
                if (entity.UserId <= 0)
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "No se encontró con el ID proporcionado."
                    };
                }
                userToRemove.IsActive = false;
                userToRemove.UpdatedAt = entity.UpdatedAt;

                operationResult = await base.Update(userToRemove);
            }
            catch (Exception ex)
            {
                operationResult = HandleException("Error desactivando el usuario.", ex);
            }
            return operationResult;

        }
        public async override Task<OperationResult> GetAll()
        {
            OperationResult operationResult = new OperationResult();

            try
            {
                // Obtiene todos los usuarios activos con sus roles.
                operationResult.Data = await GetUsersWithRolesQuery().ToListAsync();
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "Error obteniendo los usuarios.";
                _logger.LogError(operationResult.Message, ex.ToString());
            }

            return operationResult;


        }
        public async override Task<OperationResult> GetEntityBy(int Id)
        {
            OperationResult operationResult = new OperationResult();

            try
            {
                // Busca el usuario por ID junto con su rol.
                var userWithRole = await GetUsersWithRolesQuery()
                                         .FirstOrDefaultAsync(user => user.UserId == Id);

                if (userWithRole != null)
                {
                    operationResult.Success = true;
                    operationResult.Data = userWithRole;
                }
                else
                {
                    operationResult.Success = false;
                    operationResult.Message = "Usuario no encontrado.";
                }
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "Error obteniendo el usuario.";
                _logger.LogError(operationResult.Message, ex.ToString());
            }

            return operationResult;
        }
        public async Task<OperationResult> GetUserById(int userId)
        {
            OperationResult operationResult = new OperationResult();
            try
            {
                var user = await this._medicalAppointmentContext.Users.FindAsync(userId);

                if (user is null)
                {
                    operationResult.Success = false;
                    operationResult.Message = "El usuario no se encuentra registrado.";
                    return operationResult;
                }
                operationResult.Data = user;
            }
            catch (Exception ex) 
            {
                operationResult.Message = "Ocurrio un error obteniendo el usuario.";
                operationResult.Success = false;
                this._logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }

        private IQueryable<UserRoleModel> GetUsersWithRolesQuery()
        {
            return from user in _medicalAppointmentContext.Users
                   join role in _medicalAppointmentContext.Roles 
                   on user.RoleID equals role.RoleID into userRoles
                   from role in userRoles.DefaultIfEmpty()
                   where user.IsActive == true
                   orderby user.CreatedAt descending
                   select new UserRoleModel
                   {
                       UserId = user.UserId,
                       RoleID = role.RoleID,
                       Role = role.RoleName,
                       FirstName = user.FirstName,
                       LastName = user.LastName,
                       CreatedAt = user.CreatedAt,
                       UpdatedAt = user.UpdatedAt,
                       IsActive = user.IsActive
                   };
        }
        private OperationResult HandleException(string message, Exception ex)
        {
            _logger.LogError(message, ex.ToString());
            return new OperationResult { Success = false, Message = message };
        }
        private void UpdateUserProperties(User target, User source)
        {
            target.RoleID = source.RoleID;
            target.FirstName = source.FirstName;
            target.LastName = source.LastName;
            target.Email = source.Email;
            target.Password = source.Password;
            target.UpdatedAt = source.UpdatedAt;
        }


    }


}
