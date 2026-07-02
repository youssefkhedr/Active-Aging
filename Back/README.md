# ElderCare Backend API

Healthcare backend system for elderly physical and cognitive assessment.

## Tech Stack
- ASP.NET Core 8
- PostgreSQL (Configured for SQLite in Dev)
- Entity Framework Core
- JWT Authentication
- Clean Architecture
- QuestPDF for Reporting

## Roles
- Admin
- Doctor
- Patient

## Features
- **Assessments**: Physical (ROM, Balance) & Cognitive (MMSE, Mini-Cog)
- **Screening**: Sarcopenia Risk
- **Training**: Doctor-curated training plans
- **AI**: Rule-based predictions for fall risk
- **Reports**: PDF generation for patient records
- **Dashboards**: Admin and Doctor specialized views

## Getting Started

1. **Configure Database**: Update `ConnectionStrings` in `appsettings.json` if needed.
2. **Migrations**:
   ```bash
   dotnet ef database update --project ElderCare.Infrastructure --startup-project ElderCare.API
   ```
3.  **Run**:
    ```bash
    dotnet run --project ElderCare.API
    ```

## API Documentation
Swagger UI is available at `/swagger` when running in Development mode.
