-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetTravelPlanByUser] 
	@UserIdentityId		INT,
	@RecordCount		INT OUTPUT 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SET @RecordCount = 0;

	SELECT 
		[PlanId]
		, [Source]
		, [Destination]
		, [TravelDateTime]
		, [VehicleType]
		, [Capacity]
		, [TotalFare]
	FROM 
		[dbo].[TravelPlan]
    WHERE [UserIdentityId] = @UserIdentityId
	AND [ExpirationDateTime] IS NULL
	ORDER BY [LastUpdatedDateTime] DESC, [TravelDateTime] ASC

	SET @RecordCount = @@ROWCOUNT;

END

GO