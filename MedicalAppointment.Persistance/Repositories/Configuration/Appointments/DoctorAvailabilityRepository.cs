using MedicalAppointment.Persistance.Base;
using MedicalAppointment.Persistance.Context;
using MedicalAppointment.Persistance.Interfaces.Configuration;
using MedicalAppointmentApp.Domain.Entities.Appoinments;
using MedicalAppointmentApp.Domain.Result;
using Microsoft.Extensions.Logging;

using System.Linq.Expressions;

namespace MedicalAppointment.Persistance.Repositories.Configuration.Appointments
{
    public class DoctorAvailabilityRepository(MedicalAppointmentContext context, ILogger<DoctorAvailabilityRepository> logger)
      : BaseRepository<DoctorAvailability>(context), IDoctorAvailabilityRepository
    {
        private readonly MedicalAppointmentContext context = context;
        private readonly ILogger<DoctorAvailabilityRepository> logger = logger;

        public async override Task<OperationResult> Save(DoctorAvailability entity)
        {

            OperationResult result = new OperationResult();


            if (entity == null)
            {
                result.Success = false;
                result.Message = "La entidad no puede ser nula";
                return result;
            }


            if (entity.StartTime >= entity.EndTime)
            {
                result.Success = false;
                result.Message = "La hora de Inicio debe ser antes de la hora de Salida";
                return result;
            }

            if (entity.EndTime <= entity.StartTime)
            {
                result.Success = false;
                result.Message = "La hora de Salida debe ser despues de la hora Inicio";
                return result;
            }

            result = ValidarAppointmentDate(entity);
            if (!result.Success) return result;

            try
            {
                result = await base.Save(entity);
            }
            catch
            {
                result.Success = false;
                result.Message = "Ocurrió un error al registrar la disponibilidad";
            }

            return result;
        }


        public async override Task<OperationResult> Update(DoctorAvailability entity)
        {

            OperationResult result = new OperationResult();



            if (entity.AvailabilityID <= 0)
            {

                result.Success = false;
                result.Message = "Debe seleccionar el horario que desea cambiar";
                return result;


            }



            result = ValidarAppointmentDate(entity);
            if (!result.Success) return result;

            result = ValidarStartTime(entity);
            if (!result.Success) return result;

            result = ValidarEndTime(entity);
            if (!result.Success) return result;

            await base.Update(entity);

            try
            {
                DoctorAvailability? doctorAvailabilityToUpdate = await context.DoctorAvailability.FindAsync(entity.AvailabilityID);

                doctorAvailabilityToUpdate.AvailaDate = entity.AvailaDate;
                doctorAvailabilityToUpdate.StartTime = entity.StartTime;
                doctorAvailabilityToUpdate.EndTime = entity.EndTime;


                result = await base.Update(doctorAvailabilityToUpdate);

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ocurrió un error al Reprogramar la cita.";
                logger.LogError(result.Message, ex.ToString);

            }

            return result;




        }

        public Task<bool> Exists(Expression<Func<IDoctorAvailabilityRepository, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public async override Task<OperationResult> Remove(DoctorAvailability entity)
        {

            OperationResult result = new OperationResult();

            if (entity == null)
            {

                result.Success = false;
                result.Message = "Debe seleccionar el horario que desea Eliminar";
                return result;

            }

            if (entity.AvailabilityID < 0)
            {
                result.Success = false;
                result.Message = "Debe seleccionar el horario que desea Eliminar";
                return result;

            }

            try
            {

                DoctorAvailability? doctorAvailabilityToRemove = await context.DoctorAvailability.FindAsync(entity.AvailabilityID);

                doctorAvailabilityToRemove.IsActive = false;

            }

            catch
            {

                result.Success = false;
                result.Message = "Ocurrió un error al Eliminar Horario.";

            }

            return result;

        }

        private OperationResult ValidarAppointmentDate(DoctorAvailability entity)
        {
            OperationResult result = new OperationResult();

            if (entity.AvailaDate <= DateTime.Now)
            {
                result.Success = false;
                result.Message = "La fecha seleccionada es inválida.";
                return result;
            }

            result.Success = true;
            return result;
        }
        private OperationResult ValidarStartTime(DoctorAvailability entity)
        {
            OperationResult result = new OperationResult();

            if (entity.StartTime >= entity.EndTime)
            {
                result.Success = false;
                result.Message = "La hora de Inicio debe ser antes de la hora de Salida";
                return result;
            }

            result.Success = true;
            return result;
        }
        private OperationResult ValidarEndTime(DoctorAvailability entity)
        {
            OperationResult result = new OperationResult();

            if (entity.AvailaDate <= DateTime.Now)
            {
                result.Success = false;
                result.Message = "La fecha seleccionada es inválida.";
                return result;
            }

            result.Success = true;
            return result;
        }
    }



}

