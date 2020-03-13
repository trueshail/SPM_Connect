

CREATE view  [dbo].[SpareBOM]
as
SELECT       [SPMDB].[dbo].[Bom].Item AS ItemNumber,  CONVERT(INT, [SPMDB].[dbo].[Bom].Qte) AS QuantityPerAssembly,  [SPMDB].[dbo].[Edb].Des AS Description,
 [SPMDB].[dbo].[Edb].Famille, [SPMDB].[dbo].[Edb].Des4 AS Manufacturer,  [SPMDB].[dbo].[Edb].Des2 as ManufacturerItemNumber,
 [SPMDB].[dbo].[Edb].Spec1 as Spare,
                         [SPMDB].[dbo].[Bom].Produit as Product
FROM            [SPMDB].[dbo].[Bom] INNER JOIN
                        [SPMDB].[dbo].[Edb] ON [SPMDB].[dbo].[Bom].Item = [SPMDB].[dbo].[Edb].Item




