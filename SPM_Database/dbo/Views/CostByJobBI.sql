

CREATE view [dbo].[CostByJobBI]
as
select
	D.PurchaseOrder,
	H.Buyer,
	H.BoughtFromVendorName,
	H.DatePurchase,
	D.Item,D.ItemDescription1,
	D.ItemDescription2,
	D.ItemDescription3,
	D.ItemDescription4,
	--D.QuantityOrdered as oldqtyordered,
	isnull(M.QtyOrigin,D.QuantityOrdered) as QuantityOrdered,
	ISNULL((M.QtyOrigin-M.QtyAllocated),D.QuantityReceived) as QuantityReceived,
	--D.QuantityReceived as oldqtyreceived,
	D.QuantityRejected,
	isnull(M.QtyAllocated,D.OpenQuantity) as OpenQuantity,
	--D.OpenQuantity as oldopenqty,
	D.PriceItem ,
	D.PurchaseUnit,
	isnull(ISNULL(M.job,S.JobCode),'Stock') as Job,
	isnull(isnull(M.ParentWorkOrder,P.WorkOrder),0) as WorkOrder,
	D.DateDelivery,
	ed.Famille,
	D.PriceItem * D.QuantityOrdered as total
from [SPMDB].[dbo].vgPoeDetail D 
left join [SPMDB].[dbo].vgPoeHeader H on D.PurchaseOrderHeaderLink=H.PurchaseOrder
left join  [SPMDB].[dbo].vgPoeAutomaticAllocationUponReceipt M on D.Link=M.PurchaseOrderDetailLink
left join [SPMDB].[dbo].vgPoeSubcontractingAssignment S on D.Link=S.PurchaseOrderDetailLink
left join [SPMDB].[dbo].vgJcoProductionTasks P on S.ProductionTasksLink=P.Link
left join [SPMDB].[dbo].Edb ed on D.Item=ed.Item
