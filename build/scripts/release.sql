IF NOT EXISTS(SELECT [Database_id] FROM sys.databases WHERE [Name] = 'AuthorizationServiceDb')
BEGIN
    CREATE DATABASE [AuthorizationServiceDb]
END
GO

USE [AuthorizationServiceDb]
GO

IF OBJECT_ID(N'dbo.Users', N'U') IS NULL
BEGIN
CREATE TABLE [Users]
(
    [UserId]    INT          NOT NULL PRIMARY KEY IDENTITY,
    [Login]     VARCHAR(30)  NOT NULL
)
END

IF NOT EXISTS (SELECT name FROM [sys].[indexes] WHERE name = 'UQ_Users_Login')
   CREATE UNIQUE INDEX [UQ_Users_Login] ON [dbo].[Users](Login);
GO

IF OBJECT_ID(N'dbo.UserRoles', N'U') IS NULL
BEGIN
CREATE TABLE [dbo].[UserRoles]
(
    [UserRoleId]    INT    NOT NULL PRIMARY KEY IDENTITY,
    [UserId]        INT    NOT NULL,
    [RoleId]        INT    NOT NULL
)
END

IF OBJECT_ID(N'dbo.Roles', N'U') IS NULL
BEGIN
CREATE TABLE [dbo].[Roles]
(
    [RolesId]       INT             NOT NULL PRIMARY KEY IDENTITY,
    [RoleName]      VARCHAR(50)     NOT NULL,
    [RoleCode]      VARCHAR(50)     NOT NULL,
    [Description]   VARCHAR(256)    NULL
)
END

IF OBJECT_ID(N'dbo.RolePermissions', N'U') IS NULL
BEGIN 
CREATE TABLE [dbo].[RolePermissions]
(
    [RolePermissionId]  INT     NOT NULL PRIMARY KEY IDENTITY,
    [RoleId]            INT     NOT NULL,
    [PermissionId]      INT     NOT NULL
)
END

IF OBJECT_ID(N'dbo.Permissions', N'U') IS NULL
BEGIN
CREATE TABLE [dbo].[Permissions]
(
    [PermissionId]      INT             NOT NULL PRIMARY KEY IDENTITY,
    [PermissionName]    VARCHAR(50)     NOT NULL,
    [PermissionCode]    VARCHAR(50)     NOT NULL,
    [Description]       VARCHAR(256)    NULL
)
END

GO
