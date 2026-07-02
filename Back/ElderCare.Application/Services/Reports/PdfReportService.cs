using ElderCare.Application.DTOs.Reports;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace ElderCare.Application.Services.Reports
{
    public class PdfReportService
    {
        public byte[] GeneratePatientReport(PatientReportDto dto)
        {
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);
                    page.Header().Text("ElderCare Medical Report")
                        .FontSize(20).Bold().AlignCenter();

                    page.Content().Column(col =>
                    {
                        col.Spacing(10);

                        col.Item().Text($"Patient: {dto.PatientName}");
                        col.Item().Text($"Age: {dto.Age} | Gender: {dto.Gender}");

                        col.Item().LineHorizontal(1);

                        col.Item().Text($"ROM Status: {dto.RomStatus}");
                        col.Item().Text($"Balance Score: {dto.BalanceScore}");

                        col.Item().Text($"Sarcopenia Risk: {(dto.SarcopeniaRisk ? "YES" : "NO")}");
                        col.Item().Text($"Cognitive Score: {dto.CognitiveScore}");

                        col.Item().Text($"Prediction: {dto.PredictionSummary}");

                        col.Item().Text($"Generated At: {dto.GeneratedAt}");
                    });
                });
            });

            return document.GeneratePdf();
        }
    }
}
