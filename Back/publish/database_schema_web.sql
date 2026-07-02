-- Create Tables for ElderCare Database (Web Compatible - No GO statements)

-- Users Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Users')
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
    CREATE UNIQUE INDEX [IX_Users_Email] ON [Users] ([Email]);
END;

-- Doctors Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Doctors')
BEGIN
    CREATE TABLE [Doctors] (
        [Id] uniqueidentifier NOT NULL,
        [UserId] uniqueidentifier NOT NULL,
        [Specialization] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Doctors] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Doctors_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
    CREATE INDEX [IX_Doctors_UserId] ON [Doctors] ([UserId]);
END;

-- Patients Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Patients')
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
    CREATE INDEX [IX_Patients_DoctorId] ON [Patients] ([DoctorId]);
    CREATE INDEX [IX_Patients_UserId] ON [Patients] ([UserId]);
END;

-- BalanceAssessments Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'BalanceAssessments')
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

-- CognitiveMiniCogResults Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'CognitiveMiniCogResults')
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

-- CognitiveMmseResults Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'CognitiveMmseResults')
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

-- FiveTstsResults Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'FiveTstsResults')
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

-- RomAssessments Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'RomAssessments')
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

-- SarcFResults Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'SarcFResults')
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

-- TrainingPlans Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'TrainingPlans')
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

-- MedicalRecords Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'MedicalRecords')
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
        CONSTRAINT [FK_MedicalRecords_Patients_PatientId] FOREIGN KEY ([PatientId]) REFERENCES [Patients] ([Id]) ON DELETE NO ACTION
    );
    CREATE INDEX [IX_MedicalRecords_DoctorId] ON [MedicalRecords] ([DoctorId]);
    CREATE INDEX [IX_MedicalRecords_PatientId] ON [MedicalRecords] ([PatientId]);
END;

-- TreatmentPlans Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'TreatmentPlans')
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
        CONSTRAINT [FK_TreatmentPlans_Patients_PatientId] FOREIGN KEY ([PatientId]) REFERENCES [Patients] ([Id]) ON DELETE NO ACTION
    );
    CREATE INDEX [IX_TreatmentPlans_DoctorId] ON [TreatmentPlans] ([DoctorId]);
    CREATE INDEX [IX_TreatmentPlans_PatientId] ON [TreatmentPlans] ([PatientId]);
END;

-- Update Migration History
IF NOT EXISTS (SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260120151901_InitialSqlServer')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260120151901_InitialSqlServer', N'8.0.2');
END;

PRINT 'All tables created successfully!';
