namespace SPMConnectAPI
{
    public class UserInfo : ConnectAPI
    {
        public int Emp_Id { get; set; }
        public string UserName { get; set; }
        public Department Dept { get; set; }
        public string Name { get; set; }
        public string ActiveBlockNumber { get; set; }
        public bool Admin { get; set; }
        public bool Developer { get; set; }
        public bool Management { get; set; }
        public bool Quote { get; set; }
        public bool PurchaseReq { get; set; }
        public bool PurchaseReqApproval { get; set; }
        public bool PurchaseReqApproval2 { get; set; }
        public bool PurchaseReqBuyer { get; set; }
        public bool PriceRight { get; set; }
        public bool Shipping { get; set; }
        public bool WOScan { get; set; }
        public bool CribCheckout { get; set; }
        public bool CribShort { get; set; }
        public bool ECR { get; set; }
        public bool ECRApproval { get; set; }
        public bool ECRApproval2 { get; set; }
        public bool ECRHandler { get; set; }
        public int ECRSup { get; set; }
        public bool ItemDependencies { get; set; }
        public bool WORelease { get; set; }
        public bool ShipSupervisor { get; set; }
        public int ShipSup { get; set; }
        public bool ShippingManager { get; set; }
        public bool CheckDrawing { get; set; }
        public bool ApproveDrawing { get; set; }
        public bool ReleasePackage { get; set; }
        public int Supervisor { get; set; }
        public string Email { get; set; }
        public string SharesFolder { get; set; }
        public bool ReadWhatsNew { get; set; }
        public int ConnectId { get; set; }
    }



}