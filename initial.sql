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

CREATE TABLE [Countries] (
    [Id] int NOT NULL IDENTITY,
    [Name] nchar(50) NOT NULL,
    [Iso2] char(2) NOT NULL,
    [Iso3] char(3) NOT NULL,
    CONSTRAINT [PK_Countries] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Cities] (
    [Id] int NOT NULL IDENTITY,
    [Name] nchar(50) NOT NULL,
    [Lat] decimal(18,4) NOT NULL,
    [Lon] decimal(18,4) NOT NULL,
    [CountryId] int NOT NULL,
    CONSTRAINT [PK_Cities] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Cities_Countries] FOREIGN KEY ([CountryId]) REFERENCES [Countries] ([Id])
);
GO

CREATE INDEX [IX_Cities_CountryId] ON [Cities] ([CountryId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231006203054_Initial', N'7.0.11');
GO

COMMIT;
GO

