using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace SearchDataSPM.Controls
{
    public partial class AutocadAssembly : Form

    {
        #region steupvariables

        private readonly string cntrlconnection;
        private readonly DataTable _acountsTb;
        private readonly SqlConnection _connection;
        private readonly SqlCommand _command;
        private SqlDataAdapter _adapter;
        private readonly TreeNode root = new TreeNode();
        private string txtvalue;
        private log4net.ILog log;

        #endregion steupvariables

        #region loadtree

        public AutocadAssembly()
        {
            InitializeComponent();
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
        }

        private string itemnumber;

        public string Item(string item)
        {
            if (item.Length > 0)
                return itemnumber = item;
            return null;
        }

        private void ParentView_Load(object sender, EventArgs e)
        {
            Assy_txtbox.Select();
            savebttn.Visible = false;
            qtylbl.Visible = false;
            qtytxtbox.Visible = false;

            Assy_txtbox.Focus();

            //Assy_txtbox.Text = SPM_ConnectControls.assytree;
            Assy_txtbox.Text = itemnumber;

            if (Assy_txtbox.Text.Length == 5 || Assy_txtbox.Text.Length == 6)
            {
                Assy_txtbox.Focus();
                itemnumber = null;
                //SendKeys.Send("~");
                startprocessofwhereused();
            }
            log4net.Config.XmlConfigurator.Configure();
            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log.Info("Opened AutoCadAssembly Form ");
        }

        #endregion loadtree

        #region assytextbox and button events

        private void Assy_txtbox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // treeView1.TopNode.Nodes.Clear();
            treeView1.Nodes.Clear();
            treeView1.ResetText();
            RemoveChildNodes(root);
            Assy_txtbox.Clear();
            Expandchk.Checked = false;
            txtSearch.Clear();
            ItemTxtBox.Clear();
            Descriptiontxtbox.Clear();
            oemtxtbox.Clear();
            oemitemtxtbox.Clear();
            qtytxtbox.Clear();
            _acountsTb.Clear();
            // SendKeys.Send("~");
        }

        private void filldatatable()
        {
            const string sql = "SELECT *  FROM [SPMControlCatalog].[dbo].[ControlsBOM] ORDER BY [QUERY2]";
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

        private void Assy_txtbox_KeyDown(object sender, KeyEventArgs e)
        {
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
                Fillrootnode();
            }
            catch (Exception)

            {
                if (!String.IsNullOrEmpty(txtvalue) && Char.IsLetter(txtvalue[0]))
                {
                    MessageBox.Show(" Item does not contain a BOM.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show("Invalid Search Parameter / Item Not Found On Autocad Catalog.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Cleanup2();
                    Assy_txtbox.BackColor = Color.IndianRed; //to add high light
                }
            }
        }

        private void Cleanup2()
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

        private void Fillrootnode()
        {
            Assy_txtbox.BackColor = Color.White; //to add high light
            try
            {
                treeView1.Nodes.Clear();
                RemoveChildNodes(root);
                treeView1.ResetText();
                Expandchk.Checked = false;
                DataRow[] dr = _acountsTb.Select("AssyNo = '" + txtvalue + "'");
                if (dr.Length > 0)
                {
                    root.Text = dr[0]["AssyNo"].ToString() + " - " + dr[0]["AssyDesc"].ToString();
                    root.Tag = dr[0]["AssyNo"].ToString() + "][" + dr[0]["AssyDesc"].ToString() + "][" + dr[0]["AssyNo"].ToString() + "][" + dr[0]["AssyManufacturer"].ToString() + "][" + dr[0]["AssyOem"].ToString() + "][1";

                    treeView1.Nodes.Add(root);

                    PopulateTreeView(Assy_txtbox.Text, root);

                    chekroot = "Assy";
                    ItemTxtBox.Text = dr[0]["AssyNo"].ToString();
                    Descriptiontxtbox.Text = dr[0]["AssyDesc"].ToString();
                    oemtxtbox.Text = dr[0]["AssyManufacturer"].ToString();
                    oemitemtxtbox.Text = dr[0]["AssyOem"].ToString();
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

            foreach (DataRow dr in _acountsTb.Select("[ASSEMBLYLIST] ='" + parentId + "'"))
            {
                TreeNode t = new TreeNode
                {
                    Text = dr["QUERY2"].ToString() + " - " + dr["DESCRIPTION"].ToString() + " ( " + dr["AssyQty"].ToString() + " ) ",
                    Name = dr["QUERY2"].ToString(),
                    Tag = dr["QUERY2"].ToString() + "][" + dr["DESCRIPTION"].ToString() + "][" + dr["QUERY2"].ToString() + "][" + dr["USER3"].ToString() + "][" + dr["TEXTVALUE"].ToString() + "][" + dr["AssyQty"].ToString()
                };
                if (parentNode == null)
                {
                    Font f = new Font("Arial", 10, FontStyle.Bold);
                    t.NodeFont = f;
                    t.Text = dr["AssyNo"].ToString() + " - " + dr["DESCRIPTION"].ToString() + " ( " + dr["AssyQty"].ToString() + " ) ";
                    t.Name = dr["AssyNo"].ToString();
                    t.Tag = dr["AssyNo"].ToString() + "][" + dr["AssyDesc"].ToString() + "][" + dr["AssyNo"].ToString() + "][" + dr["AssyManufacturer"].ToString() + "][" + dr["AssyOem"].ToString() + "][1";
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
                PopulateTreeView(dr["QUERY2"].ToString(), childNode);
            }
            // treeView1.SelectedNode = treeView1.Nodes[0];
            treeView1.ExpandAll();
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

        private void SPM_DoubleClick(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.spm-automation.com/");
        }

        #endregion assytextbox and button events

        #region search tree

        private readonly List<TreeNode> CurrentNodeMatches = new List<TreeNode>();

        private int LastNodeIndex;

        private string LastSearchText;

        private void SearchNodes(string SearchText, TreeNode StartNode)
        {
            //TreeNode node = null;

            while (StartNode != null)
            {
                string searchwithin = StartNode.Tag.ToString();

                //if (StartNode.Parent == null)
                //{
                //    searchwithin = r["AssyNo"].ToString() + r["AssyDesc"].ToString() + r["AssyManufacturer"].ToString() + r["AssyOem"].ToString();
                //}
                //else
                //{
                //    searchwithin = r["QUERY2"].ToString() + r["DESCRIPTION"].ToString() + r["USER3"].ToString() + r["TEXTVALUE"].ToString() ;
                //}

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
            if (root.IsSelected && chekroot == "Assy")
            {
                //DataRow r = _acountsTb.Rows[int.Parse(treeView1.SelectedNode.Tag.ToString())];
                //ItemTxtBox.Text = r["AssyNo"].ToString();
                //Descriptiontxtbox.Text = r["AssyDesc"].ToString();
                //oemtxtbox.Text = r["AssyManufacturer"].ToString();
                //oemitemtxtbox.Text = r["AssyOem"].ToString();
                //// familytxtbox.Text = r["AssyFamily"].ToString();
                ////qtytxtbox.Text = r["AssyDescription"].ToString();
                ////sparetxtbox.Text = r["AssySpare"].ToString();
                qtylbl.Visible = false;
                qtytxtbox.Visible = false;
                // qtytxtbox.ReadOnly = true;

                // qtytxtbox.BackColor = Color.Gray;
                string s = treeView1.SelectedNode.Tag.ToString();
                string[] values = s.Replace("][", "~").Split('~');
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = values[i].Trim();
                }

                ItemTxtBox.Text = values[0];
                Descriptiontxtbox.Text = values[1];
                oemtxtbox.Text = values[3];
                oemitemtxtbox.Text = values[4];
                // familytxtbox.Text = values[2];
                qtytxtbox.Text = "";
            }
            else
            {
                //DataRow r = _acountsTb.Rows[int.Parse(treeView1.SelectedNode.Tag.ToString())];
                //ItemTxtBox.Text = r["QUERY2"].ToString();
                //Descriptiontxtbox.Text = r["DESCRIPTION"].ToString();
                //oemtxtbox.Text = r["USER3"].ToString();
                //oemitemtxtbox.Text = r["TEXTVALUE"].ToString();
                //qtytxtbox.Text = r["AssyQty"].ToString();
                ////familytxtbox.Text = r["ItemFamily"].ToString();
                ////sparetxtbox.Text = r["Spare"].ToString();

                qtylbl.Visible = true;
                qtytxtbox.Visible = true;

                string s = treeView1.SelectedNode.Tag.ToString();
                string[] values = s.Replace("][", "~").Split('~');
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = values[i].Trim();
                }

                ItemTxtBox.Text = values[0];
                Descriptiontxtbox.Text = values[1];
                oemtxtbox.Text = values[3];
                oemitemtxtbox.Text = values[4];
                //familytxtbox.Text = values[2];
                qtytxtbox.Text = values[5];
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

        #region search before addding item

        private readonly List<TreeNode> _CurrentNodeMatches = new List<TreeNode>();

        private string itemexists;

        private void _SearchNodes(string SearchText, TreeNode StartNode)
        {
            //TreeNode node = null;
            while (StartNode != null)
            {
                if (StartNode.Text.IndexOf(SearchText, StringComparison.CurrentCultureIgnoreCase) >= 0)
                {
                    MessageBox.Show("Item already added to the assembly list", "SPM Conect", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    itemexists = "yes";
                    treeView1.SelectedNode = StartNode;
                    _CurrentNodeMatches.Add(StartNode);
                }
                if (StartNode.Nodes.Count != 0)
                {
                    _SearchNodes(SearchText, StartNode.Nodes[0]);//Recursive Search
                }
                StartNode = StartNode.NextNode;
            }
        }

        #endregion search before addding item

        #region start edit procedure

        private void Editbttn_Click(object sender, EventArgs e)
        {
            if (!savebttn.Visible)
            {
                savebttn.Visible = true;
                savebttn.Enabled = true;
            }
            else
            {
                savebttn.Visible = false;
                savebttn.Enabled = false;
            }
            treeView1.ContextMenuStrip = Addremovecontextmenu;
            qtytxtbox.BackColor = Color.LightYellow;
            qtytxtbox.ReadOnly = false;
            Assy_txtbox.Enabled = false;
        }

        private void Addremovecontextmenu_Opening(object sender, CancelEventArgs e)
        {
            if (treeView1.SelectedNode.Parent == null && root.IsSelected && chekroot == "Assy")
            {
                Addremovecontextmenu.Items[0].Enabled = true;
                Addremovecontextmenu.Items[1].Enabled = false;
            }
            else
            {
                Addremovecontextmenu.Items[1].Enabled = true;
                Addremovecontextmenu.Items[0].Enabled = false;
            }
        }

        private void AddItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode.Parent == null && root.IsSelected && chekroot == "Assy")
            {
                //contextMenuStrip1.Items[1].Enabled = true;
                //contextMenuStrip1.Items[0].Enabled = false;
                SPM_ConnectItems connectItems = new SPM_ConnectItems();
                connectItems.ShowDialog();
                try
                {
                    string _itemno = SPM_ConnectItems.ItemNo;
                    string _description = SPM_ConnectItems.description;
                    string _family = SPM_ConnectItems.family;
                    string _oem = SPM_ConnectItems.oem;
                    string _manufacturer = SPM_ConnectItems.Manufacturer;

                    _SearchNodes(_itemno, treeView1.Nodes[0]);
                    if (itemexists != "yes")
                    {
                        TreeNode childNode;

                        TreeNode child = new TreeNode
                        {
                            Text = _itemno + " - " + _description + " (1)",
                            Tag = _itemno + "][" + _description + "][" + _family + "][" + _manufacturer + "][" + _oem + "][1"
                        };

                        childNode = child;
                        treeView1.SelectedNode.Nodes.Add(childNode);
                        childNode.Tag = _itemno + "][" + _description + "][" + _family + "][" + _manufacturer + "][" + _oem + "][1";
                    }
                    else
                    {
                        itemexists = null;
                    }
                    if (!Expandchk.Checked)
                    {
                        treeView1.ExpandAll();
                    }
                }
                catch
                {
                    return;
                }
            }
            else
            {
                Addremovecontextmenu.Items[0].Enabled = true;
                Addremovecontextmenu.Items[1].Enabled = false;
            }
        }

        private void removeItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                if (treeView1.SelectedNode.Parent == null)
                {
                    // treeView1.Nodes.Remove(treeView1.SelectedNode);
                }
                else
                {
                    DialogResult result = MessageBox.Show("Remove item from the assembly list?", "SPM Connect", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        treeView1.SelectedNode.Parent.Nodes.Remove(treeView1.SelectedNode);
                    }
                    else if (result == DialogResult.No)
                    {
                        //code for No
                    }
                }
            }
        }

        private void savebttn_Click(object sender, EventArgs e)
        {
            treeView1.SelectedNode = treeView1.Nodes[0];
            if (treeView1.SelectedNode != null && treeView1.SelectedNode.Parent == null)
            {
                treeView1.PathSeparator = ".";

                // Get the count of the child tree nodes contained in the SelectedNode.
                int myNodeCount = treeView1.SelectedNode.GetNodeCount(true);
                //MessageBox.Show(myNodeCount.ToString());
                _ = ((decimal)myNodeCount /
                  (decimal)treeView1.GetNodeCount(true)) * 100;

                //// Display the tree node path and the number of child nodes it and the tree view have.
                //MessageBox.Show("The '" + treeView1.SelectedNode.FullPath + "' node has "
                //  + myNodeCount.ToString() + " child nodes.\nThat is "
                //  + string.Format("{0:###.##}", myChildPercentage)
                //  + "% of the total tree nodes in the tree view control.");

                if (myNodeCount > 0)
                {
                    deleteassy(ItemTxtBox.Text);
                    CallRecursive();
                }
                else
                {
                    MessageBox.Show("Assembly list cannot be empty in order to save to AutoCad Catalog.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }

        private void deleteassy(string assyitem)
        {
            string sql = "DELETE FROM [SPMControlCatalog].[dbo].[SPM-Catalog] WHERE [ASSEMBLYCODE] = '" + assyitem + "' OR [ASSEMBLYLIST] = '" + assyitem + "' ";

            try
            {
                _connection.Open();
                _command.CommandText = sql;
                _command.ExecuteNonQuery();
                // MessageBox.Show("Item added to the catalog.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException)
            {
                // MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Technical error while updating to autocad catalog. Please contact the admin.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _connection.Close();
            }
        }

        private void PrintRecursive(TreeNode treeNode)
        {
            saveeachnode(treeNode);
            foreach (TreeNode tn in treeNode.Nodes)
            {
                // MessageBox.Show(treeNode.Text);
                PrintRecursive(tn);
            }
        }

        private void CallRecursive()
        {
            // Print each node recursively.
            TreeNodeCollection nodes = treeView1.Nodes;
            foreach (TreeNode n in nodes)
            {
                PrintRecursive(n);
            }

            Lockdownsave();
        }

        private string parentchild;

        private void saveeachnode(TreeNode treeNode)
        {
            if (treeNode.Parent == null)
            {
                parentchild = "parent";
                string parentnode = treeNode.Tag.ToString();

                //MessageBox.Show(parentnode);
                Splittagtovariables(parentnode);
            }
            else
            {
                parentchild = "";
                string childnode = treeNode.Tag.ToString();
                Splittagtovariables(childnode);
            }
        }

        private string assemblylist;

        private void Splittagtovariables(string s)
        {
            string[] values = s.Replace("][", "~").Split('~');

            //string[] values = s.Split(',');
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = values[i].Trim();
            }
            if (parentchild == "parent")
            {
                InsertToAutocadAssy(values[0], values[1], values[3], values[4]);
                assemblylist = values[0];
            }
            else
            {
                InsertToAutocadAssyList(values[0], values[1], values[3], values[4], values[5], assemblylist);
            }
        }

        private void InsertToAutocadAssy(string itemno, string description, string manufacturer, string manufactureritemnumber)
        {
            string sql = "INSERT INTO [SPMControlCatalog].[dbo].[SPM-Catalog] ([CATALOG], [TEXTVALUE],[QUERY2], [MANUFACTURER],[USER3],[DESCRIPTION],[MISC1],[MISC2],[ASSEMBLYCODE])" +
                "VALUES(LEFT( '" + manufactureritemnumber + "',50),'" + manufactureritemnumber + "','" + itemno + "',  LEFT('" + manufacturer + "',20)," +
                "'" + manufacturer + "','" + description + "',SUBSTRING('" + manufactureritemnumber + "',51,100),SUBSTRING('" + manufactureritemnumber + "',151,100),'" + itemno + "')";

            try
            {
                _connection.Open();
                _command.CommandText = sql;
                _command.ExecuteNonQuery();
                // MessageBox.Show("Item added to the catalog.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException)
            {
                // MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Technical error while inserting to autocad catalog. Please contact the admin.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _connection.Close();
            }
        }

        private void InsertToAutocadAssyList(string itemno, string description, string manufacturer, string manufactureritemnumber, string qty, string assemblylist)
        {
            string sql = "INSERT INTO [SPMControlCatalog].[dbo].[SPM-Catalog] ([CATALOG], [TEXTVALUE],[QUERY2], [MANUFACTURER],[USER3],[DESCRIPTION],[MISC1],[MISC2], [ASSEMBLYLIST],[ASSEMBLYQUANTITY])" +
                "VALUES(LEFT( '" + manufactureritemnumber + "',50),'" + manufactureritemnumber + "','" + itemno + "',  LEFT('" + manufacturer + "',20)," +
                "'" + manufacturer + "','" + description + "',SUBSTRING('" + manufactureritemnumber + "',51,100),SUBSTRING('" + manufactureritemnumber + "',151,100), '" + assemblylist + "', '" + (qty != "1" ? qty : null) + "')";

            try
            {
                _connection.Open();
                _command.CommandText = sql;
                _command.ExecuteNonQuery();
                // MessageBox.Show("Item added to the catalog.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException)
            {
                // MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Technical error while inserting to autocad catalog. Please contact the admin.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _connection.Close();
            }
        }

        private void Lockdownsave()
        {
            treeView1.SelectedNode = root;
            savebttn.Enabled = false;
            savebttn.Visible = false;
            qtytxtbox.ReadOnly = true;
            //qtytxtbox.Enabled = false;
            qtytxtbox.BackColor = Color.White;
            treeView1.ContextMenuStrip = null;
            //treeView1.Enabled = false;
            MessageBox.Show("Assembly updated successfully on the Autocad Catalog.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Assy_txtbox.Enabled = true;
        }

        private void qtytxtbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                if (treeView1.SelectedNode != null)
                {
                    if (qtytxtbox.Text == "0")
                    {
                        qtytxtbox.Text = "1";
                    }
                    treeView1.SelectedNode.Text = ItemTxtBox.Text + " - " + Descriptiontxtbox.Text + " (" + qtytxtbox.Text + " )";
                    treeView1.SelectedNode.Tag = ItemTxtBox.Text + "][" + Descriptiontxtbox.Text + "][ ][" + oemtxtbox.Text + "][" + oemitemtxtbox.Text + "][" + qtytxtbox.Text;
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void qtytxtbox_TextChanged(object sender, EventArgs e)
        {
            //Regex regex = new Regex(@"[^0-9^]");
            //MatchCollection matches = regex.Matches(qtytxtbox.Text);
            //if (matches.Count > 0)
            //{
            //    //tell the user
            //    MessageBox.Show("found");
            //}
        }

        private void qtytxtbox_KeyPress(object sender, KeyPressEventArgs e)
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

        #endregion start edit procedure

        private void AutocadAssembly_FormClosed(object sender, FormClosedEventArgs e)
        {
            log.Info("Closed AutoCadAssembly Form ");
            this.Dispose();
        }
    }
}