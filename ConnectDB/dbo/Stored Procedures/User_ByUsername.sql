-- =============================================
-- Author:		Shail
-- Create date: 2020/05/25
-- Description:	Get User by username
-- =============================================
CREATE PROCEDURE [dbo].[User_ByUsername] 
	-- Add the parameters for the stored procedure here
	@username nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM [SPM_Database].dbo.[Users] WHERE UserName =  @username; 
END