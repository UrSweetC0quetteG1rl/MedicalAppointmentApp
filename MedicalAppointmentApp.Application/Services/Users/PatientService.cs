using MedicalAppointment.Persistance.Interfaces.Users;
using MedicalAppointmentApp.Application.Base;
using MedicalAppointmentApp.Application.Contracts.Users;
using MedicalAppointmentApp.Application.Dtos.Users.Patient;
using MedicalAppointmentApp.Application.Responses.Users.Patient;
using MedicalAppointmentApp.Domain.Entities.User;
using Microsoft.Extensions.Logging;


namespace MedicalAppointmentApp.Application.Services.Users
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly ILogger<PatientService> _logger;

        public PatientService(IPatientRepository patientRepository, ILogger<PatientService> logger)
        {
            if (patientRepository == null) throw new ArgumentNullException(nameof(patientRepository));

            _patientRepository = patientRepository;
            _logger = logger;
        }

        public async Task<PatientResponse> GetAll()
        {
            PatientResponse patientResponse = new PatientResponse();

            try
            {
                var result = await _patientRepository.GetAll();

                if (!result.Success)
                {
                    patientResponse.Message = result.Message;
                    patientResponse.IsSuccess = result.Success;
                    return patientResponse;
                }
                patientResponse.Data = result.Data;

            }
            catch (Exception ex)
            {
                patientResponse.IsSuccess = false;
                patientResponse.Message = "Error obteninedo los pacientes";
                _logger.LogError(patientResponse.Message, ex.ToString());
            }
            return patientResponse;
        }

        public async Task<PatientResponse> GetById(int Id)
        {
            PatientResponse patientResponse = new PatientResponse();

            try
            {
                var result = await _patientRepository.GetEntityBy(Id);

                if (!result.Success)
                {
                    patientResponse.Message = result.Message;
                    patientResponse.IsSuccess = result.Success;
                    return patientResponse;
                }
                patientResponse.Data = result.Data;

            }
            catch (Exception ex)
            {
                patientResponse.IsSuccess = false;
                patientResponse.Message = "Error obteninedo el paciente.";
                _logger.LogError(patientResponse.Message, ex.ToString());
            }
            return patientResponse;
        }

        public async Task<PatientResponse> SaveAsync(PatientDtoSave dto)
        {
            PatientResponse patientResponse = new PatientResponse();

            try
            {
                Patient patient = new Patient();

                patient.PatientID = dto.PatientID;
                patient.DateOfBirth = dto.DateOfBirth;
                patient.Gender = dto.Gender;
                patient.PhoneNumber = dto.PhoneNumber;
                patient.Address = dto.Address;
                patient.EmergencyContactName = dto.EmergencyContactName;
                patient.EmergencyContactPhone = dto.EmergencyContactPhone;
                patient.BloodType = dto.BloodType;
                patient.Allergies = dto.Allergies;
                patient.InsuranceProviderID = dto.InsuranceProviderID;
            }
            catch (Exception ex)
            {
                patientResponse.IsSuccess = false;
                patientResponse.Message = "Error guardando el paciente.";
                _logger.LogError(patientResponse.Message, ex.ToString());
            }

            return patientResponse;
        }

        public async Task<PatientResponse> UpdateAsync(PatientDtoUpdate dto)
        {
            PatientResponse patientResponse = new PatientResponse();

            try
            {
                var resultGet = await _patientRepository.GetPatientById(dto.PatientID);

                if (!resultGet.Success)
                {
                    patientResponse.IsSuccess = resultGet.Success;
                    patientResponse.Message = resultGet.Message;
                    return patientResponse;
                }

                Patient patient = (Patient)resultGet.Data;

                patient.Address = dto.Address;
                patient.EmergencyContactName = dto.EmergencyContactName;
                patient.EmergencyContactPhone = dto.EmergencyContactPhone;
                patient.BloodType = dto.BloodType;
                patient.Allergies = dto.Allergies;
                patient.InsuranceProviderID = dto.InsuranceProviderID;

                var result = await _patientRepository.Update(patient);
                result.Message = "El paciente fue actualizado correctamente.";


            }
            catch (Exception ex)
            {
                patientResponse.IsSuccess = false;
                patientResponse.Message = "Error actualizando el paciente.";
                _logger.LogError(patientResponse.Message, ex.ToString());
            }
            return patientResponse;
        }
    }
}
