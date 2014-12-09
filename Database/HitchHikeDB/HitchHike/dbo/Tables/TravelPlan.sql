CREATE TABLE dbo.TravelPlan
(
	[PlanId]				INT IDENTITY(1,1) NOT NULL,
	[UserIdentityId]			INT NOT NULL,
	[Source]				NVARCHAR(30) NOT NULL,
	[Destination]			NVARCHAR(30) NOT NULL,
	[TravelDateTime]		DATETIME NOT NULL,
	[VehicleType]			NVARCHAR(10) NOT NULL,
	[TotalFare]				SMALLMONEY NOT NULL,
	[Capacity]				TINYINT NULL,
	[Availability]			TINYINT NULL,
	[Description]			NVARCHAR(200) NULL,
	[ExpirationDateTime]	DATETIME NULL,
	[CreatedDateTime]		DATETIME NOT NULL DEFAULT GETUTCDATE(),
	[LastUpdatedDateTime]	DATETIME NULL,
    CONSTRAINT PK_PlanId	PRIMARY KEY([PlanId]),
	CONSTRAINT FK_Plan_User FOREIGN KEY([UserIdentityId]) REFERENCES [dbo].[User]([UserIdentityId])
);