using MedicalAppointmentApp.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppointmentApp.Domain.Entities.insurance
{
    internal class insurance_NetworkType : BaseEntity
    {
        public int NetworkTypeID { get; set; }

        public string nombre { get; set; }

        public string? Description { get; set; }

    }
}
