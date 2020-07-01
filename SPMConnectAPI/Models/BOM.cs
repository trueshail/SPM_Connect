namespace SPMConnectAPI
{
    public class BOM
    {
        public int Id { get; set; }
        public string ItemNo { get; set; }
        public int Qty { get; set; }
        public string Description { get; set; }
        public string Family { get; set; }
        public string OEM { get; set; }
        public string OEMItemNo { get; set; }
        public string Path { get; set; }
        public bool Ischecked { get; set; }
    }
}