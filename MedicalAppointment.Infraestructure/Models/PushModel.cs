using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppointment.Infraestructure.Models
{
    public class PushModel
    {
        public string? UserId { get; set; }
        public string? Title { get; set; }
        public string? Message { get; set; }

    }
}
