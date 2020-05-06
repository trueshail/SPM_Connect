using SPMConnectAPI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SearchDataSPM
{
    public partial class ViewRelease : Form
    {
        #region steupvariables

        private DataTable treeTB;
        private DataTable woTB;
        private TreeNode root = new TreeNode();
        private TreeNode woroot = new TreeNode();
        private string workOrder;
        private string assyNumber;
        private string jobNumber;
        private bool rootnodedone;
        private bool worootnodedone;
        private bool isJob = false;
        private WorkOrder connectapi = new WorkOrder();
        private log4net.ILog log;
        private ErrorHandler errorHandler = new ErrorHandler();
        private bool doneshowingSplash = false;

        #endregion steupvariables

        public ViewRelease(string wrkorder, bool job, string jobassyno, string jobno)
        {
            InitializeComponent();
            treeTB = new DataTable();
            woTB = new DataTable();
            this.workOrder = wrkorder;
            this.isJob = job;
            this.assyNumber = jobassyno;
            this.jobNumber = jobno;
        }

        private void ParentView_Load(object sender, EventArgs e)
        {
            if (connectapi.CheckRights("WORelease"))
            {
                Treeview.ContextMenuStrip = Addremovecontextmenu;
                Treeview.AllowDrop = true;
            }
            else
            {
                Treeview.ContextMenuStrip = Addremovecontextmenu;
                Addremovecontextmenu.Items[0].Enabled = false;
                Addremovecontextmenu.Items[0].Visible = false;
                Treeview.AllowDrop = false;
            }
            Text = "Release Log Details - SPM Connect (" + workOrder + ")";
            Startprocessfortreeview();
            WOStartprocessfortreeview();
            woroot.Expand();
            root.Expand();
            log4net.Config.XmlConfigurator.Configure();
            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log.Info("Opened View WorkOrder Release " + workOrder + " ");
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
            log.Info("Closed WorkOrder Release " + workOrder + " ");
            Dispose();
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

                DataRow[] dr = treeTB.Select("[Parent] ='" + workOrder.ToString() + "'");
                if (dr.Length > 0)
                {
                    if (isJob)
                    {
                        root.Text = "(" + dr[0]["JobNo"].ToString() + ") - " + assyNumber + " - " + dr[0]["JobDescription"].ToString();
                        root.Tag = treeTB.Rows.IndexOf(dr[0]);
                        Setimageaccordingtofamily("JOB", root);
                        Treeview.Nodes.Add(root);
                        PopulateTreeView(workOrder, root);
                    }
                    else
                    {
                        root.Text = "WO " + dr[0]["Parent"].ToString() + " - " + dr[0]["Description"].ToString() + " - " + dr[0]["AssyNo"].ToString();
                        root.Tag = treeTB.Rows.IndexOf(dr[0]);
                        Setimageaccordingtofamily("AS", root);
                        Treeview.Nodes.Add(root);
                        PopulateTreeViewFirst(workOrder, root);
                    }
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
                treeTB = connectapi.GrabReleaseLogsBOM(jobNumber);
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

            foreach (DataRow dr in treeTB.Select("[Parent] ='" + parentId.ToString() + "'"))
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
                    var f = new Font("Arial", 9, FontStyle.Bold);
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

            foreach (DataRow dr in treeTB.Select("[Parent] ='" + parentId.ToString() + "'"))
            {
                var t = new TreeNode
                {
                    Text = dr["ReleaseType"].ToString() + " - (" + dr["RlogNo"].ToString() + ")",
                    Name = "Releases",
                    Tag = treeTB.Rows.IndexOf(dr)
                };
                if (parentNode == null)
                {
                    var f = new Font("Arial", 9, FontStyle.Italic);
                    t.NodeFont = f;
                    t.Text = "Job " + dr["JobNo"].ToString() + " - WO " + workOrder + " - " + dr["Description"].ToString();
                    t.Tag = treeTB.Rows.IndexOf(dr);
                    Treeview.Nodes.Add(t);
                    childNode = t;
                }
                else
                {
                    var f = new Font("Arial", 9, FontStyle.Bold);
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

            foreach (DataRow dr in treeTB.Select("[Parent] ='" + parentId.ToString() + "'"))
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
                    if (dr["IsRevised"].ToString() == "1")
                    {
                        t.BackColor = Color.LightYellow;
                    }
                    else if (dr["IsRevised"].ToString() == "0")
                    {
                        t.BackColor = Color.LightGreen;
                    }
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

        private void Treeview_AfterExpand(object sender, TreeViewEventArgs e)
        {
            Expandchk.Checked = true;
        }

        public TreeNode publicnode;

        private void Treeview_Leave(object sender, EventArgs e)
        {
            if (Treeview.Nodes.Count > 0 && Treeview.SelectedNode != null)
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

        #endregion Treeview

        #region search tree release log

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

        #endregion search tree release log

        #region search tree genius work order

        private List<TreeNode> GenCurrentNodeMatches = new List<TreeNode>();

        private int GenLastNodeIndex = 0;

        private string GenLastSearchText;

        private void GenSearchNodes(string SearchText, TreeNode StartNode)
        {
            //TreeNode node = null;
            while (StartNode != null)
            {
                DataRow r = woTB.Rows[int.Parse(StartNode.Tag.ToString())];
                string searchwithin;

                if (StartNode.Parent == null)
                {
                    searchwithin = r["AssyNo"].ToString() + r["Woprec"].ToString() + r["AssyDescription"].ToString() + r["AssyManufacturer"].ToString() + r["AssyManufacturerItemNumber"].ToString();
                }
                else
                {
                    searchwithin = r["ItemNumber"].ToString() + r["Wo"].ToString() + r["Description"].ToString() + r["Manufacturer"].ToString() + r["ManufacturerItemNumber"].ToString();
                }

                if (searchwithin.ToLower().Contains(SearchText.ToLower()))
                {
                    GenCurrentNodeMatches.Add(StartNode);
                };
                if (StartNode.Nodes.Count != 0)
                {
                    GenSearchNodes(SearchText, StartNode.Nodes[0]);//Recursive Search
                };
                StartNode = StartNode.NextNode;
            }
        }

        private void woserchtxt_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            txtSearch.Clear();
            wosearchlabel.Text = "Search:";
        }

        private void woserchtxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (woserchtxt.Text.Length > 0)
            {
                try
                {
                    string searchText = this.woserchtxt.Text;
                    if (e.KeyCode == Keys.Return)
                    {
                        woserchtxt.Select();
                        if (String.IsNullOrEmpty(searchText))
                        {
                            return;
                        };

                        if (GenLastSearchText != searchText)
                        {
                            //It's a new Search
                            GenCurrentNodeMatches.Clear();
                            GenLastSearchText = searchText;
                            GenLastNodeIndex = 0;
                            GenSearchNodes(searchText, woTreeview.Nodes[0]);
                        }

                        if (GenLastNodeIndex >= 0 && GenCurrentNodeMatches.Count > 0 && GenLastNodeIndex < GenCurrentNodeMatches.Count)
                        {
                            TreeNode selectedNode = GenCurrentNodeMatches[GenLastNodeIndex];
                            GenLastNodeIndex++;
                            this.woTreeview.SelectedNode = selectedNode;
                            this.woTreeview.SelectedNode.Expand();
                            this.woTreeview.Select();
                            if (woserchtxt.Text.Length > 0)
                                wosearchlabel.Text = "Found " + GenLastNodeIndex + " of " + GenCurrentNodeMatches.Count + " matching items containing keyword \"" + searchText + "\"";
                            else wosearchlabel.Text = "Search:";
                        }
                        else
                        {
                            GenLastSearchText = "";
                        }

                        e.Handled = true;
                        e.SuppressKeyPress = true;
                    }
                }
                catch (Exception)
                {
                }
            }
            else wosearchlabel.Text = "Search:";
        }

        #endregion search tree genius work order

        private void ShowReleaseLogDetails(string invoice)
        {
            string invoiceopen = connectapi.InvoiceOpen(invoice, ConnectAPI.CheckInModules.WO);
            if (invoiceopen.Length > 0)
            {
                MetroFramework.MetroMessageBox.Show(this, "Release Document is opened for edit by " + invoiceopen + ". ", "SPM Connect - Open Release Document Failed", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                if (connectapi.CheckinInvoice(invoice, ConnectAPI.CheckInModules.WO))
                {
                    using (AddRelease addrelease = new AddRelease(releaseLogNo: invoice))
                    {
                        addrelease.ShowDialog();
                        ProcesstreeviewReload();
                        root.Expand();
                    }
                }
            }
        }

        private void Addremovecontextmenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Treeview.SelectedNode != null)
            {
                if (Treeview.SelectedNode.Name == "Releases")
                {
                }
                else
                {
                    e.Cancel = true;
                }
            }
            else
                e.Cancel = true;
        }

        #region Genius  WO Treeview

        public TreeNode genpublicnode;

        private void woTreeview_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Down))
            {
                var node = new TreeNode();
                node = woTreeview.SelectedNode;
                woTreeview.SelectedNode = node.NextVisibleNode;
                node.TreeView.Focus();
            }
            else if (e.KeyChar == Convert.ToChar(Keys.Up))
            {
                var node = new TreeNode();
                node = woTreeview.SelectedNode;
                woTreeview.SelectedNode = node.NextVisibleNode;
                node.TreeView.Focus();
            }
        }

        private void woTreeview_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                woserchtxt.Focus();
                woserchtxt.Select();
                SendKeys.Send("~");
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void woTreeview_Leave(object sender, EventArgs e)
        {
            if (woTreeview.Nodes.Count > 0 && woTreeview.SelectedNode != null)
            {
                genpublicnode = woTreeview.SelectedNode;
                genpublicnode.BackColor = Color.LightBlue;
            }
        }

        private void woTreeview_AfterExpand(object sender, TreeViewEventArgs e)
        {
            woexpand.Checked = true;
        }

        private void woTreeview_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.ImageIndex == 1)
            {
                e.Node.SelectedImageIndex = 0;
                e.Node.ImageIndex = 0;
            }
        }

        private void woTreeview_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.ImageIndex == 0)
            {
                e.Node.SelectedImageIndex = 1;
                e.Node.ImageIndex = 1;
            }
        }

        private void woTreeview_BeforeSelect(object sender, TreeViewCancelEventArgs e)
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

        private void woexpand_Click(object sender, EventArgs e)
        {
            if (woexpand.Checked)
            {
                woTreeview.ExpandAll();
            }
            else
            {
                woTreeview.CollapseAll();
            }
        }

        private void WOCleanup()
        {
            woTreeview.Nodes.Clear();
            woTreeview.ResetText();
            RemoveChildNodes(woroot);
            woTB.Clear();
            woexpand.Checked = false;
        }

        private void WOFillrootnode()
        {
            try
            {
                woTreeview.Nodes.Clear();
                RemoveChildNodes(woroot);
                woTreeview.ResetText();
                woexpand.Checked = false;

                DataRow[] dr = woTB.Select("AssyNo = '" + assyNumber + "'");
                if (dr.Length > 0)
                {
                    woroot.Text = "(" + dr[0]["Job"].ToString() + ") " + dr[0]["AssyNo"].ToString() + " - " + dr[0]["AssyDescription"].ToString();
                    woroot.Tag = woTB.Rows.IndexOf(dr[0]);
                    Setimageaccordingtofamily(dr[0]["AssyFamily"].ToString(), woroot);
                    woTreeview.Nodes.Add(woroot);
                    WOPopulateTreeView(assyNumber, woroot, connectapi.GrabWOfromAssy(dr[0]["Job"].ToString(), dr[0]["AssyNo"].ToString()));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                woTreeview.TopNode.Nodes.Clear();
                woTreeview.Nodes.Clear();
                RemoveChildNodes(woroot);
                woTreeview.ResetText();
            }
        }

        private void WOStartprocessfortreeview()
        {
            try
            {
                woTreeview.Nodes.Clear();
                RemoveChildNodes(woroot);
                woTreeview.ResetText();
                woTB.Clear();
                woTB = connectapi.GrabJobWOBOM(isJob ? workOrder : connectapi.GetJobNoFromWO(workOrder));
                WOFillrootnode();
                WOCallRecursive();
            }
            catch (Exception)
            {
            }
        }

        private void WOPopulateTreeView(string parentId, TreeNode parentNode, string wo)
        {
            TreeNode childNode;

            foreach (DataRow dr in woTB.Select("[AssyNo] = '" + parentId.ToString() + "' AND [Woprec] = '" + wo.ToString() + "'"))
            {
                TreeNode t = new TreeNode
                {
                    Text = dr["ItemNumber"].ToString() + " (" + dr["WO"].ToString() + ") - " + dr["Description"].ToString() + " ( " + dr["QuantityPerAssembly"].ToString() + " )",
                    Name = dr["ItemNumber"].ToString(),
                    Tag = woTB.Rows.IndexOf(dr)
                };
                if (parentNode == null)
                {
                    Font f = new Font("Arial", 10, FontStyle.Bold);
                    t.NodeFont = f;
                    t.Text = dr["AssyNo"].ToString() + " - " + dr["AssyDescription"].ToString() + " ( " + dr["QuantityPerAssembly"].ToString() + " ) ";
                    t.Name = dr["ItemNumber"].ToString();
                    t.Tag = woTB.Rows.IndexOf(dr);
                    woTreeview.Nodes.Add(t);
                    childNode = t;
                }
                else
                {
                    Font f = new Font("Arial", 10, FontStyle.Bold);
                    // t.NodeFont = f;
                    parentNode.Nodes.Add(t);
                    childNode = t;
                }
                if (dr["ItemNumber"].ToString() != dr["AssyNo"].ToString())
                    WOPopulateTreeView(dr["ItemNumber"].ToString(), childNode, dr["Wo"].ToString());
                //WOPopulateTreeView(dr["ItemNumber"].ToString(), childNode);
            }
            // treeView1.SelectedNode = treeView1.Nodes[0];
        }

        private void WOCallRecursive()
        {
            // Print each node recursively.

            var nodes = woTreeview.Nodes;
            foreach (TreeNode n in nodes)
            {
                if (n.Nodes.Count > 0)
                {
                    WOPrintRecursive(n);
                }
            }
        }

        private void WOPrintRecursive(TreeNode treeNode)
        {
            // Print the node.
            if (treeNode.Nodes.Count == 0)
            {
            }

            if (treeNode.Index == 0 && worootnodedone == false)
            {
                worootnodedone = true;
            }
            else
            {
                DataRow r = woTB.Rows[int.Parse(treeNode.Tag.ToString())];
                string family = r["ItemFamily"].ToString();
                Setimageaccordingtofamily(family, treeNode);
            }

            // Print each node recursively.
            foreach (TreeNode tn in treeNode.Nodes)
                WOPrintRecursive(tn);
        }

        #endregion Genius  WO Treeview

        private void reloadconnectbttn_Click(object sender, EventArgs e)
        {
            ProcesstreeviewReload();
        }

        private async void ProcesstreeviewReload()
        {
            await Task.Run(() => SplashDialog("Refreshing Data...."));
            Thread.Sleep(1000);
            Cleanup();
            CleanupTextboxes();
            rootnodedone = false;
            Startprocessfortreeview();
            doneshowingSplash = true;
            root.Expand();
        }

        private async void reloadwobttn_Click(object sender, EventArgs e)
        {
            await Task.Run(() => SplashDialog("Refreshing Data...."));
            Thread.Sleep(1000);
            WOCleanup();
            CleanupTextboxes();
            worootnodedone = false;
            WOStartprocessfortreeview();
            doneshowingSplash = true;
            woroot.Expand();
        }

        private void SplashDialog(string message)
        {
            doneshowingSplash = false;
            ThreadPool.QueueUserWorkItem((x) =>
            {
                using (var splashForm = new Dialog())
                {
                    splashForm.TopMost = true;
                    splashForm.Focus();
                    splashForm.Activate();
                    splashForm.Message = message;
                    splashForm.Location = new Point(this.Location.X + (this.Width - splashForm.Width) / 2, this.Location.Y + (this.Height - splashForm.Height) / 2);
                    splashForm.Show();
                    while (!doneshowingSplash)
                        Application.DoEvents();
                    splashForm.Close();
                }
            });
        }

        private void CleanupTextboxes()
        {
            releaselognotxt.Clear();
            jobnotxt.Clear();
            wotxt.Clear();
            assynotxt.Clear();
            assydesctxt.Clear();
            releasetypetxt.Clear();
            releasenotestxt.Clear();
            createdbytxt.Clear();
            datecreatedtxt.Clear();
            dateeditxt.Clear();
            Lastsavedtxtbox.Clear();
            itemnotxt.Clear();
            itemdestxt.Clear();
            oemtxt.Clear();
            oemitemtxt.Clear();
            qtytxt.Clear();
            itmnotestxt.Clear();
        }

        private void Tree_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            TreeNode node = woTreeview.GetNodeAt(e.X, e.Y);
            woTreeview.SelectedNode = node;

            if (node != null)
            {
                woTreeview.DoDragDrop(node, DragDropEffects.Copy);
            }
        }

        private bool IsAssembly(string family)
        {
            bool assy = false;

            switch (family.ToLower())
            {
                case "as":
                    assy = true;
                    break;

                case "asel":
                    assy = true;
                    break;

                case "aspn":
                    assy = true;
                    break;

                default:
                    assy = false;
                    break;
            }

            return assy;
        }

        private void Treeview_DragDrop(object sender, DragEventArgs e)
        {
            DialogResult result = MetroFramework.MetroMessageBox.Show(this, "Are you sure want to create a new release?", "SPM Connect - Create New Release?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                TreeNode nodeTarget = root;
                TreeNode nodeSource = (TreeNode)e.Data.GetData(typeof(TreeNode));
                if (nodeSource == woroot)
                {
                    DataRow r = woTB.Rows[int.Parse(nodeSource.Tag.ToString())];
                    string family = r["AssyFamily"].ToString();
                    if (IsAssembly(family))
                    {
                        // check if the item exists in the tree
                        // if yes
                        string assyno = r["AssyNo"].ToString();
                        string wo = workOrder;
                        string job = r["Job"].ToString();

                        string rlogno = connectapi.EnterWOToReleaseLog(wo, job, assyno);
                        if (rlogno.Length > 1)
                        {
                            ShowReleaseLogDetails(rlogno);
                        }

                        //nodeTarget.Nodes.Add((TreeNode)nodeSource.Clone());
                        //nodeTarget.Expand();
                    }
                    else
                    {
                        MetroFramework.MetroMessageBox.Show(this, "Item family must be a \"AS\" or \"ASEL\" or \"ASPN\". In order to release into the system.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }
                else
                {
                    DataRow r = woTB.Rows[int.Parse(nodeSource.Tag.ToString())];
                    string family = r["ItemFamily"].ToString();
                    if (IsAssembly(family))
                    {
                        // check if the item exists in the tree
                        // if yes
                        string assyno = r["ItemNumber"].ToString();
                        string wo = r["WO"].ToString();
                        string job = r["Job"].ToString();

                        string rlogno = connectapi.EnterWOToReleaseLog(wo, job, assyno);
                        if (rlogno.Length > 1)
                        {
                            ShowReleaseLogDetails(rlogno);
                        }

                        //nodeTarget.Nodes.Add((TreeNode)nodeSource.Clone());
                        nodeTarget.Expand();
                    }
                    else
                    {
                        MetroFramework.MetroMessageBox.Show(this, "Item family must be a \"AS\" or \"ASEL\" or \"ASPN\". In order to release into the system.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }
            }
        }

        private bool _SearchNodes(string SearchText, TreeNode StartNode)
        {
            // TreeNode node = null;
            bool itemexists = false;
            while (StartNode != null)
            {
                if (StartNode.Text.ToLower().Contains(SearchText.ToLower()))
                {
                    MessageBox.Show("Item already added to the assembly list", "SPM Conect", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    itemexists = true;
                    Treeview.SelectedNode = StartNode;
                    CurrentNodeMatches.Add(StartNode);
                }

                if (StartNode.Nodes.Count != 0)
                {
                    _SearchNodes(SearchText, StartNode.Nodes[0]);//Recursive Search
                }

                StartNode = StartNode.NextNode;
            }
            return itemexists;
        }

        private void Treeview_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.None;

            TreeNode nodeSource = (TreeNode)e.Data.GetData(typeof(TreeNode));
            if (nodeSource != null)
            {
                if (nodeSource.TreeView != Treeview)
                {
                    TreeNode nodeTarget = root;
                    if (nodeTarget != null)
                    {
                        e.Effect = DragDropEffects.Copy;
                        Treeview.SelectedNode = nodeTarget;
                    }
                }
            }
        }

        private void getWOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProrcessreportWorkOrder(Getselectedworkorder(woTreeview.SelectedNode), "WorkOrder");
        }

        private void ProrcessreportWorkOrder(string itemvalue, string Reportname)
        {
            ReportViewer form1 = new ReportViewer(Reportname, itemvalue);
            form1.Show();
        }

        private void ProrcessReleasereportWorkOrder(string itemvalue, string Reportname, string worelease, string wonotes)
        {
            ReportViewer form1 = new ReportViewer(Reportname, itemvalue, wonotes: wonotes, woreleasetype: worelease);
            form1.Show();
        }

        private string Getselectedworkorder(TreeNode treeNode)
        {
            string wo = "";
            if (treeNode != null)
            {
                DataRow r = woTB.Rows[int.Parse(treeNode.Tag.ToString())];
                wo = r["WO"].ToString();
            }

            return wo;
        }

        private string Getselectedreleaseworkorder(TreeNode treeNode)
        {
            string wo = "";
            if (treeNode != null)
            {
                DataRow r = treeTB.Rows[int.Parse(treeNode.Tag.ToString())];
                wo = r["WO"].ToString();
            }

            return wo;
        }

        private string GetselectedreleaseworkorderReleaseNotes(TreeNode treeNode)
        {
            string wo = "";
            if (treeNode != null)
            {
                DataRow r = treeTB.Rows[int.Parse(treeNode.Tag.ToString())];
                wo = r["ReleaseNotes"].ToString();
            }

            return wo;
        }

        private string GetselectedreleaseworkorderReleaseType(TreeNode treeNode)
        {
            string wo = "";
            if (treeNode != null)
            {
                DataRow r = treeTB.Rows[int.Parse(treeNode.Tag.ToString())];
                wo = r["ReleaseType"].ToString();
            }
            if (wo == "Initial Release")
                return "";
            return wo;
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //if (woTreeview.SelectedNode != null)
            //{
            //}
            //else
            //{
            //    e.Cancel = true;
            //}
        }

        private void woTreeview_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            woTreeview.SelectedNode = e.Node;
        }

        private void viewWorkOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProrcessReleasereportWorkOrder(Getselectedreleaseworkorder(Treeview.SelectedNode), "WorkOrder", GetselectedreleaseworkorderReleaseType(Treeview.SelectedNode), GetselectedreleaseworkorderReleaseNotes(Treeview.SelectedNode));
        }
    }
}