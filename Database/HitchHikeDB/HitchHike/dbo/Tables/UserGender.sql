﻿CREATE TABLE [dbo].[UserGender](
	[GId] INT IDENTITY(1,1) NOT NULL,
	[GName] NVARCHAR(10) NOT NULL,
	[GCode] NVARCHAR(10) NOT NULL,
	CONSTRAINT PK_GCode PRIMARY KEY([GCode])
);