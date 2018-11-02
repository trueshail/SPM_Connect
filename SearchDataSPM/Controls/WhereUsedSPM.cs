using ExtractLargeIconFromFile;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using wpfPreviewFlowControl;

namespace SearchDataSPM
{

    public partial class WhereUsedSPM : Form

    {
        #region steupvariables

        String connection;
        DataTable _acountsTb = null;
        DataTable _productTB;
        SqlConnection _connection;
        SqlCommand _command;
        SqlDataAdapter _adapter;
        TreeNode root = new TreeNode();
        string txtvalue;

        #endregion

        #region loadtree

        public WhereUsedSPM()

        {
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

            string txtvalue = Assy_txtbox.Text;
            _acountsTb = new DataTable();
            _command = new SqlCommand();
            _command.Connection = _connection;
            _productTB = new DataTable();


        }

        string itemnumber;

        public string item(string item)
        {
            if (item.Length > 0)
                return itemnumber = item;
            return null;
        }

        private void ParentView_Load(object sender, EventArgs e)
        {
            Assy_txtbox.Focus();

            //Assy_txtbox.Text = SPM_ConnectControls.whereused;
            Assy_txtbox.Text = itemnumber;

            if (Assy_txtbox.Text.Length == 5 || Assy_txtbox.Text.Length == 6)
            {
                itemnumber = null;
                startprocessofbom();
            }

            Assy_txtbox.Select();


        }

        #endregion

        #region assytextbox and button events

        private void filldatatable()
        {
            String sql = "SELECT *  FROM [SPM_Database].[dbo].[SPMConnectBOM] ORDER BY [AssyNo] DESC";

            // String sql2 = "SELECT *  FROM [SPM_Database].[dbo].[UnionInventory]";

            try
            {
                _acountsTb.Clear();
                _connection.Open();
                _adapter = new SqlDataAdapter(sql, _connection);

                // _adapaterproduct = new SqlDataAdapter(sql2, _connection);

                //_adapaterproduct.Fill(_productTB);

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
            fillrootnode();
        }

        private void Assy_txtbox_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            // treeView1.TopNode.Nodes.Clear();
            cleanup();
            //SendKeys.Send("~");
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
            sparetxtbox.Clear();
            familytxtbox.Clear();
        }

        private void cleaup2()
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
            sparetxtbox.Clear();
            familytxtbox.Clear();
        }

        private void startprocessofbom()
        {
            txtvalue = Assy_txtbox.Text;
            try
            {
                treeView1.Nodes.Clear();
                RemoveChildNodes(root);
                treeView1.ResetText();
                filldatatable();

            }
            catch (Exception)

            {
                if (!String.IsNullOrEmpty(txtvalue) && Char.IsLetter(txtvalue[0]))
                {
                    MessageBox.Show(" Item does not contain a BOM.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    // Assy_txtbox.Clear();
                    this.Hide();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Invalid Search Parameter / Item Not Found On Genius.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cleaup2();
                    Assy_txtbox.BackColor = Color.IndianRed; //to add high light
                    //Assy_txtbox.Clear();
                }


            }
        }

        private void Assy_txtbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                startprocessofbom();
                e.Handled = true;
                e.SuppressKeyPress = true;

            }

        }

        private void fillrootnode()
        {

            //if (Assy_txtbox.Text.Length == 6)
            //{
                Assy_txtbox.BackColor = Color.White; //to add high light
                try
                {
                    try
                    {
                        treeView1.Nodes.Clear();
                        RemoveChildNodes(root);
                        treeView1.ResetText();
                        Expandchk.Checked = false;
                        //DataRow[] dr = _productTB.Select("ItemNumber = '" + txtvalue.ToString() + "'");
                        DataRow[] dr = _acountsTb.Select("ItemNumber = '" + txtvalue.ToString() + "'");


                        root.Text = dr[0]["ItemNumber"].ToString() + " - " + dr[0]["Description"].ToString();
                        root.Tag = _acountsTb.Rows.IndexOf(dr[0]);

                        //Font f = FontStyle.Bold);
                        // root.NodeFont = f;

                        treeView1.Nodes.Add(root);
                        PopulateTreeView(Assy_txtbox.Text, root);

                        chekroot = "Item";

                        ItemTxtBox.Text = dr[0]["ItemNumber"].ToString();
                        Descriptiontxtbox.Text = dr[0]["Description"].ToString();
                        oemtxtbox.Text = dr[0]["Manufacturer"].ToString();
                        oemitemtxtbox.Text = dr[0]["ManufacturerItemNumber"].ToString();
                        familytxtbox.Text = dr[0]["ItemFamily"].ToString();
                        sparetxtbox.Text = dr[0]["Spare"].ToString();

                    }
                    catch
                    {

                        treeView1.Nodes.Clear();
                        RemoveChildNodes(root);
                        treeView1.ResetText();
                        Expandchk.Checked = false;
                        //DataRow[] dr = _productTB.Select("ItemNumber = '" + txtvalue.ToString() + "'");
                        DataRow[] dr = _acountsTb.Select("AssyNo = '" + txtvalue.ToString() + "'");


                        root.Text = dr[0]["AssyNo"].ToString() + " - " + dr[0]["AssyDescription"].ToString();
                        root.Tag = _acountsTb.Rows.IndexOf(dr[0]);

                        //Font f = FontStyle.Bold);
                        // root.NodeFont = f;

                        treeView1.Nodes.Add(root);
                        PopulateTreeView(Assy_txtbox.Text, root);
                        chekroot = "Assy";

                        ItemTxtBox.Text = dr[0]["AssyNo"].ToString();
                        Descriptiontxtbox.Text = dr[0]["AssyDescription"].ToString();
                        oemtxtbox.Text = dr[0]["AssyManufacturer"].ToString();
                        oemitemtxtbox.Text = dr[0]["AssyManufacturerItemNumber"].ToString();
                        familytxtbox.Text = dr[0]["AssyFamily"].ToString();
                        sparetxtbox.Text = dr[0]["AssySpare"].ToString();

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
            //}
            //else
            //{
            //    if (Assy_txtbox.Text.Length == 5)
            //    {
            //        cleaup2();
            //        Assy_txtbox.BackColor = Color.IndianRed; //to add high light
            //    }
            //}
        }

        private void PopulateTreeView(string parentId, TreeNode parentNode)
        {

            TreeNode childNode;

            foreach (DataRow dr in _acountsTb.Select("[ItemNumber] ='" + parentId.ToString() + "'"))
            {
                TreeNode t = new TreeNode();
                t.Text = dr["AssyNo"].ToString() + " - " + dr["AssyDescription"].ToString() + " ( " + dr["QuantityPerAssembly"].ToString() + " ) ";
                t.Name = dr["AssyNo"].ToString();
                t.Tag = _acountsTb.Rows.IndexOf(dr);
                if (parentNode == null)
                {

                    Font f = new Font("Arial", 10, FontStyle.Bold);
                    t.NodeFont = f;
                    t.Text = dr["AssyNo"].ToString() + " - " + dr["AssyDescription"].ToString() + " ( " + dr["QuantityPerAssembly"].ToString() + " ) ";
                    t.Name = dr["ItemNumber"].ToString();
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
                PopulateTreeView((dr["AssyNo"].ToString()), childNode);
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

        string ItemNo;


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

        #endregion

        #region search tree
        private List<TreeNode> CurrentNodeMatches = new List<TreeNode>();

        private int LastNodeIndex = 0;

        private string LastSearchText;

        private void SearchNodes(string SearchText, TreeNode StartNode)
        {

            //TreeNode node = null;
            while (StartNode != null)
            {
                DataRow r = _acountsTb.Rows[int.Parse(StartNode.Tag.ToString())];
                string searchwithin;

                if (StartNode.Parent != null)
                {
                    searchwithin = r["AssyNo"].ToString() + r["AssyDescription"].ToString() + r["AssyManufacturer"].ToString() + r["AssyManufacturerItemNumber"].ToString();
                }
                else
                {
                    searchwithin = r["ItemNumber"].ToString() + r["Description"].ToString() + r["Manufacturer"].ToString() + r["ManufacturerItemNumber"].ToString();
                }

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

            catch (Exception)
            {

            }


        }

        private void txtSearch_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            txtSearch.Clear();

        }

        #endregion

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

        string chekroot;

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (root.IsSelected && chekroot == "Assy")
            {

                DataRow r = _acountsTb.Rows[int.Parse(treeView1.SelectedNode.Tag.ToString())];
                ItemTxtBox.Text = r["AssyNo"].ToString();
                Descriptiontxtbox.Text = r["AssyDescription"].ToString();
                oemtxtbox.Text = r["AssyManufacturer"].ToString();
                oemitemtxtbox.Text = r["AssyManufacturerItemNumber"].ToString();
                familytxtbox.Text = r["AssyFamily"].ToString();
                //qtytxtbox.Text = r["AssyDescription"].ToString();
                sparetxtbox.Text = r["AssySpare"].ToString();
                ItemNo = ItemTxtBox.Text;
                
                // getfilepathname(ItemNo);
            }
            else if (root.IsSelected && chekroot == "Item")
            {
                DataRow r = _acountsTb.Rows[int.Parse(treeView1.SelectedNode.Tag.ToString())];
                ItemTxtBox.Text = r["ItemNumber"].ToString();
                Descriptiontxtbox.Text = r["Description"].ToString();
                oemtxtbox.Text = r["Manufacturer"].ToString();
                oemitemtxtbox.Text = r["ManufacturerItemNumber"].ToString();
                qtytxtbox.Text = r["QuantityPerAssembly"].ToString();
                familytxtbox.Text = r["ItemFamily"].ToString();
                sparetxtbox.Text = r["Spare"].ToString();
                ItemNo = ItemTxtBox.Text;
                
                //getfilepathname(ItemNo);
            }
            else
            {
                DataRow r = _acountsTb.Rows[int.Parse(treeView1.SelectedNode.Tag.ToString())];
                ItemTxtBox.Text = r["AssyNo"].ToString();
                Descriptiontxtbox.Text = r["AssyDescription"].ToString();
                oemtxtbox.Text = r["AssyManufacturer"].ToString();
                oemitemtxtbox.Text = r["AssyManufacturerItemNumber"].ToString();
                qtytxtbox.Text = r["QuantityPerAssembly"].ToString();
                familytxtbox.Text = r["AssyFamily"].ToString();
                sparetxtbox.Text = r["AssySpare"].ToString();
                ItemNo = ItemTxtBox.Text;
              
                //getfilepathname(ItemNo);

            }
        }

        private void treeView1_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == Convert.ToChar(Keys.Down))
            {
                TreeNode node = new TreeNode();
                node = treeView1.SelectedNode;
                treeView1.SelectedNode = node.NextVisibleNode;
                node.TreeView.Focus();
            }

            else if (e.KeyChar == Convert.ToChar(Keys.Up))
            {
                TreeNode node = new TreeNode();
                node = treeView1.SelectedNode;
                treeView1.SelectedNode = node.NextVisibleNode;
                node.TreeView.Focus();
            }
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            treeView1.SelectedNode = e.Node;
        }


        #endregion        

    }

}
