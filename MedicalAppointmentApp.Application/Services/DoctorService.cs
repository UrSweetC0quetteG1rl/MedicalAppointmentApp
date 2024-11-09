

using MedicalAppointment.Persistance.Interfaces.Users;
using MedicalAppointmentApp.Application.Base;
using MedicalAppointmentApp.Application.Dtos.Users.Doctor;
using MedicalAppointmentApp.Application.Responses.Users.Doctor;
using MedicalAppointmentApp.Domain.Entities.User;
using Microsoft.Extensions.Logging;

namespace MedicalAppointmentApp.Application.Services
{
    public class DoctorService : IBaseService<DoctorResponse, DoctorDtoSave, DoctorDtoUpdate>
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly ILogger<DoctorService> _logger;
        public DoctorService(IDoctorRepository doctorRepository, ILogger<DoctorService> logger)
        {
            if (doctorRepository is null) { throw new ArgumentNullException(nameof(doctorRepository)); }

            this._doctorRepository = doctorRepository;
            this._logger = logger;
        }

        public async Task<DoctorResponse> GetAll()
        {
            DoctorResponse doctorResponse = new DoctorResponse();

            try
            {
                var result = await _doctorRepository.GetAll();

                if (!result.Success)
                {
                    doctorResponse.Message = result.Message;
                    doctorResponse.IsSuccess = result.Success;
                    return doctorResponse;
                }
                doctorResponse.Data = result.Data;
            }
            catch (Exception ex)
            {
                doctorResponse.IsSuccess = false;
                doctorResponse.Message = "Error obteninedo los doctores";
                _logger.LogError(doctorResponse.Message, ex.ToString());
            }
            return doctorResponse;
        }

        public async Task<DoctorResponse> GetById(int Id)
        {
            DoctorResponse doctorResponse = new DoctorResponse();

            try
            {
                var result = await _doctorRepository.GetEntityBy(Id);

                if (!result.Success)
                {
                    doctorResponse.Message = result.Message;
                    doctorResponse.IsSuccess = result.Success;
                    return doctorResponse;
                }
                doctorResponse.Data = result.Data;
            }
            catch (Exception ex)
            {
                doctorResponse.IsSuccess = false;
                doctorResponse.Message = "Error obteninedo el doctor";
                _logger.LogError(doctorResponse.Message, ex.ToString());
            }
            return doctorResponse;
        }

        public async Task<DoctorResponse> SaveAsync(DoctorDtoSave dto)
        {
            DoctorResponse doctorResponse = new DoctorResponse();

            try
            {
                Doctor doctor = new Doctor();

                doctor.DoctorID = dto.DoctorID;
                doctor.SpecialtyID = dto.SpecialtyID;
                doctor.LicenseNumber = dto.LicenseNumber;
                doctor.PhoneNumber = dto.PhoneNumber;
                doctor.YearsOfExperience = dto.YearsOfExperience;
                doctor.Education = dto.Education;
                doctor.Bio = dto.Bio;
                doctor.ConsultationFee = dto.ConsultationFee;
                doctor.ClinicAddress = dto.ClinicAddress;
                doctor.AvailabilityModeId = dto.AvailabilityModeId;
                doctor.LicenseExpirationDate = dto.LicenseExpirationDate;
                doctor.CreatedAt = dto.CreatedAt;

            }
            catch (Exception ex) 
            {
                doctorResponse.IsSuccess = false;
                doctorResponse.Message = "Error guardando el doctor";
                _logger.LogError(doctorResponse.Message, ex.ToString());
            }
            return doctorResponse;
        }

        public async Task<DoctorResponse> UpdateAsync(DoctorDtoUpdate dto)
        {
            DoctorResponse doctorResponse = new DoctorResponse();

            try
            {
                var resultGet = await _doctorRepository.GetEntityBy(dto.DoctorID);

                if (!resultGet.Success)
                {
                    doctorResponse.IsSuccess = resultGet.Success;
                    doctorResponse.Message = resultGet.Message;
                    return doctorResponse;
                }

                Doctor doctor = (Doctor)resultGet.Data;


                doctor.SpecialtyID = dto.SpecialtyID;
                doctor.PhoneNumber = dto.PhoneNumber;
                doctor.YearsOfExperience = dto.YearsOfExperience;
                doctor.Bio = dto.Bio;
                doctor.ConsultationFee = dto.ConsultationFee;
                doctor.ClinicAddress = dto.ClinicAddress;
                doctor.AvailabilityModeId = dto.AvailabilityModeId;
                doctor.UpdatedAt = dto.UpdatedAt;

                var result = await _doctorRepository.Update(doctor);
                result.Message = "El doctor fue actualizado correctamente.";


            }
            catch (Exception ex) 
            {
                doctorResponse.IsSuccess = false;
                doctorResponse.Message = "Error actualizando el doctor";
                _logger.LogError(doctorResponse.Message, ex.ToString());
            }
            return doctorResponse;
        }
    }
}
