namespace SPMConnectAPI
{
    public class ReleaseItem
    {
        public int RelNo { get; set; }

        public string ItemNumber { get; set; }

        public int QuantityPerAssembly { get; set; }

        public string Description { get; set; }

        public string ItemFamily { get; set; }

        public string Manufacturer { get; set; }

        public string ManufacturerItemNumber { get; set; }

        public string AssyNo { get; set; }

        public string AssyFamily { get; set; }

        public string AssyDescription { get; set; }

        public string AssyManufacturer { get; set; }

        public string AssyManufacturerItemNumber { get; set; }

        public string Path { get; set; }

        public BOM ReleaseBomItem
        {
            get
            {
                return new BOM
                {
                    ItemNo = ItemNumber,
                    Path = Path,
                    Description = Description,
                    Qty = QuantityPerAssembly,
                    OEM = Manufacturer,
                    Family = ItemFamily,
                    OEMItemNo = ManufacturerItemNumber,
                    Ischecked = false,
                };
            }
        }
    }
}