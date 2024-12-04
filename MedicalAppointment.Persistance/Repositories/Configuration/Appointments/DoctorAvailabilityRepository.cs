using MedicalAppointment.Persistance.Base;
using MedicalAppointment.Persistance.Context;
using MedicalAppointment.Persistance.Interfaces.Configuration.Appointments;
using MedicalAppointment.Persistance.Models;
using MedicalAppointment.Persistance.Models.Appointments;
using MedicalAppointmentApp.Domain.Entities.Appoinments;
using MedicalAppointmentApp.Domain.Entities.User;
using MedicalAppointmentApp.Domain.Result;
using Microsoft.EntityFrameworkCore;
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


            try
            {
                DoctorAvailability doctorAvailabilityToUpdate = await context.DoctorAvailability.FindAsync(entity.AvailabilityID);

                doctorAvailabilityToUpdate.AvailableDate = entity.AvailableDate;
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

        public async override Task<OperationResult> GetAll()
        {

            OperationResult operationResult = new OperationResult();


            try
            {

                operationResult.Data = await (from DoctorAvailability in context.DoctorAvailability
                                              join Doctor in context.Doctors on DoctorAvailability.DoctorID equals Doctor.DoctorID
                                              where DoctorAvailability.AvailabilityID >= 0                                              
                                              select new DoctorAvailabilityModel()
                                              {

                                                  AvailabilityID = DoctorAvailability.AvailabilityID,
                                                  DoctorID = DoctorAvailability.DoctorID,
                                                  AvailableDate = DoctorAvailability.AvailableDate,
                                                  StartTime = DoctorAvailability.StartTime,
                                                  EndTime = DoctorAvailability.EndTime,



                                              }).AsNoTracking()
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "Error obteniendo las Disponibilidad";
                logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;




        }

        public async override Task<OperationResult> GetEntityBy(int ID)
        {
            OperationResult result = new OperationResult();


            try
            {

                result.Data = await (from DoctorAvailability in context.DoctorAvailability
                                     join Doctor in context.Doctors on DoctorAvailability.DoctorID equals Doctor.DoctorID
                                     where DoctorAvailability.AvailabilityID == ID
                                     select new DoctorAvailabilityModel()
                                     {


                                         AvailabilityID = DoctorAvailability.AvailabilityID,
                                         DoctorID = DoctorAvailability.DoctorID,
                                         AvailableDate = DoctorAvailability.AvailableDate,
                                         StartTime = DoctorAvailability.StartTime,
                                         EndTime = DoctorAvailability.EndTime,


                                     }).AsNoTracking()
                                     .FirstOrDefaultAsync();


            }
            catch (Exception ex)
            {

                result.Success = false;
                result.Message = "Error obteniendo los seguros";
                logger.LogError(result.Message, ex.ToString());

            }
             return result;
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

            if (entity.AvailableDate <= DateTime.Now)
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

            if (entity.AvailableDate <= DateTime.Now)
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

