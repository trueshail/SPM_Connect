


CREATE VIEW [dbo].[SPMConnectBOMbACKUP]
AS
SELECT        dbo.SpareBOM.ItemNumber, dbo.SpareBOM.QuantityPerAssembly, dbo.SpareBOM.Description, dbo.SpareBOM.Famille as ItemFamily, dbo.SpareBOM.Manufacturer, dbo.SpareBOM.ManufacturerItemNumber, dbo.SpareBOM.Spare, 
                         dbo.SpareBOM.Product AS AssyNo, SPMDB.dbo.Edb.Famille AS AssyFamily, SPMDB.dbo.Edb.Des AS AssyDescription, SPMDB.dbo.Edb.Des4 as AssyManufacturer, SPMDB.dbo.Edb.Des2 as AssyManufacturerItemNumber, SPMDB.dbo.Edb.Spec1 as AssySpare
FROM            dbo.SpareBOM INNER JOIN
                         SPMDB.dbo.Edb ON dbo.SpareBOM.Product = SPMDB.dbo.Edb.Item
						 
