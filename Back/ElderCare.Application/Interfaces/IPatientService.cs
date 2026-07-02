using ElderCare.Application.DTOs.Patient;

namespace ElderCare.Application.Interfaces;

public interface IPatientService
{
    Task<List<PatientMedicalRecordDto>> GetMedicalRecordsAsync(Guid patientUserId);
    Task<List<PatientTreatmentPlanDto>> GetTreatmentPlansAsync(Guid patientUserId);
}
