









CREATE VIEW [dbo].[ItemsToSelect]
AS
SELECT
	CONCAT(ItemNumber, ' ', Description, ' ', FamilyCode, ' ', Manufacturer, ' ', ManufacturerItemNumber) AS Items
FROM [dbo].Inventory

