using ExtractLargeIconFromFile;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
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
using System.Threading.Tasks;
using System.Windows.Forms;
using wpfPreviewFlowControl;

namespace SearchDataSPM
{

    public partial class NewItem : Form

    {
        #region steupvariables

        String connection;
        DataTable _acountsTb = null;
        SqlConnection _connection;
        SqlCommand _command;
        SqlDataAdapter _adapter;
        

        #endregion

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
            _command = new SqlCommand();
            _command.Connection = _connection;



        }

        string checkedit;

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
            if(checkedit == "yes")
            {
                processsavebutton();
                processeditbutton();
            }
       

        }


        string itemnumber;

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

        #endregion

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

        private void filldescriptionsource()
        {
            using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM [SPM_Database].[dbo].[Descriptions]", _connection))
            {
                try
                {
                    _connection.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
                    while (reader.Read())
                    {
                        MyCollection.Add(reader.GetString(0));
                    }
                    Descriptiontxtbox.AutoCompleteCustomSource = MyCollection;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect New Item - Fill Description Source", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    _connection.Close();
                }

            }

        }

        private void fillmanufacturers()
        {
            using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM [SPM_Database].[dbo].[Manufacturers]", _connection))
            {
                try
                {
                    _connection.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
                    while (reader.Read())
                    {
                        MyCollection.Add(reader.GetString(0));
                    }
                    oemtxtbox.AutoCompleteCustomSource = MyCollection;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect New Item - Fill Manufacturers Source", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    _connection.Close();
                }

            }

        }

        private void filloem()
        {
            using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM [SPM_Database].[dbo].[ManufacturersItemNumbers]", _connection))
            {
                try
                {
                    _connection.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
                    while (reader.Read())
                    {
                        MyCollection.Add(reader.GetString(0));
                    }
                    oemitemtxtbox.AutoCompleteCustomSource = MyCollection;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect New Item - Fill oem items Source", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    _connection.Close();
                }

            }

        }

        private void fillmaterials()
        {
            using (SqlCommand sqlCommand = new SqlCommand("SELECT MaterialNames FROM [SPM_Database].[dbo].[Materials]", _connection))
            {
                try
                {
                    _connection.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
                    while (reader.Read())
                    {
                        MyCollection.Add(reader.GetString(0));
                    }
                    mattxt.AutoCompleteCustomSource = MyCollection;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect New Item - Fill Materials Source", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    _connection.Close();
                }

            }

        }

        private void fillsurface()
        {
            using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM [SPM_Database].[dbo].[SurfaceProtections]", _connection))
            {
                try
                {
                    _connection.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
                    while (reader.Read())
                    {
                        MyCollection.Add(reader.GetString(0));
                    }
                    surfacetxt.AutoCompleteCustomSource = MyCollection;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect New Item - Fill Surface Protections Source", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    _connection.Close();
                }

            }

        }

        private void fillheattreats()
        {
            using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM [SPM_Database].[dbo].[HeatTreatments]", _connection))
            {
                try
                {
                    _connection.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
                    while (reader.Read())
                    {
                        MyCollection.Add(reader.GetString(0));
                    }
                    heattreat.AutoCompleteCustomSource = MyCollection;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect New Item - Fill Heat Treats Source", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    _connection.Close();
                }

            }

        }

        private void fillfamilycodes()
        {
            using (SqlCommand sqlCommand = new SqlCommand("SELECT FamilyCodes FROM [SPM_Database].[dbo].[FamilyCodes]", _connection))
            {
                try
                {
                    _connection.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
                    while (reader.Read())
                    {
                        MyCollection.Add(reader.GetString(0));
                    }

                    //familytxtbox.AutoCompleteCustomSource = MyCollection;
                    familycombobox.AutoCompleteCustomSource = MyCollection;
                    familycombobox.DataSource = MyCollection;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect New Item - Fill FamilyCodes Source", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    _connection.Close();
                }

            }

        }

        private void filllistview(string item)
        {
            listFiles.Clear();
            listView.Items.Clear();
            getitemstodisplay(Makepath(item), item);
        }

        #endregion

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

        #endregion

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

        List<string> list = new List<string>();

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

        private void savebttn_Click(object sender, EventArgs e)
        {
            if(solidworks_running() == true)
            {
                listView.Refresh();
                if (listView.Items.Count != 0)
                {
                    //new Thread(() => new Engineering.WaitFormSaving().ShowDialog()).Start();
                    Thread t = new Thread(new ThreadStart(splashsave));
                    t.Start();
                    processsavebutton();
                    //Engineering.WaitFormSaving f = new Engineering.WaitFormSaving();
                    //f = (Engineering.WaitFormSaving)Application.OpenForms["WaitFormSaving"];
                    //f.Invoke(new ThreadStart(delegate { f.Close(); }));
                    t.Abort();
                }
                else
                {
                    processsavebutton();
                }
            }
            
        }

        void splashsave()
        {
            Engineering.WaitFormSaving waitFormSaving = new Engineering.WaitFormSaving();
            Application.Run(waitFormSaving);
        }

        void splashimport()
        {
            Engineering.WaitFormImport waitFormImport = new Engineering.WaitFormImport();
            Application.Run(waitFormImport);
        }

        private void processsavebutton()
        {
            Cursor.Current = Cursors.WaitCursor;
            this.Enabled = false;
            graballinfor();
            string lastsavedby = getuserfullname(get_username().ToString()).ToString();
            filllistview(ItemTxtBox.Text);
            if (listView.Items.Count == 0)
            {               
                processmodelcreattion(ItemTxtBox.Text);
            }            
            filllistview(ItemTxtBox.Text);
            chekbeforefillingcustomproperties(ItemTxtBox.Text);
            createnewitemtosql(list[0].ToString(), list[1].ToString(), list[5].ToString(), list[3].ToString(), list[4].ToString(), list[2].ToString(), list[6].ToString(), list[7].ToString(), list[8].ToString(), lastsavedby, list[10].ToString(), list[9].ToString());
            perfromlockdown();
            this.Enabled = true;
            Cursor.Current = Cursors.Default;
        }

        private void processmodelcreattion(string item)
        {
            listView.Refresh();
            if (listView.Items.Count == 0)
            {
                string category = getfamilycategory(familycombobox.SelectedItem.ToString()).ToString();
                string path = Makepath(item).ToString();
                if (category.ToLower() == "manufactured")
                {
                    //MessageBox.Show(path);
                    System.IO.Directory.CreateDirectory(path);
                    string filename = path + (item) + ".sldprt";
                    createmodel(filename);
                    string draw = path + (item) + ".slddrw";
                    createdrawingpart(draw,item);
                    
                }
                else if (category.ToLower() == "assembly")
                {
                    System.IO.Directory.CreateDirectory(path);
                    string filename = path + (item) + ".sldasm";
                    createassy(filename);
                    string draw = path + (item) + ".slddrw";
                    createdrwaingassy(draw,item);
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

                        if(fileimporttype == "STEP")
                        {
                            string stepfilter = "STEP AP203/214/242|*.step;*.stp";
                            string impfilname = importfilename(stepfilter).ToString();
                            if(Path.GetExtension(impfilname).ToLower() == ".step" || Path.GetExtension(impfilname).ToLower() == ".stp")
                            {
                                //new Thread(() => new Engineering.WaitFormImport().ShowDialog()).Start();
                                //Thread.Sleep(3000);
                                Thread t = new Thread(new ThreadStart(splashimport));
                                t.Start();
                                Thread.Sleep(3000);
                               
                                importstepfile(impfilname, filename);
                                t.Abort();
                                //Engineering.WaitFormImport f = new Engineering.WaitFormImport();                                
                                //f = (Engineering.WaitFormImport)Application.OpenForms["WaitFormImport"];
                                //f.Invoke(new ThreadStart(delegate { f.Close(); }));
                                
                            }
                            else
                            {
                                MessageBox.Show("Selected File extension did not match the file type selected to import.!", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                System.IO.Directory.CreateDirectory(path);
                                createmodel(filename);
                            }

                        }
                        else if(fileimporttype == "IGES")
                        {
                            string igesfilter = "IGES|*.igs;*.iges";
                            string impfilname = importfilename(igesfilter).ToString();
                            if (Path.GetExtension(impfilname).ToLower() == ".igs" || Path.GetExtension(impfilname).ToLower() == ".iges")
                            {

                                //new Thread(() => new Engineering.WaitFormImport().ShowDialog()).Start();
                                //Thread.Sleep(3000);
                                Thread t = new Thread(new ThreadStart(splashimport));
                                t.Start();
                                Thread.Sleep(3000);

                                if (importigesfile(impfilname, filename) == true)
                                {

                                }
                                else
                                {
                                    MessageBox.Show("Solidworks encountered importing error. Please restart solidworks in oder to prevent this error in future.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    System.IO.Directory.CreateDirectory(path);
                                    createmodel(filename);
                                }
                                t.Abort();
                                //Engineering.WaitFormImport f = new Engineering.WaitFormImport();
                                //f = (Engineering.WaitFormImport)Application.OpenForms["WaitFormImport"];
                                //f.Invoke(new ThreadStart(delegate { f.Close(); }));
                                
                            }
                            else
                            {
                                MessageBox.Show("Selected File extension did not match the file type selected to import.!", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                System.IO.Directory.CreateDirectory(path);
                                createmodel(filename);
                            }
                                
                        }
                        else if(fileimporttype == "PARASOLID")
                        {
                            string parasolidfilter = "Parasolid|*.x_t;*.x_b;*.xmt_txt;*.xmt_bin";
                            string impfilname = importfilename(parasolidfilter).ToString();
                            if (Path.GetExtension(impfilname).ToLower() == ".x_t" || Path.GetExtension(impfilname).ToLower() == ".x_b" || Path.GetExtension(impfilname).ToLower() == ".xmt_txt" || Path.GetExtension(impfilname).ToLower() == ".xmt_bin")
                            {
                                //new Thread(() => new Engineering.WaitFormImport().ShowDialog()).Start();
                                //Thread.Sleep(3000);
                                Thread t = new Thread(new ThreadStart(splashimport));
                                t.Start();
                                Thread.Sleep(3000);

                                if (importparasolidfile(impfilname, filename) == true)
                                {

                                }
                                else
                                {
                                    MessageBox.Show("Solidworks encountered importing error. Please restart solidworks in oder to prevent this error in future.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    System.IO.Directory.CreateDirectory(path);
                                    createmodel(filename);
                                }
                                t.Abort();
                                //Engineering.WaitFormImport f = new Engineering.WaitFormImport();
                                //f = (Engineering.WaitFormImport)Application.OpenForms["WaitFormImport"];
                                //f.Invoke(new ThreadStart(delegate { f.Close(); }));
                                
                            }
                            else
                            {
                                MessageBox.Show("Selected File extension did not match the file type selected to import.!", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                System.IO.Directory.CreateDirectory(path);
                                createmodel(filename);
                            }

                        }
                        else
                        {
                            System.IO.Directory.CreateDirectory(path);
                            createmodel(filename);
                        }

                        
                    }
                    else
                    {
                        System.IO.Directory.CreateDirectory(path);                        
                        createmodel(filename);
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
            processeditbutton();
         
        }

        private void processeditbutton()
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

        private void perfromlockdown()
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
                else if ( familycombobox.Text.ToLower() == "as" ||  familycombobox.Text.ToLower() == "ag" || familycombobox.Text.ToLower() == "asel" || familycombobox.Text.ToLower() == "aspn")
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

        #endregion

        #region GET METHODS

        private bool solidworks_running()
        {

            if (Process.GetProcessesByName("SLDWORKS").Length >= 1)
            {
                return true;
            }
            else if ((Process.GetProcessesByName("SLDWORKS").Length == 0))
            {

                MessageBox.Show("Soliworks application needs to be running in order for SPM Connect to perform. Thank you.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                MessageBox.Show("SPM Connect encountered more than one sesssion of solidworks running. Please close other sesssions in order for SPM Connect to perform. Thank you.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
        }

        public static bool checkforreadonly()
        {
            var progId = "SldWorks.Application";
            SldWorks swApp = Marshal.GetActiveObject(progId.ToString()) as SldWorks;
            swApp.Visible = true;
            ModelDoc2 swModel = swApp.ActiveDoc as ModelDoc2;
            if (swModel.IsOpenedReadOnly())
            {
                MessageBox.Show("Model is open read only. Please get write access from the associated user in order to edit the properties.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            else
            {
                return true;
            }

        }

        private static String get_username()
        {
            string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            if (userName.Length > 0)
            {
                return userName;
            }
            else
            {
                return null;
            }

        }

        private object getuserfullname(string username)
        {
            try
            {
                if (_connection.State == ConnectionState.Closed)
                    _connection.Open();
                SqlCommand cmd = _connection.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM [SPM_Database].[dbo].[Users] WHERE [UserName]='" + username.ToString() + "' ";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    string fullname = dr["Name"].ToString();
                    return fullname;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }
            finally
            {
                _connection.Close();
            }
            return null;
        }

        private static String get_pathname()
        {
            ModelDoc2 swModel;
            var progId = "SldWorks.Application";
            SldWorks swApp = System.Runtime.InteropServices.Marshal.GetActiveObject(progId.ToString()) as SolidWorks.Interop.sldworks.SldWorks;


            int count;
            string pathName;
            count = swApp.GetDocumentCount();

            if (count > 0)
            {

                swModel = swApp.ActiveDoc as ModelDoc2;

                pathName = swModel.GetPathName();

                return pathName;

            }
            else
            {
                return null;
            }


        }

        private static String getfilename()
        {
            ModelDoc2 swModel;
            var progId = "SldWorks.Application";
            SldWorks swApp = System.Runtime.InteropServices.Marshal.GetActiveObject(progId.ToString()) as SolidWorks.Interop.sldworks.SldWorks;


            int count;
            count = swApp.GetDocumentCount();

            if (count > 0)
            {
                // MessageBox.Show("Number of open documents in this SOLIDWORKS session: " + count);
                swModel = swApp.ActiveDoc as ModelDoc2;

                string filename = swModel.GetTitle();
                return filename;

            }
            else
            {
                return "";
            }

        }

        public string getfamilycategory(string familycode)
        {
            try
            {
                if (_connection.State == ConnectionState.Closed)
                    _connection.Open();
                SqlCommand cmd = _connection.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT Category FROM [SPM_Database].[dbo].[FamilyCodes] WHERE [FamilyCodes]='" + familycode.ToString() + "' ";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    string category = dr["Category"].ToString();
                    //MessageBox.Show(category);
                    return category;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }
            finally
            {
                _connection.Close();
            }
            return null;
        }

        #endregion

        #region solidworks createmodels and open models

        private void Open_model(string filename)
        {
            var progId = "SldWorks.Application";
            SldWorks swApp = Marshal.GetActiveObject(progId.ToString()) as SldWorks;
            swApp.Visible = true;
            int err = 0;
            int warn = 0;
            ModelDoc2 swModel = (ModelDoc2)swApp.OpenDoc6(filename, (int)swDocumentTypes_e.swDocPART, (int)swOpenDocOptions_e.swOpenDocOptions_Silent, "", ref err, ref warn);
            swApp.ActivateDoc(filename);
            swModel = swApp.ActiveDoc as ModelDoc2;


        }

        private void Open_assy(string filename)
        {

            var progId = "SldWorks.Application";
            SldWorks swApp = Marshal.GetActiveObject(progId.ToString()) as SldWorks;
            swApp.Visible = true;
            int err = 0;
            int warn = 0;
            ModelDoc2 swModel = (ModelDoc2)swApp.OpenDoc6(filename, (int)swDocumentTypes_e.swDocASSEMBLY, (int)swOpenDocOptions_e.swOpenDocOptions_Silent, "", ref err, ref warn);
            swApp.ActivateDoc(filename);
            swModel = swApp.ActiveDoc as ModelDoc2;
        }

        private void Open_drw(string filename)
        {
            var progId = "SldWorks.Application";
            SldWorks swApp = Marshal.GetActiveObject(progId.ToString()) as SldWorks;
            swApp.Visible = true;
            int err = 0;
            int warn = 0;
            ModelDoc2 swModel = (ModelDoc2)swApp.OpenDoc6(filename, (int)swDocumentTypes_e.swDocDRAWING, (int)swOpenDocOptions_e.swOpenDocOptions_Silent, "", ref err, ref warn);
            swApp.ActivateDoc(filename);
            swModel = swApp.ActiveDoc as ModelDoc2;
        }

        public void createmodel(string filename)
        {
            if (solidworks_running() == true)
            {
                var progId = "SldWorks.Application";
                SldWorks swApp = Marshal.GetActiveObject(progId.ToString()) as SldWorks;
                swApp.Visible = true;
                string PartPath = swApp.GetUserPreferenceStringValue((int)swUserPreferenceStringValue_e.swDefaultTemplatePart);
                ModelDoc2 swModel = swApp.NewDocument(PartPath, 0, 0, 0) as ModelDoc2;
                swApp.Visible = true;
                swModel = swApp.ActiveDoc as ModelDoc2;
                ModelDocExtension swExt;
                swExt = swModel.Extension;
                bool boolstatus = false;
                boolstatus = swExt.SaveAs(filename, 0, (int)swSaveAsOptions_e.swSaveAsOptions_Silent, 0, 0, 0);
                swApp.ActivateDoc(filename);
                swModel = swApp.ActiveDoc as ModelDoc2;

                if (boolstatus == true)
                {
                    //MessageBox.Show("new part created");
                    get_pathname();
                    getfilename();

                }
                else
                {
                    //MessageBox.Show("part not saved");
                }
            }
        }

        public void createassy(string filename)
        {
            if (solidworks_running() == true)
            {
                var progId = "SldWorks.Application";
                SldWorks swApp = Marshal.GetActiveObject(progId.ToString()) as SldWorks;
                swApp.Visible = true;
                string assytemplate = swApp.GetUserPreferenceStringValue((int)swUserPreferenceStringValue_e.swDefaultTemplateAssembly);
                ModelDoc2 swModel = swApp.NewDocument(assytemplate, 0, 0, 0) as ModelDoc2;
                swApp.Visible = true;
                swModel = swApp.ActiveDoc as ModelDoc2;
                ModelDocExtension swExt;
                swExt = swModel.Extension;
                bool boolstatus = false;
                //boolstatus = swExt.SaveAs(filename, 0, (int)swDocumentTypes_e.swDocASSEMBLY, 0, 0, 0);
                boolstatus = swExt.SaveAs(filename, 0, (int)swSaveAsOptions_e.swSaveAsOptions_Silent, 0, 0, 0);
                swApp.ActivateDoc(filename);
                swModel = swApp.ActiveDoc as ModelDoc2;

                if (boolstatus == true)
                {
                    //MessageBox.Show("new assy created");
                    get_pathname();
                    getfilename();
                }
                else
                {
                    //MessageBox.Show("part not saved");
                }
            }

        }

        public void createdrawingpart(string filename, string _itemnumber)
        {
            if (solidworks_running() == true)
            {
                var progId = "SldWorks.Application";
                SldWorks swApp = Marshal.GetActiveObject(progId.ToString()) as SldWorks;
                swApp.Visible = true;
                string template = swApp.GetUserPreferenceStringValue((int)swUserPreferenceStringValue_e.swDefaultTemplateDrawing);
                ModelDoc2 swModel;
                DrawingDoc swDrawing;
                ModelDocExtension swModelDocExt;

                swModel = (ModelDoc2)swApp.NewDocument(template, (int)swDwgPaperSizes_e.swDwgPaperBsize, 0, 0);
                swDrawing = (DrawingDoc)swModel;                
                swDrawing = swApp.ActiveDoc as DrawingDoc;
                swModelDocExt = (ModelDocExtension)swModel.Extension;

                string Pathpart = Makepath(_itemnumber).ToString() + _itemnumber + ".sldprt";

                swDrawing.Create3rdAngleViews2(Pathpart);

                //Sheet cursheet;
                //cursheet = swDrawing.GetCurrentSheet();
                //double sheetwidth = 0;
                //double sheethieght = 0;
                //int lRetVal;
                //lRetVal= cursheet.GetSize(sheetwidth,sheethieght);                
                //SolidWorks.Interop.sldworks.View swView;

                //swView = (SolidWorks.Interop.sldworks.View)swDrawing.CreateDrawViewFromModelView3(Pathpart, "*Isometric",sheetwidth, sheethieght, 0);
                //swDrawing.InsertModelAnnotations3(0, 327663, true, true, false, false);
                //int nNumView = 0;
                //var Voutline;
                //var Vpostion;
                //double viewweidth = 0;
                //double viewheight = 0;


                //Voutline(nNumView) = swView.GetOutline();
                //Vpostion(nNumView) = swView.Position();
                //viewweidth = Voutline(2) - Voutline(0);

                //swView.Position(6, 5);

                
                bool boolstatus = false;
                boolstatus = swModelDocExt.SaveAs(filename, 0, (int)swSaveAsOptions_e.swSaveAsOptions_Silent, 0, 0, 0);
                swApp.QuitDoc(swModel.GetTitle());
                

                if (boolstatus == true)
                {
                    //MessageBox.Show("new part created");
                    //get_pathname();
                    //getfilename();

                }
                else
                {
                    //MessageBox.Show("part not saved");
                }
            }
        }

        public void createdrwaingassy(string filename, string _itemnumber)
        {
            if (solidworks_running() == true)
            {
                var progId = "SldWorks.Application";
                SldWorks swApp = Marshal.GetActiveObject(progId.ToString()) as SldWorks;
                swApp.Visible = true;
                string template = swApp.GetUserPreferenceStringValue((int)swUserPreferenceStringValue_e.swDefaultTemplateDrawing);
                ModelDoc2 swModel;
                DrawingDoc swDrawing;
                ModelDocExtension swModelDocExt;

                swModel = (ModelDoc2)swApp.NewDocument(template, (int)swDwgPaperSizes_e.swDwgPaperBsize, 0, 0);
                swDrawing = (DrawingDoc)swModel;
                swDrawing = swApp.ActiveDoc as DrawingDoc;
                swModelDocExt = (ModelDocExtension)swModel.Extension;

                string Pathpart = Makepath(_itemnumber).ToString() + _itemnumber + ".sldasm";

                swDrawing.Create3rdAngleViews2(Pathpart);

                //Sheet cursheet;
                //cursheet = swDrawing.GetCurrentSheet();
                //double sheetwidth = 0;
                //double sheethieght = 0;
                //int lRetVal;
                //lRetVal= cursheet.GetSize(sheetwidth,sheethieght);                
                //SolidWorks.Interop.sldworks.View swView;

                //swView = (SolidWorks.Interop.sldworks.View)swDrawing.CreateDrawViewFromModelView3(Pathpart, "*Isometric",sheetwidth, sheethieght, 0);
                //swDrawing.InsertModelAnnotations3(0, 327663, true, true, false, false);
                //int nNumView = 0;
                //var Voutline;
                //var Vpostion;
                //double viewweidth = 0;
                //double viewheight = 0;


                //Voutline(nNumView) = swView.GetOutline();
                //Vpostion(nNumView) = swView.Position();
                //viewweidth = Voutline(2) - Voutline(0);

                //swView.Position(6, 5);


                bool boolstatus = false;
                boolstatus = swModelDocExt.SaveAs(filename, 0, (int)swSaveAsOptions_e.swSaveAsOptions_Silent, 0, 0, 0);
                swApp.QuitDoc(swModel.GetTitle());


                if (boolstatus == true)
                {
                    //MessageBox.Show("new part created");
                    //get_pathname();
                    //getfilename();

                }
                else
                {
                    //MessageBox.Show("part not saved");
                }
            }
        }

        public bool importstepfile(string stepFileName ,string savefilename)
        {
            if (solidworks_running() == true)
            {
                var progId = "SldWorks.Application";
                SldWorks swApp = Marshal.GetActiveObject(progId.ToString()) as SldWorks;
                PartDoc swPart = default(PartDoc);
                AssemblyDoc swassy = default(AssemblyDoc);
                ModelDoc2 swModel = default(ModelDoc2);
                ModelDocExtension swModelDocExt = default(ModelDocExtension);
                ImportStepData swImportStepData = default(ImportStepData);
                
                bool status = false;
                int errors = 0;
               
                //Get import information
                swImportStepData = (ImportStepData)swApp.GetImportFileData(stepFileName);

                //If ImportStepData::MapConfigurationData is not set, then default to
                //the environment setting swImportStepConfigData; otherwise, override
                //swImportStepConfigData with ImportStepData::MapConfigurationData
                swImportStepData.MapConfigurationData = true;

                //Import the STEP file.
                try
                {
                    swPart = (PartDoc)swApp.LoadFile4(stepFileName, "r", swImportStepData, ref errors);
                    swModel = (ModelDoc2)swPart;
                    swModelDocExt = (ModelDocExtension)swModel.Extension;
                    status = swModelDocExt.SaveAs(savefilename, 0, (int)swSaveAsOptions_e.swSaveAsOptions_Silent, 0, 0, 0);
                }
                catch
                {

                }
                try
                {
                    swassy = (AssemblyDoc)swApp.LoadFile4(stepFileName, "r", swImportStepData, ref errors);
                    swModel = (ModelDoc2)swPart;
                    swModelDocExt = (ModelDocExtension)swModel.Extension;
                    status = swModelDocExt.SaveAs(savefilename, 0, (int)swSaveAsOptions_e.swSaveAsOptions_Silent, 0, 0, 0);
                }
                catch
                {

                }

                return status;
               

            }
            return false;
        }

        public bool importigesfile(string igesfilename, string savefilename)
        {
            if (solidworks_running() == true)
            {
                var progId = "SldWorks.Application";
                SldWorks swApp = Marshal.GetActiveObject(progId.ToString()) as SldWorks;
                PartDoc swPart = default(PartDoc);
                AssemblyDoc swassy = default(AssemblyDoc);
                ModelDoc2 swModel = default(ModelDoc2);
                ModelDocExtension swModelDocExt = default(ModelDocExtension);
                ImportIgesData swImportIgesdata = default(ImportIgesData);

                bool status = false;
                int errors = 0;
                swImportIgesdata = (ImportIgesData)swApp.GetImportFileData(igesfilename);
                swImportIgesdata.IncludeSurfaces = true;
                try
                {
                    swPart = (PartDoc)swApp.LoadFile4(igesfilename, "r", swImportIgesdata, ref errors);
                    swModel = (ModelDoc2)swPart;
                    swModelDocExt = (ModelDocExtension)swModel.Extension;
                    status = swModelDocExt.SaveAs(savefilename, 0, (int)swSaveAsOptions_e.swSaveAsOptions_Silent, 0, 0, 0);
                }
                catch
                {

                }
                try
                {
                    swassy = (AssemblyDoc)swApp.LoadFile4(igesfilename, "r", swImportIgesdata, ref errors);
                    swModel = (ModelDoc2)swassy;
                    swModelDocExt = (ModelDocExtension)swModel.Extension;
                    status = swModelDocExt.SaveAs(savefilename, 0, (int)swSaveAsOptions_e.swSaveAsOptions_Silent, 0, 0, 0);
                    
                }
                catch
                {

                }

                return status;
            }
            return false;
        }

        public bool importparasolidfile(string parasolidfilename, string savefilename)
        {
            if (solidworks_running() == true)
            {
                var progId = "SldWorks.Application";
                SldWorks swApp = Marshal.GetActiveObject(progId.ToString()) as SldWorks;
                PartDoc swPart = default(PartDoc);
                AssemblyDoc swassy = default(AssemblyDoc);
                ModelDoc2 swModel = default(ModelDoc2);
                ModelDocExtension swModelDocExt = default(ModelDocExtension);
                

                bool status = false;
                int errors = 0;
                try
                {
                    swPart = (PartDoc)swApp.LoadFile4(parasolidfilename, "r", null, ref errors);
                    swModel = (ModelDoc2)swPart;
                    swModelDocExt = (ModelDocExtension)swModel.Extension;
                    status = swModelDocExt.SaveAs(savefilename, 0, (int)swSaveAsOptions_e.swSaveAsOptions_Silent, 0, 0, 0);
                   
                }
                catch
                {

                }
                try
                {
                    swassy = (AssemblyDoc)swApp.LoadFile4(parasolidfilename, "r", null, ref errors);
                    swModel = (ModelDoc2)swassy;
                    swModelDocExt = (ModelDocExtension)swModel.Extension;
                    status = swModelDocExt.SaveAs(savefilename, 0, (int)swSaveAsOptions_e.swSaveAsOptions_Silent, 0, 0, 0);
                    
                }
                catch
                {

                }

                return status;
            }
            return false;
        }

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

        #endregion

        #region solidworks custom properties

        public void fillcustomproperties()
        {
            if (solidworks_running() == true)
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

                string category = getfamilycategory(familycombobox.SelectedItem.ToString()).ToString();
                if (category.ToLower() == "manufactured")
                {
                    PartDoc swPart = default(PartDoc);
                    swPart = (PartDoc)swModel;
                    swPart.SetMaterialPropertyName2("Default", "//SPM-ADFS/CAD Data/CAD Templates SPM/SPM.sldmat", mattxt.Text);
                }               

                bool boolstatus = false;
                boolstatus = swModel.Save3((int)swSaveAsOptions_e.swSaveAsOptions_Silent, 0, 0);

            }

        }

        public void chekbeforefillingcustomproperties(string item)
        {
                       
            if (solidworks_running() == true)
            {
                string getcurrentfilename = getfilename().ToString();
                string olditemnumber = item + "-0";
                if (getcurrentfilename == item || getcurrentfilename == olditemnumber)
                {
                    fillcustomproperties();

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
                        Open_assy(Pathassy);
                        fillcustomproperties();

                    }
                    else if (File.Exists(Pathpart))
                    {
                        Open_model(Pathpart);
                        fillcustomproperties();
                    }
                    else if (File.Exists(Pathparto))
                    {
                        Open_model(Pathparto);
                        fillcustomproperties();
                    }
                    else if (File.Exists(Pathassyo))
                    {
                        Open_assy(Pathassyo);
                        fillcustomproperties();

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

        #endregion

    }

}
