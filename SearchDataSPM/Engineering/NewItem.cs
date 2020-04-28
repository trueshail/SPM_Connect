using ExtractLargeIconFromFile;
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

namespace SearchDataSPM
{
    public partial class NewItem : Form

    {
        #region steupvariables

        private string connection;
        private DataTable _acountsTb = null;
        private SqlConnection _connection;
        private SqlCommand _command;
        private SqlDataAdapter _adapter;
        private SPMConnectAPI.SPMSQLCommands connectapi = new SPMSQLCommands();
        private log4net.ILog log;
        private bool doneshowingSplash = false;

        private ErrorHandler errorHandler = new ErrorHandler();

        #endregion steupvariables

        #region form load

        public NewItem()
        {
            InitializeComponent();
            connection = System.Configuration.ConfigurationManager.ConnectionStrings["SearchDataSPM.Properties.Settings.cn"].ConnectionString;

            try
            {
                _connection = new SqlConnection(connection);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message - New Item Initialize", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            _acountsTb = new DataTable();
            _command = new SqlCommand
            {
                Connection = _connection
            };

            //connectapi.SPM_Connect();
        }

        private string checkedit;

        public string editbtn(string chekedit)
        {
            if (chekedit.Length > 0)
                return checkedit = chekedit;
            return null;
        }

        private void NewItem_Load(object sender, EventArgs e)
        {
            this.Text = "Item Details - SPM Connect (" + itemnumber + ")";

            filllistview(itemnumber);
            filldescriptionsource();
            fillmanufacturers();
            filloem();
            fillmaterials();
            fillsurface();
            fillheattreats();
            fillfamilycodes();
            filldatatable(itemnumber);
            //SPM_Connect sPM_Connect = new SPM_Connect();
            //string editbutton =  sPM_Connect.chekeditbutton.ToString();
            if (checkedit == "yes")
            {
                processsavebutton();
                Processeditbutton();
            }

            log4net.Config.XmlConfigurator.Configure();
            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log.Info("Opened Create/Edit Item " + itemnumber + " ");
        }

        private string itemnumber;

        public string item(string item)
        {
            if (item.Length > 0)
                return itemnumber = item;
            return null;
        }

        private void SPM_DoubleClick(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.spm-automation.com/");
        }

        #endregion form load

        #region Fillinformation on load

        private void filldatatable(string itemnumber)
        {
            String sql = "SELECT *  FROM [SPM_Database].[dbo].[Inventory] WHERE [ItemNumber]='" + itemnumber.ToString() + "'";

            try
            {
                _connection.Open();
                _adapter = new SqlDataAdapter(sql, _connection);
                _adapter.Fill(_acountsTb);
                fillinfo();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect New Item - Fill Data Table", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _connection.Close();
            }
        }

        private void fillinfo()
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect New Item - Fill Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void filldescriptionsource()
        {
            AutoCompleteStringCollection MyCollection = connectapi.Filldescriptionsource();
            Descriptiontxtbox.AutoCompleteCustomSource = MyCollection;
        }

        private void fillmanufacturers()
        {
            AutoCompleteStringCollection MyCollection = connectapi.Fillmanufacturers();
            oemtxtbox.AutoCompleteCustomSource = MyCollection;
        }

        private void filloem()
        {
            AutoCompleteStringCollection MyCollection = connectapi.Filloem();
            oemitemtxtbox.AutoCompleteCustomSource = MyCollection;
        }

        private void fillmaterials()
        {
            AutoCompleteStringCollection MyCollection = connectapi.FillnewitemMaterials();
            mattxt.AutoCompleteCustomSource = MyCollection;
        }

        private void fillsurface()
        {
            AutoCompleteStringCollection MyCollection = connectapi.Fillsurface();
            surfacetxt.AutoCompleteCustomSource = MyCollection;
        }

        private void fillheattreats()
        {
            AutoCompleteStringCollection MyCollection = connectapi.Fillheattreats();
            heattreat.AutoCompleteCustomSource = MyCollection;
        }

        private void fillfamilycodes()
        {
            AutoCompleteStringCollection MyCollection = connectapi.Fillfamilycodes();
            familycombobox.AutoCompleteCustomSource = MyCollection;
            familycombobox.DataSource = MyCollection;
        }

        private void filllistview(string item)
        {
            listFiles.Clear();
            listView.Items.Clear();
            getitemstodisplay(Makepath(item), item);
        }

        #endregion Fillinformation on load

        #region listview events

        private static String Makepath(string itemnumber)
        {
            if (itemnumber.Length > 0)
            {
                string first3char = itemnumber.Substring(0, 3) + @"\";
                string spmcadpath = @"\\spm-adfs\CAD Data\AAACAD\";
                string Pathpart = (spmcadpath + first3char);
                return Pathpart;
            }
            else
            {
                return null;
            }
        }

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
                    MessageBox.Show(ex.Message, "SPM Connect New Item Listview MouseClick");
                }
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
                    MessageBox.Show(ex.Message, "SPM Connect New Item ListView Keypress Enter");
                }
            }
        }

        #endregion listview events

        #region SAVE ITEM

        private void NewItem_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (savebttn.Visible == true)
            {
                DialogResult result = MessageBox.Show("Are you sure want to close without saving changes?", "SPM Connect", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                }
                else
                {
                    e.Cancel = (result == DialogResult.No);
                }
            }
        }

        private List<string> list = new List<string>();

        private void graballinfor()
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

        private async void savebttn_Click(object sender, EventArgs e)
        {
            if (connectapi.Solidworks_running() == true)
            {
                listView.Refresh();
                if (listView.Items.Count != 0)
                {
                    await Task.Run(() => SplashDialog("Saving Model..."));
                    processsavebutton();
                    doneshowingSplash = true;
                }
                else
                {
                    processsavebutton();
                }
            }
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

        private void processsavebutton()
        {
            Cursor.Current = Cursors.WaitCursor;
            this.Enabled = false;
            graballinfor();
            string lastsavedby = connectapi.Getuserfullname();
            filllistview(ItemTxtBox.Text);
            if (listView.Items.Count == 0)
            {
                processmodelcreattion(ItemTxtBox.Text);
            }
            filllistview(ItemTxtBox.Text);
            Chekbeforefillingcustomproperties(ItemTxtBox.Text);
            createnewitemtosql(list[0].ToString(), list[1].ToString(), list[5].ToString(), list[3].ToString(), list[4].ToString(), list[2].ToString(), list[6].ToString(), list[7].ToString(), list[8].ToString(), lastsavedby, list[10].ToString(), list[9].ToString());
            Perfromlockdown();
            this.Enabled = true;
            Cursor.Current = Cursors.Default;
        }

        private async void processmodelcreattion(string item)
        {
            listView.Refresh();
            if (listView.Items.Count == 0)
            {
                string category = connectapi.Getfamilycategory(familycombobox.SelectedItem.ToString()).ToString();
                string path = Makepath(item).ToString();
                if (category.ToLower() == "manufactured")
                {
                    //MessageBox.Show(path);
                    System.IO.Directory.CreateDirectory(path);
                    string filename = path + (item) + ".sldprt";
                    connectapi.Createmodel(filename);
                    string draw = path + (item) + ".slddrw";
                    connectapi.Createdrawingpart(draw, item);
                }
                else if (category.ToLower() == "assembly")
                {
                    System.IO.Directory.CreateDirectory(path);
                    string filename = path + (item) + ".sldasm";
                    connectapi.CreateAssy(filename);
                    string draw = path + (item) + ".slddrw";
                    connectapi.Createdrwaingassy(draw, item);
                }
                else
                {
                    string filename = path + (item) + ".sldprt";
                    DialogResult result = MessageBox.Show("Do you want to import model for the purchased part?", "SPM Connect", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        string fileimporttype = "";
                        Engineering.ImportFileSelector importFileSelector = new Engineering.ImportFileSelector();

                        if (importFileSelector.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            fileimporttype = importFileSelector.ValueIWant;
                        }

                        if (fileimporttype == "STEP")
                        {
                            string stepfilter = "STEP AP203/214/242|*.step;*.stp";
                            string impfilname = importfilename(stepfilter).ToString();
                            if (Path.GetExtension(impfilname).ToLower() == ".step" || Path.GetExtension(impfilname).ToLower() == ".stp")
                            {
                                //new Thread(() => new Engineering.WaitFormImport().ShowDialog()).Start();
                                //Thread.Sleep(3000);
                                await Task.Run(() => SplashDialog("Importing Model...."));
                                Thread.Sleep(1000);
                                if (connectapi.Importstepfile(impfilname, filename) == true)
                                {
                                    if (connectapi.Getfilename() != item)
                                    {
                                        connectapi.Open_model(filename);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Solidworks encountered importing error. Please restart solidworks in oder to prevent this error in future.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    System.IO.Directory.CreateDirectory(path);
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
                                System.IO.Directory.CreateDirectory(path);
                                connectapi.Createmodel(filename);
                            }
                        }
                        else if (fileimporttype == "IGES")
                        {
                            string igesfilter = "IGES|*.igs;*.iges";
                            string impfilname = importfilename(igesfilter).ToString();
                            if (Path.GetExtension(impfilname).ToLower() == ".igs" || Path.GetExtension(impfilname).ToLower() == ".iges")
                            {
                                //new Thread(() => new Engineering.WaitFormImport().ShowDialog()).Start();
                                //Thread.Sleep(3000);
                                await Task.Run(() => SplashDialog("Importing Model...."));
                                Thread.Sleep(1000);

                                if (connectapi.Importigesfile(impfilname, filename) == true)
                                {
                                    if (connectapi.Getfilename() != item)
                                    {
                                        connectapi.Open_model(filename);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Solidworks encountered importing error. Please restart solidworks in oder to prevent this error in future.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    System.IO.Directory.CreateDirectory(path);
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
                                System.IO.Directory.CreateDirectory(path);
                                connectapi.Createmodel(filename);
                            }
                        }
                        else if (fileimporttype == "PARASOLID")
                        {
                            string parasolidfilter = "Parasolid|*.x_t;*.x_b;*.xmt_txt;*.xmt_bin";
                            string impfilname = importfilename(parasolidfilter).ToString();
                            if (Path.GetExtension(impfilname).ToLower() == ".x_t" || Path.GetExtension(impfilname).ToLower() == ".x_b" || Path.GetExtension(impfilname).ToLower() == ".xmt_txt" || Path.GetExtension(impfilname).ToLower() == ".xmt_bin")
                            {
                                //new Thread(() => new Engineering.WaitFormImport().ShowDialog()).Start();
                                //Thread.Sleep(3000);
                                await Task.Run(() => SplashDialog("Importing Model...."));
                                Thread.Sleep(1000);

                                if (connectapi.Importparasolidfile(impfilname, filename) == true)
                                {
                                    if (connectapi.Getfilename() != item)
                                    {
                                        connectapi.Open_model(filename);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Solidworks encountered importing error. Please restart solidworks in oder to prevent this error in future.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    System.IO.Directory.CreateDirectory(path);
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
                                System.IO.Directory.CreateDirectory(path);
                                connectapi.Createmodel(filename);
                            }
                        }
                        else
                        {
                            System.IO.Directory.CreateDirectory(path);
                            connectapi.Createmodel(filename);
                        }
                    }
                    else
                    {
                        System.IO.Directory.CreateDirectory(path);
                        connectapi.Createmodel(filename);
                    }
                }
            }
        }

        private void createnewitemtosql(string itemnumber, string description, string family, string manufacturer, string oem, string material, string familytype, string surface, string heat, string lastsavedby, string rupture, string notes)
        {
            DateTime dateedited = DateTime.Now;
            string sqlFormattedDate = dateedited.ToString("yyyy-MM-dd HH:mm:ss.fff");
            if (_connection.State == ConnectionState.Closed)
                _connection.Open();
            try
            {
                SqlCommand cmd = _connection.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE [SPM_Database].[dbo].[Inventory] SET Description = '" + description + "',FamilyCode = '" + family + "',Manufacturer = '" + manufacturer + "',ManufacturerItemNumber = '" + oem + "',Material = '" + material + "',Spare = '" + (checkBox1.Checked ? "SPARE" : null) + "',FamilyType = '" + familytype + "',SurfaceProtection = '" + surface + "',HeatTreatment = '" + heat + "',LastSavedBy = '" + lastsavedby + "',Rupture = '" + rupture + "',Notes = '" + notes + "',LastEdited = '" + sqlFormattedDate + "' WHERE ItemNumber = '" + itemnumber + "' ";
                cmd.ExecuteNonQuery();
                _connection.Close();
                //MessageBox.Show("Item sucessfully saved SPM Connect Server.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _connection.Close();
            }

            //perfromlockdown();
        }

        private void editbttn_Click(object sender, EventArgs e)
        {
            Processeditbutton();
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

        private void familycombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (familycombobox.Text.Length > 0)
            {
                if (familycombobox.Text.ToLower() == "ma" || familycombobox.Text.ToLower() == "as" || familycombobox.Text.ToLower() == "mawe" || familycombobox.Text.ToLower() == "ag" || familycombobox.Text.ToLower() == "asel" || familycombobox.Text.ToLower() == "aspn")
                {
                    categorytxtbox.Text = "Manufactured";
                    rupturetextbox.Text = "ALWAYS";
                }
                else if (familycombobox.Text.ToLower() == "as" || familycombobox.Text.ToLower() == "ag" || familycombobox.Text.ToLower() == "asel" || familycombobox.Text.ToLower() == "aspn")
                {
                    categorytxtbox.Text = "Assembly";
                    rupturetextbox.Text = "ALWAYS";
                }
                else if (familycombobox.Text.ToLower() == "dr")
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

        #endregion SAVE ITEM

        #region solidworks createmodels and open models

        private string importfilename(string filter)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = filter;
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.

            if (result == DialogResult.OK) // Test result.
            {
                string file = openFileDialog1.FileName;
                return file.ToString();
            }
            return "";
        }

        #endregion solidworks createmodels and open models

        #region solidworks custom properties

        public void Fillcustomproperties()
        {
            if (connectapi.Solidworks_running() == true)
            {
                try
                {
                    var progId = "SldWorks.Application";

                    SldWorks swApp = System.Runtime.InteropServices.Marshal.GetActiveObject(progId.ToString()) as SolidWorks.Interop.sldworks.SldWorks;
                    ModelDoc2 swModel;
                    CustomPropertyManager cusPropMgr;
                    int lRetVal;
                    swModel = (ModelDoc2)swApp.ActiveDoc;
                    ModelDocExtension swModelDocExt = default(ModelDocExtension);
                    swModelDocExt = swModel.Extension;
                    cusPropMgr = swModelDocExt.get_CustomPropertyManager("");
                    lRetVal = cusPropMgr.Add3("PartNo", (int)swCustomInfoType_e.swCustomInfoText, list[0].ToString(), (int)swCustomPropertyAddOption_e.swCustomPropertyDeleteAndAdd);
                    lRetVal = cusPropMgr.Add3("Description", (int)swCustomInfoType_e.swCustomInfoText, list[1].ToString(), (int)swCustomPropertyAddOption_e.swCustomPropertyDeleteAndAdd);
                    lRetVal = cusPropMgr.Add3("OEM", (int)swCustomInfoType_e.swCustomInfoText, list[3].ToString(), (int)swCustomPropertyAddOption_e.swCustomPropertyDeleteAndAdd);
                    lRetVal = cusPropMgr.Add3("OEM Item Number", (int)swCustomInfoType_e.swCustomInfoText, list[4].ToString(), (int)swCustomPropertyAddOption_e.swCustomPropertyDeleteAndAdd);
                    lRetVal = cusPropMgr.Add3("cDesignedBy", (int)swCustomInfoType_e.swCustomInfoText, designbytxt.Text, (int)swCustomPropertyAddOption_e.swCustomPropertyDeleteAndAdd);
                    lRetVal = cusPropMgr.Add3("Heat Treatment", (int)swCustomInfoType_e.swCustomInfoText, list[8].ToString(), (int)swCustomPropertyAddOption_e.swCustomPropertyDeleteAndAdd);
                    lRetVal = cusPropMgr.Add3("Surface Protection", (int)swCustomInfoType_e.swCustomInfoText, list[7].ToString(), (int)swCustomPropertyAddOption_e.swCustomPropertyDeleteAndAdd);
                    lRetVal = cusPropMgr.Add3("Spare", (int)swCustomInfoType_e.swCustomInfoText, checkBox1.Checked ? "SPARE" : "", (int)swCustomPropertyAddOption_e.swCustomPropertyDeleteAndAdd);
                    lRetVal = cusPropMgr.Add3("JobPlanning", (int)swCustomInfoType_e.swCustomInfoText, "1", (int)swCustomPropertyAddOption_e.swCustomPropertyDeleteAndAdd);
                    lRetVal = cusPropMgr.Add3("Notes", (int)swCustomInfoType_e.swCustomInfoText, list[9].ToString(), (int)swCustomPropertyAddOption_e.swCustomPropertyDeleteAndAdd);
                    lRetVal = cusPropMgr.Add3("Rupture", (int)swCustomInfoType_e.swCustomInfoText, list[10].ToString(), (int)swCustomPropertyAddOption_e.swCustomPropertyDeleteAndAdd);
                    lRetVal = cusPropMgr.Add3("Heat Treatment Req'd", (int)swCustomInfoType_e.swCustomInfoText, heattreat.Text.Length > 0 ? "Checked" : "Unchecked", (int)swCustomPropertyAddOption_e.swCustomPropertyDeleteAndAdd);
                    lRetVal = cusPropMgr.Add3("Surface Protection Req'd", (int)swCustomInfoType_e.swCustomInfoText, surfacetxt.Text.Length > 0 ? "Checked" : "Unchecked", (int)swCustomPropertyAddOption_e.swCustomPropertyDeleteAndAdd);
                    lRetVal = cusPropMgr.Add3("Family Type", (int)swCustomInfoType_e.swCustomInfoText, list[6].ToString(), (int)swCustomPropertyAddOption_e.swCustomPropertyDeleteAndAdd);
                    lRetVal = cusPropMgr.Add3("cCategory", (int)swCustomInfoType_e.swCustomInfoText, list[5].ToString(), (int)swCustomPropertyAddOption_e.swCustomPropertyDeleteAndAdd);

                    string category = connectapi.Getfamilycategory(familycombobox.SelectedItem.ToString()).ToString();
                    if (category.ToLower() == "manufactured")
                    {
                        PartDoc swPart = default(PartDoc);
                        swPart = (PartDoc)swModel;
                        swPart.SetMaterialPropertyName2("Default", "//SPM-ADFS/CAD Data/CAD Templates SPM/SPM.sldmat", mattxt.Text);
                    }

                    bool boolstatus = false;
                    boolstatus = swModel.Save3((int)swSaveAsOptions_e.swSaveAsOptions_Silent, 0, 0);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect New Item Fill Custom Properties", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        public void Chekbeforefillingcustomproperties(string item)
        {
            if (connectapi.Solidworks_running() == true)
            {
                string getcurrentfilename = connectapi.Getfilename().ToString();
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

                    string Pathassy = Makepath(item).ToString() + ItemTxtBox.Text + ".sldasm";
                    string Pathpart = Makepath(item).ToString() + ItemTxtBox.Text + ".sldprt";
                    string Pathassyo = Makepath(item).ToString() + ItemTxtBox.Text + "-0" + ".sldasm";
                    string Pathparto = Makepath(item).ToString() + ItemTxtBox.Text + "-0" + ".sldprt";

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

        #endregion solidworks custom properties

        private void NewItem_FormClosed(object sender, FormClosedEventArgs e)
        {
            log.Info("Closed Create/Edit Item " + itemnumber + " ");
            this.Dispose();
        }
    }
}