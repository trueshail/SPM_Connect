namespace SPMConnectAPI
{
    public static class ConnectConstants
    {
        public enum Department
        {
            Eng,
            Controls,
            Production,
            Accounting,
            Purchasing,
            Crib
        }

        public static UserInfo ConnectUser { get; set; } = new UserInfo();

        public struct NameEmail
        {
            public string email;
            public string name;
        }

        public static class CheckInModules
        {
            public static string ECR = "ECR";
            public static string ShipInv = "ShipInv";
            public static string WO = "Work Order";
        }

        public static class UserFields
        {
            public static string ActiveBlockNumber = "ActiveBlockNumber";
            public static string Admin = "Admin";
            public static string ApproveDrawing = "ApproveDrawing";
            public static string CheckDrawing = " CheckDrawing";
            public static string CribShort = "CribShort";
            public static string CribShortEmp_Id = "CribShortEmp_Id";
            public static string Department = "Department";
            public static string Developer = "Developer";
            public static string ECR = "ECR";
            public static string ECRApproval = " ECRApproval";
            public static string ECRApproval2 = "ECRApproval2";
            public static string ECRHandler = "ECRHandler";
            public static string ECRSup = "ECRSup";
            public static string Email = "Email";
            public static string id = "id";
            public static string ItemDependencies = "ItemDependencies";
            public static string Management = "Management";
            public static string Name = "Name";
            public static string PriceRight = "PriceRight";
            public static string PurchaseReq = "PurchaseReq";
            public static string PurchaseReqApproval = "PurchaseReqApproval";
            public static string PurchaseReqApproval2 = "PurchaseReqApproval2";
            public static string PurchaseReqBuyer = "PurchaseReqBuyer";
            public static string Quote = " Quote";
            public static string ReadWhatsNew = "ReadWhatsNew";
            public static string ReleasePackage = "ReleasePackage";
            public static string SharesFolder = "SharesFolder";
            public static string Shipping = "Shipping";
            public static string ShippingManager = "ShippingManager";
            public static string ShipSup = "ShipSup";
            public static string ShipSupervisor = " ShipSupervisor";
            public static string Supervisor = "Supervisor";
            public static string UserName = "UserName";
            public static string WORelease = "WORelease";
            public static string WOScan = "WOScan";
        }
    }
}