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

    public partial class TreeView : Form

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

        public TreeView()

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


            _acountsTb = new DataTable();
            _command = new SqlCommand();
            _command.Connection = _connection;
            _productTB = new DataTable();
            Rectangle screen = Screen.PrimaryScreen.WorkingArea;
            int w = Width >= screen.Width ? screen.Width : (screen.Width + Width) / 3;
            int h = Height >= screen.Height ? screen.Height : (screen.Height + Height) / 3;
            this.Location = new Point((screen.Width - w) / 2, (screen.Height - h) / 2);
            this.Size = new Size(w, h);
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

            // Assy_txtbox.Text = SPM_Connect.assytree;
            Assy_txtbox.Text = itemnumber;

            //if (Assy_txtbox.Text.Length == 0)
            //{
            //    Assy_txtbox.Text = SPM_ConnectDuplicates.assytree;
            //}
            //if (Assy_txtbox.Text.Length == 0)
            //{
            //    Assy_txtbox.Text = SPM_ConnectProduction.assytree;
            //}
            //if (Assy_txtbox.Text.Length == 0)
            //{
            //    Assy_txtbox.Text = WhereUsed.assytree;
            //}
            //if (Assy_txtbox.Text.Length == 0)
            //{
            //    Assy_txtbox.Text = SPM_ConnectJobs.jobtree;
            //}


            if (Assy_txtbox.Text.Length == 5 || Assy_txtbox.Text.Length == 6)
            {
                //SendKeys.Send("~");
                itemnumber = null;
                startprocessofbom();

            }


            Assy_txtbox.Select();


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


            return base.ProcessCmdKey(ref msg, keyData);
        }

        #endregion

        #region assytextbox and button events


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
            listView.Clear();
            listFiles.Clear();
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
                    MessageBox.Show(" Item does not contain a Bill OF Material on Genius.", "SPM Connect - Bill Of Manufacturing", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    // Assy_txtbox.Clear();
                    this.Hide();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Invalid Search Parameter / Item Not Found On Genius.", "SPM Connect - Bill Of Manufacturing", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cleaup2();
                    Assy_txtbox.BackColor = Color.IndianRed; //to add high light
                    //Assy_txtbox.Clear();
                }


            }
        }

        private void filldatatable()
        {
            String sql = "SELECT *  FROM [SPM_Database].[dbo].[SPMConnectBOM] ORDER BY [ItemNumber]";

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
                    DataRow[] dr = _acountsTb.Select("AssyNo = '" + txtvalue.ToString() + "'");


                    root.Text = dr[0]["AssyNo"].ToString() + " - " + dr[0]["AssyDescription"].ToString();
                    root.Tag = _acountsTb.Rows.IndexOf(dr[0]);

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
                TreeNode t = new TreeNode();
                t.Text = dr["ItemNumber"].ToString() + " - " + dr["Description"].ToString() + " ( " + dr["QuantityPerAssembly"].ToString() + " ) ";
                t.Name = dr["ItemNumber"].ToString();
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

        #endregion

        #region open model and drawing

        private void checkforspmfile(string ItemNo)
        {
            string ItemNumbero = ItemNo + "-0";


            if (!String.IsNullOrWhiteSpace(ItemNo) && ItemNo.Length == 6)

            {
                string first3char = ItemNo.Substring(0, 3) + @"\";
                //MessageBox.Show(first3char);

                string spmcadpath = @"\\spm-adfs\CAD Data\AAACAD\";

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

        private void checkforspmdrwfile(string str)
        {
            string ItemNumbero = str + "-0";


            if (!String.IsNullOrWhiteSpace(str) && str.Length == 6)

            {
                string first3char = str.Substring(0, 3) + @"\";
                //MessageBox.Show(first3char);

                string spmcadpath = @"\\spm-adfs\CAD Data\AAACAD\";

                string Drawpath = (spmcadpath + first3char + str + ".SLDDRW");

                string drawpathno = (spmcadpath + first3char + ItemNumbero + ".SLDDRW");


                if (File.Exists(drawpathno) && File.Exists(Drawpath))
                {
                    MessageBox.Show($"System has found a Part two files " + str + "," + ItemNumbero + " with the same PartNo." +
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

                    MessageBox.Show($"A file with the part number" + str + " does not have Solidworks Drawing File. Please Try Again.", "SPM-Automation", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
            else
            {

            }

        }

        private void openModelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string itemstr = treeView1.SelectedNode.Text;
            itemstr = itemstr.Substring(0, 6);
            checkforspmfile(itemstr);

        }

        private void openDrawingToolStripMenuItem_Click(object sender, EventArgs e)
        {

            string itemstr = treeView1.SelectedNode.Text;
            itemstr = itemstr.Substring(0, 6);

            //MessageBox.Show(ItemNo);

            checkforspmdrwfile(itemstr);

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

        public TreeNode n;

        private void treeView1_Leave(object sender, EventArgs e)
        {
            n = treeView1.SelectedNode;
            n.BackColor = Color.LightBlue;
        }

        #endregion

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
                        System.Drawing.Size size = new Size();
                        size.Width = 256;
                        size.Height = 256;
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

        List<string> listFiles = new List<string>();
        [DllImport("shell32.dll")]
        static extern IntPtr ExtractAssociatedIcon(IntPtr hInst,
        StringBuilder lpIconPath, out ushort lpiIcon);

        public static Icon GetIconOldSchool(string fileName)
        {
            ushort uicon;
            StringBuilder strB = new StringBuilder(fileName);
            IntPtr handle = ExtractAssociatedIcon(IntPtr.Zero, strB, out uicon);
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

        string Pathpart;

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

        #endregion

        private void treeView1_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            e.Node.SelectedImageIndex = 0;
            e.Node.ImageIndex = 0;
        }

        private void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {

            e.Node.SelectedImageIndex = 1;
            e.Node.ImageIndex = 1;
            //Node doesn't exists

        }

        private void TreeView_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }
    }

}
