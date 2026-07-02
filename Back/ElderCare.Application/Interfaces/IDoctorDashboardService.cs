using ElderCare.Application.DTOs.Doctor;

namespace ElderCare.Application.Interfaces;

public interface IDoctorDashboardService
{
    List<DoctorPatientDto> GetPatients(Guid doctorId);
}
