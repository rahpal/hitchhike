CREATE TABLE [dbo].[Notifications]
(
	[NotificationId] INT IDENTITY(1,1) NOT NULL,
	[PlanId] INT NOT NULL,
	[UserIdentityId] INT NOT NULL,
	[IsActive] BIT DEFAULT(1) NOT NULL,
	[State] TINYINT DEFAULT(0) NOT NULL,
	CONSTRAINT FK_Notifications_Plan FOREIGN KEY([PlanId]) REFERENCES [dbo].[TravelPlan]([PlanId]),
	CONSTRAINT FK_Notifications_User FOREIGN KEY([UserIdentityId]) REFERENCES [dbo].[User]([UserIdentityId])
)
