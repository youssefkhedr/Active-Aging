IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260120151901_InitialSqlServer'
)
BEGIN
    CREATE TABLE [BalanceAssessments] (
        [Id] uniqueidentifier NOT NULL,
        [PatientId] uniqueidentifier NOT NULL,
        [SwayScore] float NOT NULL,
        [StabilityIndex] float NOT NULL,
        [TestType] nvarchar(max) NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        CONSTRAINT [PK_BalanceAssessments] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260120151901_InitialSqlServer'
)
BEGIN
    CREATE TABLE [CognitiveMiniCogResults] (
        [Id] uniqueidentifier NOT NULL,
        [PatientId] uniqueidentifier NOT NULL,
        [RecallScore] int NOT NULL,
        [ClockResult] nvarchar(max) NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        CONSTRAINT [PK_CognitiveMiniCogResults] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260120151901_InitialSqlServer'
)
BEGIN
    CREATE TABLE [CognitiveMmseResults] (
        [Id] uniqueidentifier NOT NULL,
        [PatientId] uniqueidentifier NOT NULL,
        [SectionScores] nvarchar(max) NOT NULL,
        [TotalScore] int NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        CONSTRAINT [PK_CognitiveMmseResults] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260120151901_InitialSqlServer'
)
BEGIN
    CREATE TABLE [FiveTstsResults] (
        [Id] uniqueidentifier NOT NULL,
        [PatientId] uniqueidentifier NOT NULL,
        [TotalTimeSeconds] float NOT NULL,
        [ValidReps] int NOT NULL,
        [ProbableSarcopenia] bit NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        CONSTRAINT [PK_FiveTstsResults] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260120151901_InitialSqlServer'
)
BEGIN
    CREATE TABLE [RomAssessments] (
        [Id] uniqueidentifier NOT NULL,
        [PatientId] uniqueidentifier NOT NULL,
        [JointType] nvarchar(max) NOT NULL,
        [MaxAngle] float NOT NULL,
        [MinAngle] float NOT NULL,
        [Status] nvarchar(max) NOT NULL,
        [SessionId] uniqueidentifier NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        CONSTRAINT [PK_RomAssessments] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260120151901_InitialSqlServer'
)
BEGIN
    CREATE TABLE [SarcFResults] (
        [Id] uniqueidentifier NOT NULL,
        [PatientId] uniqueidentifier NOT NULL,
        [Strength] int NOT NULL,
        [Walking] int NOT NULL,
        [ChairRise] int NOT NULL,
        [Stairs] int NOT NULL,
        [Falls] int NOT NULL,
        [TotalScore] int NOT NULL,
        [AtRisk] bit NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        CONSTRAINT [PK_SarcFResults] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260120151901_InitialSqlServer'
)
BEGIN
    CREATE TABLE [TrainingPlans] (
        [Id] uniqueidentifier NOT NULL,
        [PatientId] uniqueidentifier NOT NULL,
        [DoctorId] uniqueidentifier NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        [StartDate] datetime2 NOT NULL,
        [EndDate] datetime2 NULL,
        [Exercises] nvarchar(max) NOT NULL,
        [Sets] int NOT NULL,
        [Reps] int NOT NULL,
        [Schedule] nvarchar(max) NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        CONSTRAINT [PK_TrainingPlans] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260120151901_InitialSqlServer'
)
BEGIN
    CREATE TABLE [Users] (
        [Id] uniqueidentifier NOT NULL,
        [FullName] nvarchar(max) NOT NULL,
        [Email] nvarchar(450) NOT NULL,
        [PasswordHash] nvarchar(max) NOT NULL,
        [Role] nvarchar(max) NOT NULL,
        [Age] int NOT NULL,
        [Gender] nvarchar(max) NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [IsActive] bit NOT NULL,
        CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260120151901_InitialSqlServer'
)
BEGIN
    CREATE TABLE [Doctors] (
        [Id] uniqueidentifier NOT NULL,
        [UserId] uniqueidentifier NOT NULL,
        [Specialization] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Doctors] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Doctors_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260120151901_InitialSqlServer'
)
BEGIN
    CREATE TABLE [Patients] (
        [Id] uniqueidentifier NOT NULL,
        [UserId] uniqueidentifier NOT NULL,
        [DoctorId] uniqueidentifier NULL,
        [DateOfBirth] datetime2 NOT NULL,
        [Gender] nvarchar(max) NOT NULL,
        [MedicalNotes] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Patients] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Patients_Doctors_DoctorId] FOREIGN KEY ([DoctorId]) REFERENCES [Doctors] ([Id]),
        CONSTRAINT [FK_Patients_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260120151901_InitialSqlServer'
)
BEGIN
    CREATE TABLE [MedicalRecords] (
        [Id] uniqueidentifier NOT NULL,
        [PatientId] uniqueidentifier NOT NULL,
        [DoctorId] uniqueidentifier NOT NULL,
        [Diagnosis] nvarchar(max) NOT NULL,
        [Notes] nvarchar(max) NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        CONSTRAINT [PK_MedicalRecords] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_MedicalRecords_Doctors_DoctorId] FOREIGN KEY ([DoctorId]) REFERENCES [Doctors] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_MedicalRecords_Patients_PatientId] FOREIGN KEY ([PatientId]) REFERENCES [Patients] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260120151901_InitialSqlServer'
)
BEGIN
    CREATE TABLE [TreatmentPlans] (
        [Id] uniqueidentifier NOT NULL,
        [PatientId] uniqueidentifier NOT NULL,
        [DoctorId] uniqueidentifier NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        [StartDate] datetime2 NOT NULL,
        [EndDate] datetime2 NULL,
        CONSTRAINT [PK_TreatmentPlans] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_TreatmentPlans_Doctors_DoctorId] FOREIGN KEY ([DoctorId]) REFERENCES [Doctors] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_TreatmentPlans_Patients_PatientId] FOREIGN KEY ([PatientId]) REFERENCES [Patients] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260120151901_InitialSqlServer'
)
BEGIN
    CREATE INDEX [IX_Doctors_UserId] ON [Doctors] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260120151901_InitialSqlServer'
)
BEGIN
    CREATE INDEX [IX_MedicalRecords_DoctorId] ON [MedicalRecords] ([DoctorId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260120151901_InitialSqlServer'
)
BEGIN
    CREATE INDEX [IX_MedicalRecords_PatientId] ON [MedicalRecords] ([PatientId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260120151901_InitialSqlServer'
)
BEGIN
    CREATE INDEX [IX_Patients_DoctorId] ON [Patients] ([DoctorId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260120151901_InitialSqlServer'
)
BEGIN
    CREATE INDEX [IX_Patients_UserId] ON [Patients] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260120151901_InitialSqlServer'
)
BEGIN
    CREATE INDEX [IX_TreatmentPlans_DoctorId] ON [TreatmentPlans] ([DoctorId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260120151901_InitialSqlServer'
)
BEGIN
    CREATE INDEX [IX_TreatmentPlans_PatientId] ON [TreatmentPlans] ([PatientId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260120151901_InitialSqlServer'
)
BEGIN
    CREATE UNIQUE INDEX [IX_Users_Email] ON [Users] ([Email]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260120151901_InitialSqlServer'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260120151901_InitialSqlServer', N'8.0.2');
END;
GO

COMMIT;
GO

