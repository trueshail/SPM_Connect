


-- =============================================
-- Author:		Shail Patel
-- Create date: 2020/06/10
-- Description:	CRUD for SPM Connect Addin Release Remarks
-- =============================================
CREATE PROCEDURE [dbo].[RP_CRUDRemark]
-- Add the parameters for the stored procedure here
@RelNo INT,
@CommentBy VARCHAR(50) = '',
@Comment VARCHAR(MAX) = '',
@StatementType NVARCHAR(20) = ''
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON
	IF @StatementType = 'Insert'
	BEGIN
		SET NOCOUNT ON
		SET XACT_ABORT ON

		BEGIN TRAN

		INSERT INTO dbo.RP_Remarks (RelNo, CommentBy, Comment)
			SELECT
				@RelNo
			   ,@CommentBy
			   ,@Comment

		COMMIT
	END

	IF @StatementType = 'Select'
	BEGIN
		SET NOCOUNT ON
		SET XACT_ABORT ON

		BEGIN TRAN

		SELECT
			Id
		   ,RelNo
		   ,CommentBy
		   ,Comment
		   ,DateCreated
		FROM [dbo].[RP_Remarks]
		WHERE RelNo = @RelNo

		COMMIT
	END
	IF @StatementType = 'SelectAll'
	BEGIN
		SET NOCOUNT ON
		SET XACT_ABORT ON

		BEGIN TRAN

		SELECT
			Id
		   ,RelNo
		   ,CommentBy
		   ,Comment
		   ,DateCreated
		FROM [dbo].[RP_Remarks]

		COMMIT
	END

	IF @StatementType = 'Update'
	BEGIN
		SET NOCOUNT ON
		SET XACT_ABORT ON

		BEGIN TRAN

		UPDATE [dbo].[RP_Remarks]
		SET CommentBy = @CommentBy
		   ,Comment = @Comment
		WHERE RelNo = @RelNo
		COMMIT
	END
	ELSE
	IF @StatementType = 'Delete'
	BEGIN
		SET NOCOUNT ON
		SET XACT_ABORT ON

		BEGIN TRAN

		DELETE FROM [dbo].[RP_Remarks]
		WHERE RelNo = @RelNo
		COMMIT
	END
END