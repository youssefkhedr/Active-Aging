using ElderCare.Application.DTOs.Doctor;
using ElderCare.Application.Interfaces;

namespace ElderCare.Infrastructure.Services;

public class DoctorDashboardService : IDoctorDashboardService
{
    public List<DoctorPatientDto> GetPatients(Guid doctorId)
    {
        // Mock implementation as requested for Phase 1
        return new List<DoctorPatientDto>
        {
            new DoctorPatientDto
            {
                PatientId = Guid.NewGuid(),
                FullName = "Ahmed Ali",
                Age = 72,
                RiskLevel = "High",
                Gender = "Male"
            },
            new DoctorPatientDto
            {
                PatientId = Guid.NewGuid(),
                FullName = "Mona Hassan",
                Age = 68,
                RiskLevel = "Medium",
                Gender = "Female"
            }
        };
    }
}
