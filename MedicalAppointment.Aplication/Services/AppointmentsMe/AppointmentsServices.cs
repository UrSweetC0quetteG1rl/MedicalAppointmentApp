using MedicalAppointment.Aplication.Contracts.Appointment;
using MedicalAppointment.Aplication.Dtos.Configuration.Appointment.Appointments;
using MedicalAppointment.Aplication.Responses.Configuration.Appointment;
using MedicalAppointment.Persistance.Interfaces.Configuration.Appointments;
using MedicalAppointmentApp.Domain.Entities.Appoinments;
using Microsoft.Extensions.Logging;


namespace MedicalAppointment.Aplication.Services.Appointment
{
    public class AppointmentsServices : IAppointment
    {

        private readonly ILogger _logger;
        private readonly IMedicalAppointmentRepository _appointment;

        public AppointmentsServices(IMedicalAppointmentRepository appointment,
            ILogger<AppointmentsServices> logger)
        {
            if (appointment is null)
            {
                throw new ArgumentNullException(nameof(Appointment));
            }

            _logger = logger;
            _appointment = appointment;

        }

        public async Task<AppointmentResponse> GetAll()
        {
            AppointmentResponse appointment = new AppointmentResponse();

            try
            {

                var result = await _appointment.GetAll();

                if (!result.Success)
                {
                    appointment.IsSuccess = result.Success;
                    appointment.Message = result.Message;
                    return appointment;
                }
                appointment.Data = result.Data;
            }
            catch (Exception ex)
            {

                appointment.IsSuccess = false;
                appointment.Message = "error obteniendo los Citas";
                _logger.LogError(appointment.Message, ex);


            }
            return appointment;
        }

        public async Task<AppointmentResponse> GetById(int Id)
        {
            AppointmentResponse appointment = new AppointmentResponse();


            try
            {

                var result = await _appointment.GetEntityBy(Id);

                if (!result.Success)
                {
                    appointment.IsSuccess = result.Success;
                    appointment.Message = result.Message;
                    return appointment;
                }
                appointment.Data = result.Data;
            }
            catch (Exception ex)
            {

                appointment.IsSuccess = false;
                appointment.Message = "error obteniendo los Citas";
                _logger.LogError(appointment.Message, ex);


            }
            return appointment;
        }

        public async Task<AppointmentResponse> SaveAsync(AppointmentSaveDto dto)
        {
            AppointmentResponse appointment = new AppointmentResponse();


            try
            {
                Appointments appointments = new Appointments();

                appointments.PatientID = dto.PatientID;
                appointments.DoctorID = dto.DoctorID;
                appointments.AppointmentDate = dto.AppointmentDate;
                appointments.StatusID = dto.StatusID;
                appointments.CreatedAt = dto.CreatedAt;
                appointments.UpdatedAt = dto.UpdatedAt;

                var result = await _appointment.Save(appointments);
                result.Message = "Dato Guardado";


            }
            catch (Exception ex)
            {

                appointment.IsSuccess = false;
                appointment.Message = "error guardando la Cita";
                _logger.LogError(appointment.Message, ex);


            }
            return appointment;

        }

        public async Task<AppointmentResponse> UpdateAsync(AppointmentUpdateDto dto)
        {
            AppointmentResponse appointment = new AppointmentResponse();


            try
            {
                var resultGetBy = await _appointment.GetEntityBy(dto.AppointmentID);

                if (!resultGetBy.Success)
                {
                    appointment.IsSuccess = false;
                    appointment.Message = "no se encontro";
                    return appointment;
                }

                Appointments appointmentToupdate = new Appointments();

                appointmentToupdate.AppointmentID = dto.AppointmentID;
                appointmentToupdate.PatientID = dto.PatientID;
                appointmentToupdate.DoctorID = dto.DoctorID;
                appointmentToupdate.AppointmentDate = dto.AppointmentDate;
                appointmentToupdate.StatusID = dto.StatusID;               
                appointmentToupdate.UpdatedAt = dto.UpdatedAt;

                var result = await _appointment.Update(appointmentToupdate);
                result.Message = "Actualizado";

            }
            catch (Exception ex)
            {

                appointment.IsSuccess = false;
                appointment.Message = "error guardando la Cita";
                _logger.LogError(appointment.Message, ex);


            }
            return appointment;

        }
    }
}




