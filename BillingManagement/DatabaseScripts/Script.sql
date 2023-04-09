CREATE TABLE [dbo].[Vendors]
(
Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
[Name] nvarchar(200) NOT NULL,
GstNumber nvarchar(50) NOT NULL,
Address nvarchar(2000) NOT NULL,
);

CREATE TABLE [dbo].[Items]
(
Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
[Name] nvarchar(200) NOT NULL,
[Description] nvarchar(500) NOT NULL,
Unit int NOT NULL,
Quantity decimal NOT NULL,
RateRange1 decimal NOT NULL,
RateRange2 decimal NOT NULL,
Rate decimal NOT NULL,
Sgst decimal NOT NULL,
Cgst decimal NOT NULL
);