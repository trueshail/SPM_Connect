
/****** Script for SelectTopNRows command from SSMS  ******/
CREATE View [dbo].[EFTVendors]
as
SELECT [F_No]
      ,[Nom]
      ,[Adresse]
      ,[Ville]
      ,[Province]
      ,[Pays]
      ,[Codepostal]
      ,[Telephone]
      ,[Fax]
      ,[Contact]
      ,[Termeach]
      ,[Devise]     
      ,[Actif]
      ,[Datecree]
      ,[Datemod]
      ,[VendorID]
  FROM [SPM-Proto].[dbo].[Fou]
  where Actif <> 'N'
