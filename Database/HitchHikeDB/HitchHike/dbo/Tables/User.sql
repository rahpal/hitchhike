CREATE TABLE [dbo].[User]
(
	[UserIdentityId] [int] IDENTITY(1,1) NOT NULL UNIQUE,
	[Username] [nvarchar](50) NOT NULL,
	[Firstname] [nvarchar](20) NOT NULL,
	[Lastname] [nvarchar](20) NOT NULL,
	[Gender] CHAR(1) NOT NULL,
	[Password] NVARCHAR(30) NOT NULL,
	[EmailAddress] NVARCHAR(50) NOT NULL,
	[ContactNumber] NVARCHAR(15) NULL,
	CONSTRAINT PK_Username PRIMARY KEY([Username])
);
