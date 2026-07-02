-- Clerk Integration Migration Script
-- Run this SQL to add Clerk OAuth support to the Users table

-- Add ClerkUserId column (stores Clerk user ID like "user_2abc123def456")
IF NOT EXISTS (SELECT * FROM sys.columns WHERE Name = N'ClerkUserId' AND Object_ID = Object_ID(N'Users'))
BEGIN
    ALTER TABLE [Users] ADD [ClerkUserId] nvarchar(max) NULL;
    PRINT 'Added ClerkUserId column';
END
GO

-- Add ProfilePictureUrl column (stores Google profile picture URL)
IF NOT EXISTS (SELECT * FROM sys.columns WHERE Name = N'ProfilePictureUrl' AND Object_ID = Object_ID(N'Users'))
BEGIN
    ALTER TABLE [Users] ADD [ProfilePictureUrl] nvarchar(max) NULL;
    PRINT 'Added ProfilePictureUrl column';
END
GO

-- Record the migration in EF Core history (if using EF migrations)
IF NOT EXISTS (SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260122184055_AddClerkUserIdToUser')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260122184055_AddClerkUserIdToUser', N'8.0.2');
    PRINT 'Migration recorded in history';
END
GO

PRINT 'Clerk integration migration complete!';
