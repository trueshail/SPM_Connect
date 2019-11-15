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
    public partial class ViewRelease : Form
    {
        #region steupvariables

        private string connection;
        private DataTable treeTB;
        private SqlConnection _connection;
        private SqlCommand command;
        private TreeNode root = new TreeNode();
        private string workOrder;
        private bool rootnodedone;
        private bool isJob = false;
        private WorkOrder connectapi = new WorkOrder();
        private log4net.ILog log;
        private ErrorHandler errorHandler = new ErrorHandler();

        #endregion steupvariables

        public ViewRelease(string wrkorder = "", bool job = false)
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

            treeTB = new DataTable();

            command = new SqlCommand
            {
                Connection = _connection
            };
            this.workOrder = wrkorder;
            this.isJob = job;
        }

        private void ParentView_Load(object sender, EventArgs e)
        {
            Text = "Release Log Details - SPM Connect (" + workOrder + ")";
            Startprocessfortreeview();
            log4net.Config.XmlConfigurator.Configure();
            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log.Info("Opened Item Details " + workOrder + " by " + System.Environment.UserName);
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
            if (releaseLogDetails.Rows.Count > 0)
            {
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
        }

        private void ItemInfo_FormClosed(object sender, FormClosedEventArgs e)
        {
            log.Info("Closed Item Details " + workOrder + " by " + System.Environment.UserName);
            Dispose();
        }

        private void UIThreadException(object sender, ThreadExceptionEventArgs t)
        {
            log.Error(sender, t.Exception);
            errorHandler.EmailExceptionAndActionLogToSupport(sender, t.Exception, this);
        }

        private void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            log.Error(sender, (Exception)e.ExceptionObject);
            errorHandler.EmailExceptionAndActionLogToSupport(sender, (Exception)e.ExceptionObject, this);
        }

        #region Treeview

        private void Itemnotxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((sender as TextBox).SelectionStart == 0)
                e.Handled = (e.KeyChar == (char)Keys.Space);
            else
                e.Handled = false;
        }

        private void Cleanup()
        {
            Treeview.Nodes.Clear();
            Treeview.ResetText();
            RemoveChildNodes(root);
            treeTB.Clear();
            Expandchk.Checked = false;
        }

        private void Fillrootnode()
        {
            try
            {
                Treeview.Nodes.Clear();
                RemoveChildNodes(root);
                Treeview.ResetText();
                Expandchk.Checked = false;

                DataRow[] dr = treeTB.Select("JobNo = '" + workOrder + "'");
                if (isJob)
                {
                    root.Text = "Job " + dr[0]["JobNo"].ToString() + " - " + dr[0]["Description"].ToString();
                    root.Tag = treeTB.Rows.IndexOf(dr[0]);
                    Setimageaccordingtofamily("AG", root);
                    Treeview.Nodes.Add(root);
                    PopulateTreeView(workOrder, root);
                }
                else
                {
                    root.Text = "WO " + dr[0]["JobNo"].ToString() + " - " + dr[0]["Description"].ToString() + " - " + dr[0]["AssyNo"].ToString();
                    root.Tag = treeTB.Rows.IndexOf(dr[0]);
                    Setimageaccordingtofamily("AS", root);
                    Treeview.Nodes.Add(root);
                    PopulateTreeViewFirst(workOrder, root);
                }
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
                treeTB = connectapi.GrabReleaseLogsBOM();
                Fillrootnode();
                CallRecursive();
            }
            catch (Exception)
            {
            }
        }

        private void PopulateTreeView(string parentId, TreeNode parentNode)
        {
            TreeNode childNode;

            foreach (DataRow dr in treeTB.Select("[JobNo] ='" + parentId.ToString() + "'"))
            {
                var t = new TreeNode
                {
                    Text = dr["AssyNo"].ToString() + " - " + dr["Description"].ToString() + " (" + dr["WO"].ToString() + ") ",
                    Name = "Assy",
                    Tag = treeTB.Rows.IndexOf(dr)
                };
                if (parentNode == null)
                {
                    var f = new Font("Arial", 10, FontStyle.Bold);
                    t.NodeFont = f;
                    t.Text = "Job " + dr["JobNo"].ToString() + " - WO " + workOrder + " - " + dr["Description"].ToString();
                    t.Tag = treeTB.Rows.IndexOf(dr);
                    Treeview.Nodes.Add(t);
                    childNode = t;
                }
                else
                {
                    var f = new Font("Arial", 10, FontStyle.Bold);
                    t.NodeFont = f;
                    parentNode.Nodes.Add(t);
                    childNode = t;
                }
                if (parentId != dr["WO"].ToString())
                    PopulateTreeViewFirst(dr["WO"].ToString(), childNode);
            }
            // treeView1.SelectedNode = treeView1.Nodes[0];
        }

        private void PopulateTreeViewFirst(string parentId, TreeNode parentNode)
        {
            TreeNode childNode;

            foreach (DataRow dr in treeTB.Select("[JobNo] ='" + parentId.ToString() + "'"))
            {
                var t = new TreeNode
                {
                    Text = dr["ReleaseType"].ToString() + " - (" + dr["RlogNo"].ToString() + ")",
                    Name = "Releases",
                    Tag = treeTB.Rows.IndexOf(dr)
                };
                if (parentNode == null)
                {
                    var f = new Font("Arial", 10, FontStyle.Bold);
                    t.NodeFont = f;
                    t.Text = "Job " + dr["JobNo"].ToString() + " - WO " + workOrder + " - " + dr["Description"].ToString();
                    t.Tag = treeTB.Rows.IndexOf(dr);
                    Treeview.Nodes.Add(t);
                    childNode = t;
                }
                else
                {
                    var f = new Font("Arial", 10, FontStyle.Bold);
                    //t.NodeFont = f;
                    parentNode.Nodes.Add(t);
                    childNode = t;
                }

                if (parentId != dr["RlogNo"].ToString())
                    PopulateTreeViewChilds(dr["RlogNo"].ToString(), childNode);
            }
            // treeView1.SelectedNode = treeView1.Nodes[0];
        }

        private void PopulateTreeViewChilds(string parentId, TreeNode parentNode)
        {
            TreeNode childNode;

            foreach (DataRow dr in treeTB.Select("[JobNo] ='" + parentId.ToString() + "'"))
            {
                var t = new TreeNode
                {
                    Text = dr["AssyNo"].ToString() + " - " + dr["Description"].ToString() + " (" + dr["ReleaseType"].ToString() + ")",
                    Name = "Items",
                    Tag = treeTB.Rows.IndexOf(dr)
                };
                if (parentNode == null)
                {
                    var f = new Font("Arial", 10, FontStyle.Bold);
                    t.NodeFont = f;
                    t.Text = "Job " + dr["JobNo"].ToString() + " - WO " + workOrder + " - " + dr["Description"].ToString();
                    t.Tag = treeTB.Rows.IndexOf(dr);
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
                DataRow r = treeTB.Rows[int.Parse(treeNode.Tag.ToString())];
                string family = r["Family"].ToString();
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

        private void ItemInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        #endregion Treeview

        private void Treeview_AfterExpand(object sender, TreeViewEventArgs e)
        {
            Expandchk.Checked = true;
        }

        public TreeNode publicnode;

        private void Treeview_Leave(object sender, EventArgs e)
        {
            if (Treeview.Nodes.Count > 0)
            {
                publicnode = Treeview.SelectedNode;
                publicnode.BackColor = Color.LightBlue;
            }
        }

        private void Treeview_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (publicnode != null)
            {
                publicnode.BackColor = Treeview.BackColor;
                publicnode.ForeColor = Treeview.ForeColor;
            }
            if (root.IsSelected)
            {
                DataRow r = treeTB.Rows[int.Parse(Treeview.SelectedNode.Tag.ToString())];
                jobnotxt.Text = r["JobNo"].ToString();
            }
            else if (Treeview.SelectedNode.Name == "Assy")
            {
                DataRow r = treeTB.Rows[int.Parse(Treeview.SelectedNode.Tag.ToString())];
                assynotxt.Text = r["AssyNo"].ToString();
                assydesctxt.Text = r["Description"].ToString();
                wotxt.Text = r["WO"].ToString();
            }
            else if (Treeview.SelectedNode.Name == "Releases")
            {
                DataRow r = treeTB.Rows[int.Parse(Treeview.SelectedNode.Tag.ToString())];
                assynotxt.Text = r["AssyNo"].ToString();
                assydesctxt.Text = r["Description"].ToString();
                wotxt.Text = r["WO"].ToString();
                releaselognotxt.Text = r["RlogNo"].ToString();
                releasenotestxt.Text = r["ReleaseNotes"].ToString();
                createdbytxt.Text = r["CreatedBy"].ToString();
                datecreatedtxt.Text = r["CreatedOn"].ToString();
                Lastsavedtxtbox.Text = r["LastSavedBy"].ToString();
                dateeditxt.Text = r["LastSaved"].ToString();
                releasetypetxt.Text = r["ReleaseType"].ToString();
            }
            else if (Treeview.SelectedNode.Name == "Items")
            {
                DataRow r = treeTB.Rows[int.Parse(Treeview.SelectedNode.Tag.ToString())];

                wotxt.Text = r["WO"].ToString();
                releaselognotxt.Text = r["RlogNo"].ToString();

                createdbytxt.Text = r["CreatedBy"].ToString();
                datecreatedtxt.Text = r["CreatedOn"].ToString();
                Lastsavedtxtbox.Text = r["LastSavedBy"].ToString();
                dateeditxt.Text = r["LastSaved"].ToString();

                itemnotxt.Text = r["AssyNo"].ToString();
                itemdestxt.Text = r["Description"].ToString();
                oemtxt.Text = r["OEM"].ToString();
                oemitemtxt.Text = r["OEMItemNo"].ToString();
                qtytxt.Text = r["ReleaseType"].ToString();
                itmnotestxt.Text = r["ReleaseNotes"].ToString();
            }
        }

        private void Expandchk_Click(object sender, EventArgs e)
        {
            if (Expandchk.Checked)
            {
                Treeview.ExpandAll();
            }
            else
            {
                Treeview.CollapseAll();
            }
        }

        #region search tree

        private List<TreeNode> CurrentNodeMatches = new List<TreeNode>();

        private int LastNodeIndex = 0;

        private string LastSearchText;

        private void SearchNodes(string SearchText, TreeNode StartNode)
        {
            //TreeNode node = null;
            while (StartNode != null)
            {
                DataRow r = treeTB.Rows[int.Parse(StartNode.Tag.ToString())];
                string searchwithin;

                searchwithin = r["WO"].ToString() + r["AssyNo"].ToString() + r["Description"].ToString() + r["OEM"].ToString() + r["OEMItemNo"].ToString() + r["ReleaseType"].ToString() + r["ReleaseNotes"].ToString();

                if (searchwithin.ToLower().Contains(SearchText.ToLower()))
                {
                    CurrentNodeMatches.Add(StartNode);
                };
                if (StartNode.Nodes.Count != 0)
                {
                    SearchNodes(SearchText, StartNode.Nodes[0]);//Recursive Search
                };
                StartNode = StartNode.NextNode;
            }
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtSearch.Text.Length > 0)
            {
                try
                {
                    string searchText = this.txtSearch.Text;
                    if (e.KeyCode == Keys.Return)
                    {
                        txtSearch.Select();
                        if (String.IsNullOrEmpty(searchText))
                        {
                            return;
                        };

                        if (LastSearchText != searchText)
                        {
                            //It's a new Search
                            CurrentNodeMatches.Clear();
                            LastSearchText = searchText;
                            LastNodeIndex = 0;
                            SearchNodes(searchText, Treeview.Nodes[0]);
                        }

                        if (LastNodeIndex >= 0 && CurrentNodeMatches.Count > 0 && LastNodeIndex < CurrentNodeMatches.Count)
                        {
                            TreeNode selectedNode = CurrentNodeMatches[LastNodeIndex];
                            LastNodeIndex++;
                            this.Treeview.SelectedNode = selectedNode;
                            this.Treeview.SelectedNode.Expand();
                            this.Treeview.Select();
                            if (txtSearch.Text.Length > 0)
                                foundlabel.Text = "Found " + LastNodeIndex + " of " + CurrentNodeMatches.Count + " matching items containing keyword \"" + searchText + "\"";
                            else foundlabel.Text = "Search:";
                        }
                        else
                        {
                            LastSearchText = "";
                        }

                        e.Handled = true;
                        e.SuppressKeyPress = true;
                    }
                }
                catch (Exception)
                {
                }
            }
            else foundlabel.Text = "Search:";
        }

        private void txtSearch_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            txtSearch.Clear();
            foundlabel.Text = "Search:";
        }

        #endregion search tree

        private void Treeview_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                txtSearch.Focus();
                txtSearch.Select();
                SendKeys.Send("~");
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void RemoveItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Treeview.SelectedNode.Name == "Releases")
            {
                DataRow r = treeTB.Rows[int.Parse(Treeview.SelectedNode.Tag.ToString())];

                ShowReleaseLogDetails(r["RlogNo"].ToString());
            }
        }

        private void ShowReleaseLogDetails(string invoice)
        {
            string invoiceopen = connectapi.InvoiceOpen(invoice);
            if (invoiceopen.Length > 0)
            {
                MetroFramework.MetroMessageBox.Show(this, "Release Document is opened for edit by " + invoiceopen + ". ", "SPM Connect - Open Release Document Failed", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                if (connectapi.CheckinInvoice(invoice))
                {
                    AddRelease addrelease = new AddRelease(releaseLogNo: invoice);
                    addrelease.Show();
                }
            }
        }

        private void Addremovecontextmenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Treeview.SelectedNode.Name == "Releases")
            {
            }
            else
            {
                e.Cancel = true;
            }
        }
    }
}