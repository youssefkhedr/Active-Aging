using ElderCare.Application.DTOs.Patient;
using ElderCare.Application.Interfaces;
using ElderCare.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ElderCare.Infrastructure.Services;

public class PatientService : IPatientService
{
    private readonly AppDbContext _context;

    public PatientService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<PatientMedicalRecordDto>> GetMedicalRecordsAsync(Guid patientUserId)
    {
        var patient = await _context.Patients
            .FirstOrDefaultAsync(p => p.UserId == patientUserId)
            ?? throw new Exception("Patient not found");

        return await _context.MedicalRecords
            .Where(m => m.PatientId == patient.Id)
            .Select(m => new PatientMedicalRecordDto
            {
                DoctorName = m.Doctor.User.FullName,
                Diagnosis = m.Diagnosis,
                Notes = m.Notes,
                CreatedAt = m.CreatedAt
            })
            .OrderByDescending(m => m.CreatedAt)
            .ToListAsync();
    }

    public async Task<List<PatientTreatmentPlanDto>> GetTreatmentPlansAsync(Guid patientUserId)
    {
        var patient = await _context.Patients
            .FirstOrDefaultAsync(p => p.UserId == patientUserId)
            ?? throw new Exception("Patient not found");

        return await _context.TreatmentPlans
            .Where(t => t.PatientId == patient.Id)
            .Select(t => new PatientTreatmentPlanDto
            {
                DoctorName = t.Doctor.User.FullName,
                Description = t.Description,
                StartDate = t.StartDate,
                EndDate = t.EndDate
            })
            .ToListAsync();
    }
}
