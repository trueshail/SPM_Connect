using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SearchDataSPM.Controls
{
    public partial class SPM_ConnectItems : Form

    {
        #region SPM Connect Load

        private readonly SPMConnectAPI.SPMSQLCommands connectapi = new SPMConnectAPI.SPMSQLCommands();
        private DataTable dt;
        private log4net.ILog log;

        public SPM_ConnectItems()
        {
            InitializeComponent();

            dt = new DataTable();
        }

        private void Reload_Click(object sender, EventArgs e)
        {
            Clearandhide();
            txtSearch.Clear();
            txtSearch.Focus();
            SendKeys.Send("~");
            dataGridView.Refresh();
        }

        private void Showallitems()
        {
            try
            {
                dt.Clear();
                dt = connectapi.Showallitems();
                dataGridView.DataSource = dt;
                _ = dt.DefaultView;

                UpdateFont();
            }
            catch (Exception)
            {
                //MessageBox.Show(ex.Message, "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Data cannot be retrieved from server. Please contact the admin.", "SPM Connect - SQL SERVER ENG", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            finally
            {
                connectapi.cn.Close();
            }

            //dataGridView.Location = new Point(0, 40);
        }

        private void SPM_Connect_Load(object sender, EventArgs e)
        {
            Showallitems();
            dataGridView.Columns[5].Visible = false;
            dataGridView.Columns[6].Visible = false;
            dataGridView.Columns[7].Visible = false;
            dataGridView.Columns[8].Visible = false;
            dataGridView.Columns[9].Visible = false;
            dataGridView.Columns[10].Visible = false;
            dataGridView.Columns[11].Visible = false;
            dataGridView.Columns[12].Visible = false;
            dataGridView.Columns[13].Visible = false;
            dataGridView.Columns[14].Visible = false;
            dataGridView.Columns[15].Visible = false;
            dataGridView.Columns[16].Visible = false;
            dataGridView.Columns[0].Width = 60;
            dataGridView.Columns[2].Width = 55;
            dataGridView.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            string userName = connectapi.ConnectUser.UserName;
            this.Text = "SPM Connect Engineering - " + userName.Substring(4);
            log4net.Config.XmlConfigurator.Configure();
            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log.Info("Opened Connect Select Items ");
        }

        private void UpdateFont()
        {
            dataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 9.0F, FontStyle.Bold);
            dataGridView.DefaultCellStyle.Font = new Font("Arial", 9.5F, FontStyle.Bold);
            dataGridView.DefaultCellStyle.ForeColor = Color.Black;
            dataGridView.DefaultCellStyle.BackColor = Color.FromArgb(237, 237, 237);
            dataGridView.DefaultCellStyle.SelectionForeColor = Color.Yellow;
            dataGridView.DefaultCellStyle.SelectionBackColor = Color.Black;
        }

        #endregion SPM Connect Load

        #region Public Table & variables

        public static string description;

        public static string family;

        public static string ItemNo;

        public static string Manufacturer;

        public static string oem;

        // variables required outside the functions to perfrom
        private readonly string fullsearch = "Description LIKE '%{0}%' OR Manufacturer LIKE '%{0}%' OR ManufacturerItemNumber LIKE '%{0}%' OR ItemNumber LIKE '%{0}%'";

        // private readonly string fullsearch = ("FullSearch LIKE '%{0}%'");

        private DataTable table0 = new DataTable();
        private DataTable table1 = new DataTable();
        private DataTable table2 = new DataTable();
        private DataTable table3 = new DataTable();

        #endregion Public Table & variables

        #region Search Parameters

        public void TxtSearch_KeyDown(object sender, KeyEventArgs e)

        {
            if (e.KeyCode == Keys.Return)

            {
                if (Descrip_txtbox.Visible)
                {
                    Clearandhide();
                }
                Showallitems();
                Mainsearch();
                if (txtSearch.Text.Length > 0)
                {
                    Descrip_txtbox.Show();
                    SendKeys.Send("{TAB}");
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void Clearandhide()
        {
            Descrip_txtbox.Hide();
            Descrip_txtbox.Clear();
            filteroem_txtbox.Hide();
            filteroem_txtbox.Clear();
            filteroemitem_txtbox.Hide();
            filteroemitem_txtbox.Clear();
            filter4.Hide();
            filter4.Clear();
            table0.Clear();
            table1.Clear();
            table2.Clear();
            table3.Clear();
        }

        private void Descrip_txtbox_KeyDown(object sender, KeyEventArgs e)
        {
            DataView dv = table0.DefaultView;
            table0 = dv.ToTable();

            if (e.KeyCode == Keys.Return)
            {
                string search2 = Descrip_txtbox.Text;
                try
                {
                    search2 = search2.Replace("'", "''");
                    search2 = search2.Replace("[", "[[]");
                    var secondFilter = string.Format(fullsearch, search2);
                    if (string.IsNullOrEmpty(dv.RowFilter))
                        dv.RowFilter = secondFilter;
                    else
                        dv.RowFilter += " AND " + secondFilter;
                    dataGridView.DataSource = dv;
                    SearchStringPosition();
                    Searchtext(Descrip_txtbox.Text);
                    table1 = dv.ToTable();
                    dataGridView.Refresh();
                }
                catch (Exception)
                {
                    MessageBox.Show("Invalid Search Criteria Operator.", "SPM Connect");
                    Descrip_txtbox.Clear();
                    SendKeys.Send("~");
                }

                if (Descrip_txtbox.Text.Length > 0)
                {
                    filteroem_txtbox.Show();
                    SendKeys.Send("{TAB}");
                }
                else
                {
                    filteroem_txtbox.Hide();
                    filteroemitem_txtbox.Hide();
                    filter4.Hide();
                }
                if (!Descrip_txtbox.Visible)
                {
                    filteroem_txtbox.Hide();
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void Filter4_KeyDown(object sender, KeyEventArgs e)
        {
            DataView dv = table3.DefaultView;
            table3 = dv.ToTable();
            if (e.KeyCode == Keys.Return)
            {
                string search5 = filter4.Text;
                try
                {
                    search5 = search5.Replace("'", "''");
                    search5 = search5.Replace("[", "[[]");
                    var fifthfilter = string.Format(fullsearch, search5);

                    if (string.IsNullOrEmpty(dv.RowFilter))
                        dv.RowFilter = fifthfilter;
                    else
                        dv.RowFilter += " AND " + fifthfilter;
                    dataGridView.DataSource = dv;
                    SearchStringPosition();
                    Searchtext(filter4.Text);
                    dataGridView.Refresh();
                }
                catch (Exception)
                {
                    MessageBox.Show("Invalid Search Criteria Operator.", "SPM Connect");
                    filter4.Clear();
                    SendKeys.Send("~");
                }

                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void Filteroem_txtbox_KeyDown(object sender, KeyEventArgs e)
        {
            DataView dv = table1.DefaultView;
            table1 = dv.ToTable();

            if (e.KeyCode == Keys.Return)
            {
                string search3 = filteroem_txtbox.Text;
                try
                {
                    search3 = search3.Replace("'", "''");
                    search3 = search3.Replace("[", "[[]");
                    var thirdFilter = string.Format(fullsearch, search3);
                    if (string.IsNullOrEmpty(dv.RowFilter))
                        dv.RowFilter = thirdFilter;
                    else
                        dv.RowFilter += " AND " + thirdFilter;
                    dataGridView.DataSource = dv;
                    SearchStringPosition();
                    Searchtext(filteroem_txtbox.Text);
                    table2 = dv.ToTable();
                    dataGridView.Refresh();
                }
                catch (Exception)
                {
                    MessageBox.Show("Invalid Search Criteria Operator.", "SPM Connect");
                    filteroem_txtbox.Clear();
                    SendKeys.Send("~");
                }

                if (filteroem_txtbox.Text.Length > 0)
                {
                    filteroemitem_txtbox.Show();
                    SendKeys.Send("{TAB}");
                }
                else
                {
                    filteroemitem_txtbox.Hide();
                    filter4.Hide();
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void Filteroemitem_txtbox_KeyDown(object sender, KeyEventArgs e)
        {
            DataView dv = table2.DefaultView;
            table2 = dv.ToTable();
            if (e.KeyCode == Keys.Return)
            {
                string search4 = filteroemitem_txtbox.Text;
                try
                {
                    search4 = search4.Replace("'", "''");
                    search4 = search4.Replace("[", "[[]");
                    var fourthfilter = string.Format(fullsearch, search4);

                    if (string.IsNullOrEmpty(dv.RowFilter))
                        dv.RowFilter = fourthfilter;
                    else
                        dv.RowFilter += " AND " + fourthfilter;
                    dataGridView.DataSource = dv;
                    SearchStringPosition();
                    Searchtext(filteroemitem_txtbox.Text);
                    table3 = dv.ToTable();
                    dataGridView.Refresh();
                }
                catch (Exception)
                {
                    MessageBox.Show("Invalid Search Criteria Operator.", "SPM Connect");
                    filteroemitem_txtbox.Clear();
                    SendKeys.Send("~");
                }

                if (filteroemitem_txtbox.Text.Length > 0)
                {
                    filter4.Show();
                    SendKeys.Send("{TAB}");
                }
                else
                {
                    filter4.Hide();
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void Mainsearch()
        {
            string search1 = txtSearch.Text;
            if (search1.Length > 3)
            {
                if (Char.IsLetter(search1.FirstOrDefault()) && search1.Substring(3, 1) == "%")
                {
                    try
                    {
                        search1 = search1.Replace("'", "''");
                        search1 = search1.Replace("[", "[[]");
                        const string fullsearch1 = "ItemNumber LIKE '%{0}%'";
                        string s = string.Format(fullsearch1, search1);
                        table0 = connectapi.ShowFilterallitems(s, true);
                        dataGridView.DataSource = table0;
                        dataGridView.Update();
                        SearchStringPosition();
                        Searchtext(txtSearch.Text.Substring(0, txtSearch.Text.Length - 1));
                        dataGridView.Refresh();
                    }
                    catch (Exception)

                    {
                        MessageBox.Show("Invalid Search Criteria Operator.", "SPM Connect - Search1");
                        txtSearch.Clear();
                        SendKeys.Send("~");
                    }
                }
                else
                {
                    try
                    {
                        search1 = search1.Replace("'", "''");
                        search1 = search1.Replace("[", "[[]");
                        table0 = connectapi.ShowFilterallitems(search1, false);
                        dataGridView.DataSource = table0;
                        dataGridView.Update();
                        SearchStringPosition();
                        Searchtext(search1);
                        dataGridView.Refresh();
                    }
                    catch (Exception)

                    {
                        MessageBox.Show("Invalid Search Criteria Operator.", "SPM Connect - Search1");
                        txtSearch.Clear();
                        SendKeys.Send("~");
                    }
                }
            }
            else
            {
                try
                {
                    search1 = search1.Replace("'", "''");
                    search1 = search1.Replace("[", "[[]");
                    table0 = connectapi.ShowFilterallitems(search1, false);
                    dataGridView.DataSource = table0;
                    dataGridView.Update();
                    SearchStringPosition();
                    Searchtext(search1);
                    dataGridView.Refresh();
                }
                catch (Exception)

                {
                    MessageBox.Show("Invalid Search Criteria Operator.", "SPM Connect - Search1");
                    txtSearch.Clear();
                    SendKeys.Send("~");
                }
            }
        }

        #endregion Search Parameters

        #region ADD ITEM TO assembly - GetItemInfo and datagridview events

        private void DataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            _ = dataGridView.Rows[e.RowIndex];

            int columnindex = e.RowIndex;
            dataGridView.ClearSelection();
            dataGridView.Rows[columnindex].Selected = true;
            GetRowInfo();
        }

        private void GetRowInfo()
        {
            int selectedrowindex = dataGridView.SelectedCells[0].RowIndex;
            DataGridViewRow slectedrow = dataGridView.Rows[selectedrowindex];
            ItemNo = Convert.ToString(slectedrow.Cells[0].Value);
            description = Convert.ToString(slectedrow.Cells[1].Value);
            family = Convert.ToString(slectedrow.Cells[2].Value);
            Manufacturer = Convert.ToString(slectedrow.Cells[3].Value);
            oem = Convert.ToString(slectedrow.Cells[4].Value);
            DialogResult result = MessageBox.Show(
                "ItemNumber = " + ItemNo + Environment.NewLine +
                "Description = " + description + Environment.NewLine +
                "Family = " + family + Environment.NewLine +
                "Manufacturer = " + Manufacturer + Environment.NewLine +
                "OEM = " + oem, "Add To AutoCad Assembly?",
                                                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                //code for Yes
                this.Close();
            }
            else if (result == DialogResult.No)
            {
                //code for No
            }
        }

        #endregion ADD ITEM TO assembly - GetItemInfo and datagridview events

        #region Highlight Search Results

        private bool IsSelected;

        private string sw;

        private void DataGridView_CellPainting_1(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 1 && IsSelected)
            {
                e.Handled = true;
                e.PaintBackground(e.CellBounds, true);

                if (!string.IsNullOrEmpty(sw))
                {
                    string val = (string)e.FormattedValue;
                    int sindx = val.IndexOf(sw, StringComparison.CurrentCultureIgnoreCase);
                    if (sindx >= 0)
                    {
                        Rectangle hl_rect = new Rectangle
                        {
                            Y = e.CellBounds.Y + 2,
                            Height = e.CellBounds.Height - 5
                        };

                        string sBefore = val.Substring(0, sindx);
                        string sWord = val.Substring(sindx, sw.Length);
                        Size s1 = TextRenderer.MeasureText(e.Graphics, sBefore, e.CellStyle.Font, e.CellBounds.Size);
                        Size s2 = TextRenderer.MeasureText(e.Graphics, sWord, e.CellStyle.Font, e.CellBounds.Size);

                        if (s1.Width > 5)
                        {
                            hl_rect.X = e.CellBounds.X + s1.Width - 5;
                            hl_rect.Width = s2.Width - 6;
                        }
                        else
                        {
                            hl_rect.X = e.CellBounds.X + 2;
                            hl_rect.Width = s2.Width - 6;
                        }

                        SolidBrush hl_brush = (e.State & DataGridViewElementStates.Selected) != DataGridViewElementStates.None
                            ? new SolidBrush(Color.Black)
                            : new SolidBrush(Color.FromArgb(126, 206, 253));
                        e.Graphics.FillRectangle(hl_brush, hl_rect);

                        hl_brush.Dispose();
                    }
                }
                e.PaintContent(e.CellBounds);
            }
        }

        private void SearchStringPosition()
        {
            IsSelected = true;
        }

        private void Searchtext(string searchkey)
        {
            sw = searchkey;
        }

        #endregion Highlight Search Results

        #region AdminControlLabel

        private void SPM_DoubleClick(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.spm-automation.com/");
        }

        #endregion AdminControlLabel

        #region Closing SPMConnect

        private void SPM_Connect_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt && e.KeyCode.ToString() == "A")
            {
                Reload.PerformClick();
            }
        }

        #endregion Closing SPMConnect

        private void SPM_ConnectItems_FormClosed(object sender, FormClosedEventArgs e)
        {
            log.Info("Closed Select Items ");
            this.Dispose();
        }
    }
}