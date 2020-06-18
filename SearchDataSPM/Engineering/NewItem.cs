using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using SPMConnectAPI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using wpfPreviewFlowControl;

namespace SearchDataSPM.Engineering
{
    public partial class NewItem : Form

    {
        #region steupvariables

        private readonly DataTable _acountsTb;
        private readonly SPMConnectAPI.SPMSQLCommands connectapi = new SPMSQLCommands();
        private bool doneshowingSplash;
        private log4net.ILog log;

        #endregion steupvariables

        #region form load

        private string checkedit;

        private readonly string itemnumber;

        public NewItem(string item)
        {
            InitializeComponent();
            _acountsTb = new DataTable();
            this.itemnumber = item;
        }

        public string Editbtn(string chekedit)
        {
            if (chekedit.Length > 0)
                return checkedit = chekedit;
            return null;
        }

        private void NewItem_Load(object sender, EventArgs e)
        {
            // Suspend the layout logic for the form, while the application is initializing
            this.SuspendLayout();
            this.Text = "Item Details - SPM Connect (" + itemnumber + ")";

            Filllistview(itemnumber);
            Filldescriptionsource();
            Fillmanufacturers();
            Filloem();
            Fillmaterials();
            Fillsurface();
            Fillheattreats();
            Fillfamilycodes();
            Filldatatable(itemnumber);
            //SPM_Connect sPM_Connect = new SPM_Connect();
            //string editbutton =  sPM_Connect.chekeditbutton.ToString();
            if (checkedit == "yes")
            {
                Processsavebutton();
                Processeditbutton();
            }

            log4net.Config.XmlConfigurator.Configure();
            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log.Info("Opened Create/Edit Item " + itemnumber + " ");
            // Resume the layout logic
            this.ResumeLayout();
        }

        private void SPM_DoubleClick(object sender, EventArgs e)
        {
            Process.Start("http://www.spm-automation.com/");
        }

        #endregion form load

        #region Fillinformation on load

        private void Filldatatable(string itemnumber)
        {
            _acountsTb.Clear();
            string sql = "SELECT *  FROM [SPM_Database].[dbo].[Inventory] WHERE [ItemNumber]='" + itemnumber + "'";

            try
            {
                if (connectapi.cn.State == ConnectionState.Closed)
                    connectapi.cn.Open();
                SqlDataAdapter _adapter = new SqlDataAdapter(sql, connectapi.cn);
                _adapter?.Fill(_acountsTb);
                if (_acountsTb.Rows.Count > 0)
                {
                    Fillinfo();
                }
                else
                {
                    connectapi.Addcpoieditemtosqltablefromgenius(itemnumber, itemnumber);
                    Fillinfo();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect New Item - Fill Data Table", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connectapi.cn.Close();
            }
        }

        private void Filldescriptionsource()
        {
            AutoCompleteStringCollection MyCollection = connectapi.Filldescriptionsource();
            Descriptiontxtbox.AutoCompleteCustomSource = MyCollection;
        }

        private void Fillfamilycodes()
        {
            AutoCompleteStringCollection MyCollection = connectapi.Fillfamilycodes();
            familycombobox.AutoCompleteCustomSource = MyCollection;
            familycombobox.DataSource = MyCollection;
        }

        private void Fillheattreats()
        {
            AutoCompleteStringCollection MyCollection = connectapi.Fillheattreats();
            heattreat.AutoCompleteCustomSource = MyCollection;
        }

        private void Fillinfo()
        {
            try
            {
                if (_acountsTb.Rows.Count > 0)
                {
                    DataRow r = _acountsTb.Rows[0];
                    ItemTxtBox.Text = r["ItemNumber"].ToString();
                    Descriptiontxtbox.Text = r["Description"].ToString();
                    oemtxtbox.Text = r["Manufacturer"].ToString();
                    oemitemtxtbox.Text = r["ManufacturerItemNumber"].ToString();
                    //familytxtbox.Text = r["FamilyCode"].ToString();
                    //sparetxtbox.Text = r["Spare"].ToString();
                    mattxt.Text = r["Material"].ToString();
                    designbytxt.Text = r["DesignedBy"].ToString();
                    categorytxtbox.Text = r["FamilyType"].ToString();
                    surfacetxt.Text = r["SurfaceProtection"].ToString();
                    heattreat.Text = r["HeatTreatment"].ToString();
                    datecreatedtxt.Text = r["DateCreated"].ToString();
                    dateeditxt.Text = r["LastEdited"].ToString();
                    Lastsavedtxtbox.Text = r["LastSavedBy"].ToString();
                    Notestextbox.Text = r["Notes"].ToString();
                    rupturetextbox.Text = r["Rupture"].ToString();
                    string famiulycode = r["FamilyCode"].ToString();
                    if (famiulycode.Length > 0)
                    {
                        familycombobox.SelectedItem = famiulycode;
                    }
                    else
                    {
                        familycombobox.SelectedItem = "MA";
                        categorytxtbox.Text = "Manufactured";
                        rupturetextbox.Text = "ALWAYS";
                    }

                    if (r["Spare"].ToString().Equals("SPARE"))
                    {
                        checkBox1.Checked = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect New Item - Fill Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Filllistview(string item)
        {
            listFiles.Clear();
            listView.Items.Clear();
            Getitemstodisplay(Makepath(item), item);
        }

        private void Fillmanufacturers()
        {
            AutoCompleteStringCollection MyCollection = connectapi.Fillmanufacturers();
            oemtxtbox.AutoCompleteCustomSource = MyCollection;
        }

        private void Fillmaterials()
        {
            AutoCompleteStringCollection MyCollection = connectapi.FillnewitemMaterials();
            mattxt.AutoCompleteCustomSource = MyCollection;
        }

        private void Filloem()
        {
            AutoCompleteStringCollection MyCollection = connectapi.Filloem();
            oemitemtxtbox.AutoCompleteCustomSource = MyCollection;
        }

        private void Fillsurface()
        {
            AutoCompleteStringCollection MyCollection = connectapi.Fillsurface();
            surfacetxt.AutoCompleteCustomSource = MyCollection;
        }

        #endregion Fillinformation on load

        #region listview events

        private readonly List<string> listFiles = new List<string>();

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

        private static string Makepath(string itemnumber)
        {
            if (itemnumber.Length > 0)
            {
                string first3char = itemnumber.Substring(0, 3) + @"\";
                const string spmcadpath = @"\\spm-adfs\CAD Data\AAACAD\";
                string Pathpart = (spmcadpath + first3char);
                return Pathpart;
            }
            else
            {
                return null;
            }
        }

        private void Getitemstodisplay(string Pathpart, string ItemNo)
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

        private void ListView_KeyDown(object sender, KeyEventArgs e)
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
                    MessageBox.Show(ex.Message, "SPM Connect New Item ListView Keypress Enter");
                }
            }
        }

        private void ListView_MouseDoubleClick(object sender, MouseEventArgs e)
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
                    MessageBox.Show(ex.Message, "SPM Connect New Item Listview MouseClick");
                }
            }
        }

        #endregion listview events

        #region SAVE ITEM

        private readonly List<string> list = new List<string>();

        private void Createnewitemtosql(string itemnumber, string description, string family, string manufacturer, string oem, string material, string familytype, string surface, string heat, string lastsavedby, string rupture, string notes)
        {
            DateTime dateedited = DateTime.Now;
            string sqlFormattedDate = dateedited.ToString("yyyy-MM-dd HH:mm:ss.fff");
            if (connectapi.cn.State == ConnectionState.Closed)
                connectapi.cn.Open();
            try
            {
                SqlCommand cmd = connectapi.cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE [SPM_Database].[dbo].[Inventory] SET Description = '" + description + "',FamilyCode = '" + family + "',Manufacturer = '" + manufacturer + "',ManufacturerItemNumber = '" + oem + "',Material = '" + material + "',Spare = '" + (checkBox1.Checked ? "SPARE" : null) + "',FamilyType = '" + familytype + "',SurfaceProtection = '" + surface + "',HeatTreatment = '" + heat + "',LastSavedBy = '" + lastsavedby + "',Rupture = '" + rupture + "',Notes = '" + notes + "',LastEdited = '" + sqlFormattedDate + "' WHERE ItemNumber = '" + itemnumber + "' ";
                cmd.ExecuteNonQuery();
                connectapi.cn.Close();
                //MessageBox.Show("Item sucessfully saved SPM Connect Server.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connectapi.cn.Close();
            }

            //perfromlockdown();
        }

        private void Editbttn_Click(object sender, EventArgs e)
        {
            Processeditbutton();
        }

        private void Familycombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (familycombobox.Text.Length > 0)
            {
                if (string.Equals(familycombobox.Text, "ma", StringComparison.CurrentCultureIgnoreCase) || string.Equals(familycombobox.Text, "as", StringComparison.CurrentCultureIgnoreCase) || string.Equals(familycombobox.Text, "mawe", StringComparison.CurrentCultureIgnoreCase) || string.Equals(familycombobox.Text, "ag", StringComparison.CurrentCultureIgnoreCase) || string.Equals(familycombobox.Text, "asel", StringComparison.CurrentCultureIgnoreCase) || string.Equals(familycombobox.Text, "aspn", StringComparison.CurrentCultureIgnoreCase))
                {
                    categorytxtbox.Text = "Manufactured";
                    rupturetextbox.Text = "ALWAYS";
                }
                else if (string.Equals(familycombobox.Text, "as", StringComparison.CurrentCultureIgnoreCase) || string.Equals(familycombobox.Text, "ag", StringComparison.CurrentCultureIgnoreCase) || string.Equals(familycombobox.Text, "asel", StringComparison.CurrentCultureIgnoreCase) || string.Equals(familycombobox.Text, "aspn", StringComparison.CurrentCultureIgnoreCase))
                {
                    categorytxtbox.Text = "Assembly";
                    rupturetextbox.Text = "ALWAYS";
                }
                else if (string.Equals(familycombobox.Text, "dr", StringComparison.CurrentCultureIgnoreCase))
                {
                    categorytxtbox.Text = "Drawn Only";
                    rupturetextbox.Text = "ALWAYS";
                }
                else
                {
                    categorytxtbox.Text = "Purchased";
                    rupturetextbox.Text = "NEVER";
                }
            }
        }

        private void Graballinfor()
        {
            list.Clear();
            string itemnumber = ItemTxtBox.Text;
            string description = Descriptiontxtbox.Text;
            string material = mattxt.Text;
            string manufacturer = oemtxtbox.Text;
            string oem = oemitemtxtbox.Text;
            //string family = familytxtbox.Text;
            string family = familycombobox.Text;
            string familytype = categorytxtbox.Text;
            string surface = surfacetxt.Text;
            string heat = heattreat.Text;
            string notes = Notestextbox.Text;
            string rupture = rupturetextbox.Text;
            list.Add(itemnumber);
            list.Add(description);
            list.Add(material);
            list.Add(manufacturer);
            list.Add(oem);
            list.Add(family);
            list.Add(familytype);
            list.Add(surface);
            list.Add(heat);
            list.Add(notes);
            list.Add(rupture);

            for (int i = 0; i < list.Count; i++)
            {
                list[i] = list[i].Replace("'", "''");
            }
        }

        private void NewItem_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (savebttn.Visible)
            {
                DialogResult result = MessageBox.Show("Are you sure want to close without saving changes?", "SPM Connect", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result != DialogResult.Yes)
                {
                    e.Cancel = (result == DialogResult.No);
                }
            }
        }

        private void Perfromlockdown()
        {
            editbttn.Visible = true;
            savebttn.Enabled = false;
            savebttn.Visible = false;
            Descriptiontxtbox.ReadOnly = true;
            mattxt.ReadOnly = true;
            oemtxtbox.ReadOnly = true;
            oemitemtxtbox.ReadOnly = true;
            familycombobox.Enabled = false;
            categorytxtbox.ReadOnly = true;
            surfacetxt.ReadOnly = true;
            heattreat.ReadOnly = true;
            Notestextbox.ReadOnly = true;
            rupturetextbox.ReadOnly = true;
            checkBox1.Enabled = false;
            Descriptiontxtbox.AutoCompleteSource = AutoCompleteSource.None;
            mattxt.AutoCompleteSource = AutoCompleteSource.None;
            oemtxtbox.AutoCompleteSource = AutoCompleteSource.None;
            oemitemtxtbox.AutoCompleteSource = AutoCompleteSource.None;
            familycombobox.AutoCompleteSource = AutoCompleteSource.None;
            surfacetxt.AutoCompleteSource = AutoCompleteSource.None;
            heattreat.AutoCompleteSource = AutoCompleteSource.None;
            Descriptiontxtbox.AutoCompleteMode = AutoCompleteMode.None;
            mattxt.AutoCompleteMode = AutoCompleteMode.None;
            oemtxtbox.AutoCompleteMode = AutoCompleteMode.None;
            oemitemtxtbox.AutoCompleteMode = AutoCompleteMode.None;
            surfacetxt.AutoCompleteMode = AutoCompleteMode.None;
            heattreat.AutoCompleteMode = AutoCompleteMode.None;
        }

        private void Processeditbutton()
        {
            editbttn.Visible = false;
            savebttn.Enabled = true;
            savebttn.Visible = true;
            Descriptiontxtbox.ReadOnly = false;
            mattxt.ReadOnly = false;
            oemtxtbox.ReadOnly = false;
            oemitemtxtbox.ReadOnly = false;
            familycombobox.Enabled = true;
            categorytxtbox.ReadOnly = false;
            surfacetxt.ReadOnly = false;
            heattreat.ReadOnly = false;
            Notestextbox.ReadOnly = false;
            rupturetextbox.ReadOnly = false;
            checkBox1.Enabled = true;
            Descriptiontxtbox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            mattxt.AutoCompleteSource = AutoCompleteSource.CustomSource;
            oemtxtbox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            oemitemtxtbox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            familycombobox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            surfacetxt.AutoCompleteSource = AutoCompleteSource.CustomSource;
            heattreat.AutoCompleteSource = AutoCompleteSource.CustomSource;
            Descriptiontxtbox.AutoCompleteMode = AutoCompleteMode.Suggest;
            mattxt.AutoCompleteMode = AutoCompleteMode.Suggest;
            oemtxtbox.AutoCompleteMode = AutoCompleteMode.Suggest;
            oemitemtxtbox.AutoCompleteMode = AutoCompleteMode.Suggest;
            surfacetxt.AutoCompleteMode = AutoCompleteMode.Suggest;
            heattreat.AutoCompleteMode = AutoCompleteMode.Suggest;
        }

        private async void Processmodelcreattion(string item)
        {
            listView.Refresh();
            if (listView.Items.Count == 0)
            {
                string category = connectapi.Getfamilycategory(familycombobox.SelectedItem.ToString());
                string path = Makepath(item);
                if (string.Equals(category, "manufactured", StringComparison.CurrentCultureIgnoreCase))
                {
                    //MessageBox.Show(path);
                    Directory.CreateDirectory(path);
                    string filename = path + item + ".sldprt";
                    connectapi.Createmodel(filename);
                    string draw = path + item + ".slddrw";
                    connectapi.Createdrawingpart(draw, item);
                }
                else if (string.Equals(category, "assembly", StringComparison.CurrentCultureIgnoreCase))
                {
                    Directory.CreateDirectory(path);
                    string filename = path + item + ".sldasm";
                    connectapi.CreateAssy(filename);
                    string draw = path + item + ".slddrw";
                    connectapi.Createdrwaingassy(draw, item);
                }
                else
                {
                    string filename = path + item + ".sldprt";
                    DialogResult result = MessageBox.Show("Do you want to import model for the purchased part?", "SPM Connect", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        string fileimporttype = "";
                        Engineering.ImportFileSelector importFileSelector = new Engineering.ImportFileSelector();

                        if (importFileSelector.ShowDialog() == DialogResult.OK)
                        {
                            fileimporttype = importFileSelector.ValueIWant;
                        }

                        if (fileimporttype == "STEP")
                        {
                            const string stepfilter = "STEP AP203/214/242|*.step;*.stp";
                            string impfilname = Importfilename(stepfilter);
                            if (string.Equals(Path.GetExtension(impfilname), ".step", StringComparison.CurrentCultureIgnoreCase) || string.Equals(Path.GetExtension(impfilname), ".stp", StringComparison.CurrentCultureIgnoreCase))
                            {
                                //new Thread(() => new Engineering.WaitFormImport().ShowDialog()).Start();
                                //Thread.Sleep(3000);
                                await Task.Run(() => SplashDialog("Importing Model....")).ConfigureAwait(true);
                                Thread.Sleep(1000);
                                if (connectapi.Importstepfile(impfilname, filename))
                                {
                                    if (connectapi.Getfilename() != item)
                                    {
                                        connectapi.Open_model(filename);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Solidworks encountered importing error. Please restart solidworks in oder to prevent this error in future.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    Directory.CreateDirectory(path);
                                    connectapi.Createmodel(filename);
                                }
                                doneshowingSplash = true;
                                //Engineering.WaitFormImport f = new Engineering.WaitFormImport();
                                //f = (Engineering.WaitFormImport)Application.OpenForms["WaitFormImport"];
                                //f.Invoke(new ThreadStart(delegate { f.Close(); }));
                            }
                            else
                            {
                                MessageBox.Show("Selected File extension did not match the file type selected to import.!", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Directory.CreateDirectory(path);
                                connectapi.Createmodel(filename);
                            }
                        }
                        else if (fileimporttype == "IGES")
                        {
                            const string igesfilter = "IGES|*.igs;*.iges";
                            string impfilname = Importfilename(igesfilter);
                            if (string.Equals(Path.GetExtension(impfilname), ".igs", StringComparison.CurrentCultureIgnoreCase) || string.Equals(Path.GetExtension(impfilname), ".iges", StringComparison.CurrentCultureIgnoreCase))
                            {
                                //new Thread(() => new Engineering.WaitFormImport().ShowDialog()).Start();
                                //Thread.Sleep(3000);
                                await Task.Run(() => SplashDialog("Importing Model....")).ConfigureAwait(true);
                                Thread.Sleep(1000);

                                if (connectapi.Importigesfile(impfilname, filename))
                                {
                                    if (connectapi.Getfilename() != item)
                                    {
                                        connectapi.Open_model(filename);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Solidworks encountered importing error. Please restart solidworks in oder to prevent this error in future.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    Directory.CreateDirectory(path);
                                    connectapi.Createmodel(filename);
                                }
                                doneshowingSplash = true;
                                //Engineering.WaitFormImport f = new Engineering.WaitFormImport();
                                //f = (Engineering.WaitFormImport)Application.OpenForms["WaitFormImport"];
                                //f.Invoke(new ThreadStart(delegate { f.Close(); }));
                            }
                            else
                            {
                                MessageBox.Show("Selected File extension did not match the file type selected to import.!", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Directory.CreateDirectory(path);
                                connectapi.Createmodel(filename);
                            }
                        }
                        else if (fileimporttype == "PARASOLID")
                        {
                            const string parasolidfilter = "Parasolid|*.x_t;*.x_b;*.xmt_txt;*.xmt_bin";
                            string impfilname = Importfilename(parasolidfilter);
                            if (string.Equals(Path.GetExtension(impfilname), ".x_t", StringComparison.CurrentCultureIgnoreCase) || string.Equals(Path.GetExtension(impfilname), ".x_b", StringComparison.CurrentCultureIgnoreCase) || string.Equals(Path.GetExtension(impfilname), ".xmt_txt", StringComparison.CurrentCultureIgnoreCase) || string.Equals(Path.GetExtension(impfilname), ".xmt_bin", StringComparison.CurrentCultureIgnoreCase))
                            {
                                //new Thread(() => new Engineering.WaitFormImport().ShowDialog()).Start();
                                //Thread.Sleep(3000);
                                await Task.Run(() => SplashDialog("Importing Model....")).ConfigureAwait(true);
                                Thread.Sleep(1000);

                                if (connectapi.Importparasolidfile(impfilname, filename))
                                {
                                    if (connectapi.Getfilename() != item)
                                    {
                                        connectapi.Open_model(filename);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Solidworks encountered importing error. Please restart solidworks in oder to prevent this error in future.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    Directory.CreateDirectory(path);
                                    connectapi.Createmodel(filename);
                                }
                                doneshowingSplash = true;
                                //Engineering.WaitFormImport f = new Engineering.WaitFormImport();
                                //f = (Engineering.WaitFormImport)Application.OpenForms["WaitFormImport"];
                                //f.Invoke(new ThreadStart(delegate { f.Close(); }));
                            }
                            else
                            {
                                MessageBox.Show("Selected File extension did not match the file type selected to import.!", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Directory.CreateDirectory(path);
                                connectapi.Createmodel(filename);
                            }
                        }
                        else
                        {
                            Directory.CreateDirectory(path);
                            connectapi.Createmodel(filename);
                        }
                    }
                    else
                    {
                        Directory.CreateDirectory(path);
                        connectapi.Createmodel(filename);
                    }
                }
            }
        }

        private void Processsavebutton()
        {
            Cursor.Current = Cursors.WaitCursor;
            this.Enabled = false;
            Graballinfor();
            string lastsavedby = connectapi.ConnectUser.Name;
            Filllistview(ItemTxtBox.Text);
            if (listView.Items.Count == 0)
            {
                Processmodelcreattion(ItemTxtBox.Text);
            }
            Filllistview(ItemTxtBox.Text);
            Chekbeforefillingcustomproperties(ItemTxtBox.Text);
            Createnewitemtosql(list[0], list[1], list[5], list[3], list[4], list[2], list[6], list[7], list[8], lastsavedby, list[10], list[9]);
            Perfromlockdown();
            this.Enabled = true;
            Cursor.Current = Cursors.Default;
        }

        private async void Savebttn_Click(object sender, EventArgs e)
        {
            if (connectapi.Solidworks_running())
            {
                listView.Refresh();
                if (listView.Items.Count != 0)
                {
                    await Task.Run(() => SplashDialog("Saving Model...")).ConfigureAwait(true);
                    Processsavebutton();
                    doneshowingSplash = true;
                }
                else
                {
                    Processsavebutton();
                }
            }
        }

        private void SplashDialog(string message)
        {
            doneshowingSplash = false;
            ThreadPool.QueueUserWorkItem((_) =>
            {
                using (var splashForm = new Dialog())
                {
                    splashForm.TopMost = true;
                    splashForm.Focus();
                    splashForm.Activate();
                    splashForm.Message = message;
                    splashForm.Location = new Point(this.Location.X + ((this.Width - splashForm.Width) / 2), this.Location.Y + ((this.Height - splashForm.Height) / 2));
                    splashForm.Show();
                    while (!doneshowingSplash)
                        Application.DoEvents();
                    splashForm.Close();
                }
            });
        }

        #endregion SAVE ITEM

        #region solidworks createmodels and open models

        private string Importfilename(string filter)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = filter;
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.

            if (result == DialogResult.OK) // Test result.
            {
                string file = openFileDialog1.FileName;
                return file;
            }
            return "";
        }

        #endregion solidworks createmodels and open models

        #region solidworks custom properties

        public void Chekbeforefillingcustomproperties(string item)
        {
            if (connectapi.Solidworks_running())
            {
                string getcurrentfilename = connectapi.Getfilename();
                if (string.IsNullOrEmpty(getcurrentfilename))
                    return;
                string olditemnumber = item + "-0";
                if (getcurrentfilename == item || getcurrentfilename == olditemnumber)
                {
                    Fillcustomproperties();
                }
                else
                {
                    //if (checkforreadonly() == true)
                    //{
                    //    fillcustomproperties();
                    //}

                    string Pathassy = Makepath(item) + ItemTxtBox.Text + ".sldasm";
                    string Pathpart = Makepath(item) + ItemTxtBox.Text + ".sldprt";
                    string Pathassyo = Makepath(item) + ItemTxtBox.Text + "-0.sldasm";
                    string Pathparto = Makepath(item) + ItemTxtBox.Text + "-0.sldprt";

                    if (File.Exists(Pathassy))
                    {
                        connectapi.Open_assy(Pathassy);
                        Fillcustomproperties();
                    }
                    else if (File.Exists(Pathpart))
                    {
                        connectapi.Open_model(Pathpart);
                        Fillcustomproperties();
                    }
                    else if (File.Exists(Pathparto))
                    {
                        connectapi.Open_model(Pathparto);
                        Fillcustomproperties();
                    }
                    else if (File.Exists(Pathassyo))
                    {
                        connectapi.Open_assy(Pathassyo);
                        Fillcustomproperties();
                    }
                    else
                    {
                        MessageBox.Show("Please have the active model open in order to save custom properties to the soliworks document..", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }

            //swApp.Visible = true;
            //ModelDoc2 swModel;
            //swModel = swApp.ActiveDoc;
        }

        public void Fillcustomproperties()
        {
            if (connectapi.Solidworks_running())
            {
                try
                {
                    const string progId = "SldWorks.Application";

                    SldWorks swApp = Marshal.GetActiveObject(progId) as SolidWorks.Interop.sldworks.SldWorks;
                    ModelDoc2 swModel;
                    CustomPropertyManager cusPropMgr;
                    int lRetVal;
                    swModel = (ModelDoc2)swApp.ActiveDoc;
                    if (swModel == null)
                        return;
                    ModelDocExtension swModelDocExt = swModel.Extension;
                    cusPropMgr = swModelDocExt.get_CustomPropertyManager("");
                    lRetVal = cusPropMgr.Add3("PartNo", (int)swCustomInfoType_e.swCustomInfoText, list[0], (int)swCustomPropertyAddOption_e.swCustomPropertyDeleteAndAdd);
                    lRetVal = cusPropMgr.Add3("Description", (int)swCustomInfoType_e.swCustomInfoText, list[1], (int)swCustomPropertyAddOption_e.swCustomPropertyDeleteAndAdd);
                    lRetVal = cusPropMgr.Add3("OEM", (int)swCustomInfoType_e.swCustomInfoText, list[3], (int)swCustomPropertyAddOption_e.swCustomPropertyDeleteAndAdd);
                    lRetVal = cusPropMgr.Add3("OEM Item Number", (int)swCustomInfoType_e.swCustomInfoText, list[4], (int)swCustomPropertyAddOption_e.swCustomPropertyDeleteAndAdd);
                    lRetVal = cusPropMgr.Add3("cDesignedBy", (int)swCustomInfoType_e.swCustomInfoText, designbytxt.Text, (int)swCustomPropertyAddOption_e.swCustomPropertyDeleteAndAdd);
                    lRetVal = cusPropMgr.Add3("Heat Treatment", (int)swCustomInfoType_e.swCustomInfoText, list[8], (int)swCustomPropertyAddOption_e.swCustomPropertyDeleteAndAdd);
                    lRetVal = cusPropMgr.Add3("Surface Protection", (int)swCustomInfoType_e.swCustomInfoText, list[7], (int)swCustomPropertyAddOption_e.swCustomPropertyDeleteAndAdd);
                    lRetVal = cusPropMgr.Add3("Spare", (int)swCustomInfoType_e.swCustomInfoText, checkBox1.Checked ? "SPARE" : "", (int)swCustomPropertyAddOption_e.swCustomPropertyDeleteAndAdd);
                    lRetVal = cusPropMgr.Add3("JobPlanning", (int)swCustomInfoType_e.swCustomInfoText, "1", (int)swCustomPropertyAddOption_e.swCustomPropertyDeleteAndAdd);
                    lRetVal = cusPropMgr.Add3("Notes", (int)swCustomInfoType_e.swCustomInfoText, list[9], (int)swCustomPropertyAddOption_e.swCustomPropertyDeleteAndAdd);
                    lRetVal = cusPropMgr.Add3("Rupture", (int)swCustomInfoType_e.swCustomInfoText, list[10], (int)swCustomPropertyAddOption_e.swCustomPropertyDeleteAndAdd);
                    lRetVal = cusPropMgr.Add3("Heat Treatment Req'd", (int)swCustomInfoType_e.swCustomInfoText, heattreat.Text.Length > 0 ? "Checked" : "Unchecked", (int)swCustomPropertyAddOption_e.swCustomPropertyDeleteAndAdd);
                    lRetVal = cusPropMgr.Add3("Surface Protection Req'd", (int)swCustomInfoType_e.swCustomInfoText, surfacetxt.Text.Length > 0 ? "Checked" : "Unchecked", (int)swCustomPropertyAddOption_e.swCustomPropertyDeleteAndAdd);
                    lRetVal = cusPropMgr.Add3("Family Type", (int)swCustomInfoType_e.swCustomInfoText, list[6], (int)swCustomPropertyAddOption_e.swCustomPropertyDeleteAndAdd);
                    lRetVal = cusPropMgr.Add3("cCategory", (int)swCustomInfoType_e.swCustomInfoText, list[5], (int)swCustomPropertyAddOption_e.swCustomPropertyDeleteAndAdd);

                    string category = connectapi.Getfamilycategory(familycombobox.SelectedItem.ToString());
                    if (string.Equals(category, "manufactured", StringComparison.CurrentCultureIgnoreCase))
                    {
                        PartDoc swPart = (PartDoc)swModel;
                        swPart.SetMaterialPropertyName2("Default", "//SPM-ADFS/CAD Data/CAD Templates SPM/SPM.sldmat", mattxt.Text);
                    }

                    bool boolstatus = swModel.Save3((int)swSaveAsOptions_e.swSaveAsOptions_Silent, 0, 0);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect New Item Fill Custom Properties", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        #endregion solidworks custom properties

        private void NewItem_FormClosed(object sender, FormClosedEventArgs e)
        {
            log.Info("Closed Create/Edit Item " + itemnumber + " ");
            this.Dispose();
        }
    }
}