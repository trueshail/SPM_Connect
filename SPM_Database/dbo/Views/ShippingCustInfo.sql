








CREATE View [dbo].[ShippingCustInfo]
as
	Select	a.InvoiceNo,
			c.Nom as SoldToName, 
			c.Adresse as SoldToAddress, 
			c.CTR_Address2 as SoldToAddress2, 
			c.Ville as SoldToCity, 
			c.Province as SoldToProvince, 
			c.Pays as SoldToCountry, 
			c.Codepostal as SoldToPostalCode, 
			c.Telephone as SoldToTelephone, 
			c.Fax as SoldToFax, 
			cd.Nom as ShipToName, 
			cd.Adresse as ShipToAddress, 
			cd.CTR_Address2 as ShipToAddress2, 
			cd.Ville as ShipToCity, 
			cd.Province as ShipToProvince, 
			cd.Pays as ShipToCountry, 
			cd.Codepostal as ShipToPostalCode, 
			cd.Telephone as ShipToTelephone, 
			cd.Fax as ShipToFax
			From [dbo].[ShippingBase] a	
			left join [dbo].[CustomersShipTo]	c	on c.[C_No] = a.[SoldTo] 
			left join [dbo].[CustomersShipTo]	cd	on cd.[C_No] = a.[ShipTo]
			where a.[Vendor_Cust] = 1
union all
	Select	a2.InvoiceNo,
			c2.Name as SoldToName, 
			c2.Address1 as SoldToAddress, 
			c2.Address2 as SoldToAddress2, 
			c2.City as SoldToCity, 
			c2.Province as SoldToProvince, 
			c2.Country as SoldToCountry, 
			c2.ZipCode as SoldToPostalCode, 
			c2.Phone as SoldToTelephone, 
			c2.Fax as SoldToFax, 
			cd2.Name as ShipToName, 
			cd2.Address1 as ShipToAddress, 
			cd2.Address2 as ShipToAddress2, 
			cd2.City as ShipToCity, 
			cd2.Province as ShipToProvince, 
			cd2.Country as ShipToCountry, 
			cd2.ZipCode as ShipToPostalCode, 
			cd2.Phone as ShipToTelephone, 
			cd2.Fax as ShipToFax
			From [dbo].[ShippingBase] a2 
			left join [dbo].[VendorsShipTo]	c2	on c2.[Code] = a2.[SoldTo] 
			left join [dbo].[VendorsShipTo]	cd2	on cd2.[Code] = a2.[ShipTo]
			where a2.[Vendor_Cust] = 0
