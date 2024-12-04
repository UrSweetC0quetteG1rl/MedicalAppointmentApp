using MedicalAppointment.Aplication.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppointment.Aplication.Responses.Configuration.DoctorAvailability
{
    public class DoctorAvailabilityResponse : BaseResponse
    {
        public dynamic? Data { get; set; }
    }
}
