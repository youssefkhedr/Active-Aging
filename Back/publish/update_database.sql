BEGIN TRANSACTION;
GO

ALTER TABLE [Users] ADD [RefreshToken] nvarchar(max) NULL;
GO

ALTER TABLE [Users] ADD [RefreshTokenExpiry] datetime2 NULL;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Age', N'CreatedAt', N'Email', N'FullName', N'Gender', N'IsActive', N'PasswordHash', N'RefreshToken', N'RefreshTokenExpiry', N'Role') AND [object_id] = OBJECT_ID(N'[Users]'))
    SET IDENTITY_INSERT [Users] ON;
INSERT INTO [Users] ([Id], [Age], [CreatedAt], [Email], [FullName], [Gender], [IsActive], [PasswordHash], [RefreshToken], [RefreshTokenExpiry], [Role])
VALUES ('a1b2c3d4-e5f6-4a5b-8c9d-0e1f2a3b4c5d', 30, '2026-01-22T17:54:47.7831719Z', N'yousefahmed012732@gmail.com', N'Admin User', N'Male', CAST(1 AS bit), N'$2a$11$N9qo8uLOickgx2ZMRZoMyeIjZAgNoTfG6YmD8oJ8e.973jR.2W6U.', NULL, NULL, N'Admin');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Age', N'CreatedAt', N'Email', N'FullName', N'Gender', N'IsActive', N'PasswordHash', N'RefreshToken', N'RefreshTokenExpiry', N'Role') AND [object_id] = OBJECT_ID(N'[Users]'))
    SET IDENTITY_INSERT [Users] OFF;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260122175449_UpdateUserForRefreshTokenAndSeed', N'8.0.2');
GO

COMMIT;
GO

