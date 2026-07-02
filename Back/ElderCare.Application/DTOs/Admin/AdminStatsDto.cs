namespace ElderCare.Application.DTOs.Admin
{
    public class AdminStatsDto
    {
        public int TotalUsers { get; set; }
        public int Doctors { get; set; }
        public int Patients { get; set; }
        public int ActiveSessions { get; set; }
        public int ReportsGenerated { get; set; }
    }
}
