








CREATE View  [dbo].[EmpHoursByJob]

as

select Job, Des,  Code_Ope, Des_Ope,  sum(EmpHOursSpent) as EmpHoursSpent, CostCategoryId,  Code, Nom from
(SELECT
		wd.Job
		,j.Des
		,wd.Code_Ope
		,wd.Des_Ope
		,tt.EmployeeTimeSheetId
		,sum(tt.Temps) as EmpHoursSpent
		,CONCAT(emp.Prenom, ' ', emp.Nom) as EmpName
		,ccc.CostCategoryId
		,tcc.Code
		,emp.Nom
FROM	[SPMDB].[dbo].[Wodet] wd
INNER JOIN [SPMDB].[dbo].[Carte] tt ON tt.Wo = wd.Wo and tt.Code_Ope = wd.Code_Ope
INNER JOIN [SPMDB].[dbo].[Emp] emp ON emp.No_Emp = tt.No_Emp
INNER JOIN [SPMDB].[dbo].[Car] j ON j.Produit  = wd.Item
INNER JOIN [SPMDB].[dbo].[Edb] i ON i.Item = wd.Item
INNER JOIN[SPM_Database].[dbo].[OperationCostCode] ccc ON ccc.Code  = wd.Code_Ope
INNER JOIN [SPMDB].[dbo]. tcCostCategory tcc on tcc.Id = ccc.CostCategoryId
where LEN(wd.Job ) = 5 and ISNUMERIC(SUBSTRING(wd.Job, 0, 6)) = 1

group by wd.Job
		,j.Des
		,wd.Code_Ope
		,wd.Des_Ope
		,tt.EmployeeTimeSheetId
		,CONCAT(emp.Prenom, ' ', emp.Nom)
		,ccc.CostCategoryId
		,tcc.Code
		,emp.Nom) a

	group by Job, Des,  Code_Ope, Des_Ope,  CostCategoryId,  Code,Nom


