-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CheckUsernameAvailability]
	(
		@Username		NVARCHAR(50),
		@RecordCount	INT OUTPUT
	)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SET @RecordCount = 0;

    SELECT @RecordCount = COUNT(*)
	FROM dbo.[User]
	WHERE Username = @Username

END

GO