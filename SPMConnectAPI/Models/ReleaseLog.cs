using System.Collections.Generic;

namespace SPMConnectAPI
{
    public class ReleaseLog
    {
        public int Id { get; set; }

        public int RelNo { get; set; }

        public int Job { get; set; }

        public string SubAssy { get; set; }

        public int PckgQty { get; set; }

        public bool IsSubmitted { get; set; }

        public int SubmittedTo { get; set; }

        public string SubmittedOn { get; set; }

        public bool IsChecked { get; set; }

        public string CheckedBy { get; set; }

        public string CheckedOn { get; set; }

        public int ApprovalTo { get; set; }

        public bool IsApproved { get; set; }

        public string ApprovedBy { get; set; }

        public string ApprovedOn { get; set; }

        public bool IsReleased { get; set; }

        public string ReleasedBy { get; set; }

        public string ReleasedOn { get; set; }

        public string DateCreated { get; set; }

        public int CreatedById { get; set; }

        public string CreatedBy { get; set; }

        public string LastSaved { get; set; }

        public int LastSavedById { get; set; }

        public string LastSavedBy { get; set; }

        public string Priority { get; set; }

        public bool IsActive { get; set; }

        public string ConnectRelNo { get; set; }

        public string WorkOrder { get; set; }

        public string JobDes { get; set; }

        public string SubAssyDes { get; set; }

        public List<ReleaseItem> ReleaseItems { get; set; } = new List<ReleaseItem>();

        public List<ReleaseComment> ReleaseComments { get; set; } = new List<ReleaseComment>();
    }
}