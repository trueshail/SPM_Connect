using SPMConnect.UserActionLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace SearchDataSPM
{

    public partial class CreateAssyToCatalog : Form

    {
        #region steupvariables
        string connection;
        string cntrlconnection;
        SqlConnection _connection;
        SqlConnection cn;
        SqlCommand _command;
        TreeNode root = new TreeNode();

        string ItemNo;
        string Description;
        string OEM;
        string manufacturer;
        string family;

        log4net.ILog log;
        private UserActions _userActions;
        ErrorHandler errorHandler = new ErrorHandler();

        #endregion

        #region loadtree

        public CreateAssyToCatalog()
        {
            Application.ThreadException += new ThreadExceptionEventHandler(UIThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledException);
            InitializeComponent();
            connection = ConfigurationManager.ConnectionStrings["SearchDataSPM.Properties.Settings.cn"].ConnectionString;
            cntrlconnection = System.Configuration.ConfigurationManager.ConnectionStrings["SearchDataSPM.Properties.Settings.cntrlscn"].ConnectionString;

            try
            {
                cn = new SqlConnection(connection);

                _connection = new SqlConnection(cntrlconnection);
                _command = new SqlCommand
                {
                    Connection = _connection
                };

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void ParentView_Load(object sender, EventArgs e)
        {
            Loadadding();
            log4net.Config.XmlConfigurator.Configure();
            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log.Info("Opened Create Assy To Catalog by " + System.Environment.UserName);
            _userActions = new UserActions(this);
        }

        public void getallitems(string ItemNoimp, string descriptionimp, string familyimp, string Manufacturerimp, string oemimp)
        {
            ItemNo = ItemNoimp;
            Description = descriptionimp;
            OEM = oemimp;
            manufacturer = Manufacturerimp;
            family = familyimp;
        }

        #endregion

        #region assytextbox and button events

        private void Loadadding()
        {
            try
            {
                try
                {
                    treeView1.Nodes.Clear();
                    RemoveChildNodes(root);
                    treeView1.ResetText();
                    Expandchk.Checked = false;

                    root.Text = ItemNo.ToString() + " - " + Description.ToString();
                    //root.Tag = ItemNo.IndexOf(ItemNo);
                    root.Tag = ItemNo + "][" + Description + "][" + family + "][" + manufacturer + "][" + OEM + "][" + "1";

                    string s = root.Tag.ToString();
                    string[] values = s.Replace("][", "~").Split('~');
                    for (int i = 0; i < values.Length; i++)
                    {
                        values[i] = values[i].Trim();

                    }
                    //MessageBox.Show(values[0]);
                    //MessageBox.Show(root.Tag.ToString());
                    treeView1.Nodes.Add(root);

                    chekroot = "Assy";
                    ItemTxtBox.Text = values[0];
                    Descriptiontxtbox.Text = values[1];
                    oemtxtbox.Text = values[3];
                    oemitemtxtbox.Text = values[4];
                    familytxtbox.Text = values[2];

                }
                catch
                {
                    treeView1.Nodes.Clear();
                    RemoveChildNodes(root);
                    treeView1.ResetText();
                    Expandchk.Checked = false;

                }

            }
            catch (Exception)

            {
                MessageBox.Show("Invalid Search Parameter / Item Not Found On Genius.", "SPM Connect");

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

        #region treeview events

        private void treeView1_AfterExpand(object sender, TreeViewEventArgs e)
        {
            Expandchk.Checked = true;

        }

        private void treeView1_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            Expandchk.Checked = true;
        }

        string chekroot;

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (root.IsSelected && chekroot == "Assy")
            {
                qtylbl.Visible = false;
                qtytxtbox.Visible = false;


                qtytxtbox.ReadOnly = true;

                qtytxtbox.BackColor = Color.Gray;
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
                familytxtbox.Text = values[2];
                qtytxtbox.Text = "";

            }

            else
            {
                qtylbl.Visible = true;
                qtytxtbox.Visible = true;

                qtytxtbox.BackColor = Color.LightYellow;
                qtytxtbox.ReadOnly = false;

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
                familytxtbox.Text = values[2];
                qtytxtbox.Text = values[5];
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

        #region add item or remove item

        private void addItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(treeView1.SelectedNode.Index.ToString());
            doadditemoepration();

        }

        private void Additembttn_Click(object sender, EventArgs e)
        {
            doadditemoepration();
        }

        private void doadditemoepration()
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

                    SearchNodes(_itemno, treeView1.Nodes[0]);
                    if (itemexists != "yes")
                    {
                        TreeNode childNode;

                        TreeNode child = new TreeNode
                        {
                            Text = _itemno.ToString() + " - " + _description.ToString() + " " + "(1)",
                            Tag = _itemno + "][" + _description + "][" + _family + "][" + _manufacturer + "][" + _oem + "][" + "1"
                        };

                        childNode = child;
                        treeView1.SelectedNode.Nodes.Add(childNode);
                        childNode.Tag = _itemno + "][" + _description + "][" + _family + "][" + _manufacturer + "][" + _oem + "][" + "1";
                    }
                    else
                    {
                        itemexists = null;
                    }
                    if (Expandchk.Checked == false)
                    {
                        treeView1.ExpandAll();
                    }
                }
                catch (Exception)
                {
                    return;
                    //MessageBox.Show(ex.Message);
                }


            }
            else
            {
                contextMenuStrip1.Items[0].Enabled = true;
                contextMenuStrip1.Items[1].Enabled = false;
            }
        }

        private void removeItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if(root.IsSelected == false)
            //{
            //    if (treeView1.SelectedNode != null)
            //    {
            //        //treeView1.Nodes.Remove(treeView1.SelectedNode);
            //        treeView1.SelectedNode.Parent.Nodes.Remove(treeView1.SelectedNode);
            //    }

            //}
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

        private void save_Click(object sender, EventArgs e)
        {
            CallRecursive();
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

            lockdownsave();
        }
        string parentchild;

        private void saveeachnode(TreeNode treeNode)
        {
            if (treeNode.Parent == null)
            {
                parentchild = "parent";
                string parentnode = treeNode.Tag.ToString();


                //MessageBox.Show(parentnode);
                splittagtovariables(parentnode);

            }
            else
            {
                parentchild = "";
                string childnode = treeNode.Tag.ToString();
                splittagtovariables(childnode);
            }

        }
        string assemblylist;

        private void splittagtovariables(string s)
        {
            string[] values = s.Replace("][", "~").Split('~');
            //string[] values = s.Split('][');
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = values[i].Trim();

            }
            if (parentchild == "parent")
            {
                InsertToAutocadAssy(values[0], values[1], values[3], values[4], values[5]);
                assemblylist = values[0];
            }
            else
            {
                InsertToAutocadAssyList(values[0], values[1], values[3], values[4], values[5], assemblylist);
            }



        }

        private void InsertToAutocadAssy(string itemno, string description, string manufacturer, string manufactureritemnumber, string qty)
        {
            string sql;
            sql = "INSERT INTO [SPMControlCatalog].[dbo].[SPM-Catalog] ([CATALOG], [TEXTVALUE],[QUERY2], [MANUFACTURER],[USER3],[DESCRIPTION],[MISC1],[MISC2],[ASSEMBLYCODE])" +
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
            string sql;
            sql = "INSERT INTO [SPMControlCatalog].[dbo].[SPM-Catalog] ([CATALOG], [TEXTVALUE],[QUERY2], [MANUFACTURER],[USER3],[DESCRIPTION],[MISC1],[MISC2], [ASSEMBLYLIST],[ASSEMBLYQUANTITY])" +
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

        private void lockdownsave()
        {
            treeView1.SelectedNode = root;
            save.Enabled = false;
            qtytxtbox.ReadOnly = true;
            qtytxtbox.Enabled = false;
            treeView1.Enabled = false;
            Additembttn.Enabled = false;
            MessageBox.Show("Assembly created successfully on the Autocad Catalog.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (treeView1.SelectedNode.Parent == null && root.IsSelected && chekroot == "Assy")
            {
                contextMenuStrip1.Items[0].Enabled = true;
                contextMenuStrip1.Items[1].Enabled = false;
            }
            else
            {
                contextMenuStrip1.Items[1].Enabled = true;
                contextMenuStrip1.Items[0].Enabled = false;
            }

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
                    treeView1.SelectedNode.Tag = ItemTxtBox.Text + "][" + Descriptiontxtbox.Text + "][" + familytxtbox.Text + "][" + oemtxtbox.Text + "][" + oemitemtxtbox.Text + "][" + qtytxtbox.Text;
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }

        }

        #endregion

        #region search tree

        private List<TreeNode> CurrentNodeMatches = new List<TreeNode>();

        private int LastNodeIndex = 0;

        private string LastSearchText;

        string itemexists;

        private void SearchNodes(string SearchText, TreeNode StartNode)
        {
            //TreeNode node = null;
            while (StartNode != null)
            {
                if (StartNode.Text.ToLower().Contains(SearchText.ToLower()))
                {
                    MessageBox.Show("Item already added to the assembly list", "SPM Conect", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    itemexists = "yes";
                    treeView1.SelectedNode = StartNode;
                    CurrentNodeMatches.Add(StartNode);
                };
                if (StartNode.Nodes.Count != 0)
                {
                    SearchNodes(SearchText, StartNode.Nodes[0]);//Recursive Search 

                };
                StartNode = StartNode.NextNode;


            }

        }

        private void searchnode(string searchText)
        {

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
                MessageBox.Show("yes");
                LastNodeIndex++;
                this.treeView1.SelectedNode = selectedNode;
                this.treeView1.SelectedNode.Expand();
                this.treeView1.Select();

            }

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



        #endregion

        private void CreateAssyToCatalog_FormClosed(object sender, FormClosedEventArgs e)
        {
            _userActions.FinishLoggingUserActions(this);
            log.Info("Closed Creat Assy To Catalog by " + System.Environment.UserName);
            this.Dispose();
        }

        private void UIThreadException(object sender, ThreadExceptionEventArgs t)
        {
            errorHandler.EmailExceptionAndActionLogToSupport(sender, t.Exception, _userActions, this);
        }

        private void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            errorHandler.EmailExceptionAndActionLogToSupport(sender, (Exception)e.ExceptionObject, _userActions, this);
        }
    }

}
