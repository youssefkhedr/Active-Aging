using ElderCare.Application.DTOs.Doctor;

namespace ElderCare.Application.Interfaces;

public interface IDoctorService
{
    Task<List<DoctorPatientDto>> GetMyPatientsAsync(Guid doctorUserId);
    Task CreateTrainingPlanAsync(Guid doctorUserId, CreateTrainingPlanDto dto);
}
