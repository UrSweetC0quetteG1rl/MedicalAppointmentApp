

using MedicalAppointment.Persistance.Interfaces.Users;
using MedicalAppointmentApp.Application.Base;
using MedicalAppointmentApp.Application.Dtos.Users.User;
using MedicalAppointmentApp.Application.Responses.Users.User;
using MedicalAppointmentApp.Domain.Entities.User;
using Microsoft.Extensions.Logging;

namespace MedicalAppointmentApp.Application.Services
{
    public class UserService : IBaseService<UserResponse, UserDtoSave, UserDtoUpdate>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserService> _logger;
        public UserService(IUserRepository userRepository, ILogger<UserService> logger)
        {
            if (userRepository is null) { throw new ArgumentNullException(nameof(userRepository)); }

            this._userRepository = userRepository;
            this._logger = logger;
        }
        public async Task<UserResponse> GetAll()
        {
            UserResponse userResponse = new UserResponse();

            try
            {
                var result = await _userRepository.GetAll();

                if (!result.Success)
                {
                    userResponse.Message = result.Message;
                    userResponse.IsSuccess = result.Success;
                    return userResponse;
                }
                userResponse.Data = result.Data;
            }
            catch (Exception ex) 
            {
                userResponse.IsSuccess = false;
                userResponse.Message = "Error obteninedo los usuarios";
                _logger.LogError(userResponse.Message, ex.ToString());
            }
            return userResponse;
        }

        public async Task<UserResponse> GetById(int Id)
        {
            UserResponse userResponse = new UserResponse();

            try
            {
                var result = await _userRepository.GetEntityBy(Id);

                if (!result.Success)
                {
                    userResponse.Message = result.Message;
                    userResponse.IsSuccess = result.Success;
                    return userResponse;
                }
                userResponse.Data = result.Data;
            }
            catch (Exception ex)
            {
                userResponse.IsSuccess = false;
                userResponse.Message = "Error obteninedo los usuarios";
                _logger.LogError(userResponse.Message, ex.ToString());
            }
            return userResponse;
        }

        public async Task<UserResponse> SaveAsync(UserDtoSave dto)
        {
            UserResponse userResponse = new UserResponse();

            try
            {
                User user = new User();

                user.FirstName = dto.FirstName;
                user.LastName = dto.LastName;
                user.Email = dto.Email;
                user.Password = dto.Password;
                user.RoleID = dto.RoleID;
                user.CreatedAt = dto.CreatedAt;
                user.IsActive = dto.IsActive;

                var result = await _userRepository.Save(user);
                result.Message = "El usuario fue creado correctamente.";

            }
            catch (Exception ex) 
            {
                userResponse.IsSuccess = false;
                userResponse.Message = "Error guardando los usuarios";
                _logger.LogError(userResponse.Message, ex.ToString());
            }
            return userResponse;
        }

        public async Task<UserResponse> UpdateAsync(UserDtoUpdate dto)
        {
            UserResponse userResponse = new UserResponse();

            try
            {
                var resultGet = await _userRepository.GetEntityBy(dto.UserId);

                if (!resultGet.Success)
                {
                    userResponse.IsSuccess = resultGet.Success;
                    userResponse.Message = resultGet.Message;
                    return userResponse;
                }

                User user = (User)resultGet.Data;

                user.FirstName = dto.FirstName;
                user.LastName = dto.LastName;
                user.Email = dto.Email;
                user.Password = dto.Password;
                user.RoleID = dto.RoleID;
                user.CreatedAt = dto.CreatedAt;
                user.IsActive = dto.IsActive;
                user.UpdatedAt = dto.UpdatedAt;

                var result = await _userRepository.Update(user);
                result.Message = "El usuario fue actualizado correctamente.";

            }
            catch (Exception ex) 
            {
                userResponse.IsSuccess = false;
                userResponse.Message = "Error actualizando el usuario.";
                _logger.LogError(userResponse.Message, ex.ToString());
            }
            return userResponse;
        }
    }
}
