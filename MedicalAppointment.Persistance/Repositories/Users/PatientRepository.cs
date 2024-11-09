using MedicalAppointment.Persistance.Base;
using MedicalAppointment.Persistance.Context;
using MedicalAppointment.Persistance.Interfaces.Users;
using MedicalAppointment.Persistance.Models.User;
using MedicalAppointmentApp.Domain.Entities.User;
using MedicalAppointmentApp.Domain.Result;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MedicalAppointment.Persistance.Repositories.Users
{
    public class PatientRepository(MedicalAppointmentContext medicalAppointmentContext, ILogger<PatientRepository> logger) 
        : BaseRepository<Patient>(medicalAppointmentContext), IPatientRepository
    {
        private readonly MedicalAppointmentContext _medicalAppointmentContext = medicalAppointmentContext;
        private readonly ILogger<PatientRepository> _logger = logger;

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

            if (string.IsNullOrEmpty(entity.PhoneNumber))
            {
                operationResult.Success = false;
                operationResult.Message = "Es necesario el número de telefono.";
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

            operationResult.Success = true;
            return operationResult;
        }
       

        public async override Task<OperationResult> Save(Patient entity)
        {
            OperationResult operationResult = ValidatePatientEntity(entity);

            if (!operationResult.Success) return operationResult;

            try { operationResult = await base.Save(entity); }
            catch (Exception ex) { operationResult = HandleException("Ocurrió un error al guardar el paciente.", ex); }
            
            return operationResult;
        }
        public async override Task<OperationResult> Update(Patient entity)
        {
            OperationResult operationResult = ValidatePatientEntity(entity);
            if (!operationResult.Success)   return operationResult;
            try
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
                UpdatePatientProperties(patientToUpdate, entity);
                operationResult = await base.Update(patientToUpdate);

            }
            catch(Exception ex)
            {
                operationResult = HandleException("Error actualizando el usuario.", ex);
            }
            return operationResult;
        }
        public async override Task<OperationResult> Remove(Patient entity)
        {
            OperationResult operationResult = ValidatePatientEntity(entity);
            if (!operationResult.Success) { return operationResult; }


            try
            {
                Patient? patientToRemove = await _medicalAppointmentContext.Patients.FindAsync(entity.PatientID);

                if (patientToRemove == null)
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "El paciente no existe."
                    };
                }
                patientToRemove.IsActive = false;
                patientToRemove.UpdatedAt = entity.UpdatedAt;

                operationResult = await base.Update(patientToRemove);
            }
            catch(Exception ex)
            {
                operationResult = HandleException("Error desactivando el paciente.", ex);
            }
            return operationResult;
        }
        public async override Task<OperationResult> GetAll()
        {
            OperationResult operationResult = new OperationResult();

            try
            {
                var patiensWithInsurance = await GetPatiensWithRolesQuery().ToListAsync();

                operationResult.Success = true;
                operationResult.Data = patiensWithInsurance;
            }
            catch(Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "Error obteniendo los pacientes.";
                _logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;

        }
        public async override Task<OperationResult> GetEntityBy(int Id)
        {
            OperationResult operationResult = new OperationResult();

            try
            {
                var patiensWithInsurance = await GetPatiensWithRolesQuery()
                                       .FirstOrDefaultAsync(patient => patient.PatientID == Id);

                if(patiensWithInsurance != null)
                {
                    operationResult.Success = true;
                    operationResult.Data = patiensWithInsurance;
                }
                else
                {
                    operationResult.Success = false;
                    operationResult.Message = "Paciente no encontrado.";
                }
            }
            catch(Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "Error obteniendo el paciente.";
                _logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }


        private void UpdatePatientProperties(Patient target, Patient source)
        {
            target.DateOfBirth = source.DateOfBirth;
            target.Gender = source.Gender;
            target.PhoneNumber = source.PhoneNumber;
            target.Address = source.Address;
            target.EmergencyContactName = source.EmergencyContactName;
            target.EmergencyContactPhone = source.EmergencyContactPhone;
            target.BloodType = source.BloodType;
            target.Allergies = source.Allergies;
            target.InsuranceProviderID = source.InsuranceProviderID;
        }
        private OperationResult HandleException(string message, Exception ex)
        {
            _logger.LogError(message, ex.ToString());
            return new OperationResult { Success = false, Message = message };
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
