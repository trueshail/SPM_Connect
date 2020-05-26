
CREATE View  [dbo].[JobBudgetBI]

as

SELECT
		j.Job,
		j.Des as JobDescription,
		i.Item as ItemNo,
		i.Des as ItemDescription,
		ib.CalculatedAmount,
		ib.RevisedAmount,
		ib.UsedAmount,
		ib.EmpHours,
		cc.Code as Catergory,
		cc.Type as CostCatergoryType 
 
FROM	[SPM-Proto].[dbo].[tcItemBudget] ib

INNER JOIN [SPM-Proto].[dbo].[Edb] i ON i.ItemID = ib.ItemId
INNER JOIN [SPM-Proto].[dbo].[tcCostCategory] cc ON cc.Id = ib.CostCategoryId
INNER JOIN [SPM-Proto].[dbo].[Car] j ON j.Produit  = i.Item

