using SPMConnect.UserActionLog;
using SPMConnectAPI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace SearchDataSPM
{
    public partial class AddRelease : Form
    {
        #region steupvariables

        private string connection;
        private DataTable releaseDetailsTb;
        private DataTable treeTB;
        private SqlConnection _connection;
        private SqlCommand command;
        private TreeNode root = new TreeNode();
        private string releaseLogNumber;
        private bool rootnodedone;
        private WorkOrder connectapi = new WorkOrder();

        private log4net.ILog log;
        private UserActions userActions;
        private ErrorHandler errorHandler = new ErrorHandler();

        #endregion steupvariables

        public AddRelease(string releaseLogNo = "")
        {
            Application.ThreadException += new ThreadExceptionEventHandler(UIThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledException);
            InitializeComponent();

            connection = ConfigurationManager.ConnectionStrings["SearchDataSPM.Properties.Settings.cn"].ConnectionString;

            try
            {
                _connection = new SqlConnection(connection);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            releaseDetailsTb = new DataTable();
            treeTB = new DataTable();

            command = new SqlCommand
            {
                Connection = _connection
            };
            releaseLogNumber = releaseLogNo;
        }

        private void ParentView_Load(object sender, EventArgs e)
        {
            Text = "Release Log Details - SPM Connect (" + releaseLogNumber + ")";
            FillReleaseinfo(connectapi.GrabReleaseLogDetails(releaseLogNumber));
            Startprocessfortreeview();
            // CallRecursive();
            // Treeview.ExpandAll();
            log4net.Config.XmlConfigurator.Configure();
            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log.Info("Opened Item Details " + releaseLogNumber + " by " + System.Environment.UserName);
            userActions = new UserActions(this);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.W))
            {
                Close();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void FillReleaseinfo(DataTable releaseLogDetails)
        {
            var r = releaseLogDetails.Rows[0];
            releaselognotxt.Text = r["RlogNo"].ToString();
            jobnotxt.Text = r["JobNo"].ToString();
            wotxt.Text = r["WO"].ToString();
            assynotxt.Text = r["AssyNo"].ToString();
            createdbytxt.Text = r["CreatedBy"].ToString();
            datecreatedtxt.Text = r["CreatedOn"].ToString();
            dateeditxt.Text = r["LastSaved"].ToString();
            Lastsavedtxtbox.Text = r["LastSavedBy"].ToString();
            releasenotestxt.Text = r["ReleaseNotes"].ToString();
            releasetypetxt.Text = r["ReleaseType"].ToString();

            var iteminfo = new DataTable();
            iteminfo.Clear();
            iteminfo = connectapi.GetIteminfo(assynotxt.Text);
            var ra = iteminfo.Rows[0];
            assydesctxt.Text = ra["Description"].ToString();
            iteminfo.Clear();
        }

        private string Getuserfullname(string username)
        {
            try
            {
                if (_connection.State == ConnectionState.Closed)
                    _connection.Open();
                var cmd = _connection.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM [SPM_Database].[dbo].[Users] WHERE [UserName]='" + username.ToString() + "' ";
                cmd.ExecuteNonQuery();
                var dt = new DataTable();
                var da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    var fullname = dr["Name"].ToString();
                    return fullname;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _connection.Close();
            }

            return null;
        }

        private void ItemInfo_FormClosed(object sender, FormClosedEventArgs e)
        {
            userActions.FinishLoggingUserActions(this);
            log.Info("Closed Item Details " + releaseLogNumber + " by " + System.Environment.UserName);
            Dispose();
        }

        private void UIThreadException(object sender, ThreadExceptionEventArgs t)
        {
            errorHandler.EmailExceptionAndActionLogToSupport(sender, t.Exception, userActions, this);
        }

        private void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            errorHandler.EmailExceptionAndActionLogToSupport(sender, (Exception)e.ExceptionObject, userActions, this);
        }

        #region Treeview

        private void Itemnotxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((sender as TextBox).SelectionStart == 0)
                e.Handled = (e.KeyChar == (char)Keys.Space);
            else
                e.Handled = false;
        }

        private void Itemnotxt_TextChanged(object sender, EventArgs e)
        {
            if (itemnotxt.Text.Length == 6 && char.IsLetter(itemnotxt.Text[0]))
            {
                Fetchitemdetails(itemnotxt.Text.Trim());
            }
            else
            {
                qtytxtbox.Clear();
                itmdeslbl.Text = "Description : ";
                itmoemlbl.Text = "Manufacturer : ";
                itemoemitmlbl.Text = "OEM Item No : ";
            }
        }

        private void Fetchitemdetails(string itemno)
        {
            if (itemno.Length == 6)
            {
                Fillselectediteminfo(itemno);
            }
            else
            {
                qtytxtbox.Clear();
                itmdeslbl.Text = "Description : ";
                itmoemlbl.Text = "Manufacturer : ";
                itemoemitmlbl.Text = "OEM Item No : ";
            }
        }

        private void Fillselectediteminfo(string item)
        {
            var iteminfo = new DataTable();
            iteminfo.Clear();
            iteminfo = connectapi.GetIteminfo(item);
            if (iteminfo.Rows.Count > 0)
            {
                var r = iteminfo.Rows[0];
                itmdeslbl.Text = "Description : " + r["Description"].ToString();
                itmoemlbl.Text = "Manufacturer : " + r["Manufacturer"].ToString();
                itemoemitmlbl.Text = "OEM Item No : " + r["ManufacturerItemNumber"].ToString();
            }
        }

        private void Cleanup()
        {
            Treeview.Nodes.Clear();
            Treeview.ResetText();
            RemoveChildNodes(root);
            treeTB.Clear();
        }

        private void Fillrootnode()
        {
            try
            {
                Treeview.Nodes.Clear();
                RemoveChildNodes(root);
                Treeview.ResetText();

                root.Text = "Job " + jobnotxt.Text + " - WO " + wotxt.Text + " - " + releasetypetxt.Text;
                root.Tag = jobnotxt.Text + "][" + wotxt.Text + "][" + releaselognotxt.Text + "][" + releasetypetxt.Text;
                Setimageaccordingtofamily("AS", root);
                Treeview.Nodes.Add(root);
                PopulateTreeView(releaseLogNumber, root);
            }
            catch
            {
                Treeview.TopNode.Nodes.Clear();
                Treeview.Nodes.Clear();
                RemoveChildNodes(root);
                Treeview.ResetText();
            }
        }

        private void Startprocessfortreeview()
        {
            try
            {
                Treeview.Nodes.Clear();
                RemoveChildNodes(root);
                Treeview.ResetText();
                treeTB.Clear();
                treeTB = connectapi.GrabReleaseLogItemDetails(wotxt.Text, releaseLogNumber);
                Fillrootnode();
                // CallRecursive();
            }
            catch (Exception)
            {
            }
        }

        private void PopulateTreeView(string parentId, TreeNode parentNode)
        {
            TreeNode childNode;

            foreach (DataRow dr in treeTB.Select("[RlogNo] ='" + parentId.ToString() + "'"))
            {
                var t = new TreeNode
                {
                    Text = dr["ItemId"].ToString() + " - " + dr["ItemId"].ToString() + " (" + dr["ItemQty"].ToString() + ") ",
                    Name = dr["ItemId"].ToString(),
                    Tag = dr["ItemId"].ToString() + "][" + dr["ItemId"].ToString() + "][" + dr["ItemId"].ToString() + "][" + dr["ItemId"].ToString() + "][" + dr["ItemId"].ToString() + "][" + dr["ItemQty"].ToString()
                };
                if (parentNode == null)
                {
                    var f = new Font("Arial", 10, FontStyle.Bold);
                    t.NodeFont = f;
                    t.Text = jobnotxt.Text + " - " + wotxt.Text + " - " + releaselognotxt.Text + " - " + releasetypetxt.Text;
                    t.Tag = jobnotxt.Text + "][" + wotxt.Text + "][" + releaselognotxt.Text + "][" + releasetypetxt.Text;
                    Treeview.Nodes.Add(t);
                    childNode = t;
                }
                else
                {
                    var f = new Font("Arial", 10, FontStyle.Bold);
                    // t.NodeFont = f;
                    parentNode.Nodes.Add(t);
                    childNode = t;
                }
            }
            // treeView1.SelectedNode = treeView1.Nodes[0];
        }

        private void RemoveChildNodes(TreeNode parentNode)
        {
            if (parentNode.Nodes.Count > 0)
            {
                for (int i = parentNode.Nodes.Count - 1; i >= 0; i--)
                    parentNode.Nodes[i].Remove();
            }
        }

        private void TreeView1_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            // e.Node.SelectedImageIndex = 0;
            // e.Node.ImageIndex = 0;
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

        private void PrintRecursive(TreeNode treeNode)
        {
            // Print the node.
            if (treeNode.Nodes.Count == 0)
            {
            }

            if (treeNode.Index == 0 && rootnodedone == false)
            {
                rootnodedone = true;
            }
            else
            {
                var s = treeNode.Tag.ToString();
                string[] values = s.Replace("][", "~").Split('~');
                for (int i = 0; i < values.Length; i++)
                    values[i] = values[i].Trim();

                // DataRow r = _treeTB.Rows[int.Parse(treeNode.Tag.ToString())];
                // string family = r["ItemFamily"].ToString();
                var family = values[2];
                Setimageaccordingtofamily(family, treeNode);
            }

            // Print each node recursively.
            foreach (TreeNode tn in treeNode.Nodes)
                PrintRecursive(tn);
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
            else if (family == "DR")
            {
                treeNode.ImageIndex = 11;
            }
            else
            {
                treeNode.ImageIndex = 2;
            }
        }

        private void CallRecursive()
        {
            // Print each node recursively.
            var nodes = Treeview.Nodes;
            foreach (TreeNode n in nodes)
            {
                if (n.Nodes.Count > 0)
                {
                    PrintRecursive(n);
                }
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

        private void TreeView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Down))
            {
                var node = new TreeNode();
                node = Treeview.SelectedNode;
                Treeview.SelectedNode = node.NextVisibleNode;
                node.TreeView.Focus();
            }
            else if (e.KeyChar == Convert.ToChar(Keys.Up))
            {
                var node = new TreeNode();
                node = Treeview.SelectedNode;
                Treeview.SelectedNode = node.NextVisibleNode;
                node.TreeView.Focus();
            }
        }

        private void TreeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            Treeview.SelectedNode = e.Node;
        }

        private void Qtytxtbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), @"[0-9+\b]"))
            {
                // Stop the character from being entered into the control since it is illegal.
            }
            else
            {
                e.Handled = true;
            }
        }

        private void Addbutton_Click(object sender, EventArgs e)
        {
            try
            {
                errorProvider1.Clear();
                if (qtytxtbox.Text.Length > 0 && qtytxtbox.Text != "0")
                {
                    var iteminfo = new DataTable();
                    iteminfo.Clear();
                    iteminfo = connectapi.GetIteminfo(itemnotxt.Text);
                    var r = iteminfo.Rows[0];

                    var _itemno = itemnotxt.Text;
                    var _description = r["Description"].ToString();
                    var _family = r["FamilyCode"].ToString();
                    var _oem = r["Manufacturer"].ToString();
                    var _manufacturer = r["ManufacturerItemNumber"].ToString();
                    var qty = qtytxtbox.Text;
                    var notes = itemnotestxt.Text;

                    _SearchNodes(_itemno, Treeview.Nodes[0]);
                    if (itemexists != "yes")
                    {
                        var child = new TreeNode
                        {
                            Text = _itemno.ToString() + " - " + _description.ToString() + " (" + qty.ToString() + ")",
                            Tag = _itemno + "][" + _description + "][" + _family + "][" + _manufacturer + "][" + _oem + "][" + qty + "][" + notes
                        };

                        Treeview.SelectedNode = Treeview.Nodes[0];
                        root.Nodes.Add(child);
                        CallRecursive();
                        if (!(root.IsExpanded))
                        {
                            Treeview.ExpandAll();
                        }

                        savbttn.Visible = true;
                        itemnotxt.Clear();
                    }
                    else
                    {
                        savbttn.Visible = false;
                        itemexists = null;
                        itemnotxt.Clear();
                    }
                }
                else
                {
                    errorProvider1.SetError(qtytxtbox, "Cannot be null");
                }
            }
            catch
            {
                return;
            }
        }

        #region search before addding item

        private List<TreeNode> CurrentNodeMatches = new List<TreeNode>();

        private string itemexists;

        private void _SearchNodes(string SearchText, TreeNode StartNode)
        {
            // TreeNode node = null;
            while (StartNode != null)
            {
                if (StartNode.Text.ToLower().Contains(SearchText.ToLower()))
                {
                    MessageBox.Show("Item already added to the assembly list", "SPM Conect", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    itemexists = "yes";
                    Treeview.SelectedNode = StartNode;
                    CurrentNodeMatches.Add(StartNode);
                }

                if (StartNode.Nodes.Count != 0)
                {
                    _SearchNodes(SearchText, StartNode.Nodes[0]);//Recursive Search
                }

                StartNode = StartNode.NextNode;
            }
        }

        #endregion search before addding item

        private void TreeView1_DragDrop(object sender, DragEventArgs e)
        {
            var rowToMove = e.Data.GetData(typeof(DataGridViewRow)) as DataGridViewRow;
            var itemnumber = Convert.ToString(rowToMove.Cells[0].Value);
            var description = Convert.ToString(rowToMove.Cells[1].Value);
            var family = Convert.ToString(rowToMove.Cells[2].Value);
            var oem = Convert.ToString(rowToMove.Cells[3].Value);
            var oemitem = Convert.ToString(rowToMove.Cells[4].Value);

            _SearchNodes(itemnumber, Treeview.Nodes[0]);

            if (itemexists != "yes")
            {
                var child = new TreeNode
                {
                    Text = itemnumber + " - " + description + " (1)",
                    Tag = itemnumber + "][" + description + "][" + family + "][" + oem + "][" + oemitem + "][" + "1" + "][" + " "
                };

                root.Nodes.Add(child);

                CallRecursive();
                if (!(root.IsExpanded))
                {
                    Treeview.ExpandAll();
                }

                savbttn.Visible = true;
            }
            else
            {
                itemexists = null;
            }
        }

        private void TreeView1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void Savbttn_Click(object sender, EventArgs e) => ProcessSaveButton();

        private void ProcessSaveButton()
        {
            Treeview.SelectedNode = Treeview.Nodes[0];
            if (Treeview.SelectedNode != null && Treeview.SelectedNode.Parent == null)
            {
                Treeview.PathSeparator = ".";

                // Get the count of the child tree nodes contained in the SelectedNode.
                var myNodeCount = Treeview.SelectedNode.GetNodeCount(true);
                // MessageBox.Show(myNodeCount.ToString());
                var myChildPercentage = ((decimal)myNodeCount /
                  (decimal)Treeview.GetNodeCount(true)) * 100;

                if (myNodeCount > 0)
                {
                    connectapi.Deleteassy(releaseLogNumber);
                    AddItems();
                }
                else
                {
                    connectapi.Deleteassy(releaseLogNumber);
                    Lockdownsave();
                    // MessageBox.Show("Assembly list cannot be empty in order to save to AutoCad Catalog.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }

        private void AddItems()
        {
            // Print each node recursively.
            var nodes = Treeview.Nodes;
            foreach (TreeNode n in nodes)
                SaveRecursive(n);

            Lockdownsave();
        }

        private void Lockdownsave()
        {
            Treeview.SelectedNode = root;
            savbttn.Visible = false;
            Treeview.ContextMenuStrip = null;
            connectapi.UpdateReleaseLogNotes(releaseLogNumber, releasenotestxt.Text);
            MessageBox.Show("Release log saved successfully.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);
            addbutton.Visible = true;
        }

        private void SaveEachnode(TreeNode treeNode)
        {
            if (treeNode.Parent == null)
            {
                // string parentnode = treeNode.Tag.ToString();
                // //MessageBox.Show(parentnode);
                // Splittagtovariables(parentnode);
            }
            else
            {
                var childnode = treeNode.Tag.ToString();
                Splittagtovariables(childnode);
            }
        }

        private void SaveRecursive(TreeNode treeNode)
        {
            SaveEachnode(treeNode);
            foreach (TreeNode tn in treeNode.Nodes)
                // MessageBox.Show(treeNode.Text);
                SaveRecursive(tn);
        }

        private void Splittagtovariables(string s)
        {
            string[] values = s.Replace("][", "~").Split('~');

            // string[] values = s.Split(',');
            for (int i = 0; i < values.Length; i++)
                values[i] = values[i].Trim();

            connectapi.AddItemToReleaseLog(wotxt.Text, assynotxt.Text, releaseLogNumber, values[0], values[5], values[6]);
        }

        private void Qtytxtbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void Updatebttn_Click(object sender, EventArgs e)
        {
            if (Treeview.SelectedNode != null && !(root.IsSelected))
            {
                if (qtytxtbox.Text == "0")
                {
                    qtytxtbox.Text = "1";
                }

                var iteminfo = new DataTable();
                iteminfo.Clear();
                iteminfo = connectapi.GetIteminfo(itemnotxt.Text);
                var r = iteminfo.Rows[0];
                Treeview.SelectedNode.Text = itemnotxt.Text + " - " + r["Description"].ToString() + " (" + qtytxtbox.Text + ")";
                Treeview.SelectedNode.Tag = itemnotxt.Text + "][" + r["Description"].ToString() + "][" + r["FamilyCode"].ToString() + "][" + r["Manufacturer"].ToString() + "][" + r["ManufacturerItemNumber"].ToString() + "][" + qtytxtbox.Text;
                itemnotxt.Clear();
                savbttn.Visible = true;
                addbutton.Visible = true;
                updatebttn.Visible = false;
                cancelbutton.Visible = false;
                Treeview.AllowDrop = true;
                Addremovecontextmenu.Enabled = true;
            }
        }

        private void RemoveItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Treeview.SelectedNode != null)
            {
                if (Treeview.SelectedNode.Parent == null)
                {
                    // treeView1.Nodes.Remove(treeView1.SelectedNode);
                }
                else
                {
                    var result = MessageBox.Show("Remove item from the dependency list?", "SPM Connect", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        Treeview.SelectedNode.Parent.Nodes.Remove(Treeview.SelectedNode);
                        savbttn.Visible = true;
                    }
                    else if (result == DialogResult.No)
                    {
                        // code for No
                    }
                }
            }
        }

        private void Cancelbutton_Click(object sender, EventArgs e)
        {
            updatebttn.Visible = false;
            addbutton.Visible = true;
            savbttn.Visible = true;
            cancelbutton.Visible = false;
            itemnotxt.Clear();
            Addremovecontextmenu.Enabled = true;
        }

        private void TreeView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (itemupdatetools.Visible)
                if (!(root.IsSelected))
                {
                    Addremovecontextmenu.Enabled = false;
                    addbutton.Visible = false;
                    savbttn.Visible = false;
                    updatebttn.Visible = true;
                    cancelbutton.Visible = true;
                    Treeview.AllowDrop = false;
                    var s = Treeview.SelectedNode.Tag.ToString();
                    string[] values = s.Replace("][", "~").Split('~');
                    for (int i = 0; i < values.Length; i++)
                        values[i] = values[i].Trim();

                    itemnotxt.Text = values[0];
                    qtytxtbox.Text = values[5];
                }
        }

        private void ItemInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (savbttn.Visible && itemupdatetools.Visible)
            {
                errorProvider1.SetError(savbttn, "Save before closing");
                var result = MetroFramework.MetroMessageBox.Show(this, "Do you want to save changes?", "SPM Connect", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    ProcessSaveButton();
                    connectapi.CheckoutInvoice(releaseLogNumber);
                }
                else
                {
                    connectapi.CheckoutInvoice(releaseLogNumber);
                }
            }
        }

        #endregion Treeview

        private void releasenotestxt_TextChanged(object sender, EventArgs e) => savbttn.Visible = true;
    }
}