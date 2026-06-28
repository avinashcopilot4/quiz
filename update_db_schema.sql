-- Safe SQL migration script for Quiz DB schema updates
-- This version uses a separate UserRoles table so one user can have multiple roles
-- such as both admin and employee.

SET NOCOUNT ON;
GO

-- 1) Add Gender column to Users if it does not exist
IF COL_LENGTH('dbo.Users', 'Gender') IS NULL
BEGIN
    ALTER TABLE dbo.Users
    ADD Gender NVARCHAR(20) NULL;
END
GO

-- 2) Create UserRoles table if it does not exist
IF NOT EXISTS (
    SELECT 1
    FROM sys.objects
    WHERE object_id = OBJECT_ID(N'dbo.UserRoles')
      AND type in (N'U')
)
BEGIN
    CREATE TABLE dbo.UserRoles (
        UserRoleID INT IDENTITY(1,1) PRIMARY KEY,
        UserID INT NOT NULL,
        RoleName NVARCHAR(50) NOT NULL,
        CreatedDate DATETIME NULL DEFAULT GETDATE(),
        CONSTRAINT FK_UserRoles_Users FOREIGN KEY (UserID) REFERENCES dbo.Users(UserID) ON DELETE CASCADE,
        CONSTRAINT CK_UserRoles_RoleName CHECK (LOWER(RoleName) IN ('admin', 'employee'))
    );
END
GO

-- 3) Migrate existing single-role data from Users.Role into UserRoles
--    Only if the table is empty and there is existing role data
IF EXISTS (
    SELECT 1
    FROM dbo.Users
    WHERE Role IS NOT NULL
      AND LTRIM(RTRIM(Role)) <> ''
)
AND NOT EXISTS (
    SELECT 1
    FROM dbo.UserRoles
)
BEGIN
    INSERT INTO dbo.UserRoles (UserID, RoleName)
    SELECT UserID, LOWER(LTRIM(RTRIM(Role))) AS RoleName
    FROM dbo.Users
    WHERE Role IS NOT NULL
      AND LTRIM(RTRIM(Role)) <> '';
END
GO

-- 4) Drop the old role constraint safely if it exists
IF EXISTS (
    SELECT 1
    FROM sys.check_constraints
    WHERE name = 'CK_Users_Role' AND parent_object_id = OBJECT_ID('dbo.Users')
)
BEGIN
    ALTER TABLE dbo.Users
    DROP CONSTRAINT CK_Users_Role;
END
GO

-- 5) Drop the old default role constraint safely if it exists
IF EXISTS (
    SELECT 1
    FROM sys.default_constraints
    WHERE parent_object_id = OBJECT_ID('dbo.Users')
      AND name = 'DF__Users__Role'
)
BEGIN
    ALTER TABLE dbo.Users
    DROP CONSTRAINT DF__Users__Role;
END
GO

-- 6) Remove the old Role column from Users (safe guard: only if UserRoles table exists and column exists)
IF COL_LENGTH('dbo.Users', 'Role') IS NOT NULL
   AND EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.UserRoles') AND type in (N'U'))
BEGIN
    ALTER TABLE dbo.Users DROP COLUMN Role;
END
GO

-- 7) Add PhoneNumber column to Users if it does not exist
IF COL_LENGTH('dbo.Users', 'PhoneNumber') IS NULL
BEGIN
    ALTER TABLE dbo.Users
    ADD PhoneNumber NVARCHAR(20) NULL;
END
GO

-- 8) Backfill missing Gender values safely
UPDATE dbo.Users
SET Gender = 'Other'
WHERE Gender IS NULL OR LTRIM(RTRIM(Gender)) = '';
GO

PRINT 'Database schema update completed successfully.';
GO
