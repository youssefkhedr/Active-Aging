using ElderCare.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using ElderCare.API.Reports;
using ElderCare.Application.DTOs.Reports;
using ElderCare.Application.Services.Reports;

namespace ElderCare.API.Controllers;

[ApiController]
[Route("api/reports")]
[Authorize(Roles = "Doctor,Admin")]
public class ReportsController : ControllerBase
{
    private readonly IPdfReportService _pdfService;
    private readonly PdfReportService _newPdfService;

    public ReportsController(IPdfReportService pdfService, PdfReportService newPdfService)
    {
        _pdfService = pdfService;
        _newPdfService = newPdfService;
    }

    [HttpGet("patient/{patientId:guid}")]
    public async Task<IActionResult> Download(Guid patientId)
    {
        var pdf = await _pdfService.GeneratePatientReportAsync(patientId);

        return File(pdf, "application/pdf", "Patient_Report.pdf");
    }

    [HttpGet("patient/{id:int}")]
    public IActionResult Generate(int id)
    {
        // Fake data (replace with DB later)
        var dto = new PatientReportDto
        {
            PatientName = "Ahmed Ali",
            Age = 68,
            Gender = "Male",
            RomStatus = "Limited",
            BalanceScore = 42,
            SarcopeniaRisk = true,
            CognitiveScore = 22,
            PredictionSummary = "High fall risk",
            GeneratedAt = DateTime.UtcNow
        };

        var pdf = _newPdfService.GeneratePatientReport(dto);
        return File(pdf, "application/pdf", "PatientReport.pdf");
    }

    [HttpGet("medical/{patientId}")]
    public IActionResult Generate(Guid patientId)
    {
        var report = new MedicalReportDto
        {
            PatientName = "Ahmed Ali",
            Age = 72,
            Diagnosis = "Balance impairment",
            Exercises = new() { "Standing balance", "Resistance bands" },
            Cognitive = new() { "Memory cards" },
            CreatedAt = DateTime.Now
        };

        var pdf = new MedicalReportDocument(report).GeneratePdf();
        return File(pdf, "application/pdf", "MedicalReport.pdf");
    }
}
