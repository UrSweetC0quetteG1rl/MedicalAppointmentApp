
using MedicalAppointment.Persistance.Base;
using MedicalAppointment.Persistance.Context;
using MedicalAppointmentApp.Domain.Entities.User;
using MedicalAppointmentApp.Domain.Result;
using Microsoft.Extensions.Logging;

namespace MedicalAppointment.Persistance.Repositories.Users
{
    public class PatientRepository(MedicalAppointmentContext medicalAppointmentContext, ILogger<PatientRepository> logger)
        : BaseRepository<Patient>(medicalAppointmentContext)
    {
        private readonly MedicalAppointmentContext _medicalAppointmentContext;
        private readonly ILogger<PatientRepository> logger;

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
                logger.LogError(ex, errorMessage);
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
    }
}
