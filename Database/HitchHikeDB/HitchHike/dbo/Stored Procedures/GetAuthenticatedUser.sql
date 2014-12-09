-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetAuthenticatedUser]
(
	@Username NVARCHAR(50),
	@Password NVARCHAR(20)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT 
		UserIdentityId	AS UserId,
		Username,
		Firstname,
		Lastname,
		UG.GId			AS Gender,
		EmailAddress
	FROM 
		dbo.[User] U
		INNER JOIN dbo.[UserGender] UG
		ON U.Gender = UG.GCode
	WHERE Username = @Username AND [Password] = @Password 
END

GO