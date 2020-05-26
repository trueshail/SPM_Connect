













CREATE View [dbo].[ShippingBaseWithNames]
as
	Select	a.InvoiceNo,
			a.DateCreated,
			a.CreatedBy,
			a.DateLastSaved,
			a.LastSavedBy,
			a.JobNumber,
			a.SalesPerson,
			a.Requistioner,
			a.Carrier,
			a.Collect_Prepaid,
			a.FobPoint,
			a.Terms,
			a.Currency,
			a.Total,
			a.Vendor_Cust,
			a.SoldTo,
			a.ShipTo,
			a.Notes,
			a.CarrierCode,
			c.Nom as SoldToName, 
			cd.Nom as ShipToName,
			concat(a.InvoiceNo,' ',c.Nom,' ',cd.Nom,' ',a.JobNumber) as FullSearch,
			a.IsSubmitted,
			a.SubmittedTo,
			a.IsApproved,
			a.IsShipped
			From [dbo].[ShippingBase] a	
			left join [dbo].[CustomersShipTo]	c	on c.[C_No] = a.[SoldTo] 
			left join [dbo].[CustomersShipTo]	cd	on cd.[C_No] = a.[ShipTo]
			where a.[Vendor_Cust] = 1
union all
	Select	a2.InvoiceNo,
			a2.DateCreated,
			a2.CreatedBy,
			a2.DateLastSaved,
			a2.LastSavedBy,
			a2.JobNumber,
			a2.SalesPerson,
			a2.Requistioner,
			a2.Carrier,
			a2.Collect_Prepaid,
			a2.FobPoint,
			a2.Terms,
			a2.Currency,
			a2.Total,
			a2.Vendor_Cust,
			a2.SoldTo,
			a2.ShipTo,
			a2.Notes,
			a2.CarrierCode,
			c2.Name as SoldToName, 
			cd2.Name as ShipToName,
			concat(a2.InvoiceNo,' ',c2.Name,' ',cd2.Name,' ',a2.JobNumber) as FullSearch,
			a2.IsSubmitted,
			a2.SubmittedTo,
			a2.IsApproved,
			a2.IsShipped
			From [dbo].[ShippingBase] a2 
			left join [dbo].[VendorsShipTo]	c2	on c2.[Code] = a2.[SoldTo] 
			left join [dbo].[VendorsShipTo]	cd2	on cd2.[Code] = a2.[ShipTo]
			where a2.[Vendor_Cust] = 0
