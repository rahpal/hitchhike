-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetAllSearchPlan]
	(
		@From				NVARCHAR(30),
		@To					NVARCHAR(30),
		@UserIdentityId		INT
	)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT 
		[PlanId]
		, [Source]
		, [Destination]
		, [TravelDateTime]
		, [VehicleType]
		, [TotalFare]
		, [Capacity]
		, [Availability]
		, U.[UserIdentityId] AS [UserId]
		, U.[Username]
		, U.[Firstname]
		, U.[Lastname]
		, U.[Gender]
		, U.[EmailAddress]
		, U.[ContactNumber]
		, PF.[FollowingId]
	FROM [dbo].[TravelPlan] TP
	INNER JOIN [dbo].[User] U
	ON TP.[UserIdentityId] = U.[UserIdentityId]
	LEFT JOIN [dbo].[PlanFollowed] PF
	ON TP.PlanId = PF.TravelPlanId 
	AND TP.UserIdentityId = PF.[CreatedById] AND PF.[FollowedById] = @UserIdentityId
	WHERE [Source] LIKE '%'+ @From +'%'
	AND [ExpirationDateTime] IS NULL 
    
END

GO