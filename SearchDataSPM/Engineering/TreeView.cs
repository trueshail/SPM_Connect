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
using System.Threading;
using System.Windows.Forms;
using wpfPreviewFlowControl;

namespace SearchDataSPM
{
    public partial class TreeView : Form
    {
        #region steupvariables

        private string connection;
        private DataTable _acountsTb = null;
        private DataTable _productTB;
        private SqlConnection _connection;
        private SqlCommand _command;
        private SqlDataAdapter _adapter;
        private TreeNode root = new TreeNode();
        private string txtvalue;
        private bool eng = false;
        private bool rootnodedone = false;
        private SPMConnectAPI.SPMSQLCommands connectapi = new SPMConnectAPI.SPMSQLCommands();
        private log4net.ILog log;

        private ErrorHandler errorHandler = new ErrorHandler();
        private string itemnumber;

        #endregion steupvariables

        #region loadtree

        public TreeView(string item = "")
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

            _acountsTb = new DataTable();
            _command = new SqlCommand
            {
                Connection = _connection
            };
            _productTB = new DataTable();
            this.itemnumber = item;
        }

        private void ParentView_Load(object sender, EventArgs e)
        {
            Assy_txtbox.Focus();
            Assy_txtbox.Text = itemnumber;
            if (Assy_txtbox.Text.Length == 5 || Assy_txtbox.Text.Length == 6)
            {
                //SendKeys.Send("~");
                itemnumber = null;
                Startprocessofbom();
                CallRecursive();
                //connectapi.SPM_Connect();
                if (connectapi.getdepartment() == "Eng") eng = true;
                Assy_txtbox.Select();
            }

            log4net.Config.XmlConfigurator.Configure();
            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log.Info("Opened Engineering BOM " + itemnumber + " by " + System.Environment.UserName);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.W))
            {
                this.Close();
                return true;
            }

            if (keyData == (Keys.Control | Keys.F))
            {
                Assy_txtbox.Focus();
                Assy_txtbox.SelectAll();
                return true;
            }

            if (keyData == (Keys.Control | Keys.S))
            {
                txtSearch.Focus();
                txtSearch.SelectAll();
                return true;
            }

            if (keyData == Keys.Home)
            {
                if (Assy_txtbox.Text.Length > 0)
                {
                    Assy_txtbox.Focus();
                    Assy_txtbox.SelectAll();
                    SendKeys.Send("~");
                }

                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        #endregion loadtree

        #region assytextbox and button events

        private void Assy_txtbox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // treeView1.TopNode.Nodes.Clear();
            Cleanup();
            //SendKeys.Send("~");
        }

        private void Cleanup()
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
            listView.Clear();
            listFiles.Clear();
            foundlabel.Text = "Search:";
        }

        private void Cleaup2()
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
            listView.Clear();
            listFiles.Clear();
            foundlabel.Text = "Search:";
        }

        private void Assy_txtbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                Startprocessofbom();
                rootnodedone = false;
                CallRecursive();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void Startprocessofbom()
        {
            txtvalue = Assy_txtbox.Text;
            try
            {
                treeView1.Nodes.Clear();
                RemoveChildNodes(root);
                treeView1.ResetText();
                Filldatatable();
            }
            catch
            {
                if (!String.IsNullOrEmpty(txtvalue) && Char.IsLetter(txtvalue[0]))
                {
                    MessageBox.Show(" Item does not contain a Bill OF Material on Genius.", "SPM Connect - Bill Of Manufacturing", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    // Assy_txtbox.Clear();
                    //this.Hide();
                    //this.Close();
                }
                else
                {
                    MessageBox.Show("Invalid Search Parameter / Item Not Found On Genius.", "SPM Connect - Bill Of Manufacturing", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Cleaup2();
                    Assy_txtbox.BackColor = Color.IndianRed; //to add high light
                    //Assy_txtbox.Clear();
                }
            }
        }

        private void Filldatatable()
        {
            string sql = "SELECT *  FROM [SPM_Database].[dbo].[SPMConnectBOM] ORDER BY [ItemNumber]";

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
                MessageBox.Show(ex.Message, "BOM - Fill data table Items", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _connection.Close();
            }

            Fillrootnode();
        }

        private void Fillrootnode()
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
                    DataRow[] dr = _acountsTb.Select("AssyNo = '" + txtvalue.ToString() + "'");

                    root.Text = dr[0]["AssyNo"].ToString() + " - " + dr[0]["AssyDescription"].ToString();
                    root.Tag = _acountsTb.Rows.IndexOf(dr[0]);
                    setimageaccordingtofamily(dr[0]["AssyFamily"].ToString(), root);
                    //Font f = FontStyle.Bold);
                    // root.NodeFont = f;

                    treeView1.Nodes.Add(root);
                    //root.ImageIndex = 0;

                    PopulateTreeView(Assy_txtbox.Text, root);

                    chekroot = "Assy";
                    treeView1.SelectedNode = treeView1.Nodes[0];
                    ItemTxtBox.Text = dr[0]["AssyNo"].ToString();
                    Descriptiontxtbox.Text = dr[0]["AssyDescription"].ToString();
                    oemtxtbox.Text = dr[0]["AssyManufacturer"].ToString();
                    oemitemtxtbox.Text = dr[0]["AssyManufacturerItemNumber"].ToString();
                    familytxtbox.Text = dr[0]["AssyFamily"].ToString();
                    sparetxtbox.Text = dr[0]["AssySpare"].ToString();
                }
                catch
                {
                    treeView1.Nodes.Clear();
                    RemoveChildNodes(root);
                    treeView1.ResetText();
                    Expandchk.Checked = false;
                    //DataRow[] dr = _productTB.Select("ItemNumber = '" + txtvalue.ToString() + "'");
                    DataRow[] dr = _acountsTb.Select("ItemNumber = '" + txtvalue.ToString() + "'");

                    root.Text = dr[0]["ItemNumber"].ToString() + " - " + dr[0]["Description"].ToString();
                    root.Tag = _acountsTb.Rows.IndexOf(dr[0]);
                    setimageaccordingtofamily(dr[0]["ItemFamily"].ToString(), root);
                    //Font f = FontStyle.Bold);
                    // root.NodeFont = f;

                    treeView1.Nodes.Add(root);

                    PopulateTreeView(Assy_txtbox.Text, root);
                    chekroot = "Item";
                    treeView1.SelectedNode = treeView1.Nodes[0];
                    ItemTxtBox.Text = dr[0]["ItemNumber"].ToString();
                    Descriptiontxtbox.Text = dr[0]["Description"].ToString();
                    oemtxtbox.Text = dr[0]["Manufacturer"].ToString();
                    oemitemtxtbox.Text = dr[0]["ManufacturerItemNumber"].ToString();
                    familytxtbox.Text = dr[0]["ItemFamily"].ToString();
                    sparetxtbox.Text = dr[0]["Spare"].ToString();
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

            foreach (DataRow dr in _acountsTb.Select("[AssyNo] ='" + parentId.ToString() + "'"))
            {
                TreeNode t = new TreeNode
                {
                    Text = dr["ItemNumber"].ToString() + " - " + dr["Description"].ToString() + " ( " + dr["QuantityPerAssembly"].ToString() + " ) ",
                    Name = dr["ItemNumber"].ToString(),
                    Tag = _acountsTb.Rows.IndexOf(dr)
                };
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
                PopulateTreeView((dr["ItemNumber"].ToString()), childNode);
            }
            // treeView1.SelectedNode = treeView1.Nodes[0];
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

        #region open model and drawing

        private void openModelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string itemstr = treeView1.SelectedNode.Text;
            itemstr = itemstr.Substring(0, 6);
            if (eng)
            {
                connectapi.checkforspmfile(itemstr);
            }
            else
            {
                connectapi.checkforspmfileprod(itemstr);
            }
        }

        private void openDrawingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string itemstr = treeView1.SelectedNode.Text;
            itemstr = itemstr.Substring(0, 6);
            if (eng)
            {
                connectapi.checkforspmdrwfile(itemstr);
            }
            else
            {
                connectapi.checkforspmdrwfileprod(itemstr);
            }
        }

        #endregion open model and drawing

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

                if (StartNode.Parent == null)
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
                            SearchNodes(searchText, treeView1.Nodes[0]);
                        }

                        if (LastNodeIndex >= 0 && CurrentNodeMatches.Count > 0 && LastNodeIndex < CurrentNodeMatches.Count)
                        {
                            TreeNode selectedNode = CurrentNodeMatches[LastNodeIndex];
                            LastNodeIndex++;
                            this.treeView1.SelectedNode = selectedNode;
                            this.treeView1.SelectedNode.Expand();
                            this.treeView1.Select();
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

        #region treeview events

        private void treeView1_AfterExpand(object sender, TreeViewEventArgs e)
        {
            Expandchk.Checked = true;
            // CallRecursive();
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
            string ItemNo;
            if (publicnode != null)
            {
                publicnode.BackColor = treeView1.BackColor;
                publicnode.ForeColor = treeView1.ForeColor;
            }
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

                //getfilepathname(ItemNo);
                filllistview(ItemNo);
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
                filllistview(ItemNo);
            }
            else
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

                // getfilepathname(ItemNo);
                filllistview(ItemNo);
            }
        }

        private void filllistview(string item)
        {
            try
            {
                listFiles.Clear();
                listView.Items.Clear();

                // MessageBox.Show(ItemNo);
                //getfilepathname(ItemNo);
                string first3char = item.Substring(0, 3) + @"\";
                //MessageBox.Show(first3char);

                string spmcadpath = @"\\spm-adfs\CAD Data\AAACAD\";

                string Pathpart = (spmcadpath + first3char);
                getitemstodisplay(Pathpart, item);
            }
            catch
            {
                return;
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

        private void treeView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
            //string[] fList = new string[1];
            //fList[0] = fName;
            //DataObject dataObj = new DataObject(DataFormats.FileDrop, fList);
            //DragDropEffects eff = DoDragDrop(dataObj, DragDropEffects.Link | DragDropEffects.Copy);
        }

        public TreeNode publicnode;

        private void treeView1_Leave(object sender, EventArgs e)
        {
            if (treeView1.Nodes.Count > 0)
            {
                publicnode = treeView1.SelectedNode;
                publicnode.BackColor = Color.LightBlue;
            }
        }

        #endregion treeview events

        #region Listview Events

        private void getitemstodisplay(string Pathpart, string ItemNo)
        {
            if (Directory.Exists(Pathpart))
            {
                foreach (string item in Directory.GetFiles(Pathpart, "*" + ItemNo.ToString() + "*").Where(str => !str.Contains(@"\~$")).OrderByDescending(fi => fi))
                {
                    try
                    {
                        string sDocFileName = item;
                        wpfThumbnailCreator pvf;
                        pvf = new wpfThumbnailCreator();
                        System.Drawing.Size size = new Size
                        {
                            Width = 256,
                            Height = 256
                        };
                        pvf.DesiredSize = size;
                        System.Drawing.Bitmap pic = pvf.GetThumbNail(sDocFileName);
                        imageList.Images.Add(pic);
                        //axEModelViewControl1 = new EModelViewControl();
                        //axEModelViewControl1.OpenDoc(item, false, false, true, "");
                    }
                    catch (Exception)
                    {
                        //MessageBox.Show(ex.Message);

                        var size = ShellEx.IconSizeEnum.ExtraLargeIcon;
                        imageList.Images.Add(ShellEx.GetBitmapFromFilePath(item, size));
                        // imageList.Images.Add(GetIcon(item));
                    }

                    // imageList.Images.Add(GetIcon(item));

                    FileInfo fi = new FileInfo(item);
                    listFiles.Add(fi.FullName);
                    listView.Items.Add(fi.Name, imageList.Images.Count - 1);
                }
            }
        }

        private List<string> listFiles = new List<string>();

        [DllImport("shell32.dll")]
        private static extern IntPtr ExtractAssociatedIcon(IntPtr hInst,
        StringBuilder lpIconPath, out ushort lpiIcon);

        public static Icon GetIconOldSchool(string fileName)
        {
            StringBuilder strB = new StringBuilder(fileName);
            IntPtr handle = ExtractAssociatedIcon(IntPtr.Zero, strB, out ushort uicon);
            Icon ico = Icon.FromHandle(handle);

            return ico;
        }

        public static Icon GetIcon(string fileName)
        {
            try
            {
                Icon icon = Icon.ExtractAssociatedIcon(fileName);
                ShellEx.IconSizeEnum ExtraLargeIcon = default(ShellEx.IconSizeEnum);
                var size = (ShellEx.IconSizeEnum)ExtraLargeIcon;

                ShellEx.GetBitmapFromFilePath(fileName, size);

                return icon;
            }
            catch
            {
                try
                {
                    Icon icon2 = GetIconOldSchool(fileName);
                    return icon2;
                }
                catch
                {
                    return null;
                }
            }
        }

        private void listView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            string[] fList = new string[1];
            fList[0] = Pathpart;
            DataObject dataObj = new DataObject(DataFormats.FileDrop, fList);
            DragDropEffects eff = DoDragDrop(dataObj, DragDropEffects.Link | DragDropEffects.Copy);
        }

        private void listView_KeyDown(object sender, KeyEventArgs e)
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

        private string Pathpart;

        private void listView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (listView.FocusedItem != null)
            {
                string txt = listView.FocusedItem.Text;
                //string txt = listView.SelectedItems[0].Text;
                //string path = listView.FocusedItem.Text;
                string first3char = txt.Substring(0, 3) + @"\";
                // //MessageBox.Show(first3char);
                string spmcadpath = @"\\spm-adfs\CAD Data\AAACAD\";
                Pathpart = (spmcadpath + first3char + txt);
                // //MessageBox.Show(Pathpart);
            }
        }

        private void listView_MouseDoubleClick(object sender, MouseEventArgs e)
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

        #endregion Listview Events

        private void treeView1_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            //e.Node.SelectedImageIndex = 0;
            // e.Node.ImageIndex = 0;
            if (e.Node.ImageIndex == 1)
            {
                e.Node.SelectedImageIndex = 0;
                e.Node.ImageIndex = 0;
            }
        }

        private void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            //e.Node.SelectedImageIndex = 1;
            //e.Node.ImageIndex = 1;
            if (e.Node.ImageIndex == 0)
            {
                e.Node.SelectedImageIndex = 1;
                e.Node.ImageIndex = 1;
            }

            //Node doesn't exists
        }

        private void TreeView_FormClosed(object sender, FormClosedEventArgs e)
        {
            log.Info("Closed Engineering BOM " + itemnumber + " by " + System.Environment.UserName);
            this.Dispose();
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
                DataRow r = _acountsTb.Rows[int.Parse(treeNode.Tag.ToString())];
                string family = r["ItemFamily"].ToString();
                setimageaccordingtofamily(family, treeNode);
            }

            // Print each node recursively.
            foreach (TreeNode tn in treeNode.Nodes)
            {
                PrintRecursive(tn);
            }
        }

        private void setimageaccordingtofamily(string family, TreeNode treeNode)
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
            TreeNodeCollection nodes = treeView1.Nodes;
            foreach (TreeNode n in nodes)
            {
                if (n.Nodes.Count > 0)
                {
                    PrintRecursive(n);
                }
            }
        }

        private void treeView1_BeforeSelect(object sender, TreeViewCancelEventArgs e)
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

        private void SPM_MouseEnter(object sender, EventArgs e)
        {
        }

        #region ContextMenuStrip

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (treeView1.Nodes.Count > 0)
            {
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count == 1)
            {
                string txt = listView.FocusedItem.Text;
                txt = txt.Substring(0, 6);
                connectapi.addtofavorites(txt);
            }
        }

        private void Listviewcontextmenu_Opening(object sender, CancelEventArgs e)
        {
            if (listView.SelectedItems.Count == 1)
            {
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void revelInExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string filePath = Makepathfordrag();
            if (!File.Exists(filePath))
            {
                return;
            }
            string argument = "/select, \"" + filePath + "\"";
            System.Diagnostics.Process.Start("explorer.exe", argument);
        }

        private string Makepathfordrag()
        {
            string txt = listView.FocusedItem.Text;
            string first3char = txt.Substring(0, 3) + @"\";
            string spmcadpath = @"\\spm-adfs\CAD Data\AAACAD\";
            string Pathpart = (spmcadpath + first3char + txt);
            return Pathpart;
        }

        private void iteminfolistviewStripMenu_Click(object sender, EventArgs e)
        {
            if (listView.FocusedItem != null)
            {
                string txt = listView.FocusedItem.Text;
                txt = txt.Substring(0, 6);
                ItemInfo itemInfo = new ItemInfo(itemno: txt);
                itemInfo.Show();
            }
        }

        private void whereusedlistviewStripMenu_Click(object sender, EventArgs e)
        {
            if (listView.FocusedItem != null)
            {
                string txt = listView.FocusedItem.Text;
                txt = txt.Substring(0, 6);
                Processwhereused(txt);
            }
        }

        private void Processwhereused(string item)
        {
            WhereUsed whereUsed = new WhereUsed(item: item);
            whereUsed.Show();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                string txt = treeView1.SelectedNode.Text;
                txt = txt.Substring(0, 6);
                Processwhereused(txt);
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                string txt = treeView1.SelectedNode.Text;
                txt = txt.Substring(0, 6);
                ItemInfo itemInfo = new ItemInfo(itemno: txt);
                itemInfo.Show();
            }
        }

        private void addToFavoritesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                string itemstr = treeView1.SelectedNode.Text;
                itemstr = itemstr.Substring(0, 6);
                connectapi.addtofavorites(itemstr);
            }
        }

        #endregion ContextMenuStrip

        private void UIThreadException(object sender, ThreadExceptionEventArgs t)
        {
            log.Error(sender, t.Exception); errorHandler.EmailExceptionAndActionLogToSupport(sender, t.Exception, this);
        }

        private void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            log.Error(sender, (Exception)e.ExceptionObject); errorHandler.EmailExceptionAndActionLogToSupport(sender, (Exception)e.ExceptionObject, this);
        }
    }
}