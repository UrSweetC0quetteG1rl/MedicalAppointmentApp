using MedicalAppointmentApp.Domain.Entities.Medical;
using MedicalAppointmentApp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppointment.Persistance.Interfaces.Configuration.Medical
{
    public interface IAvailabilityModesRepository : IBaseRepository<AvailabilityModes>
    {
    }
}
