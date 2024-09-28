using MedicalAppointmentApp.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppointmentApp.Domain.Entities.insurance
{
    internal class Insurance_NetworkType : BaseEntity
    {
        public int NetworkTypeID { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

    }
}
