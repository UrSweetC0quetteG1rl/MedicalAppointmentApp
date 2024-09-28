using MedicalAppointmentApp.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppointmentApp.Domain.Entities
{
    internal class insurance_InsuranceProviders : BaseEntity
    {

        public string Nombre { get; set; }

        public string ContactNumber { get; set;}

        public string Email { get; set;}

        public string? Website { get; set;}

        public string Address { get; set;}

        public string? City { get; set;}

        public string? State { get; set;}

        public string? Country { get; set;}

        public string? ZipCode { get; set;}

        public string CoverageDetails { get; set;}

        public string? LogoUrl { get; set;}

        public bool IsPreferred { get; set;}

        public int NetworkTyped { get; set;}

        public string? CustomerSupport  { get; set;}

        public string? AcceptedRegions  { get; set;}

        public decimal? MaxCovereageAmount { get; set;}

    }
}
