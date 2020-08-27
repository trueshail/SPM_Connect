using SearchDataSPM.Engineering;
using SearchDataSPM.Helper;
using SPMConnectAPI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using wpfPreviewFlowControl;
using static SPMConnectAPI.ConnectHelper;

namespace SearchDataSPM.WorkOrder.ReleaseManagement
{
    public partial class NewRelease : Form
    {
        public enum IssuePriority { None = 0, Normal, Urgent };

        private readonly SPMConnectAPI.WorkOrder releaseModule = new SPMConnectAPI.WorkOrder();
        private log4net.ILog log;
        private bool rootnodedone;
        private readonly TreeNode root = new TreeNode();
        private SPMConnectAPI.ReleaseLog rlog = new SPMConnectAPI.ReleaseLog();
        private readonly int releaseNo;
        private readonly List<ComboBoxItem> PriorityList = new List<ComboBoxItem>();
        private TreeNode publicnode = new TreeNode();

        public NewRelease(int releaseNo)
        {
            InitializeComponent();
            this.releaseNo = releaseNo;
        }

        private void NewRelease_Load(object sender, EventArgs e)
        {
            // Suspend the layout logic for the form, while the application is initializing
            this.SuspendLayout();
            WinTopMost.SetWindowPos(this.Handle, WinTopMost.HWND_TOPMOST, 0, 0, 0, 0, WinTopMost.TOPMOST_FLAGS);
            foreach (IssuePriority p in Enum.GetValues(typeof(IssuePriority)))
            {
                if (p != IssuePriority.None)
                {
                    PriorityList.Add(new ComboBoxItem(Convert.ToInt32(p), p.ToString()));
                }
            }
            this.Text = "Engineering Release - " + releaseNo;
            log4net.Config.XmlConfigurator.Configure();
            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log.Info("Opened Engineering Release Detail " + releaseNo + " ");
            // Initialize and populate the Priority combobox
            prioritycombobox.AutoCompleteMode = AutoCompleteMode.None;
            prioritycombobox.DropDownStyle = ComboBoxStyle.DropDownList;
            prioritycombobox.DataSource = PriorityList;
            prioritycombobox.ValueMember = "Value";
            prioritycombobox.DisplayMember = "Text";
            GetReleaseInfo(releaseNo);
            ViewAdvanceFeatures();
            Filllistview(rlog.SubAssy);
            // Resume the layout logic
            this.ResumeLayout();
        }

        private void GetReleaseInfo(int RelNo)
        {
            rlog = releaseModule.GetRelease(Convert.ToInt32(RelNo));
            FillReleaseForm();
        }

        private void FillReleaseForm()
        {
            // Fill the basic details
            FillBasicInfo();

            // Fill the Treeview for items
            Startprocessfortreeview(rlog.ReleaseItems);
        }

        private void FillBasicInfo()
        {
            if (rlog != null)
            {
                string text = "";
                try
                {
                    foreach (SPMConnectAPI.ReleaseComment comment in rlog.ReleaseComments)
                    {
                        text += Environment.NewLine + "(" + comment.CommentBy.Split(' ')[0] + ") " + comment.DateCreated + " - " + comment.Comment;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    throw;
                }

                logstxt.Text = text;
                List<SPMConnectAPI.UserInfo> users = releaseModule.GetConnectUsersList();
                FillCheckingUsers(users);
                FillApprovingUsers(users);
                FillControlsApprovingUsers(users);
                relnotxt.Text = rlog.RelNo.ToString();
                jobtxt.Text = rlog.Job.ToString();
                jobdestxt.Text = rlog.JobDes;

                satxt.Text = rlog.SubAssy;
                sadestxt.Text = rlog.SubAssyDes;

                qty.Value = rlog.PckgQty;

                prioritycombobox.SelectedText = rlog.Priority;

                Createdon.Text = "Created On: " + rlog.DateCreated;
                CreatedBy.Text = "Created By: " + rlog.CreatedBy;

                LastSavedBy.Text = "Last Saved By: " + rlog.LastSavedBy;
                LastSavedOn.Text = "Last Saved On: " + rlog.LastSaved;

                cntrlsSubmitOnlbl.Text = "Submitted On: " + rlog.CntrlsSubmittedOn;
                cntrlsAppbylbl.Text = "Approved By: " + rlog.CntrlsApprovedBy;
                cntrlsApprvOnlbl.Text = "Approved On: " + rlog.CntrlsApprovedOn;

                submittedoblbl.Text = "Submitted On: " + rlog.SubmittedOn;
                submittedbylbl.Text = "Submitted By: " + rlog.CreatedBy;
                checkedonlbl.Text = "Checked On: " + rlog.CheckedOn;
                checkedbylbl.Text = "Checked By: " + rlog.CheckedBy;
                approvedonlbl.Text = "Approved On: " + rlog.ApprovedOn;
                approvedbylbl.Text = "Approved By: " + rlog.ApprovedBy;
                releasedonlbl.Text = "Released On: " + rlog.ReleasedOn;
                releasedbylbl.Text = "Released By: " + rlog.ReleasedBy;

                if (!string.IsNullOrEmpty(rlog.ConnectRelNo))
                {
                    releaseinfolbl.Visible = true;
                    releaseinfolbl.Text = "Connect Release No: " + rlog.ConnectRelNo;
                }
                if (!string.IsNullOrEmpty(rlog.WorkOrder))
                {
                    wolbl.Visible = true;
                    wolbl.Text = "Work Order: " + rlog.WorkOrder;
                }
            }
        }

        private void LogWrite(string commentby, string msg, DateTime datetime)
        {
            logstxt.Invoke((MethodInvoker)delegate
            {
                if (logstxt.Text.Length > 0)
                {
                    logstxt.AppendText(Environment.NewLine);
                }
                logstxt.AppendText("(" + commentby.Split(' ')[0] + ") " + datetime.TimeAgo() + " - " + msg);
            });
        }

        #region Filling up Comboxes

        private void FillCheckingUsers(List<SPMConnectAPI.UserInfo> users)
        {
            AutoCompleteStringCollection MyCollection = releaseModule.FillCheckingUsers();
            CheckingUserscombobox.AutoCompleteCustomSource = MyCollection;
            CheckingUserscombobox.DataSource = MyCollection;
            CheckingUserscombobox.AutoCompleteMode = AutoCompleteMode.None;
            CheckingUserscombobox.DropDownStyle = ComboBoxStyle.DropDownList;
            if (rlog.SubmittedTo.ToString().Length > 0 && rlog.SubmittedTo != 0)
            {
                string MyString = rlog.SubmittedTo.ToString();
                MyString += " ";
                MyString += users.Find(i => i.ConnectId == rlog.SubmittedTo).Name;
                CheckingUserscombobox.SelectedItem = MyString;
            }
            else
            {
                CheckingUserscombobox.SelectedItem = "";
            }
        }

        private void FillApprovingUsers(List<SPMConnectAPI.UserInfo> users)
        {
            AutoCompleteStringCollection MyCollection = releaseModule.FillApprovingUsers();
            ApprovingUserscomboBox.AutoCompleteCustomSource = MyCollection;
            ApprovingUserscomboBox.DataSource = MyCollection;
            ApprovingUserscomboBox.AutoCompleteMode = AutoCompleteMode.None;
            ApprovingUserscomboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            if (rlog.ApprovalTo.ToString().Length > 0 && rlog.ApprovalTo != 0)
            {
                string MyString = rlog.ApprovalTo.ToString();
                MyString += " ";
                MyString += users.Find(i => i.ConnectId == rlog.ApprovalTo).Name;
                ApprovingUserscomboBox.SelectedItem = MyString;
            }
            else
            {
                ApprovingUserscomboBox.SelectedItem = "";
            }
        }

        private void FillControlsApprovingUsers(List<SPMConnectAPI.UserInfo> users)
        {
            AutoCompleteStringCollection MyCollection = releaseModule.FillControlsApprovingUsers();
            controlsUserCombobox.AutoCompleteCustomSource = MyCollection;
            controlsUserCombobox.DataSource = MyCollection;
            controlsUserCombobox.AutoCompleteMode = AutoCompleteMode.None;
            controlsUserCombobox.DropDownStyle = ComboBoxStyle.DropDownList;
            if (rlog.CntrlsSubmittedTo.ToString().Length > 0 && rlog.CntrlsSubmittedTo != 0)
            {
                string MyString = rlog.CntrlsSubmittedTo.ToString();
                MyString += " ";
                MyString += users.Find(i => i.ConnectId == rlog.CntrlsSubmittedTo).Name;
                controlsUserCombobox.SelectedItem = MyString;
            }
            else
            {
                controlsUserCombobox.SelectedItem = "";
            }
        }

        #endregion Filling up Comboxes

        #region Handle Tree View

        private void Startprocessfortreeview(List<SPMConnectAPI.ReleaseItem> releaseItem)
        {
            try
            {
                treeView1.Nodes.Clear();
                RemoveChildNodes(root);
                treeView1.ResetText();
                Fillrootnode(releaseItem);
                CallRecursive();
                treeView1.ExpandAll();
                treeView1.SelectedNode = treeView1.Nodes[0];
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
            }
        }

        private void RemoveChildNodes(TreeNode parentNode)
        {
            if (parentNode.Nodes.Count > 0)
            {
                for (int i = parentNode.Nodes.Count - 1; i >= 0; i--)
                {
                    parentNode.Nodes[i].Remove();
                }
            }
        }

        private void Fillrootnode(List<SPMConnectAPI.ReleaseItem> releaseItems)
        {
            try
            {
                treeView1.Nodes.Clear();
                RemoveChildNodes(root);
                treeView1.ResetText();
                SPMConnectAPI.ReleaseItem sa = releaseItems.Find(i => i.ItemNumber == rlog.SubAssy);
                root.Text = sa.AssyNo + " - " + sa.AssyDescription;
                root.Name = sa.AssyNo;
                root.Tag = sa.AssyNo + "][" + sa.AssyDescription + "][" + sa.AssyFamily + "][" + sa.AssyManufacturer + "][" + sa.AssyManufacturerItemNumber + "][1";
                Setimageaccordingtofamily(sa.AssyFamily, root);
                treeView1.Nodes.Add(root);
                var itemToRemove = releaseItems.SingleOrDefault(r => r.ItemNumber == satxt.Text.Trim());
                if (itemToRemove != null)
                    releaseItems.Remove(itemToRemove);

                PopulateTreeView(releaseItems, root);
            }
            catch
            {
                treeView1.TopNode.Nodes.Clear();
                treeView1.Nodes.Clear();
                RemoveChildNodes(root);
                treeView1.ResetText();
            }
        }

        private void PopulateTreeView(List<SPMConnectAPI.ReleaseItem> releaseItems, TreeNode parentNode)
        {
            TreeNode childNode;

            foreach (SPMConnectAPI.ReleaseItem item in releaseItems)
            {
                TreeNode t = new TreeNode
                {
                    Text = item.ItemNumber + " - " + item.Description + " (" + item.QuantityPerAssembly.ToString() + ") ",
                    Name = item.ItemNumber,
                    Tag = item.ItemNumber + "][" + item.Description + "][" + item.ItemFamily + "][" + item.Manufacturer + "][" + item.ManufacturerItemNumber + "][" + item.QuantityPerAssembly.ToString()
                };
                if (parentNode == null)
                {
                    t.NodeFont = new Font("Arial", 10, FontStyle.Bold);
                    t.Text = item.AssyNo + " - " + item.AssyDescription + " (" + item.QuantityPerAssembly.ToString() + ") ";
                    t.Name = item.AssyNo;
                    t.Tag = item.AssyNo + "][" + item.AssyDescription + "][" + item.AssyFamily + "][" + item.AssyManufacturer + "][" + item.AssyManufacturerItemNumber + "][1";

                    treeView1.Nodes.Add(t);
                    childNode = t;
                }
                else
                {
                    Font f = new Font("Arial", 10, FontStyle.Bold);
                    // t.NodeFont = f;
                    parentNode.Nodes.Add(t);
                    childNode = t;
                }
            }
        }

        private void CallRecursive()
        {
            // Print each node recursively.
            foreach (TreeNode n in treeView1.Nodes)
            {
                if (n.Nodes.Count > 0)
                {
                    PrintRecursive(n);
                }
            }
        }

        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (publicnode != null)
            {
                publicnode.BackColor = treeView1.BackColor;
                publicnode.ForeColor = treeView1.ForeColor;
            }
            SPMConnectAPI.ReleaseItem sa = rlog.ReleaseItems.Find(i => i.ItemNumber == treeView1.SelectedNode.Name);
            if (sa != null)
            {
                itemnolbl.Text = "Item No : " + sa.ItemNumber;
                itmdeslbl.Text = "Description : " + sa.Description;
                itmoemlbl.Text = "Manufacturer : " + sa.Manufacturer;
                itemoemitmlbl.Text = "OEM Item No : " + sa.ManufacturerItemNumber;
                Filllistview(sa.ItemNumber);
            }
        }

        private void TreeView1_Leave(object sender, EventArgs e)
        {
            if (treeView1.Nodes.Count > 0)
            {
                publicnode = treeView1.SelectedNode;
                publicnode.BackColor = Color.LightBlue;
            }
        }

        private void PrintRecursive(TreeNode treeNode)
        {
            // Print the node.
            if (treeNode.Nodes.Count == 0)
            {
            }
            if (treeNode.Index == 0 && !rootnodedone)
            {
                rootnodedone = true;
            }
            else
            {
                string s = treeNode.Tag.ToString();
                string[] values = s.Replace("][", "~").Split('~');
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = values[i].Trim();
                }
                //DataRow r = _treeTB.Rows[int.Parse(treeNode.Tag.ToString())];
                //string family = r["ItemFamily"].ToString();
                string family = values[2];
                Setimageaccordingtofamily(family, treeNode);
            }

            // Print each node recursively.
            foreach (TreeNode tn in treeNode.Nodes)
            {
                PrintRecursive(tn);
            }
        }

        private void Setimageaccordingtofamily(string family, TreeNode treeNode)
        {
            if (family == "AG")
            {
                treeNode.ImageIndex = 4;
            }
            else if (family == "JOB")
            {
                treeNode.ImageIndex = 12;
            }
            else if (family == "AS" || family == "ASPN")
            {
                treeNode.ImageIndex = 0;
            }
            else if (family == "ECC")
            {
                treeNode.ImageIndex = 3;
            }
            else if (family == "MPC" || family == "PU")
            {
                treeNode.ImageIndex = 5;
            }
            else if (family == "MA" || family == "MAWE")
            {
                treeNode.ImageIndex = 6;
            }
            else if (family == "FAHW")
            {
                treeNode.ImageIndex = 7;
            }
            else if (family == "ASEL")
            {
                treeNode.ImageIndex = 8;
            }
            else if (family == "PCC")
            {
                treeNode.ImageIndex = 9;
            }
            else if (family == "MT")
            {
                treeNode.ImageIndex = 10;
            }
            else
            {
                treeNode.ImageIndex = family == "DR" ? 11 : 2;
            }
        }

        private void TreeView1_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.ImageIndex == 1)
            {
                e.Node.SelectedImageIndex = 0;
                e.Node.ImageIndex = 0;
            }
        }

        private void TreeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.ImageIndex == 0)
            {
                e.Node.SelectedImageIndex = 1;
                e.Node.ImageIndex = 1;
            }
        }

        private void TreeView1_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            switch (e.Node.ImageIndex)
            {
                case 1:
                    e.Node.SelectedImageIndex = 1;
                    e.Node.ImageIndex = 1;
                    break;

                case 2:
                    e.Node.SelectedImageIndex = 2;
                    e.Node.ImageIndex = 2;
                    break;

                case 3:
                    e.Node.SelectedImageIndex = 3;
                    e.Node.ImageIndex = 3;
                    break;

                case 4:
                    e.Node.SelectedImageIndex = 4;
                    e.Node.ImageIndex = 4;
                    break;

                case 5:
                    e.Node.SelectedImageIndex = 5;
                    e.Node.ImageIndex = 5;
                    break;

                case 6:
                    e.Node.SelectedImageIndex = 6;
                    e.Node.ImageIndex = 6;
                    break;

                case 7:
                    e.Node.SelectedImageIndex = 7;
                    e.Node.ImageIndex = 7;
                    break;

                case 8:
                    e.Node.SelectedImageIndex = 8;
                    e.Node.ImageIndex = 8;
                    break;

                case 9:
                    e.Node.SelectedImageIndex = 9;
                    e.Node.ImageIndex = 9;
                    break;

                case 10:
                    e.Node.SelectedImageIndex = 10;
                    e.Node.ImageIndex = 10;
                    break;

                case 11:
                    e.Node.SelectedImageIndex = 11;
                    e.Node.ImageIndex = 11;
                    break;

                case 12:
                    e.Node.SelectedImageIndex = 12;
                    e.Node.ImageIndex = 12;
                    break;

                default:
                    e.Node.SelectedImageIndex = 0;
                    e.Node.ImageIndex = 0;
                    break;
            }
        }

        #endregion Handle Tree View

        private void Jobtxt_TextChanged(object sender, EventArgs e)
        {
            rlog.Job = Convert.ToInt32(jobtxt.Text);
        }

        private void Satxt_TextChanged(object sender, EventArgs e)
        {
            rlog.SubAssy = satxt.Text;
        }

        private void Qty_ValueChanged(object sender, EventArgs e)
        {
            rlog.PckgQty = Convert.ToInt32(qty.Value);
        }

        private void Prioritycombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            rlog.Priority = prioritycombobox.SelectedItem.ToString();
        }

        private void NewRelease_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void Notifyuser_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Have you specified the valid reasons in the comments section in order for the user to process changes?", "SPM Connect - Notify for Changes",
              MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            if (result == DialogResult.Yes)
            {
                TakeScreenShot();
            }
        }

        private void UpdateRejectedReleaseLog(ApprovalType approvalType)
        {
            if (approvalType == ApprovalType.checking)
            {
                rlog.IsSubmitted = false;
                rlog.SubmittedTo = 0;

                rlog.SubmittedOn = "";

                rlog.ReqCntrlsApp = false;
                rlog.CntrlsSubmittedTo = 0;
                rlog.CntrlsSubmittedOn = "";

                rlog.CntrlsApproved = false;

                rlog.CntrlsApprovedBy = "";
                rlog.CntrlsApprovedOn = "";

                rlog.IsChecked = false;
                rlog.CheckedOn = "";

                rlog.CheckedBy = "";
            }
            else if (approvalType == ApprovalType.controls)

            {
                rlog.IsSubmitted = false;
                rlog.SubmittedTo = 0;
                rlog.SubmittedOn = "";

                rlog.ReqCntrlsApp = false;
                rlog.CntrlsSubmittedTo = 0;
                rlog.CntrlsSubmittedOn = "";

                rlog.CntrlsApproved = false;
                rlog.CntrlsApprovedBy = "";
                rlog.CntrlsApprovedOn = "";
            }
            else if (approvalType == ApprovalType.approval)
            {
                rlog.IsSubmitted = false;
                rlog.SubmittedTo = 0;
                rlog.SubmittedOn = "";

                rlog.IsChecked = false;
                rlog.CheckedOn = "";
                rlog.CheckedBy = "";

                rlog.IsApproved = false;
                rlog.ApprovalTo = 0;
                rlog.ApprovedOn = "";
                rlog.ApprovedBy = "";
            }
            else if (approvalType == ApprovalType.release)
            {
                rlog.IsSubmitted = false;
                rlog.SubmittedTo = 0;
                rlog.SubmittedOn = "";

                rlog.IsChecked = false;
                rlog.CheckedOn = "";
                rlog.CheckedBy = "";

                rlog.IsApproved = false;
                rlog.ApprovalTo = 0;
                rlog.ApprovedOn = "";
                rlog.ApprovedBy = "";

                rlog.IsReleased = false;
                rlog.ReleasedOn = "";
                rlog.ReleasedBy = "";
            }
        }

        private void ApproveControls()
        {
            DialogResult result = MetroFramework.MetroMessageBox.Show(this, "Are you sure want to confirm controls approval?", "SPM Connect - Confirm Controls Approval?",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Cursor.Current = Cursors.WaitCursor;
                rlog.CntrlsApproved = true;
                rlog.CntrlsApprovedBy = releaseModule.ConnectUser.Name;
                rlog.CntrlsApprovedOn = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                PassItForChecking();
                releaseModule.UpdateReleaseInvoice(rlog);
                this.DialogResult = DialogResult.OK;
                Cursor.Current = Cursors.Default;
                this.Close();
            }
        }

        private void TakeScreenShot()
        {
            Cursor.Current = Cursors.WaitCursor;
            Thread.Sleep(500);
            try
            {
                Rectangle bounds = this.Bounds;
                using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
                {
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        g.CopyFromScreen(new Point(bounds.Left, bounds.Top), Point.Empty, bounds.Size);
                    }
                    const string filepath = @"c:\Temp\screenshot.jpg";

                    if (!Directory.Exists(@"c:\Temp"))
                    {
                        Directory.CreateDirectory(@"c:\Temp");
                    }
                    if (File.Exists(filepath))
                    {
                        File.Delete(filepath);
                    }
                    ApplicationSettings.ScreenshotLocation = filepath;
                    bitmap.Save(filepath, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to capture the comments for the release. Pleas notify the user for the changes required." + Environment.NewLine
                    + " Error Message : " + ex.Message, "SPM Connect Comments", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                log.Error(ex.Message);
            }
            DeleteDirectory(rlog.IsChecked ? ApprovalType.approval : ApprovalType.checking, true);
            if (rlog.IsChecked)
            {
                if (rlog.IsApproved)

                {
                    NotifyUserForChange(ApprovalType.release);
                }
                else
                {
                    NotifyUserForChange(ApprovalType.approval);
                }
            }
            else
            {
                if (releaseModule.ConnectUser.ControlsApprovalDrawing && rlog.ReqCntrlsApp && rlog.CntrlsSubmittedTo == releaseModule.ConnectUser.ConnectId && !rlog.CntrlsApproved && !rlog.IsChecked)
                {
                    NotifyUserForChange(ApprovalType.controls);
                }
                else
                {
                    NotifyUserForChange(ApprovalType.checking);
                }
            }

            if (releaseModule.ConnectUser.ControlsApprovalDrawing && rlog.ReqCntrlsApp && rlog.CntrlsSubmittedTo == releaseModule.ConnectUser.ConnectId && !rlog.CntrlsApproved && !rlog.IsChecked)
            {
                UpdateRejectedReleaseLog(ApprovalType.controls);
            }
            else
            {
                UpdateRejectedReleaseLog(!rlog.IsChecked ? ApprovalType.checking : rlog.IsApproved ? ApprovalType.release : ApprovalType.approval);
            }

            releaseModule.UpdateReleaseInvoice(rlog);
            Cursor.Current = Cursors.Default;
            this.Close();
        }

        private void Closebttn_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure want to mark this release inactive? " +
               "Marking a release inactive removes from the system and cannot be restored.", "SPM Connect - Inactive Release",
               MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            if (result == DialogResult.Yes)
            {
                Cursor.Current = Cursors.WaitCursor;
                rlog.IsActive = false;
                closebttn.Enabled = false;
                DeleteDirectory(ApprovalType.approval, true);
                NotifyUserForChange(rlog.IsApproved ? ApprovalType.release : ApprovalType.approval);
                UpdateRejectedReleaseLog(rlog.IsApproved ? ApprovalType.release : ApprovalType.approval);
                releaseModule.UpdateReleaseInvoice(rlog);
                this.DialogResult = DialogResult.Abort;
                Cursor.Current = Cursors.Default;
                this.Close();
            }
        }

        private void DeleteDirectory(ApprovalType approvalType, bool reject)
        {
            string filepath;
            if (approvalType == ApprovalType.checking)
            {
                filepath = ApplicationSettings.GetConnectParameterValue("ReqChecking");
                filepath = filepath + @"\JOB#" + rlog.Job.ToString() + @"\SA#" + rlog.SubAssy + @"\REL#" + rlog.RelNo.ToString();
            }
            else if (approvalType == ApprovalType.approval)
            {
                filepath = ApplicationSettings.GetConnectParameterValue("ReqApproval");
                filepath = filepath + @"\JOB#" + rlog.Job.ToString() + @"\SA#" + rlog.SubAssy + @"\REL#" + rlog.RelNo.ToString();
            }
            else
            {
                return;
            }
            if (!Directory.Exists(filepath)) return;

            bool isEmpty = !Directory.EnumerateFiles(filepath).Any();
            if (isEmpty && Directory.Exists(filepath))
            {
                Directory.Delete(filepath, true);
            }
            else if (reject && Directory.Exists(filepath))
            {
                Directory.Delete(filepath, true);
            }
        }

        private void NotifyUserForChange(ApprovalType approvalType)
        {
            NameEmail creator = releaseModule.GetNameEmailByParaValue(UserFields.id, rlog.CreatedById.ToString())[0];
            string subject;
            string body;

            if (!rlog.IsActive)
            {
                subject = "Drawing Package Marked Inactive - " + rlog.Job.ToString() + "(" + rlog.SubAssy + ") - (" + rlog.RelNo.ToString() + ")";
                body = releaseModule.ConnectUser.Name + " has removed the drawing package from release for build. <br>" +
                    "Job Number : " + rlog.Job.ToString() + "<br>" +
                    "Sub Assy : " + rlog.SubAssy + "<br>" +
                    "Release No : " + rlog.RelNo.ToString();
                NameEmail checkor = releaseModule.GetNameEmailByParaValue(UserFields.Name, rlog.CheckedBy)[0];
                if (approvalType == ApprovalType.release)
                {
                    NameEmail approvor = releaseModule.GetNameEmailByParaValue(UserFields.Name, rlog.ApprovedBy)[0];
                    releaseModule.TriggerEmail(creator.email, subject, creator.name, body, "", checkor.email, approvor.email, "Normal");
                }
                else
                {
                    releaseModule.TriggerEmail(creator.email, subject, creator.name, body, "", checkor.email, "", "Normal");
                }
                return;
            }

            if (approvalType == ApprovalType.checking)
            {
                subject = "Drawings Check Rejected - " + rlog.Job.ToString() + "(" + rlog.SubAssy + ") - (" + rlog.RelNo.ToString() + ")";
                body = releaseModule.ConnectUser.Name + " has declined the checking of the drawing package. <br>" +
                    "Job Number : " + rlog.Job.ToString() + "<br>" +
                    "Sub Assy : " + rlog.SubAssy + "<br>" +
                    "Release No : " + rlog.RelNo.ToString() + "<br>" +
                    "Please check attached screen shot.";

                releaseModule.TriggerEmail(creator.email, subject, creator.name, body, ApplicationSettings.ScreenshotLocation, "", "", "Normal");
            }
            else if (approvalType == ApprovalType.controls)
            {
                subject = "Drawings Approval Rejected - " + rlog.Job.ToString() + "(" + rlog.SubAssy + ") - (" + rlog.RelNo.ToString() + ")";
                body = releaseModule.ConnectUser.Name + " has declined the controls approval of the drawing package. <br>" +
                    "Job Number : " + rlog.Job.ToString() + "<br>" +
                    "Sub Assy : " + rlog.SubAssy + "<br>" +
                    "Release No : " + rlog.RelNo.ToString() + "<br>" +
                    "Please check attached screen shot.";

                releaseModule.TriggerEmail(creator.email, subject, creator.name, body, ApplicationSettings.ScreenshotLocation, "", "", "Normal");
            }
            else if (approvalType == ApprovalType.release)
            {
                subject = "Drawing Release Rejected - " + rlog.Job.ToString() + "(" + rlog.SubAssy + ") - (" + rlog.RelNo.ToString() + ")";
                body = releaseModule.ConnectUser.Name + " has declined releasing the drawing package for release for build. <br>" +
                    "Job Number : " + rlog.Job.ToString() + "<br>" +
                    "Sub Assy : " + rlog.SubAssy + "<br>" +
                    "Release No : " + rlog.RelNo.ToString() + "<br>" +
                    "Please check attached screen shot.";
                NameEmail checkor = releaseModule.GetNameEmailByParaValue(UserFields.Name, rlog.CheckedBy)[0];
                NameEmail approvor = releaseModule.GetNameEmailByParaValue(UserFields.Name, rlog.ApprovedBy)[0];
                releaseModule.TriggerEmail(creator.email, subject, creator.name, body, ApplicationSettings.ScreenshotLocation, checkor.email, approvor.email, "Normal");
            }
            else if (approvalType == ApprovalType.approval)
            {
                subject = "Drawing Approval Rejected - " + rlog.Job.ToString() + "(" + rlog.SubAssy + ") - (" + rlog.RelNo.ToString() + ")";
                body = releaseModule.ConnectUser.Name + " has declined approving the drawing package. <br>" +
                    "Job Number : " + rlog.Job.ToString() + "<br>" +
                    "Sub Assy : " + rlog.SubAssy + "<br>" +
                    "Release No : " + rlog.RelNo.ToString() + "<br>" +
                    "Please check attached screen shot.";
                NameEmail checkor = releaseModule.GetNameEmailByParaValue(UserFields.Name, rlog.CheckedBy)[0];
                releaseModule.TriggerEmail(creator.email, subject, creator.name, body, ApplicationSettings.ScreenshotLocation, checkor.email, "", "Normal");
            }
            ApplicationSettings.ScreenshotLocation = "";
        }

        private void PassItForChecking()
        {
            NameEmail creator = releaseModule.GetNameEmailByParaValue(UserFields.id, rlog.CreatedById.ToString())[0];
            string subject;
            string body;
            NameEmail receiver = releaseModule.GetNameEmailByParaValue(UserFields.id, rlog.SubmittedTo.ToString())[0];
            subject = "Requires Checking - " + rlog.Job.ToString() + "(" + rlog.SubAssy + ") - (" + rlog.RelNo + ")";
            body = creator.name + " has drawings that needs to be checked. <br>" +
                "Job Number : " + rlog.Job.ToString() + "<br>" +
                "Sub Assy : " + rlog.SubAssy + "<br>" +
                "Sub Assy Description: " + rlog.SubAssyDes + "<br>" +
                "FolderPath : ";

            releaseModule.TriggerEmail(receiver.email, subject, receiver.name, body, "", creator.email, "", "Normal");
        }

        private void Txtmsg_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                if (txtmsg.Text.Length > 0)
                {
                    string msg = txtmsg.Text.Trim();
                    ReleaseComment comment = new ReleaseComment
                    {
                        RelNo = rlog.RelNo,
                        Comment = msg,
                        CommentBy = releaseModule.ConnectUser.Name
                    };
                    releaseModule.AddReleaseComment(comment);
                    txtmsg.Clear();
                    LogWrite(comment.CommentBy, msg, DateTime.Now);
                }
            }
        }

        private void ViewAdvanceFeatures()
        {
            if (rlog.SubmittedTo == releaseModule.ConnectUser.ConnectId || releaseModule.ConnectUser.ApproveDrawing || releaseModule.ConnectUser.ReleasePackage)
            {
                if (rlog.IsSubmitted)
                {
                    if (rlog.IsReleased)
                        notifyuser.Visible = false;
                    if (rlog.IsChecked && rlog.ApprovalTo == releaseModule.ConnectUser.ConnectId && !rlog.IsReleased)
                        notifyuser.Visible = true;
                    if (!rlog.IsChecked && !rlog.IsApproved)
                        notifyuser.Visible = true;
                    if (!rlog.IsChecked && rlog.ReqCntrlsApp && releaseModule.ConnectUser.ControlsApprovalDrawing && rlog.CntrlsSubmittedTo == releaseModule.ConnectUser.ConnectId && !rlog.CntrlsApproved)
                        notifyuser.Visible = true;
                }
                if ((releaseModule.ConnectUser.ReleasePackage || releaseModule.ConnectUser.ApproveDrawing) && rlog.IsChecked)
                {
                    closebttn.Visible = true;
                }

                if (releaseModule.ConnectUser.ControlsApprovalDrawing && rlog.ReqCntrlsApp && rlog.CntrlsSubmittedTo == releaseModule.ConnectUser.ConnectId && !rlog.CntrlsApproved && !rlog.IsChecked)
                {
                    cntrlsappbttn.Visible = true;
                }

                txtmsg.Visible = true;
                sendlbl.Visible = true;
                logstxt.Size = new Size(logstxt.Size.Width, panel1.Size.Height - 65);
            }
            else
            {
                //logstxt.Size.Height = panel1.Size.Height;
                logstxt.Size = new Size(logstxt.Size.Width, panel1.Size.Height - 25);
            }
        }

        private void Cntrlsappbttn_Click(object sender, EventArgs e)
        {
            ApproveControls();
        }

        private readonly List<string> listFiles = new List<string>();

        public static Icon GetIcon(string fileName)
        {
            try
            {
                Icon icon = Icon.ExtractAssociatedIcon(fileName);
                const ShellEx.IconSizeEnum ExtraLargeIcon = default;
                const ShellEx.IconSizeEnum size = (ShellEx.IconSizeEnum)ExtraLargeIcon;

                ShellEx.GetBitmapFromFilePath(fileName, size);

                return icon;
            }
            catch
            {
                try
                {
                    return GetIconOldSchool(fileName);
                }
                catch
                {
                    return null;
                }
            }
        }

        public static Icon GetIconOldSchool(string fileName)
        {
            StringBuilder strB = new StringBuilder(fileName);
            IntPtr handle = ExtractAssociatedIcon(IntPtr.Zero, strB, out _);
            return Icon.FromHandle(handle);
        }

        [DllImport("shell32.dll")]
        private static extern IntPtr ExtractAssociatedIcon(IntPtr hInst,
        StringBuilder lpIconPath, out ushort lpiIcon);

        private void Filllistview(string item)
        {
            try
            {
                listFiles.Clear();
                listView.Items.Clear();
                string first3char = item.Substring(0, 3) + @"\";
                const string spmcadpath = @"\\spm-adfs\CAD Data\AAACAD\";
                string Pathpart = spmcadpath + first3char;
                Getitemstodisplay(Pathpart, item);
            }
            catch
            {
                return;
            }
        }
        private void Getitemstodisplay(string Pathpart, string ItemNo)
        {
            if (Directory.Exists(Pathpart))
            {
                foreach (string item in Directory.GetFiles(Pathpart, "*" + ItemNo + "*").Where(str => !str.Contains(@"\~$")).OrderByDescending(fi => fi))
                {
                    try
                    {
                        string sDocFileName = item;
                        wpfThumbnailCreator pvf = new wpfThumbnailCreator
                        {
                            DesiredSize = new Size
                            {
                                Width = 256,
                                Height = 256
                            }
                        };
                        System.Drawing.Bitmap pic = pvf.GetThumbNail(sDocFileName);
                        imageList.Images.Add(pic);
                    }
                    catch (Exception)
                    {
                        const ShellEx.IconSizeEnum size = ShellEx.IconSizeEnum.ExtraLargeIcon;
                        imageList.Images.Add(ShellEx.GetBitmapFromFilePath(item, size));
                    }

                    FileInfo fi = new FileInfo(item);
                    listFiles.Add(fi.FullName);
                    listView.Items.Add(fi.Name, imageList.Images.Count - 1);
                    if (Directory.Exists(rlog.Path))
                        webBrowser1.Url = new Uri(rlog.Path);
                }
            }
        }

        private void ListView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                try
                {
                    if (listView.FocusedItem != null)
                        Process.Start(listFiles[listView.FocusedItem.Index]);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect");
                }
            }
        }

        private void ListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                try
                {
                    if (listView.FocusedItem != null)
                        Process.Start(listFiles[listView.FocusedItem.Index]);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect");
                }
            }
        }
    }

    public class ComboBoxItem
    {
        public int Value { get; set; }
        public string Text { get; set; }

        public ComboBoxItem(int value, string text)
        {
            Value = value;
            Text = text;
        }

        public override string ToString()
        {
            return Text;
        }
    }
}