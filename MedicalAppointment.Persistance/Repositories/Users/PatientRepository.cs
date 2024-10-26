using MedicalAppointment.Persistance.Base;
using MedicalAppointment.Persistance.Context;
using MedicalAppointment.Persistance.Interfaces.Users;
using MedicalAppointment.Persistance.Models.User;
using MedicalAppointmentApp.Domain.Entities.User;
using MedicalAppointmentApp.Domain.Result;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MedicalAppointment.Persistance.Repositories.Users
{
    public class PatientRepository: BaseRepository<Patient>, IPatientRepository
    {
        private readonly MedicalAppointmentContext _medicalAppointmentContext;
        private readonly ILogger<PatientRepository> _logger;

        public PatientRepository(MedicalAppointmentContext medicalAppointmentContext, ILogger<PatientRepository> logger)
        : base(medicalAppointmentContext)
        {
            _medicalAppointmentContext = medicalAppointmentContext;
            _logger = logger;
        }

        private OperationResult ValidatePatientEntity(Patient entity)
        {
            OperationResult operationResult = new OperationResult();

            if (entity == null)
            {
                operationResult.Success = false;
                operationResult.Message = "La entidad es requerida.";
                return operationResult;
            }

            if(char.IsWhiteSpace(entity.Gender) || entity.Gender == null)
            {
                operationResult.Success = false;
                operationResult.Message = "Es necesario elegir un genero.";
                return operationResult;
            }

            if (string.IsNullOrEmpty(entity.Address))
            {
                operationResult.Success = false;
                operationResult.Message = "Es necesario indicar su dirección.";
                return operationResult;
            }

            if (string.IsNullOrEmpty(entity.EmergencyContactName)){
                operationResult.Success = false;
                operationResult.Message = "Es necesario ingresar el nombre de su contacto de emergencia.";
                return operationResult;
            }

            if (string.IsNullOrEmpty(entity.EmergencyContactPhone))
            {
                operationResult.Success = false;
                operationResult.Message = "Es necesario ingresar el número de su contacto de emergencia.";
                return operationResult;
            }

            if (string.IsNullOrEmpty(entity.BloodType))
            {
                operationResult.Success = false;
                operationResult.Message = "Es necesario indicar su tipo de sangre.";
                return operationResult;
            }

            if (string.IsNullOrEmpty(entity.Allergies))
            {
                operationResult.Success = false;
                operationResult.Message = "Es necesario indicar si tiene algún tipo de alergias.";
                return operationResult;
            }

            if(entity.InsuranceProviderID == 0)
            {
                operationResult.Success = false;
                operationResult.Message = "Es necesario saber el seguro que usted tiene.";
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
                _logger.LogError(ex, errorMessage);
                return operationResult;
            }
        }

        public async override Task<OperationResult> Save(Patient entity)
        {
            OperationResult operationResult = ValidatePatientEntity(entity);

            if (!operationResult.Success)
            {
                return operationResult;
            }

            if (await base.Exists(patient => patient.PatientID == entity.PatientID))
            {
                return new OperationResult
                {
                    Success = false,
                    Message = "Este paciente ya se encuentra registrado."
                };

            }
            return await ExecuteOperationWithLogging(() => base.Save(entity), "Error guardando el paciente.");
        }
        public async override Task<OperationResult> Update(Patient entity)
        {
            OperationResult operationResult = ValidatePatientEntity(entity);

            if (!operationResult.Success)
            {
                return operationResult;
            }

            return await ExecuteOperationWithLogging(async () =>
            {
                Patient? patientToUpdate = await _medicalAppointmentContext.Patients.FindAsync(entity.PatientID);
                if (patientToUpdate == null)
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "Este paciente no existe."
                    };
                }
                patientToUpdate.DateOfBirth = entity.DateOfBirth;
                patientToUpdate.Gender = entity.Gender;
                patientToUpdate.Address = entity.Address;
                patientToUpdate.EmergencyContactName = entity.EmergencyContactName;
                patientToUpdate.EmergencyContactPhone = entity.EmergencyContactPhone;
                patientToUpdate.BloodType = entity.BloodType;
                patientToUpdate.Allergies = entity.Allergies;
                patientToUpdate.InsuranceProviderID = entity.InsuranceProviderID;

                return await base.Update(patientToUpdate);
            }, "Error actualizando el paciente.");
        }
        public async override Task<OperationResult> Remove(Patient entity)
        {
            OperationResult operationResult = ValidatePatientEntity(entity);

            if (!operationResult.Success) { return operationResult; }

            return await ExecuteOperationWithLogging(async () =>
            {
                Patient? patientToRemove = await _medicalAppointmentContext.Patients.FindAsync(entity.PatientID);

                if (patientToRemove == null)
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "El usuario no existe."
                    };
                }
                patientToRemove.IsActive = false;
                patientToRemove.UpdatedAt = entity.UpdatedAt;

                return await base.Update(patientToRemove);
            }, "Error desactivando el paciente.");
        }
        public async override Task<OperationResult> GetAll()
        {
            return await ExecuteOperationWithLogging(async () =>
            {
                var patiensWithInsurance = await GetPatiensWithRolesQuery().ToListAsync();

                return new OperationResult
                {
                    Success = true,
                    Data = patiensWithInsurance
                };
            }, "Error obteniendo los pacientes.");

        }
        public async override Task<OperationResult> GetEntityBy(int Id)
        {
            return await ExecuteOperationWithLogging(async () =>
            {
                var atiensWithInsurance = await GetPatiensWithRolesQuery()
                                       .FirstOrDefaultAsync(patient => patient.PatientID == Id);

                return new OperationResult
                {
                    Success = true,
                    Data = atiensWithInsurance
                };
            }, "Error obteniendo el paciente.");
        }

        private IQueryable<PatientInsurenceProviderModel> GetPatiensWithRolesQuery()
        {
            return from patient in _medicalAppointmentContext.Patients
                   join insuranceProvider in _medicalAppointmentContext.InsuranceProviders on patient.InsuranceProviderID equals  insuranceProvider.InsuranceProviderID
                   where patient.IsActive
                   orderby patient.CreatedAt descending
                   select new PatientInsurenceProviderModel
                   {
                       PatientID = patient.PatientID,
                       DateOfBirth = patient.DateOfBirth,
                       Gender = patient.Gender,
                       Address = patient.Address,
                       EmergencyContactName = patient.EmergencyContactName,
                       EmergencyContactPhone = patient.EmergencyContactPhone,
                       BloodType = patient.BloodType,
                       Allergies = patient.Allergies,
                       InsuranceProviderID = insuranceProvider.InsuranceProviderID,
                       InsuranceProviderName = insuranceProvider.Name,
                       CreatedAt = patient.CreatedAt,
                       UpdatedAt = patient.UpdatedAt,
                       IsActive = patient.IsActive
                   };
        }
    }
}
