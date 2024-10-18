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
        private readonly MedicalAppointmentContext _medicalAppointmentContext;
        private readonly ILogger<UserRepository> logger;

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

            if (entity.PhoneNumber == null)
            {
                operationResult.Success = false;
                operationResult.Message = "El campo teléfono es obligatorio.";
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

            if (entity.RoleID == 0)
            {
                operationResult.Success = false;
                operationResult.Message = "Su rol es requerido.";
                return operationResult;
            }

            operationResult.Success = true;
            return operationResult;
        }
        private async Task<OperationResult> ExecuteOperationWithLogging(Func<Task<OperationResult>> operation, string errorMessage)
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
                logger.LogError(ex, errorMessage);
                return operationResult;
            }
        }

        public async override Task<OperationResult> Save(User entity)
        {
            OperationResult operationResult = ValidateUserEntity(entity);

            if (!operationResult.Success)
            {
                return operationResult;
            }

            if (await base.Exists(user => user.Email == entity.Email))
            {
                return new OperationResult
                {
                    Success = false,
                    Message = "Este usuario ya se encuentra registrado."
                };

            }
            return await ExecuteOperationWithLogging(() => base.Save(entity), "Error guardando el usuario.");
        }
        public async override Task<OperationResult> Update(User entity)
        {
            OperationResult operationResult = ValidateUserEntity(entity);

            if (!operationResult.Success)
            {
                return operationResult;
            }

            return await ExecuteOperationWithLogging(async () =>
            {
                User? userToUpdate = await _medicalAppointmentContext.User.FindAsync(entity.UserId);
                if (userToUpdate == null)
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "Este usuario no existe."
                    };
                }
                userToUpdate.RoleID = entity.RoleID;
                userToUpdate.FirstName = entity.FirstName;
                userToUpdate.LastName = entity.LastName;
                userToUpdate.PhoneNumber = entity.PhoneNumber;
                userToUpdate.Email = entity.Email;
                userToUpdate.Password = entity.Password;
                userToUpdate.UpdatedAt = entity.UpdatedAt;//o DateTime.Now ya que se acaba de actualizar

                return await base.Update(userToUpdate);
            }, "Error actualizando el usuario.");
        }
        public async override Task<OperationResult> Remove(User entity)
        {
            OperationResult operationResult = ValidateUserEntity(entity);

            if (!operationResult.Success) { return operationResult; }

            return await ExecuteOperationWithLogging(async () =>
            {
                User? userToRemove = await _medicalAppointmentContext.User.FindAsync(entity.UserId);

                if (userToRemove == null)
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "El usuario no existe."
                    };
                }
                userToRemove.IsActive = false;
                userToRemove.UpdatedAt = entity.UpdatedAt;

                return await base.Update(userToRemove);
            }, "Error desactivando el usuario.");
        }
        public async override Task<OperationResult> GetAll()
        {
            return await ExecuteOperationWithLogging(async () =>
            {
                var usersWithRoles = await GetUsersWithRolesQuery().ToListAsync();

                return new OperationResult
                {
                    Success = true,
                    Data = usersWithRoles
                };
            }, "Error obteniendo los usuarios.");

        }
        public async override Task<OperationResult> GetEntityBy(int Id)
        {
            return await ExecuteOperationWithLogging(async () =>
            {
                var userWithRol = await GetUsersWithRolesQuery()
                                       .FirstOrDefaultAsync(user => user.UserId == Id);

                return new OperationResult
                {
                    Success = true,
                    Data = userWithRol
                };
            }, "Error obteniendo el usuario.");
        }

        public async Task<OperationResult> ConfirmUserRegistration(string email)
        {
            return await ExecuteOperationWithLogging(async () =>
            {
                var user = await _medicalAppointmentContext.Users
                    .FirstOrDefaultAsync(user => user.Email == email && user.IsActive == false);

                if (user == null)
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "El usuario no existe o ya ha sido confirmado."
                    };
                }

                user.IsActive = true;
                user.UpdatedAt = DateTime.Now;

                return await base.Update(user);
            }, "Error confirmando el registro del usuario.");
        }
        public async Task<OperationResult> ForgotPassword(string email)
        {
            return await ExecuteOperationWithLogging(async () =>
            {
                var user = await _medicalAppointmentContext.Users
                    .FirstOrDefaultAsync(user => user.Email == email && user.IsActive == true);

                if (user == null)
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "El usuario no está registrado o está inactivo."
                    };
                }

                //Simular que es el link al que se le enviara un token para cambiar password
                var resetToken = Guid.NewGuid().ToString();
                var resetLink = $"https://MedicalAppointmentApp.com/reset-password?token={resetToken}&email={email}";

                return new OperationResult
                {
                    Success = true,
                    Message = $"Enlace para recuperar contraseña enviado al correo: {resetLink}"
                };
            }, "Error enviando la recuperación de contraseña.");
        }
        public async Task<OperationResult> GetUserByEmailAndPassword(string email, string password)
        {
            return await ExecuteOperationWithLogging(async () =>
            {
                var user = await _medicalAppointmentContext.Users
                    .FirstOrDefaultAsync(user => user.Email == email && user.Password == password && user.IsActive == true);

                if (user == null)
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "Credenciales invalidas."
                    };
                }

                return new OperationResult
                {
                    Success = true,
                    Data = user
                };
            }, "Error verificando sus credenciales.");
        }
        public async Task<OperationResult> IsEmailInUse(string email)
        {
            return await ExecuteOperationWithLogging(async () =>
            {
                var emailExists = await _medicalAppointmentContext.Users
                    .AnyAsync(user => user.Email == email);

                if (emailExists)
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "Ya existe un usuario con este correo."
                    };
                }

                return new OperationResult
                {
                    Success = true,
                    Message = "El correo está disponible."
                };

            }, "Error verificando si el correo está en uso.");
        }

        private IQueryable<UserRoleModel> GetUsersWithRolesQuery()
        {
            return from user in _medicalAppointmentContext.Users
                   join role in _medicalAppointmentContext.Roles on user.RoleID equals role.RoleID
                   where user.IsActive
                   orderby user.CreatedAt descending
                   select new UserRoleModel
                   {
                       UserId = user.UserId,
                       RoleID = role.RoleID,
                       Role = role.RoleName,
                       FirstName = user.FirstName,
                       LastName = user.LastName,
                       PhoneNumber = user.PhoneNumber,
                       Email = user.Email,
                       Password = user.Password,
                       CreatedAt = user.CreatedAt,
                       UpdatedAt = user.UpdatedAt,
                       IsActive = user.IsActive
                   };
        }

    }

}
