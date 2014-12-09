CREATE PROCEDURE [dbo].[InsertTravelPlanDetails]
(
	@Source						NVARCHAR(50),
	@Destination				NVARCHAR(20), 
	@TravelDateTime				DATETIME,
	@VehicleType				NVARCHAR(10),
	@Capacity					INT,
	@TotalFare	INT,
	@UserIdentityId			INT
)	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	INSERT INTO dbo.[TravelPlan]
	(
		[Source],
		[Destination], 
		[TravelDateTime],
		[VehicleType],
		[Capacity],
		[TotalFare],
		[UserIdentityId]
	)
	SELECT 
		@Source,
		@Destination, 
		@TravelDateTime,
		@VehicleType,
		@Capacity,
		@TotalFare,
		@UserIdentityId		

END


GO