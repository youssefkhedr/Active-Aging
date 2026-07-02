namespace ElderCare.Application.Interfaces;

public interface IPdfReportService
{
    Task<byte[]> GeneratePatientReportAsync(Guid patientId);
}
