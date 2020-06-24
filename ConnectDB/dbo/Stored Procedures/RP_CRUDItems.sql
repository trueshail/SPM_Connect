

-- =============================================
-- Author:		Shail Patel
-- Create date: 2020/06/10
-- Description:	CRUD for SPM Connect Addin ReleaseBase
-- =============================================
CREATE PROCEDURE [dbo].[RP_CRUDItems]
-- Add the parameters for the stored procedure here
@RelNo INT,
@AssyNo VARCHAR(50) = '',
@ItemId VARCHAR(50) = '',
@ItemNotes VARCHAR(250) = '',
@ItemQty INT = 1,
@IsRevised INT = 0,
@Path VARCHAR(500) = '',
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
				FROM [dbo].[RP_Items]
				WHERE RelNo = @RelNo AND AssyNo = @AssyNo AND ItemId = @ItemId)
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

		INSERT INTO [dbo].[RP_Items] (RelNo, AssyNo, ItemId, ItemQty, Path)
			SELECT
				@RelNo
			   ,@AssyNo
			   ,@ItemId
			   ,@ItemQty
			   ,@Path

		COMMIT
	END

	IF @StatementType = 'Select'
	BEGIN
		SET NOCOUNT ON
		SET XACT_ABORT ON

		BEGIN TRAN

		SELECT
			a.RelNo
		   ,a.ItemNumber
		   ,a.QuantityPerAssembly
		   ,a.Description
		   ,a.ItemFamily
		   ,a.Manufacturer
		   ,a.ManufacturerItemNumber
		   ,a.AssyNo
		   ,eb2.FamilyCode AS AssyFamily
		   ,eb2.Description AS AssyDescription
		   ,eb2.Manufacturer AS AssyManufacturer
		   ,eb2.ManufacturerItemNumber AS AssyManufacturerItemNumber
		   ,a.Path
		FROM (SELECT
				bm.RelNo
			   ,bm.ItemId AS ItemNumber
			   ,CONVERT(INT, bm.ItemQty) AS QuantityPerAssembly
			   ,eb.Description AS Description
			   ,eb.FamilyCode AS ItemFamily
			   ,eb.Manufacturer AS Manufacturer
			   ,eb.ManufacturerItemNumber AS ManufacturerItemNumber
			   ,bm.AssyNo AS AssyNo
			   ,bm.Path
			FROM [SPM_Database].[dbo].[RP_Items] bm
			INNER JOIN [SPM_Database].[dbo].[Inventory] eb
				ON bm.ItemId = eb.ItemNumber) a
		INNER JOIN [SPM_Database].[dbo].[Inventory] eb2
			ON a.AssyNo = eb2.ItemNumber
		WHERE a.RelNo = @RelNo

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
		   ,AssyNo
		   ,ItemId
		   ,ItemNotes
		   ,ItemQty
		   ,IsRevised
		   ,DateCreated
		   ,[Path]
		FROM [dbo].[RP_Items]

		COMMIT
	END

	IF @StatementType = 'Update'
	BEGIN
		SET NOCOUNT ON
		SET XACT_ABORT ON

		BEGIN TRAN

		UPDATE [dbo].[RP_Items]
		SET RelNo = @RelNo
		   ,AssyNo = @AssyNo
		   ,ItemId = @ItemId
		   ,ItemNotes = @ItemNotes
		   ,ItemQty = @ItemQty
		   ,IsRevised = @IsRevised
		   ,[Path] = @Path
		WHERE RelNo = @RelNo
		COMMIT
	END
	ELSE
	IF @StatementType = 'Delete'
	BEGIN
		SET NOCOUNT ON
		SET XACT_ABORT ON

		BEGIN TRAN

		DELETE FROM [dbo].[RP_Items]
		WHERE RelNo = @RelNo
		COMMIT
	END
END