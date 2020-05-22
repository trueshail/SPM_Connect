
CREATE View [dbo].[PriceItemsFromPO]
as
  SELECT	Item,			
			PriceItem,
			Currency,  
			LastUpdate,
			PurchaseOrder
			FROM [SPMDB].[dbo].[vgPoeDetailCurrency]	
			WHERE IsCompleted = '1' AND QuantityOrdered  not like '-%'
			union 
SELECT		concat('A',[Item Number]) as Item,
			Price,
			null,
			null,
			concat('AAA - ',[Purchase Order Number])
			FROM Purchase_Order_Lineitems
			
