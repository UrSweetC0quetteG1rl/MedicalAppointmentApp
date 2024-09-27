using MedicalAppointmentApp.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppointmentApp.Domain.Entities
{
    internal class medical_AvailabilityModes : BaseEntity
    {
        public short SAvailability {  get; set; }

        public string AvailabilityMobe { get; set; }

    }
}
