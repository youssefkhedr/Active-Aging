namespace ElderCare.Application.DTOs.Reports
{
    public class MedicalReportDto
    {
        public string PatientName { get; set; }
        public int Age { get; set; }
        public string Diagnosis { get; set; }
        public List<string> Exercises { get; set; } = new();
        public List<string> Cognitive { get; set; } = new();
        public DateTime CreatedAt { get; set; }
    }
}
