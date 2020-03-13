

/****** Script for SelectTopNRows command from SSMS  ******/
CREATE View [dbo].[CustomersShipTo]
as

SELECT [C_No]
      ,[Nom]
      ,[Adresse]
	  ,[CTR_Address2]
      ,[Ville]
      ,[Province]
      ,[Pays]
      ,[Codepostal]
      ,[Telephone]
      ,[Fax]
      ,[Contact]
      ,[Transport]
      ,[Devise]
      ,[Terri]
      ,[Taxes]      
      ,[Abrege]      
      ,[Groupe]      
      ,[Datecree]
      ,[Datemod]     
      ,[CTR_FullSearch]	 
  FROM [SPMDB].[dbo].[Cli]
 

