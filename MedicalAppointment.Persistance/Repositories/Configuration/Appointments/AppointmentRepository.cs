using MedicalAppointment.Persistance.Base;
using MedicalAppointment.Persistance.Context;
using MedicalAppointment.Persistance.Interfaces.Configuration;
using MedicalAppointment.Persistance.Models;
using MedicalAppointmentApp.Domain.Entities.Appoinments;
using MedicalAppointmentApp.Domain.Entities.User;
using MedicalAppointmentApp.Domain.Result;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MedicalAppointment.Persistance.Repositories.Configuration.Appointments
{
    public class AppointmentRepository(MedicalAppointmentContext appointmentContext, ILogger<AppointmentRepository> logger)
        : BaseRepository<Appointment>(appointmentContext), IMedicalAppointmentRepository
    {
        private readonly MedicalAppointmentContext _MedicalAppointmentContext = appointmentContext;
        private readonly ILogger<AppointmentRepository> logger = logger;


        public async override Task<OperationResult> Save(Appointment entity)
        {
            OperationResult operationResult = new OperationResult();



            if (entity == null)
            {

                operationResult.Success = false;
                operationResult.Message = "Esta entidad no Puede ser Nula.";
                return operationResult;


            }

            if (entity.AppointmentDate <= DateTime.Now)
            {
                operationResult.Success = false;
                operationResult.Message = "La fecha seleccionada invalida.";
                return operationResult;
            }

            if (entity.DoctorID == 0)

            {

                operationResult.Success = false;
                operationResult.Message = "Debe seleccionar un Doctor.";
                return operationResult;
            }

            if (await base.Exists(appointment => appointment.AppointmentDate == entity.AppointmentDate && appointment.DoctorID == entity.DoctorID))
            {


                operationResult.Success = false;
                operationResult.Message = "Ya existe una cita para ese horario";
                return operationResult;



            }

            try
            {


                operationResult = await base.Save(entity);

            }
            catch (Exception ex)
            {


                operationResult.Success = false;
                operationResult.Message = "Ocurrió un error al guardar la cita.";
                this.logger.LogError(operationResult.Message, ex.ToString());
            }

            return operationResult;
        }


        public async override Task<OperationResult> Update(Appointment entity)
        {
            OperationResult operationResult = new OperationResult();



            operationResult = ValidarAppointmentID(entity);
            if (!operationResult.Success) return operationResult;


            operationResult = ValidarEntityNotNull(entity);
            if (!operationResult.Success) return operationResult;


            operationResult = ValidateAppointmentDate(entity);
            if (!operationResult.Success) return operationResult;

            try
            {
                Appointment? appointmentToUpdate = await _MedicalAppointmentContext.Appointments.FindAsync(entity.AppointmentsID);

                if (appointmentToUpdate == null)
                {
                    operationResult.Success = false;
                    operationResult.Message = "No se encontró la cita con el ID proporcionado.";
                    return operationResult;
                }


                if (entity.AppointmentsID <= 0)
                {
                    operationResult.Success = false;
                    operationResult.Message = "No se encontró la cita con el ID proporcionado.";
                    return operationResult;
                }

                appointmentToUpdate.AppointmentDate = entity.AppointmentDate;
                appointmentToUpdate.UpdatedAt = DateTime.Today;
                appointmentToUpdate.IsActive = entity.IsActive;

                operationResult = await base.Update(appointmentToUpdate);
            }


            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "Ocurrió un error al Reprogramar la cita.";
                logger.LogError(operationResult.Message, ex.ToString);
            }
            return operationResult;
        }

        public async override Task<OperationResult> Remove(Appointment entity)
        {


            OperationResult operationResult = new OperationResult();

            

            operationResult = ValidarEntityNotNull(entity);
            if (!operationResult.Success) return operationResult;


            operationResult = ValidarAppointmentID(entity);
            if (!operationResult.Success) return operationResult;

            try
            {

                Appointment? appointmentToRemove = await _MedicalAppointmentContext.Appointments.FindAsync(entity.AppointmentsID);

                appointmentToRemove.IsActive = false;

                await base.Update(appointmentToRemove);

            }

            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "Ocurrió un error al Eliminar la cita.";
                logger.LogError(operationResult.Message, ex.ToString);
            }



            return operationResult;
        }

        public async override Task<OperationResult> GetAll()
        {

            OperationResult operationResult = new OperationResult();


            try
            {

                operationResult.Data = await (from appointment in _MedicalAppointmentContext.Appointments
                                              join Doctor in _MedicalAppointmentContext.Doctors on appointment.DoctorID equals Doctor.DoctorID
                                              where appointment.StatusID >= 0
                                              orderby appointment.CreatedAt descending
                                              select new AppointmentDoctorModel
                                              {

                                                  AppointmentID = appointment.AppointmentsID,
                                                  DoctorID = appointment.DoctorID,
                                                  AppointmentDate = appointment.AppointmentDate,
                                                  CreatedAt = appointment.CreatedAt,
                                                  PatientID = appointment.PatientID,

                                              }).AsNoTracking()
                                              .ToListAsync();
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "Error obteniendo las citas";
                logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;




        }


        private OperationResult ValidarAppointmentID(Appointment entity)
        {
            OperationResult result = new OperationResult();

            if (entity.AppointmentsID <= 0)
            {
                result.Success = false;
                result.Message = "El numero cita es requerida.";
                return result;
            }

            result.Success = true;
            return result;
        }

        private OperationResult ValidarEntityNotNull(Appointment entity)
        {
            OperationResult Operationresult = new OperationResult();

            if (entity == null)
            {
                Operationresult.Success = false;
                Operationresult.Message = "Esta entidad no puede ser nula.";
                return Operationresult;
            }
            Operationresult.Success = true;
            return Operationresult;
        }


        private OperationResult ValidateAppointmentDate(Appointment entity)
        {
            OperationResult result = new OperationResult();

            if (entity.AppointmentDate <= DateTime.Now)
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
