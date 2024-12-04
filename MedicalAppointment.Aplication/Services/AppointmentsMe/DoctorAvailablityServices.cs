

using MedicalAppointment.Aplication.Contracts.Appointment;
using MedicalAppointment.Aplication.Dtos.Configuration.Appointment.Appointments;
using MedicalAppointment.Aplication.Dtos.Configuration.Appointment.DoctorAvailability;
using MedicalAppointment.Aplication.Responses.Configuration.Appointment;
using MedicalAppointment.Aplication.Responses.Configuration.DoctorAvailability;
using MedicalAppointment.Persistance.Interfaces.Configuration.Appointments;
using MedicalAppointmentApp.Domain.Entities.Appoinments;
using Microsoft.Extensions.Logging;

namespace MedicalAppointment.Aplication.Services.AppointmentsMe
{
    public class DoctorAvailablityServices : IDoctorAvailability
    {

        private readonly ILogger _logger;
        private readonly IDoctorAvailabilityRepository _availability;


        public DoctorAvailablityServices(IDoctorAvailabilityRepository availability,
            ILogger<DoctorAvailablityServices>logger)
        {
            if (availability is null)
            {
                throw new ArgumentNullException(nameof(availability));
            }
            _logger = logger;
            _availability = availability;
        }

        public async Task<DoctorAvailabilityResponse> GetAll()
        {
            DoctorAvailabilityResponse availability = new DoctorAvailabilityResponse();

            try
            {

                var result = await _availability.GetAll();

                if (!result.Success)
                {
                    availability.IsSuccess = result.Success;
                    availability.Message = result.Message;
                    return availability;
                }
                availability.Data = result.Data;
            }
            catch (Exception ex)
            {

                availability.IsSuccess = false;
                availability.Message = "error obteniendo los Citas";
                _logger.LogError(availability.Message, ex);


            }
            return availability;
        }

        public async Task<DoctorAvailabilityResponse> GetById(int Id)
        {
            DoctorAvailabilityResponse availability = new DoctorAvailabilityResponse();


            try
            {

                var result = await _availability.GetEntityBy(Id);

                if (!result.Success)
                {
                    availability.IsSuccess = result.Success;
                    availability.Message = result.Message;
                    return availability;
                }
                availability.Data = result.Data;
            }
            catch (Exception ex)
            {

                availability.IsSuccess = false;
                availability.Message = "error obteniendo los Citas";
                _logger.LogError(availability.Message, ex);


            }
            return availability;
        }

        public async Task<DoctorAvailabilityResponse> SaveAsync(DoctorAvailabilitySaveDto dto)
        {
            DoctorAvailabilityResponse availability = new DoctorAvailabilityResponse();


            try
            {
               DoctorAvailability availability1 = new DoctorAvailability();

                availability1.DoctorID = dto.DoctorID;
                availability1.AvailableDate = dto.AvailableDate;
                availability1.StartTime = dto.StartTime;
                availability1.EndTime = dto.EndTime;

                var result = await _availability.Save(availability1);
                result.Message = "Dato Guardado";
            }
            catch (Exception ex)
            {

                availability.IsSuccess = false;
                availability.Message = "error guardando la Cita";
                _logger.LogError(availability.Message, ex);


            }
            return availability;

        }

        public async Task<DoctorAvailabilityResponse> UpdateAsync(DoctorAvailabilityUpdateDto dto)
        {
            DoctorAvailabilityResponse availability = new DoctorAvailabilityResponse();


            try
            {
                var resultGetBy = await _availability.GetEntityBy(dto.AvailabilityID);

                if (!resultGetBy.Success)
                {
                    availability.IsSuccess = false;
                    availability.Message = "no se encontro";
                    return availability;
                }

                DoctorAvailability availabilityToupdate = new DoctorAvailability();

                availabilityToupdate.AvailabilityID = dto.AvailabilityID;               
                availabilityToupdate.DoctorID = dto.DoctorID;
                availabilityToupdate.AvailableDate = dto.AvailableDate;
                availabilityToupdate.StartTime = dto.StartTime;
                availabilityToupdate.EndTime = dto.EndTime;

                var result = await _availability.Update(availabilityToupdate);
                result.Message = "Actualizado";

            }
            catch (Exception ex)
            {

                availability.IsSuccess = false;
                availability.Message = "error guardando la Cita";
                _logger.LogError(availability.Message, ex);


            }
            return availability;

        }
    }
}
