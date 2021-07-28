USE master;
GO

IF NOT EXISTS (
        SELECT name
        FROM [sys].[server_principals]
        WHERE [type] = N'S' AND  [name] = N'TheGarageAdminLogin'
    )
BEGIN
    CREATE LOGIN [TheGarageAdminLogin] WITH PASSWORD='dummy', CHECK_POLICY = OFF;
END
GO

IF NOT EXISTS(
    SELECT name
    FROM master.dbo.sysdatabases
    WHERE name=N'TheGarage'
)
CREATE DATABASE [TheGarage]
GO

-- USE [TheGarage];
-- GO

-- IF NOT EXISTS (
--         SELECT name
--         FROM [sys].[database_principals]
--         WHERE [type] = N'S' AND  [name] = N'TheGarageAdminUser'
--     )
-- BEGIN
--     USE [TheGarage];
--     CREATE USER [TheGarageAdminUser] FOR LOGIN [TheGarageAdminLogin];
-- END
-- GO

USE master
GO
DENY VIEW ANY DATABASE TO [TheGarageAdminLogin];
GO
ALTER AUTHORIZATION ON DATABASE::[TheGarage] TO TheGarageAdminLogin;
GO

-- USE [TheGarage]
-- GO
-- ALTER ROLE [db_owner] ADD MEMBER [TheGarageAdminUser];
-- GO

-- USE master
-- GO
-- DROP DATABASE [TheGarage]
-- DROP LOGIN [TheGarageAdminLogin]
-- USE [TheGarage]
-- GO
-- DROP USER [TheGarageAdminUser]
-- GO