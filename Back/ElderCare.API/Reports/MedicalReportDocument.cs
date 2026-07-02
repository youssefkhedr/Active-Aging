using ElderCare.Application.DTOs.Reports;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace ElderCare.API.Reports
{
    public class MedicalReportDocument : IDocument
    {
        private readonly MedicalReportDto _data;

        public MedicalReportDocument(MedicalReportDto data)
        {
            _data = data;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Margin(40);

                page.Header().Text("ElderCare Medical Report")
                    .FontSize(20).Bold().AlignCenter();

                page.Content().Column(col =>
                {
                    col.Spacing(10);

                    col.Item().Text($"Patient: {_data.PatientName}");
                    col.Item().Text($"Age: {_data.Age}");
                    col.Item().Text($"Diagnosis: {_data.Diagnosis}");

                    col.Item().Text("Physical Exercises").Bold();
                    foreach (var ex in _data.Exercises)
                        col.Item().Text($"- {ex}");

                    col.Item().Text("Cognitive Exercises").Bold();
                    foreach (var c in _data.Cognitive)
                        col.Item().Text($"- {c}");

                    col.Item().Text($"Generated At: {_data.CreatedAt}");
                });

                page.Footer().AlignCenter().Text("Doctor Signature");
            });
        }
    }
}
