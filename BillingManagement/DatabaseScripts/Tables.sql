IF NOT EXISTS(SELECT NAME FROM SYS.OBJECTS WHERE object_id = OBJECT_ID(N'[dbo].[Vendors]') AND TYPE in (N'U'))
BEGIN

CREATE TABLE [dbo].[Vendors]
(
Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
[Name] nvarchar(200) NOT NULL,
GstNumber nvarchar(50) NOT NULL,
[Address] nvarchar(2000) NOT NULL,
);

END
GO


IF NOT EXISTS(SELECT NAME FROM SYS.OBJECTS WHERE object_id = OBJECT_ID(N'[dbo].[Items]') AND TYPE in (N'U'))
BEGIN

CREATE TABLE [dbo].[Items]
(
Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
[Name] nvarchar(200) NOT NULL,
[Description] nvarchar(500) NOT NULL,
Unit int NOT NULL,
RateRange1 decimal NOT NULL,
RateRange2 decimal NOT NULL,
Rate decimal NOT NULL,
Sgst decimal NOT NULL,
Cgst decimal NOT NULL
);

END
GO

IF NOT EXISTS(SELECT NAME FROM SYS.OBJECTS WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND TYPE in (N'U'))
BEGIN

CREATE TABLE [dbo].[Users]
(
Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
[Name] nvarchar(200) NOT NULL,
[Password] nvarchar(500) NOT NULL
);

END
GO