using MedicalAppointmentApp.Domain.Base;


namespace MedicalAppointmentApp.Domain.Entities.Insurance
{
    //Orlando Martinez 2020-10382
    public class Insurance_InsuranceProviders : BaseEntity
    {
     
            public string Name { get; set; }

            public string ContactNumber { get; set; }

            public string Email { get; set; }

            public string? Website { get; set; }

            public string Address { get; set; }

            public string? City { get; set; }

            public string? State { get; set; }

            public string? Country { get; set; }

            public string? ZipCode { get; set; }

            public string CoverageDetails { get; set; }

            public string? LogoUrl { get; set; }

            public bool IsPreferred { get; set; }

            public int NetworkTyped { get; set; }

            public string? CustomerSupport { get; set; }

            public string? AcceptedRegions { get; set; }

            public decimal? MaxCovereageAmount { get; set; }

}
}
