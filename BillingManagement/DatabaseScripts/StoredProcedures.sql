/* 
Vendors
*/

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetVendors]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetVendors]
GO

create procedure [dbo].[GetVendors]
as
BEGIN
Select * from [dbo].[Vendors]
END

GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetVendor]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetVendor]
GO

create procedure [dbo].[GetVendor]
@Id UniqueIdentifier
as
BEGIN
Select * from [dbo].[Vendors]
Where Id = @Id
END

GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeleteVendor]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[DeleteVendor]
GO

create procedure [dbo].[DeleteVendor]
@Id UniqueIdentifier
as
BEGIN
Delete from [dbo].[Vendors]
Where Id = @Id
END

GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddVendor]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[AddVendor]
GO

create procedure [dbo].[AddVendor]
@name nvarchar(200),
@gstNumber nvarchar(50),
@address nvarchar(2000)

as
BEGIN
Insert int [dbo].[Vendors] ([Name], GstNumber, [Address])
values(@name, @gstNumber, @address)
END

GO

/* 
Users
*/

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetUserByName]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetUserByName]
GO

create procedure [dbo].[GetUserByName]
@name nvarchar(200)
as
BEGIN
Select * from [dbo].[Users]
Where [Name] = @name
END

GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddUser]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[AddUser]
GO

create procedure [dbo].[AddUser]
@name nvarchar(200),
@password nvarchar(500)

as
BEGIN
Insert into [dbo].[Users] ([Name], [Password])
values(@name, @password)
END

GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ChangePassword]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ChangePassword]
GO

create procedure [dbo].[ChangePassword]
@name nvarchar(200),
@newPassword nvarchar(500)

as
BEGIN

Update [dbo].[Users]
Set [Password] = @newPassword
Where [Name] = @name 
END

GO