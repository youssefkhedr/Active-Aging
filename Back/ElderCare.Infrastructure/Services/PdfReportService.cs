using ElderCare.Application.Interfaces;
using ElderCare.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace ElderCare.Infrastructure.Services;

public class PdfReportService : IPdfReportService
{
    private readonly AppDbContext _context;

    public PdfReportService(AppDbContext context)
    {
        _context = context;
        // QuestPDF license configuration
        QuestPDF.Settings.License = LicenseType.Community;
    }

    public async Task<byte[]> GeneratePatientReportAsync(Guid patientId)
    {
        var patient = await _context.Patients
            .Include(p => p.User)
            .FirstAsync(p => p.Id == patientId);

        var balance = await _context.BalanceAssessments
            .OrderByDescending(b => b.CreatedAt)
            .FirstOrDefaultAsync(b => b.PatientId == patientId);

        // Using CognitiveMmseResults as verified in database schema
        var mmse = await _context.CognitiveMmseResults
            .OrderByDescending(m => m.CreatedAt)
            .FirstOrDefaultAsync(m => m.PatientId == patientId);

        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(30);
                page.Size(PageSizes.A4);

                page.Content().Column(col =>
                {
                    col.Item().Text("ElderCare Medical Report")
                        .FontSize(20).Bold().AlignCenter();

                    col.Item().Text($"Patient: {patient.User.FullName}");
                    col.Item().Text($"Age: {patient.User.Age}"); // Accessed via User as per Patient entity definition
                    col.Item().Text($"Generated: {DateTime.Now:d}");

                    col.Item().LineHorizontal(1);

                    if (balance != null)
                        col.Item().Text($"Balance Stability Index: {balance.StabilityIndex}");

                    if (mmse != null)
                        col.Item().Text($"MMSE Score: {mmse.TotalScore}");

                    col.Item().LineHorizontal(1);
                    col.Item().Text("Doctor Notes:")
                        .Italic();

                    col.Item().Text("Follow exercise plan and reassess after 4 weeks.");
                });
            });
        });

        return document.GeneratePdf();
    }
}
