-- Batch submitted through debugger: SQLQuery83.sql|7|0|C:\Users\spatel.SPM\AppData\Local\Temp\~vsBA97.sql

-- =============================================
-- Author:		Shail Patel
-- Create date: 2020/06/10
-- Description:	CRUD for SPM Connect Addin ReleaseBase
-- =============================================
CREATE PROCEDURE [dbo].[RP_CRUDBase]
-- Add the parameters for the stored procedure here
@RelNo INT = NULL,
@Job INT = NULL,
@SubAssy VARCHAR(25) = NULL,
@PckgQty INT = 0,
@IsSubmitted INT = 0,
@SubmittedTo INT = 0,
@SubmittedOn VARCHAR(50) = NULL,
@IsChecked INT = 0,
@CheckedBy VARCHAR(50) = NULL,
@CheckedOn VARCHAR(50) = NULL,
@ApprovalTo INT = 0,
@IsApproved INT = 0,
@ApprovedBy VARCHAR(50) = NULL,
@ApprovedOn VARCHAR(50) = NULL,
@IsReleased INT = 0,
@ReleasedBy VARCHAR(50) = NULL,
@ReleasedOn VARCHAR(50) = NULL,
@DateCreated VARCHAR(50) = NULL,
@CreatedById INT = NULL,
@LastSaved VARCHAR(50) = NULL,
@LastSavedById INT = NULL,
@Priority VARCHAR(50) = NULL,
@IsActive INT = 1,
@ConnectRelNo VARCHAR(150) = NULL,
@WorkOrder VARCHAR(150) = NULL,
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
				FROM [dbo].[RP_Base]
				WHERE RelNo = @RelNo)
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

		INSERT INTO dbo.RP_Base (RelNo, Job, SubAssy,
		CreatedById, LastSaved, LastSavedById)
			SELECT
				@RelNo
			   ,@Job
			   ,@SubAssy
			   ,@CreatedById
			   ,@LastSaved
			   ,@LastSavedById

		COMMIT
	END

	IF @StatementType = 'Select'
	BEGIN
		SET NOCOUNT ON
		SET XACT_ABORT ON

		BEGIN TRAN

		SELECT
			rb.Id
		   ,rb.RelNo
		   ,rb.Job
		   ,vj.Description1 AS JobDes
		   ,rb.SubAssy
		   ,i.Description AS SubAssyDes
		   ,rb.PckgQty
		   ,rb.IsSubmitted
		   ,rb.SubmittedTo
		   ,rb.SubmittedOn
		   ,rb.IsChecked
		   ,rb.CheckedBy
		   ,rb.CheckedOn
		   ,rb.ApprovalTo
		   ,rb.IsApproved
		   ,rb.ApprovedBy
		   ,rb.ApprovedOn
		   ,rb.IsReleased
		   ,rb.ReleasedBy
		   ,rb.ReleasedOn
		   ,rb.DateCreated
		   ,rb.CreatedById
		   ,u.Name AS CreatedBy
		   ,rb.LastSaved
		   ,rb.LastSavedById
		   ,ul.Name AS LastSavedBy
		   ,rb.Priority
		   ,rb.IsActive
		   ,rb.ConnectRelNo
		   ,rb.WorkOrder
		FROM RP_Base rb
		LEFT OUTER JOIN [SPMDB].[dbo].[vgJcoJob] vj
			ON CONVERT(VARCHAR, rb.Job) = vj.Job
		LEFT OUTER JOIN [dbo].[Inventory] i
			ON i.ItemNumber = rb.SubAssy
		LEFT OUTER JOIN [dbo].Users u
			ON u.Id = rb.CreatedById
		LEFT OUTER JOIN [dbo].Users ul
			ON ul.Id = rb.LastSavedById
		WHERE rb.RelNo = @RelNo

		COMMIT
	END
	IF @StatementType = 'SelectAll'
	BEGIN
		SET NOCOUNT ON
		SET XACT_ABORT ON

		BEGIN TRAN

		SELECT
			rb.Id
		   ,rb.RelNo
		   ,rb.Job
		   ,vj.Description1 AS JobDes
		   ,rb.SubAssy
		   ,i.Description AS SubAssyDes
		   ,rb.PckgQty
		   ,rb.IsSubmitted
		   ,rb.SubmittedTo
		   ,rb.SubmittedOn
		   ,rb.IsChecked
		   ,rb.CheckedBy
		   ,rb.CheckedOn
		   ,rb.ApprovalTo
		   ,rb.IsApproved
		   ,rb.ApprovedBy
		   ,rb.ApprovedOn
		   ,rb.IsReleased
		   ,rb.ReleasedBy
		   ,rb.ReleasedOn
		   ,rb.DateCreated
		   ,rb.CreatedById
		   ,u.Name AS CreatedBy
		   ,rb.LastSaved
		   ,rb.LastSavedById
		   ,ul.Name AS LastSavedBy
		   ,rb.Priority
		   ,rb.IsActive
		   ,rb.ConnectRelNo
		   ,rb.WorkOrder
		FROM RP_Base rb
		LEFT OUTER JOIN [SPMDB].[dbo].[vgJcoJob] vj
			ON CONVERT(VARCHAR, rb.Job) = vj.Job
		LEFT OUTER JOIN [dbo].[Inventory] i
			ON i.ItemNumber = rb.SubAssy
		LEFT OUTER JOIN [dbo].Users u
			ON u.Id = rb.CreatedById
		LEFT OUTER JOIN [dbo].Users ul
			ON ul.Id = rb.LastSavedById

		COMMIT
	END

	IF @StatementType = 'Update'
	BEGIN
		SET NOCOUNT ON
		SET XACT_ABORT ON

		BEGIN TRAN

		UPDATE [dbo].[RP_Base]
		SET Job = @Job
		   ,SubAssy = @SubAssy
		   ,PckgQty = @PckgQty
		   ,IsSubmitted = @IsSubmitted
		   ,SubmittedTo = @SubmittedTo
		   ,SubmittedOn = @SubmittedOn
		   ,IsChecked = @IsChecked
		   ,CheckedBy = @CheckedBy
		   ,CheckedOn = @CheckedOn
		   ,ApprovalTo = @ApprovalTo
		   ,IsApproved = @IsApproved
		   ,ApprovedBy = @ApprovedBy
		   ,ApprovedOn = @ApprovedOn
		   ,IsReleased = @IsReleased
		   ,ReleasedBy = @ReleasedBy
		   ,ReleasedOn = @ReleasedOn
		   ,LastSaved = @LastSaved
		   ,LastSavedById = @LastSavedById
		   ,[Priority] = @Priority
		   ,IsActive = @IsActive
		   ,ConnectRelNo = @ConnectRelNo
		   ,WorkOrder = @WorkOrder
		WHERE RelNo = @RelNo
		COMMIT
	END
	ELSE
	IF @StatementType = 'Delete'
	BEGIN
		SET NOCOUNT ON
		SET XACT_ABORT ON

		BEGIN TRAN

		DELETE FROM [dbo].[RP_Base]
		WHERE RelNo = @RelNo
		COMMIT
	END
END