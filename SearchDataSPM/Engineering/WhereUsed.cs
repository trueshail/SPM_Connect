using SPMConnectAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using wpfPreviewFlowControl;
using static SPMConnectAPI.ConnectHelper;

namespace SearchDataSPM.Engineering
{
    public partial class WhereUsed : Form
    {
        #region steupvariables

        private readonly DataTable _acountsTb;
        private readonly SPMSQLCommands connectapi = new SPMConnectAPI.SPMSQLCommands();
        private readonly TreeNode root = new TreeNode();
        private bool eng;
        private string itemnumber;
        private log4net.ILog log;
        private bool rootnodedone;
        private string txtvalue;

        #endregion steupvariables

        #region loadtree

        public WhereUsed(string item = "")
        {
            InitializeComponent();
            _acountsTb = new DataTable();
            this.itemnumber = item;
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

        private void ParentView_Load(object sender, EventArgs e)
        {
            Assy_txtbox.Focus();
            Assy_txtbox.Text = itemnumber;
            if (Assy_txtbox.Text.Length == 5 || Assy_txtbox.Text.Length == 6)
            {
                //SendKeys.Send("~");
                itemnumber = null;
                Assy_txtbox.Select();
                startprocessofwhereused();
                CallRecursive();
                // connectapi.SPM_Connect();
                if (connectapi.ConnectUser.Dept == Department.Eng) eng = true;
            }
            log4net.Config.XmlConfigurator.Configure();
            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log.Info("Opened Where Used " + itemnumber + " ");
        }

        #endregion loadtree

        #region assytextbox and button events

        private void Assy_txtbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                startprocessofwhereused();
                rootnodedone = false;
                CallRecursive();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
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
            listFiles.Clear();
            listView.Clear();
            foundlabel.Text = "Search:";
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
            listView.Clear();
            listFiles.Clear();
            foundlabel.Text = "Search:";
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

        private void Filldatatable()
        {
            const String sql = "SELECT *  FROM [SPM_Database].[dbo].[SPMConnectBOM] ORDER BY [AssyNo] DESC";

            try
            {
                _acountsTb.Clear();
                if (connectapi.cn.State == ConnectionState.Closed)
                    connectapi.cn.Open();
                SqlDataAdapter _adapter = new SqlDataAdapter(sql, connectapi.cn);

                _adapter.Fill(_acountsTb);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connectapi.cn.Close();
            }
        }

        private void Fillrootnode()
        {
            //if (Assy_txtbox.Text.Length == 6)
            //{
            Assy_txtbox.BackColor = Color.White; //to add high light
            try
            {
                treeView1.Nodes.Clear();
                RemoveChildNodes(root);
                treeView1.ResetText();
                Expandchk.Checked = false;
                //DataRow[] dr = _productTB.Select("ItemNumber = '" + txtvalue.ToString() + "'");
                DataRow[] dr = _acountsTb.Select("ItemNumber = '" + txtvalue + "'");
                if (dr.Length > 0)
                {
                    root.Text = dr[0]["ItemNumber"].ToString() + " - " + dr[0]["Description"].ToString();
                    root.Tag = _acountsTb.Rows.IndexOf(dr[0]);
                    Setimageaccordingtofamily(dr[0]["ItemFamily"].ToString(), root);

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
                else
                {
                    treeView1.Nodes.Clear();
                    RemoveChildNodes(root);
                    treeView1.ResetText();
                    Expandchk.Checked = false;
                    //DataRow[] dr = _productTB.Select("ItemNumber = '" + txtvalue.ToString() + "'");
                    dr = _acountsTb.Select("AssyNo = '" + txtvalue + "'");

                    if (dr.Length > 0)
                    {
                        root.Text = dr[0]["AssyNo"].ToString() + " - " + dr[0]["AssyDescription"].ToString();
                        root.Tag = _acountsTb.Rows.IndexOf(dr[0]);
                        Setimageaccordingtofamily(dr[0]["AssyFamily"].ToString(), root);
                        //Font f = FontStyle.Bold);
                        // root.NodeFont = f;

                        treeView1.Nodes.Add(root);
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
                    else
                    {
                        MessageBox.Show(" Item does not belong to any assembly on Genius.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
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

            foreach (DataRow dr in _acountsTb.Select("[ItemNumber] ='" + parentId + "'"))
            {
                TreeNode t = new TreeNode
                {
                    Text = dr["AssyNo"].ToString() + " - " + dr["AssyDescription"].ToString() + " ( " + dr["QuantityPerAssembly"].ToString() + " ) ",
                    Name = dr["AssyNo"].ToString(),
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
                PopulateTreeView(dr["AssyNo"].ToString(), childNode);
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

        private void startprocessofwhereused()
        {
            txtvalue = Assy_txtbox.Text;
            try
            {
                treeView1.Nodes.Clear();
                RemoveChildNodes(root);
                treeView1.ResetText();
                Filldatatable();
                Fillrootnode();
            }
            catch (Exception)

            {
                if (!String.IsNullOrEmpty(txtvalue) && Char.IsLetter(txtvalue[0]))
                {
                    MessageBox.Show(" Item does not belong to any assembly on Genius.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show("Invalid Search Parameter / Item Not Found On Genius.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cleaup2();
                    Assy_txtbox.BackColor = Color.IndianRed; //to add high light
                }
            }
        }

        #endregion assytextbox and button events

        #region open model and drawing

        private void openDrawingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string Item = treeView1.SelectedNode.Text;
            Item = Item.Substring(0, 6);
            if (eng)
            {
                connectapi.Checkforspmfile(Item);
            }
            else
            {
                connectapi.Checkforspmfileprod(Item);
            }
        }

        private void openModelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string Item = treeView1.SelectedNode.Text;
            Item = Item.Substring(0, 6);
            if (eng)
            {
                connectapi.Checkforspmfile(Item);
            }
            else
            {
                connectapi.Checkforspmfileprod(Item);
            }
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
                    ? r["AssyNo"].ToString() + r["AssyDescription"].ToString() + r["AssyManufacturer"].ToString() + r["AssyManufacturerItemNumber"].ToString()
                    : r["ItemNumber"].ToString() + r["Description"].ToString() + r["Manufacturer"].ToString() + r["ManufacturerItemNumber"].ToString();
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
                            foundlabel.Text = txtSearch.Text.Length > 0
                                ? "Found " + LastNodeIndex + " of " + CurrentNodeMatches.Count + " matching items containing keyword \"" + searchText + "\""
                                : "Search:";
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
            else
            {
                foundlabel.Text = "Search:";
            }
        }

        private void txtSearch_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            txtSearch.Clear();
            foundlabel.Text = "Search:";
        }

        #endregion search tree

        #region treeview events

        public TreeNode n;

        private string chekroot;

        private void treeView1_AfterExpand(object sender, TreeViewEventArgs e)
        {
            Expandchk.Checked = true;
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string ItemNo;
            if (n != null)
            {
                n.BackColor = treeView1.BackColor;
                n.ForeColor = treeView1.ForeColor;
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
                filllistview(ItemNo);
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
                filllistview(ItemNo);
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
                filllistview(ItemNo);
                //getfilepathname(ItemNo);
            }
        }

        private void treeView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
            //string[] fList = new string[1];
            //fList[0] = fName;
            //DataObject dataObj = new DataObject(DataFormats.FileDrop, fList);
            //DragDropEffects eff = DoDragDrop(dataObj, DragDropEffects.Link | DragDropEffects.Copy);
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

        private void treeView1_Leave(object sender, EventArgs e)
        {
            if (treeView1.Nodes.Count > 0)
            {
                n = treeView1.SelectedNode;
                n.BackColor = Color.Gray;
                n.ForeColor = Color.White;
            }
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            treeView1.SelectedNode = e.Node;
        }

        #endregion treeview events

        #region Listview Events

        private readonly List<string> listFiles = new List<string>();

        private string Pathpart;

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
                    Icon icon2 = GetIconOldSchool(fileName);
                    return icon2;
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
            Icon ico = Icon.FromHandle(handle);

            return ico;
        }

        [DllImport("shell32.dll")]
        private static extern IntPtr ExtractAssociatedIcon(IntPtr hInst,
        StringBuilder lpIconPath, out ushort lpiIcon);

        private void filllistview(string item)
        {
            try
            {
                listFiles.Clear();
                listView.Items.Clear();
                string first3char = item.Substring(0, 3) + @"\";
                const string spmcadpath = @"\\spm-adfs\CAD Data\AAACAD\";
                string Pathpart = (spmcadpath + first3char);
                getitemstodisplay(Pathpart, item);
            }
            catch
            {
                return;
            }
        }

        private void getitemstodisplay(string Pathpart, string ItemNo)
        {
            if (Directory.Exists(Pathpart))
            {
                foreach (string item in Directory.GetFiles(Pathpart, "*" + ItemNo + "*").Where(str => !str.Contains(@"\~$")).OrderByDescending(fi => fi))
                {
                    try
                    {
                        string sDocFileName = item;
                        wpfThumbnailCreator pvf = new wpfThumbnailCreator();
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

                        const ShellEx.IconSizeEnum size = ShellEx.IconSizeEnum.ExtraLargeIcon;
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

        private void listView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            string[] fList = new string[1];
            fList[0] = Pathpart;
            DataObject dataObj = new DataObject(DataFormats.FileDrop, fList);
            _ = DoDragDrop(dataObj, DragDropEffects.Link | DragDropEffects.Copy);
        }

        private void listView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (listView.FocusedItem != null)
            {
                string txt = listView.FocusedItem.Text;
                string first3char = txt.Substring(0, 3) + @"\";
                const string spmcadpath = @"\\spm-adfs\CAD Data\AAACAD\";
                Pathpart = (spmcadpath + first3char + txt);
            }
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

        #region getbomtree

        private void getBOMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string item = treeView1.SelectedNode != null ? ItemTxtBox.Text : "";
            processbom(item);
        }

        //public static string assytree;
        private void processbom(string itemvalue)
        {
            //assytree = itemvalue;
            TreeView treeView = new TreeView(item: itemvalue);
            treeView.Show();
            //assytree = null;
        }

        #endregion getbomtree

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

        private void PrintRecursive(TreeNode treeNode)
        {
            if (treeNode.Index == 0 && !rootnodedone)
            {
                rootnodedone = true;
            }
            else
            {
                DataRow r = _acountsTb.Rows[int.Parse(treeNode.Tag.ToString())];
                string family = r["AssyFamily"].ToString();
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

        #region Contextmenu Strip

        private void addToFavoritesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                string itemstr = treeView1.SelectedNode.Text;
                itemstr = itemstr.Substring(0, 6);
                connectapi.Addtofavorites(itemstr, false);
            }
        }

        private void bomlistviewmenustrpc_Click(object sender, EventArgs e)
        {
            if (listView.FocusedItem != null)
            {
                string txt = listView.FocusedItem.Text;
                txt = txt.Substring(0, 6);
                processbom(txt);
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (treeView1.Nodes.Count <= 0)
            {
                e.Cancel = true;
            }
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

        private void Listviewcontextmenu_Opening(object sender, CancelEventArgs e)
        {
            if (listView.SelectedItems.Count != 1)
            {
                e.Cancel = true;
            }
        }

        private string makepathfordrag()
        {
            string txt = listView.FocusedItem.Text;
            string first3char = txt.Substring(0, 3) + @"\";
            const string spmcadpath = @"\\spm-adfs\CAD Data\AAACAD\";
            string Pathpart = (spmcadpath + first3char + txt);
            return Pathpart;
        }

        private void revelInExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string filePath = makepathfordrag();
            if (!File.Exists(filePath))
            {
                return;
            }
            string argument = "/select, \"" + filePath + "\"";
            Process.Start("explorer.exe", argument);
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

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count == 1)
            {
                string txt = listView.FocusedItem.Text;
                txt = txt.Substring(0, 6);
                connectapi.Addtofavorites(txt, false);
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

        #endregion Contextmenu Strip

        private void WhereUsed_FormClosed(object sender, FormClosedEventArgs e)
        {
            log.Info("Closed Where Used " + itemnumber + " ");
            this.Dispose();
        }
    }
}