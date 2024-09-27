using MedicalAppointmentApp.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppointmentApp.Domain.Entities
{
    internal class medical_Specialties : BaseEntity
    {
        public short SpecialtyID { get; set; }

        public string SpecialtyName { get; set;}

    }
}
