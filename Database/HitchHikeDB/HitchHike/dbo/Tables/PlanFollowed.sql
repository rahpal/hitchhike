CREATE TABLE [dbo].[PlanFollowed](
	[FollowingId] [int] IDENTITY(1,1) NOT NULL,
	[TravelPlanId] [int] NULL,
	[FollowedById] [int] NULL,
	[CreatedById] [int] NULL,
	[LastUpdatedDateTime] [datetime] NULL
);