-- =============================================
-- Author:		Shail Patel
-- Create date: 2020/05/25
-- Description:	Get user name and email by column values
-- =============================================
CREATE PROCEDURE [dbo].[User_GetNameEmail]
-- Add the parameters for the stored procedure here
@column VARCHAR(50),
@value VARCHAR(50)
AS
BEGIN
	DECLARE @SQL NVARCHAR(4000)
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	-- Insert statements for procedure here

	SET @SQL = 'SELECT * FROM [SPM_Database].[dbo].[Users]  where ' + QUOTENAME(@column) + ' = ' + QUOTENAME(@value, '''');
	EXEC sp_executesql @SQL

END