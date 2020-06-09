

-- =============================================
-- Author:		Shail
-- Create date: 2020/05/25
-- Description:	Get All FamilyCodes
-- =============================================
CREATE PROCEDURE [dbo].[FamilyCodes_GetAll]
-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	-- Insert statements for procedure here
	SELECT
		*
	FROM FamilyCodes fc 
	ORDER BY fc.FamilyCodes;
END