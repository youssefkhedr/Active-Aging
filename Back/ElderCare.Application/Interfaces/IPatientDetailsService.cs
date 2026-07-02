using ElderCare.Application.DTOs.Patient;

namespace ElderCare.Application.Interfaces
{
    public interface IPatientDetailsService
    {
        PatientDetailsDto GetPatientDetails(Guid patientId);
    }
}
