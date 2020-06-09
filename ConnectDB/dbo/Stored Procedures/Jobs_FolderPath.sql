-- =============================================
-- Author:		Shail Patel
-- Create date: 2020/06/01
-- Description:	CRUD for SPM Jobs Folder Path
-- =============================================
CREATE PROCEDURE [dbo].[Jobs_FolderPath]
-- Add the parameters for the stored procedure here
@JobNo VARCHAR(50),
@BOMNo VARCHAR(50),
@Path NVARCHAR(500),
@StatementType NVARCHAR(20) = ''
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON
	IF (@StatementType = 'Insert')
	BEGIN
		IF EXISTS (SELECT
					*
				FROM dbo.SPMJobsPath
				WHERE JobNo = @JobNo)
		BEGIN
			--do what you need if exists
			SET @StatementType = 'Update'
		END
	END
	IF @StatementType = 'Insert'
	BEGIN
		SET NOCOUNT ON
		SET XACT_ABORT ON

		BEGIN TRAN

		INSERT INTO dbo.SPMJobsPath (JobNo, BOMNo, Path)
			SELECT
				@JobNo
			   ,@BOMNo
			   ,@Path

		COMMIT
	END

	IF @StatementType = 'Select'
	BEGIN
		SET NOCOUNT ON
		SET XACT_ABORT ON

		BEGIN TRAN

		SELECT
			Path
		FROM dbo.SPMJobsPath
		WHERE JobNo = @JobNo

		COMMIT
	END

	IF @StatementType = 'Update'
	BEGIN
		SET NOCOUNT ON
		SET XACT_ABORT ON

		BEGIN TRAN

		UPDATE dbo.SPMJobsPath
		SET BOMNo = @BOMNo
		   ,Path = @Path
		WHERE JobNo = @JobNo
		COMMIT
	END
	ELSE
	IF @StatementType = 'Delete'
	BEGIN
		SET NOCOUNT ON
		SET XACT_ABORT ON

		BEGIN TRAN

		DELETE FROM dbo.SPMJobsPath
		WHERE JobNo = @JobNo

		COMMIT
	END
END