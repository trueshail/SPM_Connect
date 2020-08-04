using SearchDataSPM.Helper;
using SPMConnectAPI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

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
            else
            {
                return;
            }
        }

        private void TakeScreenShot()
        {
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

            releaseModule.UpdateReleaseInvoice(rlog);
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void Closebttn_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure want to mark this release inactive? " +
               "Marking a release inactive removes from the system and cannot be restored.", "SPM Connect - Inactive Release",
               MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            if (result == DialogResult.Yes)
            {
                rlog.IsActive = false;
                closebttn.Enabled = false;
                releaseModule.UpdateReleaseInvoice(rlog);
                this.DialogResult = DialogResult.Abort;
                this.Close();
            }
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
                    // notifyuser.Visible = true;
                }
                if ((releaseModule.ConnectUser.ReleasePackage || releaseModule.ConnectUser.ApproveDrawing) && rlog.IsChecked)
                {
                    // closebttn.Visible = true;
                }

                txtmsg.Visible = true;
                sendlbl.Visible = true;
                logstxt.Size = new Size(391, 160);

            }
            else
            {
                logstxt.Size = new Size(391, 200);
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