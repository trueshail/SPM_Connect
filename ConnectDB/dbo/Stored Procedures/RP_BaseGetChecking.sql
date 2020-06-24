
-- =============================================
-- Author:		Shail Patel
-- Create date: 2020/06/15
-- Description:	Get the customized list of releases available for checking to specific user
-- =============================================
CREATE PROCEDURE [dbo].[RP_BaseGetChecking]
-- Add the parameters for the stored procedure here
@CreatorId INT,
@SubAssy VARCHAR(25)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SET XACT_ABORT ON

	BEGIN TRAN

	SELECT
		rb.id
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
	FROM RP_Base rb
	LEFT OUTER JOIN [SPMDB].[dbo].[vgJcoJob] vj
		ON CONVERT(VARCHAR, rb.Job) = vj.Job
	LEFT OUTER JOIN [dbo].[Inventory] i
		ON i.ItemNumber = rb.SubAssy
	LEFT OUTER JOIN [dbo].Users u
		ON u.id = rb.CreatedById
	LEFT OUTER JOIN [dbo].Users ul
		ON ul.id = rb.LastSavedById
	WHERE rb.IsActive = '1'
	AND rb.SubmittedTo = @CreatorId
	AND rb.SubAssy = @SubAssy
	AND rb.IsChecked = '0'
	AND rb.IsSubmitted = '1'
	ORDER BY rb.RelNo DESC

	COMMIT
END