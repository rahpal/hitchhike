CREATE PROCEDURE [dbo].[InsertUserDetails]
(
	@Username				NVARCHAR(50),
	@Firstname				NVARCHAR(20), 
	@Lastname				NVARCHAR(20),
	@Gender					CHAR(1),
	@Password				NVARCHAR(30),
	@PrimaryEmailAddress	NVARCHAR(50),
	@ContactNumber			NVARCHAR(15)
)	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	INSERT INTO dbo.[User]
	(
		Username,
		Firstname, 
		Lastname,
		Gender,
		[Password],
		EmailAddress,
		ContactNumber
	)
	SELECT 
		@Username,
		@Firstname, 
		@Lastname,
		@Gender,
		@Password,
		@PrimaryEmailAddress,
		@ContactNumber			

END

GO