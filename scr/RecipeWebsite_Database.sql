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
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260919155952_InitialCreate'
)
BEGIN
    CREATE TABLE [Categories] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NULL,
        CONSTRAINT [PK_Categories] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260919155952_InitialCreate'
)
BEGIN
    CREATE TABLE [Comments] (
        [Id] int NOT NULL IDENTITY,
        [Content] nvarchar(max) NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [UserId] int NOT NULL,
        [RecipeId] int NOT NULL,
        CONSTRAINT [PK_Comments] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260919155952_InitialCreate'
)
BEGIN
    CREATE TABLE [Ingredients] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Ingredients] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260919155952_InitialCreate'
)
BEGIN
    CREATE TABLE [RecipeIngredients] (
        [Id] int NOT NULL IDENTITY,
        [RecipeId] int NOT NULL,
        [IngredientId] int NOT NULL,
        [Quantity] decimal(18,2) NOT NULL,
        [Unit] nvarchar(max) NULL,
        CONSTRAINT [PK_RecipeIngredients] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260919155952_InitialCreate'
)
BEGIN
    CREATE TABLE [Recipes] (
        [Id] int NOT NULL IDENTITY,
        [Title] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NULL,
        [Instructions] nvarchar(max) NOT NULL,
        [CookingTime] int NOT NULL,
        [Servings] int NOT NULL,
        [ImageUrl] nvarchar(max) NULL,
        [CreatedAt] datetime2 NOT NULL,
        [UserId] int NOT NULL,
        [CategoryId] int NOT NULL,
        CONSTRAINT [PK_Recipes] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260919155952_InitialCreate'
)
BEGIN
    CREATE TABLE [Users] (
        [Id] int NOT NULL IDENTITY,
        [FullName] nvarchar(max) NOT NULL,
        [Email] nvarchar(max) NOT NULL,
        [Password] nvarchar(max) NOT NULL,
        [Role] nvarchar(max) NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260919155952_InitialCreate'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260919155952_InitialCreate', N'10.0.12');
END;

COMMIT;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260920041043_FixQuantityPrecision'
)
BEGIN
    DECLARE @var nvarchar(max);
    SELECT @var = QUOTENAME([d].[name])
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[RecipeIngredients]') AND [c].[name] = N'Quantity');
    IF @var IS NOT NULL EXEC(N'ALTER TABLE [RecipeIngredients] DROP CONSTRAINT ' + @var + ';');
    ALTER TABLE [RecipeIngredients] ALTER COLUMN [Quantity] decimal(10,2) NOT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260920041043_FixQuantityPrecision'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260920041043_FixQuantityPrecision', N'10.0.12');
END;

COMMIT;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260922121101_RenamePasswordToPasswordHash'
)
BEGIN
    EXEC sp_rename N'[Users].[Password]', N'PasswordHash', 'COLUMN';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260922121101_RenamePasswordToPasswordHash'
)
BEGIN
    CREATE INDEX [IX_Recipes_CategoryId] ON [Recipes] ([CategoryId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260922121101_RenamePasswordToPasswordHash'
)
BEGIN
    CREATE INDEX [IX_RecipeIngredients_IngredientId] ON [RecipeIngredients] ([IngredientId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260922121101_RenamePasswordToPasswordHash'
)
BEGIN
    CREATE INDEX [IX_RecipeIngredients_RecipeId] ON [RecipeIngredients] ([RecipeId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260922121101_RenamePasswordToPasswordHash'
)
BEGIN
    CREATE INDEX [IX_Comments_RecipeId] ON [Comments] ([RecipeId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260922121101_RenamePasswordToPasswordHash'
)
BEGIN
    CREATE INDEX [IX_Comments_UserId] ON [Comments] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260922121101_RenamePasswordToPasswordHash'
)
BEGIN
    ALTER TABLE [Comments] ADD CONSTRAINT [FK_Comments_Recipes_RecipeId] FOREIGN KEY ([RecipeId]) REFERENCES [Recipes] ([Id]) ON DELETE CASCADE;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260922121101_RenamePasswordToPasswordHash'
)
BEGIN
    ALTER TABLE [Comments] ADD CONSTRAINT [FK_Comments_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260922121101_RenamePasswordToPasswordHash'
)
BEGIN
    ALTER TABLE [RecipeIngredients] ADD CONSTRAINT [FK_RecipeIngredients_Ingredients_IngredientId] FOREIGN KEY ([IngredientId]) REFERENCES [Ingredients] ([Id]) ON DELETE CASCADE;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260922121101_RenamePasswordToPasswordHash'
)
BEGIN
    ALTER TABLE [RecipeIngredients] ADD CONSTRAINT [FK_RecipeIngredients_Recipes_RecipeId] FOREIGN KEY ([RecipeId]) REFERENCES [Recipes] ([Id]) ON DELETE CASCADE;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260922121101_RenamePasswordToPasswordHash'
)
BEGIN
    ALTER TABLE [Recipes] ADD CONSTRAINT [FK_Recipes_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([Id]) ON DELETE CASCADE;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260922121101_RenamePasswordToPasswordHash'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260922121101_RenamePasswordToPasswordHash', N'10.0.12');
END;

COMMIT;
GO

