using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SearchDataSPM
{
    public partial class AutocadWhereUsed : Form

    {
        #region setup variables

        private readonly string connection;
        private readonly string cntrlconnection;
        private readonly DataTable _acountsTb;
        private readonly DataTable _productTB;
        private readonly SqlConnection _connection;
        private readonly SqlCommand _command;
        private SqlDataAdapter _adapter;

        //SqlDataAdapter _adapaterproduct;
        private readonly TreeNode root = new TreeNode();

        private string txtvalue;
        private log4net.ILog log;

        #endregion setup variables

        #region whereused loading

        public AutocadWhereUsed()
        {
            InitializeComponent();
            connection = ConfigurationManager.ConnectionStrings["SearchDataSPM.Properties.Settings.cn"].ConnectionString;
            cntrlconnection = ConfigurationManager.ConnectionStrings["SearchDataSPM.Properties.Settings.cntrlscn"].ConnectionString;

            try
            {
                _connection = new SqlConnection(cntrlconnection);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            _ = Assy_txtbox.Text;
            _acountsTb = new DataTable();
            _command = new SqlCommand
            {
                Connection = _connection
            };
            _productTB = new DataTable();
        }

        private string itemnumber;

        public string item(string item)
        {
            if (item.Length > 0)
                return itemnumber = item;
            return null;
        }

        private void ParentView_Load(object sender, EventArgs e)
        {
            Assy_txtbox.Focus();

            //Assy_txtbox.Text = SPM_ConnectControls.whereusedcontrols;
            Assy_txtbox.Text = itemnumber;

            if (Assy_txtbox.Text.Length == 5 || Assy_txtbox.Text.Length == 6)
            {
                itemnumber = null;
                startprocessofwhereused();
            }
            Assy_txtbox.Select();
            log4net.Config.XmlConfigurator.Configure();
            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log.Info("Opened AutoCadWhereUsed Form ");
        }

        private void filldatatable()
        {
            const String sql = "SELECT *  FROM [SPMControlCatalog].[dbo].[ControlsBOM] ORDER BY [QUERY2]";
            try
            {
                _acountsTb.Clear();
                _connection.Open();
                _adapter = new SqlDataAdapter(sql, _connection);
                _adapter.Fill(_acountsTb);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _connection.Close();
            }
        }

        #endregion whereused loading

        #region assytextbox and button events

        private void Assy_txtbox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            cleanup();
        }

        private void cleanup()
        {
            treeView1.Nodes.Clear();
            treeView1.ResetText();
            RemoveChildNodes(root);
            _acountsTb.Clear();
            Assy_txtbox.Clear();
            Expandchk.Checked = false;
            txtSearch.Clear();
            ItemTxtBox.Clear();
            Descriptiontxtbox.Clear();
            oemtxtbox.Clear();
            oemitemtxtbox.Clear();
            qtytxtbox.Clear();
        }

        private void cleanup2()
        {
            treeView1.Nodes.Clear();
            treeView1.ResetText();
            RemoveChildNodes(root);
            _acountsTb.Clear();
            Expandchk.Checked = false;
            txtSearch.Clear();
            ItemTxtBox.Clear();
            Descriptiontxtbox.Clear();
            oemtxtbox.Clear();
            oemitemtxtbox.Clear();
            qtytxtbox.Clear();
        }

        private string assemblycode;

        private void Assy_txtbox_KeyDown(object sender, KeyEventArgs e)
        {
            txtvalue = Assy_txtbox.Text;

            if (e.KeyCode == Keys.Return)
            {
                startprocessofwhereused();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void startprocessofwhereused()
        {
            txtvalue = Assy_txtbox.Text;
            try
            {
                treeView1.Nodes.Clear();
                RemoveChildNodes(root);
                treeView1.ResetText();
                filldatatable();
                fillrootnode();
            }
            catch (Exception)

            {
                if (!String.IsNullOrEmpty(txtvalue) && Char.IsLetter(txtvalue[0]))
                {
                    MessageBox.Show(" Item does not belong to any assembly in AutoCad.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show("Invalid Search Parameter / Item Not Found On Autocad Catalog.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cleanup2();
                    Assy_txtbox.BackColor = Color.IndianRed; //to add high light
                }
            }
        }

        private void fillrootnode()
        {
            Assy_txtbox.BackColor = Color.White; //to add high light
            try
            {
                treeView1.Nodes.Clear();
                RemoveChildNodes(root);
                treeView1.ResetText();
                Expandchk.Checked = false;
                //DataRow[] dr = _productTB.Select("ItemNumber = '" + txtvalue.ToString() + "'");
                DataRow[] dr = _acountsTb.Select("QUERY2 = '" + txtvalue + "'");
                if (dr.Length > 0)
                {
                    root.Text = dr[0]["QUERY2"].ToString() + " - " + dr[0]["DESCRIPTION"].ToString();
                    root.Tag = _acountsTb.Rows.IndexOf(dr[0]);

                    //Font f = FontStyle.Bold);
                    // root.NodeFont = f;

                    treeView1.Nodes.Add(root);
                    PopulateTreeView(Assy_txtbox.Text, root);

                    chekroot = "Item";

                    ItemTxtBox.Text = dr[0]["QUERY2"].ToString();
                    Descriptiontxtbox.Text = dr[0]["DESCRIPTION"].ToString();
                    oemtxtbox.Text = dr[0]["USER3"].ToString();
                    oemitemtxtbox.Text = dr[0]["TEXTVALUE"].ToString();
                    assemblycode = dr[0]["ASSEMBLYLIST"].ToString();
                    // PopulateTreeView(assemblycode, root);
                }
            }
            catch
            {
                treeView1.TopNode.Nodes.Clear();
                treeView1.Nodes.Clear();
                RemoveChildNodes(root);
                treeView1.ResetText();
                Expandchk.Checked = false;
            }
        }

        private void PopulateTreeView(string parentId, TreeNode parentNode)
        {
            TreeNode childNode;

            foreach (DataRow dr in _acountsTb.Select("[QUERY2] ='" + parentId + "'"))
            {
                TreeNode t = new TreeNode
                {
                    Text = dr["AssyNo"].ToString() + " - " + dr["AssyDesc"].ToString() + " ( " + dr["AssyQty"].ToString() + " ) ",
                    Name = dr["AssyNo"].ToString(),
                    Tag = _acountsTb.Rows.IndexOf(dr)
                };
                if (parentNode == null)
                {
                    Font f = new Font("Arial", 10, FontStyle.Bold);
                    t.NodeFont = f;
                    t.Text = dr["AssyNo"].ToString() + " - " + dr["AssyDesc"].ToString() + " ( " + dr["AssyQty"].ToString() + " ) ";
                    t.Name = dr["AssyNo"].ToString();
                    t.Tag = _acountsTb.Rows.IndexOf(dr);
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
                PopulateTreeView(dr["AssyNo"].ToString(), childNode);
            }

            treeView1.ExpandAll();
        }

        private string ItemNo;
        private string str;

        private void Expandchk_Click(object sender, EventArgs e)
        {
            if (Expandchk.Checked)
            {
                treeView1.ExpandAll();
            }
            else
            {
                treeView1.CollapseAll();
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

        private void SPM_DoubleClick(object sender, EventArgs e)
        {
            Process.Start("http://www.spm-automation.com/");
        }

        #endregion assytextbox and button events

        #region open model and drawing

        private void checkforspmfile(string first3char)
        {
            string ItemNumbero = ItemNo + "-0";

            if (!String.IsNullOrWhiteSpace(ItemNo) && ItemNo.Length == 6)

            {
                first3char = ItemNo.Substring(0, 3) + @"\";
                //MessageBox.Show(first3char);

                const string spmcadpath = @"\\spm-adfs\CAD Data\AAACAD\";

                string Pathpart = (spmcadpath + first3char + ItemNo + ".sldprt");
                string Pathassy = (spmcadpath + first3char + ItemNo + ".sldasm");
                string PathPartNo = (spmcadpath + first3char + ItemNumbero + ".sldprt");
                string PathAssyNo = (spmcadpath + first3char + ItemNumbero + ".sldasm");

                if (File.Exists(Pathassy) && File.Exists(Pathpart))
                {
                    MessageBox.Show($"System has found a Part file and Assembly file with the same PartNo." + ItemNo + "." +
                        " So please contact the administrator.", "SPM-Automation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (File.Exists(PathAssyNo) && File.Exists(PathPartNo))
                {
                    MessageBox.Show($"System has found a Part file and Assembly file with the same PartNo. " + ItemNumbero + "." +
                        " So please contact the administrator.", "SPM-Automation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (File.Exists(PathAssyNo) && File.Exists(Pathpart))
                {
                    MessageBox.Show($"System has found a Part file " + ItemNo + "and Assembly file " + ItemNumbero + " with the same PartNo." +
                        " So please contact the administrator.", "SPM-Automation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (File.Exists(Pathassy) && File.Exists(PathPartNo))
                {
                    MessageBox.Show($"System has found a Part file " + ItemNumbero + "and Assembly file" + ItemNo + " with the same PartNo." +
                        " So please contact the administrator.", "SPM-Automation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (File.Exists(PathPartNo) && File.Exists(Pathpart))
                {
                    MessageBox.Show($"System has found a Part two files " + ItemNo + "," + ItemNumbero + " with the same PartNo." +
                        " So please contact the administrator.", "SPM-Automation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (File.Exists(PathAssyNo) && File.Exists(Pathassy))
                {
                    MessageBox.Show($"System has found a assembly files " + ItemNo + "," + ItemNumbero + " with the same PartNo." +
                        " So please contact the administrator.", "SPM-Automation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (File.Exists(Pathassy))
                {
                    Process.Start("explorer.exe", Pathassy);
                }
                else if (File.Exists(PathAssyNo))
                {
                    Process.Start("explorer.exe", PathAssyNo);
                }
                else if (File.Exists(Pathpart))
                {
                    Process.Start("explorer.exe", Pathpart);
                }
                else if (File.Exists(PathPartNo))
                {
                    Process.Start("explorer.exe", PathPartNo);
                }
                else
                {
                    MessageBox.Show($"A file with the part number" + ItemNo + " does not have Solidworks CAD Model. Please Try Again.", "SPM-Automation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
            }
        }

        private void checkforspmdrwfile(string first3char)
        {
            string ItemNumbero = str + "-0";

            if (!String.IsNullOrWhiteSpace(str) && str.Length == 6)

            {
                first3char = str.Substring(0, 3) + @"\";
                //MessageBox.Show(first3char);

                const string spmcadpath = @"\\spm-adfs\CAD Data\AAACAD\";

                string Drawpath = (spmcadpath + first3char + str + ".SLDDRW");

                string drawpathno = (spmcadpath + first3char + ItemNumbero + ".SLDDRW");

                if (File.Exists(drawpathno) && File.Exists(Drawpath))
                {
                    MessageBox.Show($"System has found a Part two files " + ItemNo + "," + ItemNumbero + " with the same PartNo." +
                        " So please contact the administrator.", "SPM-Automation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (File.Exists(Drawpath))
                {
                    Process.Start("explorer.exe", Drawpath);
                }
                else if (File.Exists(drawpathno))
                {
                    Process.Start("explorer.exe", drawpathno);
                }
                else
                {
                    MessageBox.Show($"A file with the part number" + ItemNo + " does not have Solidworks Drawing File. Please Try Again.", "SPM-Automation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
            }
        }

        private void openModelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ItemNo = treeView1.SelectedNode.Text;
            ItemNo = ItemNo.Substring(0, 6);
            //MessageBox.Show(ItemNo);
            checkforspmfile(ItemNo);
        }

        private void openDrawingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            str = treeView1.SelectedNode.Text;
            str = str.Substring(0, 6);
            //MessageBox.Show(ItemNo);
            checkforspmdrwfile(str);
        }

        #endregion open model and drawing

        #region search tree

        private readonly List<TreeNode> CurrentNodeMatches = new List<TreeNode>();

        private int LastNodeIndex;

        private string LastSearchText;

        private void SearchNodes(string SearchText, TreeNode StartNode)
        {
            //TreeNode node = null;
            while (StartNode != null)
            {
                DataRow r = _acountsTb.Rows[int.Parse(StartNode.Tag.ToString())];
                string searchwithin = StartNode.Parent != null
                    ? r["AssyNo"].ToString() + r["AssyDesc"].ToString() + r["AssyManufacturer"].ToString() + r["AssyOem"].ToString()
                    : r["QUERY2"].ToString() + r["DESCRIPTION"].ToString() + r["USER3"].ToString() + r["TEXTVALUE"].ToString();
                if (searchwithin.IndexOf(SearchText, StringComparison.CurrentCultureIgnoreCase) >= 0)
                {
                    CurrentNodeMatches.Add(StartNode);
                }
                if (StartNode.Nodes.Count != 0)
                {
                    SearchNodes(SearchText, StartNode.Nodes[0]);//Recursive Search
                }
                StartNode = StartNode.NextNode;
            }
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
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
                    }

                    if (LastSearchText != searchText)
                    {
                        //It's a new Search
                        CurrentNodeMatches.Clear();
                        LastSearchText = searchText;
                        LastNodeIndex = 0;
                        SearchNodes(searchText, treeView1.Nodes[0]);
                    }

                    if (LastNodeIndex >= 0 && CurrentNodeMatches.Count > 0 && LastNodeIndex < CurrentNodeMatches.Count)
                    {
                        TreeNode selectedNode = CurrentNodeMatches[LastNodeIndex];
                        LastNodeIndex++;
                        this.treeView1.SelectedNode = selectedNode;
                        this.treeView1.SelectedNode.Expand();
                        this.treeView1.Select();
                    }
                    else
                    {
                        LastSearchText = "";
                    }

                    e.Handled = true;
                    e.SuppressKeyPress = true;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
            }
        }

        private void txtSearch_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            txtSearch.Clear();
        }

        #endregion search tree

        #region treeview events

        private void treeView1_AfterExpand(object sender, TreeViewEventArgs e)
        {
            Expandchk.Checked = true;
        }

        private void treeView1_KeyDown(object sender, KeyEventArgs e)
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

        private string chekroot;

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (root.IsSelected && chekroot == "Item")
            {
                DataRow r = _acountsTb.Rows[int.Parse(treeView1.SelectedNode.Tag.ToString())];
                ItemTxtBox.Text = r["QUERY2"].ToString();
                Descriptiontxtbox.Text = r["DESCRIPTION"].ToString();
                oemtxtbox.Text = r["USER3"].ToString();
                oemitemtxtbox.Text = r["TEXTVALUE"].ToString();
                qtytxtbox.Text = r["AssyQty"].ToString();
            }
            else
            {
                DataRow r = _acountsTb.Rows[int.Parse(treeView1.SelectedNode.Tag.ToString())];
                ItemTxtBox.Text = r["AssyNo"].ToString();
                Descriptiontxtbox.Text = r["AssyDesc"].ToString();
                oemtxtbox.Text = r["AssyManufacturer"].ToString();
                oemitemtxtbox.Text = r["AssyOem"].ToString();
                qtytxtbox.Text = r["AssyQty"].ToString();
            }
        }

        private void treeView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Down))
            {
                _ = new TreeNode();
                TreeNode node = treeView1.SelectedNode;
                treeView1.SelectedNode = node.NextVisibleNode;
                node.TreeView.Focus();
            }
            else if (e.KeyChar == Convert.ToChar(Keys.Up))
            {
                _ = new TreeNode();
                TreeNode node = treeView1.SelectedNode;
                treeView1.SelectedNode = node.NextVisibleNode;
                node.TreeView.Focus();
            }
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            treeView1.SelectedNode = e.Node;
        }

        #endregion treeview events

        private void AutocadWhereUsed_FormClosed(object sender, FormClosedEventArgs e)
        {
            log.Info("Closed AutoCadWhereUsed Form ");
            this.Dispose();
        }
    }
}