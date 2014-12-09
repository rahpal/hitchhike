-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateTravelPlan]
	-- Add the parameters for the stored procedure here
	@TravelId	INT 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	BEGIN TRAN 
	
	-- Update table
	UPDATE [dbo].[TravelPlan]
	SET [ExpirationDateTime] = GETUTCDATE()
	WHERE [PlanId] = @TravelId

	COMMIT TRAN

	IF(@@TRANCOUNT > 0)
	BEGIN 
		ROLLBACK TRAN;
		RETURN 1
	END

END

GO


