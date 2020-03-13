





CREATE View  [dbo].[TimeBudgetBI]

as

select Job, Des, Wo, Code_Ope, Des_Ope, Code_Id, sum(EmpHOursSpent) as EmpHoursSpent, CostCategoryId, EmpHours as HoursQuoted, Code from 
(SELECT
		wd.Job
		,j.Des
		,wd.Wo
		,wd.Code_Ope
		,wd.Des_Ope
		,wd.Code_Id
		,tt.EmployeeTimeSheetId
		,sum(tt.Temps) as EmpHoursSpent
		,CONCAT(emp.Prenom, ' ', emp.Nom) as EmpName
		,ccc.CostCategoryId
		,ib.EmpHours
		,tcc.Code
FROM	[SPM-Proto].[dbo].[Wodet] wd
INNER JOIN [SPM-Proto].[dbo].[Carte] tt ON tt.Wo = wd.Wo and tt.Code_Ope = wd.Code_Ope 
INNER JOIN [SPM-Proto].[dbo].[Emp] emp ON emp.No_Emp = tt.No_Emp
INNER JOIN [SPM-Proto].[dbo].[Car] j ON j.Produit  = wd.Item
INNER JOIN [SPM-Proto].[dbo].[Edb] i ON i.Item = wd.Item
INNER JOIN[SPM_Database].[dbo].[OperationCostCode] ccc ON ccc.Code  = wd.Code_Ope
INNER JOIN [SPM-Proto].[dbo]. tcItemBudget ib on ib.CostCategoryId = ccc.CostCategoryId and ib.ItemId = i.ItemID
INNER JOIN [SPM-Proto].[dbo]. tcCostCategory tcc on tcc.Id = ccc.CostCategoryId

group by wd.Job
		,j.Des
		,wd.Wo
		,wd.Code_Ope
		,wd.Des_Ope
		,wd.Code_Id
		,tt.EmployeeTimeSheetId
		,CONCAT(emp.Prenom, ' ', emp.Nom) 
		,ccc.CostCategoryId
		,ib.EmpHours
		,tcc.Code) a

	group by Job, Des, Wo, Code_Ope, Des_Ope, Code_Id, CostCategoryId, EmpHours, Code


