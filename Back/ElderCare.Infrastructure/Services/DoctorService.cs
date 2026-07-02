using ElderCare.Application.DTOs.Doctor;
using ElderCare.Application.Interfaces;
using ElderCare.Core.Entities;
using ElderCare.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ElderCare.Infrastructure.Services;

public class DoctorService : IDoctorService
{
    private readonly AppDbContext _context;

    public DoctorService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<DoctorPatientDto>> GetMyPatientsAsync(Guid doctorUserId)
    {
        var doctor = await _context.Doctors
            .FirstOrDefaultAsync(d => d.UserId == doctorUserId)
            ?? throw new Exception("Doctor not found");

        return await _context.Patients
            .Where(p => p.DoctorId == doctor.Id)
            .Include(p => p.User)
            .Select(p => new DoctorPatientDto
            {
                PatientId = p.Id,
                FullName = p.User.FullName,
                Age = p.User.Age,
                Gender = p.User.Gender,
                RiskLevel = "Low" // Default, populated by advanced services later
            })
            .ToListAsync();
    }

    public async Task CreateTrainingPlanAsync(Guid doctorUserId, CreateTrainingPlanDto dto)
    {
        var doctor = await _context.Doctors
            .FirstOrDefaultAsync(d => d.UserId == doctorUserId)
            ?? throw new Exception("Doctor not found");

        var plan = new TrainingPlan
        {
            Id = Guid.NewGuid(),
            DoctorId = doctor.Id,
            PatientId = dto.PatientId,
            Description = dto.Description,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            // Default empty values for now as they might be updated later
            Exercises = "[]", 
            Schedule = ""
        };

        _context.TrainingPlans.Add(plan);
        await _context.SaveChangesAsync();
    }

    // Keeping these methods for backward compatibility if needed, or implementing empty if interface changed completely
    // Note: The interface update above removed AddMedicalRecordAsync and AddTreatmentPlanAsync
    // If the previous IDoctorService methods are still needed, they should be added back to the interface or this file adjusted.
    // Based on user request "Step 13.2", the interface ONLY has GetMyPatientsAsync and CreateTrainingPlanAsync.
    // So I will stick to what the user explicitly asked for in this file.
}
