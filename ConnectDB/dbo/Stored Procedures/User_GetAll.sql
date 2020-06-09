
-- =============================================
-- Author:		Shail
-- Create date: 2020/05/25
-- Description:	Get All Users
-- =============================================
CREATE PROCEDURE [dbo].[User_GetAll]
-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	-- Insert statements for procedure here
	SELECT
		*
	FROM Users u
	ORDER BY u.Name;
END