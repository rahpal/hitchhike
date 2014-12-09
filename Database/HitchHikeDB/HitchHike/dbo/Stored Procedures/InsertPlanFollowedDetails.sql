-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[InsertPlanFollowedDetails]
(
	@PlanId		INT,
	@CreatorId	INT,
	@FollowerId	INT			
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	BEGIN TRAN 
	
	INSERT INTO [dbo].[PlanFollowed]
	(
		[TravelPlanId]
		, [CreatedById]
		, [FollowedById]
	)
	SELECT 
		@PlanId
		, @CreatorId
		, @FollowerId

	COMMIT TRAN

	IF(@@TRANCOUNT > 0)
	BEGIN 
		ROLLBACK TRAN;
		RETURN 1
	END
END