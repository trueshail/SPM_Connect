Create procedure UpdateOrderId
@itemnumber int

as	

with cte as
(
  select *
    , new_row_id=row_number() over (partition by ReqNumber order by ReqNumber)
  from [dbo].[PurchaseReq]
  where ReqNumber = @itemnumber
)
update cte
set OrderId = new_row_id