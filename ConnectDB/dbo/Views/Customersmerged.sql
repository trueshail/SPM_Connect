

/****** Script for SelectTopNRows command from SSMS  ******/
CREATE View [dbo].[Customersmerged]
as
SELECT DISTINCT Customers from
(SELECT *
  FROM [dbo].[CustomersAAA]
  UNION ALL
  select Name from [dbo].[Customers]
  UNION ALL
  SELECT SPMDB.dbo.Cli.Nom AS Customers FROM [SPMDB].[dbo].[Cli])a
