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

