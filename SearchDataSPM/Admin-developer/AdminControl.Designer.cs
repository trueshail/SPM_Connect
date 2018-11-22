namespace SearchDataSPM
{
    partial class spmadmin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(spmadmin));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openModelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openDrawingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LabelTooltips = new System.Windows.Forms.ToolTip(this.components);
            this.SPM = new System.Windows.Forms.Label();
            this.nametextbox = new System.Windows.Forms.TextBox();
            this.domaintxtbox = new System.Windows.Forms.TextBox();
            this.activecadblocktxt = new System.Windows.Forms.TextBox();
            this.addnewbttn = new System.Windows.Forms.Button();
            this.updatebttn = new System.Windows.Forms.Button();
            this.delbttn = new System.Windows.Forms.Button();
            this.updatesavebttn = new System.Windows.Forms.Button();
            this.cnclbttn = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.reluanchbttn = new System.Windows.Forms.Button();
            this.UserStats = new System.Windows.Forms.Button();
            this.custbttn = new System.Windows.Forms.Button();
            this.matbttn = new System.Windows.Forms.Button();
            this.Userlistbox = new System.Windows.Forms.ListBox();
            this.department = new System.Windows.Forms.Label();
            this.username = new System.Windows.Forms.Label();
            this.admin = new System.Windows.Forms.Label();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.UserFirstname = new System.Windows.Forms.Label();
            this.engradio = new System.Windows.Forms.RadioButton();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.Cadblock = new System.Windows.Forms.Label();
            this.manageradioButtonNo = new System.Windows.Forms.RadioButton();
            this.manageradioButtonyes = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.DevradioButtonNo = new System.Windows.Forms.RadioButton();
            this.DevradioButtonYes = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.quoteno = new System.Windows.Forms.RadioButton();
            this.quoteyes = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.contextMenuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openModelToolStripMenuItem,
            this.openDrawingToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(151, 48);
            // 
            // openModelToolStripMenuItem
            // 
            this.openModelToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openModelToolStripMenuItem.Image")));
            this.openModelToolStripMenuItem.Name = "openModelToolStripMenuItem";
            this.openModelToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.openModelToolStripMenuItem.Text = "Open Model";
            this.openModelToolStripMenuItem.ToolTipText = "Open Selected Item Model";
            // 
            // openDrawingToolStripMenuItem
            // 
            this.openDrawingToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openDrawingToolStripMenuItem.Image")));
            this.openDrawingToolStripMenuItem.Name = "openDrawingToolStripMenuItem";
            this.openDrawingToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.openDrawingToolStripMenuItem.Text = "Open Drawing";
            this.openDrawingToolStripMenuItem.ToolTipText = "Opens Selected Item Drawing";
            // 
            // LabelTooltips
            // 
            this.LabelTooltips.AutoPopDelay = 4000;
            this.LabelTooltips.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.LabelTooltips.InitialDelay = 500;
            this.LabelTooltips.ReshowDelay = 100;
            // 
            // SPM
            // 
            this.SPM.BackColor = System.Drawing.Color.Transparent;
            this.SPM.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SPM.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.SPM.Image = ((System.Drawing.Image)(resources.GetObject("SPM.Image")));
            this.SPM.Location = new System.Drawing.Point(181, 9);
            this.SPM.MaximumSize = new System.Drawing.Size(300, 100);
            this.SPM.MinimumSize = new System.Drawing.Size(100, 100);
            this.SPM.Name = "SPM";
            this.SPM.Size = new System.Drawing.Size(232, 100);
            this.SPM.TabIndex = 32;
            this.SPM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.LabelTooltips.SetToolTip(this.SPM, "SPM Automation Inc.");
            this.SPM.DoubleClick += new System.EventHandler(this.SPM_DoubleClick);
            // 
            // nametextbox
            // 
            this.nametextbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nametextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nametextbox.Location = new System.Drawing.Point(365, 114);
            this.nametextbox.MaximumSize = new System.Drawing.Size(180, 20);
            this.nametextbox.MinimumSize = new System.Drawing.Size(180, 20);
            this.nametextbox.Name = "nametextbox";
            this.nametextbox.ReadOnly = true;
            this.nametextbox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.nametextbox.Size = new System.Drawing.Size(180, 22);
            this.nametextbox.TabIndex = 47;
            this.LabelTooltips.SetToolTip(this.nametextbox, "Item Family Group");
            // 
            // domaintxtbox
            // 
            this.domaintxtbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.domaintxtbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.domaintxtbox.Location = new System.Drawing.Point(365, 158);
            this.domaintxtbox.MaximumSize = new System.Drawing.Size(180, 20);
            this.domaintxtbox.MinimumSize = new System.Drawing.Size(180, 20);
            this.domaintxtbox.Name = "domaintxtbox";
            this.domaintxtbox.ReadOnly = true;
            this.domaintxtbox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.domaintxtbox.Size = new System.Drawing.Size(180, 22);
            this.domaintxtbox.TabIndex = 49;
            this.LabelTooltips.SetToolTip(this.domaintxtbox, "Item Quantities Per Assembly");
            // 
            // activecadblocktxt
            // 
            this.activecadblocktxt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.activecadblocktxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.activecadblocktxt.Location = new System.Drawing.Point(366, 196);
            this.activecadblocktxt.MaximumSize = new System.Drawing.Size(180, 20);
            this.activecadblocktxt.MinimumSize = new System.Drawing.Size(180, 20);
            this.activecadblocktxt.Name = "activecadblocktxt";
            this.activecadblocktxt.ReadOnly = true;
            this.activecadblocktxt.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.activecadblocktxt.Size = new System.Drawing.Size(180, 22);
            this.activecadblocktxt.TabIndex = 68;
            this.LabelTooltips.SetToolTip(this.activecadblocktxt, "Item Family Group");
            this.activecadblocktxt.TextChanged += new System.EventHandler(this.activecadblocktxt_TextChanged);
            this.activecadblocktxt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.activecadblocktxt_KeyPress);
            // 
            // addnewbttn
            // 
            this.addnewbttn.Location = new System.Drawing.Point(293, 406);
            this.addnewbttn.Name = "addnewbttn";
            this.addnewbttn.Size = new System.Drawing.Size(84, 23);
            this.addnewbttn.TabIndex = 57;
            this.addnewbttn.Text = "Add New User";
            this.LabelTooltips.SetToolTip(this.addnewbttn, "Add new user access");
            this.addnewbttn.UseVisualStyleBackColor = true;
            this.addnewbttn.Click += new System.EventHandler(this.addnewbttn_Click);
            // 
            // updatebttn
            // 
            this.updatebttn.Location = new System.Drawing.Point(393, 406);
            this.updatebttn.Name = "updatebttn";
            this.updatebttn.Size = new System.Drawing.Size(76, 23);
            this.updatebttn.TabIndex = 58;
            this.updatebttn.Text = "Update User";
            this.LabelTooltips.SetToolTip(this.updatebttn, "Update Existing User");
            this.updatebttn.UseVisualStyleBackColor = true;
            this.updatebttn.Click += new System.EventHandler(this.updatebttn_Click);
            // 
            // delbttn
            // 
            this.delbttn.Location = new System.Drawing.Point(479, 406);
            this.delbttn.Name = "delbttn";
            this.delbttn.Size = new System.Drawing.Size(72, 23);
            this.delbttn.TabIndex = 59;
            this.delbttn.Text = "Delete User";
            this.LabelTooltips.SetToolTip(this.delbttn, "Delete User from System");
            this.delbttn.UseVisualStyleBackColor = true;
            this.delbttn.Click += new System.EventHandler(this.delbttn_Click);
            // 
            // updatesavebttn
            // 
            this.updatesavebttn.Location = new System.Drawing.Point(325, 443);
            this.updatesavebttn.Name = "updatesavebttn";
            this.updatesavebttn.Size = new System.Drawing.Size(76, 23);
            this.updatesavebttn.TabIndex = 64;
            this.updatesavebttn.Text = "Save";
            this.LabelTooltips.SetToolTip(this.updatesavebttn, "Save Data ");
            this.updatesavebttn.UseVisualStyleBackColor = true;
            this.updatesavebttn.Visible = false;
            this.updatesavebttn.Click += new System.EventHandler(this.updatesavebttn_Click);
            // 
            // cnclbttn
            // 
            this.cnclbttn.Location = new System.Drawing.Point(418, 443);
            this.cnclbttn.Name = "cnclbttn";
            this.cnclbttn.Size = new System.Drawing.Size(69, 23);
            this.cnclbttn.TabIndex = 65;
            this.cnclbttn.Text = "Exit";
            this.LabelTooltips.SetToolTip(this.cnclbttn, "Clear and Exit");
            this.cnclbttn.UseVisualStyleBackColor = true;
            this.cnclbttn.Visible = false;
            this.cnclbttn.Click += new System.EventHandler(this.cnclbttn_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(286, 490);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(115, 30);
            this.button1.TabIndex = 66;
            this.button1.Text = "Show Duplicates";
            this.LabelTooltips.SetToolTip(this.button1, "Show Duplicate Items");
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // reluanchbttn
            // 
            this.reluanchbttn.Location = new System.Drawing.Point(423, 490);
            this.reluanchbttn.Name = "reluanchbttn";
            this.reluanchbttn.Size = new System.Drawing.Size(117, 30);
            this.reluanchbttn.TabIndex = 67;
            this.reluanchbttn.Text = "Relaunch Application";
            this.LabelTooltips.SetToolTip(this.reluanchbttn, "Relaunch SPM Connect");
            this.reluanchbttn.UseVisualStyleBackColor = true;
            this.reluanchbttn.Click += new System.EventHandler(this.reluanchbttn_Click);
            // 
            // UserStats
            // 
            this.UserStats.Location = new System.Drawing.Point(347, 585);
            this.UserStats.Name = "UserStats";
            this.UserStats.Size = new System.Drawing.Size(115, 30);
            this.UserStats.TabIndex = 66;
            this.UserStats.Text = "Licences In Use";
            this.LabelTooltips.SetToolTip(this.UserStats, "Manage Licences in Use");
            this.UserStats.UseVisualStyleBackColor = true;
            this.UserStats.Click += new System.EventHandler(this.UserStats_Click);
            // 
            // custbttn
            // 
            this.custbttn.Location = new System.Drawing.Point(286, 538);
            this.custbttn.Name = "custbttn";
            this.custbttn.Size = new System.Drawing.Size(115, 30);
            this.custbttn.TabIndex = 66;
            this.custbttn.Text = "Manage Customers";
            this.LabelTooltips.SetToolTip(this.custbttn, "Manage Customers Data");
            this.custbttn.UseVisualStyleBackColor = true;
            this.custbttn.Click += new System.EventHandler(this.custbttn_Click);
            // 
            // matbttn
            // 
            this.matbttn.Location = new System.Drawing.Point(423, 538);
            this.matbttn.Name = "matbttn";
            this.matbttn.Size = new System.Drawing.Size(115, 30);
            this.matbttn.TabIndex = 66;
            this.matbttn.Text = "Manage Materials";
            this.LabelTooltips.SetToolTip(this.matbttn, "Manage Material Data");
            this.matbttn.UseVisualStyleBackColor = true;
            this.matbttn.Click += new System.EventHandler(this.matbttn_Click);
            // 
            // Userlistbox
            // 
            this.Userlistbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Userlistbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Userlistbox.FormattingEnabled = true;
            this.Userlistbox.ItemHeight = 20;
            this.Userlistbox.Location = new System.Drawing.Point(12, 118);
            this.Userlistbox.Name = "Userlistbox";
            this.Userlistbox.Size = new System.Drawing.Size(212, 484);
            this.Userlistbox.TabIndex = 33;
            this.Userlistbox.SelectedIndexChanged += new System.EventHandler(this.Userlistbox_SelectedIndexChanged);
            // 
            // department
            // 
            this.department.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.department.AutoSize = true;
            this.department.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.department.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.department.Location = new System.Drawing.Point(262, 229);
            this.department.MaximumSize = new System.Drawing.Size(90, 15);
            this.department.Name = "department";
            this.department.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.department.Size = new System.Drawing.Size(90, 15);
            this.department.TabIndex = 52;
            this.department.Text = "Department : ";
            // 
            // username
            // 
            this.username.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.username.AutoSize = true;
            this.username.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.username.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.username.Location = new System.Drawing.Point(273, 150);
            this.username.Name = "username";
            this.username.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.username.Size = new System.Drawing.Size(79, 30);
            this.username.TabIndex = 50;
            this.username.Text = "Domain\\\r\nusername :";
            // 
            // admin
            // 
            this.admin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.admin.AutoSize = true;
            this.admin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.admin.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.admin.Location = new System.Drawing.Point(295, 261);
            this.admin.MaximumSize = new System.Drawing.Size(90, 15);
            this.admin.Name = "admin";
            this.admin.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.admin.Size = new System.Drawing.Size(59, 15);
            this.admin.TabIndex = 53;
            this.admin.Text = "Admin : ";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Enabled = false;
            this.radioButton1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.radioButton1.Location = new System.Drawing.Point(6, 4);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(43, 17);
            this.radioButton1.TabIndex = 54;
            this.radioButton1.Text = "Yes";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Enabled = false;
            this.radioButton2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.radioButton2.Location = new System.Drawing.Point(55, 3);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(39, 17);
            this.radioButton2.TabIndex = 55;
            this.radioButton2.Text = "No";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // UserFirstname
            // 
            this.UserFirstname.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UserFirstname.AutoSize = true;
            this.UserFirstname.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UserFirstname.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.UserFirstname.Location = new System.Drawing.Point(299, 114);
            this.UserFirstname.Name = "UserFirstname";
            this.UserFirstname.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.UserFirstname.Size = new System.Drawing.Size(53, 15);
            this.UserFirstname.TabIndex = 56;
            this.UserFirstname.Text = "Name :";
            // 
            // engradio
            // 
            this.engradio.AutoSize = true;
            this.engradio.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(69)))), ((int)(((byte)(76)))));
            this.engradio.Enabled = false;
            this.engradio.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.engradio.Location = new System.Drawing.Point(3, 9);
            this.engradio.Name = "engradio";
            this.engradio.Size = new System.Drawing.Size(47, 17);
            this.engradio.TabIndex = 61;
            this.engradio.Text = "Eng.";
            this.engradio.UseVisualStyleBackColor = false;
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(69)))), ((int)(((byte)(76)))));
            this.radioButton4.Enabled = false;
            this.radioButton4.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.radioButton4.Location = new System.Drawing.Point(62, 9);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(63, 17);
            this.radioButton4.TabIndex = 62;
            this.radioButton4.Text = "Controls";
            this.radioButton4.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(69)))), ((int)(((byte)(76)))));
            this.panel1.Controls.Add(this.radioButton3);
            this.panel1.Controls.Add(this.engradio);
            this.panel1.Controls.Add(this.radioButton4);
            this.panel1.Location = new System.Drawing.Point(363, 222);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(218, 33);
            this.panel1.TabIndex = 63;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(69)))), ((int)(((byte)(76)))));
            this.radioButton3.Enabled = false;
            this.radioButton3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.radioButton3.Location = new System.Drawing.Point(139, 9);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(76, 17);
            this.radioButton3.TabIndex = 63;
            this.radioButton3.Text = "Production";
            this.radioButton3.UseVisualStyleBackColor = false;
            // 
            // Cadblock
            // 
            this.Cadblock.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Cadblock.AutoSize = true;
            this.Cadblock.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cadblock.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Cadblock.Location = new System.Drawing.Point(230, 196);
            this.Cadblock.Name = "Cadblock";
            this.Cadblock.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Cadblock.Size = new System.Drawing.Size(122, 15);
            this.Cadblock.TabIndex = 69;
            this.Cadblock.Text = "Active CAD Block :";
            // 
            // manageradioButtonNo
            // 
            this.manageradioButtonNo.AutoSize = true;
            this.manageradioButtonNo.Enabled = false;
            this.manageradioButtonNo.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.manageradioButtonNo.Location = new System.Drawing.Point(56, 6);
            this.manageradioButtonNo.Name = "manageradioButtonNo";
            this.manageradioButtonNo.Size = new System.Drawing.Size(39, 17);
            this.manageradioButtonNo.TabIndex = 72;
            this.manageradioButtonNo.Text = "No";
            this.manageradioButtonNo.UseVisualStyleBackColor = true;
            // 
            // manageradioButtonyes
            // 
            this.manageradioButtonyes.AutoSize = true;
            this.manageradioButtonyes.Enabled = false;
            this.manageradioButtonyes.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.manageradioButtonyes.Location = new System.Drawing.Point(6, 6);
            this.manageradioButtonyes.Name = "manageradioButtonyes";
            this.manageradioButtonyes.Size = new System.Drawing.Size(43, 17);
            this.manageradioButtonyes.TabIndex = 71;
            this.manageradioButtonyes.Text = "Yes";
            this.manageradioButtonyes.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(253, 290);
            this.label1.MaximumSize = new System.Drawing.Size(100, 15);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label1.Size = new System.Drawing.Size(99, 15);
            this.label1.TabIndex = 70;
            this.label1.Text = "Management : ";
            // 
            // DevradioButtonNo
            // 
            this.DevradioButtonNo.AutoSize = true;
            this.DevradioButtonNo.Enabled = false;
            this.DevradioButtonNo.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.DevradioButtonNo.Location = new System.Drawing.Point(56, 5);
            this.DevradioButtonNo.Name = "DevradioButtonNo";
            this.DevradioButtonNo.Size = new System.Drawing.Size(39, 17);
            this.DevradioButtonNo.TabIndex = 75;
            this.DevradioButtonNo.Text = "No";
            this.DevradioButtonNo.UseVisualStyleBackColor = true;
            // 
            // DevradioButtonYes
            // 
            this.DevradioButtonYes.AutoSize = true;
            this.DevradioButtonYes.Enabled = false;
            this.DevradioButtonYes.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.DevradioButtonYes.Location = new System.Drawing.Point(6, 5);
            this.DevradioButtonYes.Name = "DevradioButtonYes";
            this.DevradioButtonYes.Size = new System.Drawing.Size(43, 17);
            this.DevradioButtonYes.TabIndex = 74;
            this.DevradioButtonYes.Text = "Yes";
            this.DevradioButtonYes.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(272, 353);
            this.label2.MaximumSize = new System.Drawing.Size(90, 15);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label2.Size = new System.Drawing.Size(80, 15);
            this.label2.TabIndex = 73;
            this.label2.Text = "Developer :";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.radioButton2);
            this.panel2.Controls.Add(this.radioButton1);
            this.panel2.Location = new System.Drawing.Point(363, 261);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(106, 24);
            this.panel2.TabIndex = 76;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.manageradioButtonNo);
            this.panel3.Controls.Add(this.manageradioButtonyes);
            this.panel3.Location = new System.Drawing.Point(362, 288);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(107, 26);
            this.panel3.TabIndex = 77;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.DevradioButtonNo);
            this.panel4.Controls.Add(this.DevradioButtonYes);
            this.panel4.Location = new System.Drawing.Point(362, 353);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(107, 27);
            this.panel4.TabIndex = 78;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.quoteno);
            this.panel5.Controls.Add(this.quoteyes);
            this.panel5.Location = new System.Drawing.Point(362, 320);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(107, 26);
            this.panel5.TabIndex = 80;
            // 
            // quoteno
            // 
            this.quoteno.AutoSize = true;
            this.quoteno.Enabled = false;
            this.quoteno.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.quoteno.Location = new System.Drawing.Point(56, 6);
            this.quoteno.Name = "quoteno";
            this.quoteno.Size = new System.Drawing.Size(39, 17);
            this.quoteno.TabIndex = 72;
            this.quoteno.Text = "No";
            this.quoteno.UseVisualStyleBackColor = true;
            // 
            // quoteyes
            // 
            this.quoteyes.AutoSize = true;
            this.quoteyes.Enabled = false;
            this.quoteyes.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.quoteyes.Location = new System.Drawing.Point(6, 6);
            this.quoteyes.Name = "quoteyes";
            this.quoteyes.Size = new System.Drawing.Size(43, 17);
            this.quoteyes.TabIndex = 71;
            this.quoteyes.Text = "Yes";
            this.quoteyes.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label3.Location = new System.Drawing.Point(253, 326);
            this.label3.MaximumSize = new System.Drawing.Size(120, 15);
            this.label3.Name = "label3";
            this.label3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label3.Size = new System.Drawing.Size(101, 15);
            this.label3.TabIndex = 79;
            this.label3.Text = "Quote Access :";
            // 
            // spmadmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(69)))), ((int)(((byte)(76)))));
            this.ClientSize = new System.Drawing.Size(595, 622);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Cadblock);
            this.Controls.Add(this.activecadblocktxt);
            this.Controls.Add(this.reluanchbttn);
            this.Controls.Add(this.matbttn);
            this.Controls.Add(this.custbttn);
            this.Controls.Add(this.UserStats);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cnclbttn);
            this.Controls.Add(this.updatesavebttn);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.delbttn);
            this.Controls.Add(this.updatebttn);
            this.Controls.Add(this.addnewbttn);
            this.Controls.Add(this.UserFirstname);
            this.Controls.Add(this.admin);
            this.Controls.Add(this.department);
            this.Controls.Add(this.nametextbox);
            this.Controls.Add(this.username);
            this.Controls.Add(this.domaintxtbox);
            this.Controls.Add(this.Userlistbox);
            this.Controls.Add(this.SPM);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(600, 600);
            this.Name = "spmadmin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Admin Control - SPM Connect";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.spmadmin_FormClosed);
            this.Load += new System.EventHandler(this.ParentView_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem openModelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openDrawingToolStripMenuItem;
        private System.Windows.Forms.ToolTip LabelTooltips;
        private System.Windows.Forms.Label SPM;
        private System.Windows.Forms.ListBox Userlistbox;
        private System.Windows.Forms.Label department;
        private System.Windows.Forms.TextBox nametextbox;
        private System.Windows.Forms.Label username;
        private System.Windows.Forms.TextBox domaintxtbox;
        private System.Windows.Forms.Label admin;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.Label UserFirstname;
        private System.Windows.Forms.Button addnewbttn;
        private System.Windows.Forms.Button updatebttn;
        private System.Windows.Forms.Button delbttn;
        private System.Windows.Forms.RadioButton engradio;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button updatesavebttn;
        private System.Windows.Forms.Button cnclbttn;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button reluanchbttn;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.Button UserStats;
        private System.Windows.Forms.Label Cadblock;
        private System.Windows.Forms.TextBox activecadblocktxt;
        private System.Windows.Forms.RadioButton manageradioButtonNo;
        private System.Windows.Forms.RadioButton manageradioButtonyes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton DevradioButtonNo;
        private System.Windows.Forms.RadioButton DevradioButtonYes;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button custbttn;
        private System.Windows.Forms.Button matbttn;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.RadioButton quoteno;
        private System.Windows.Forms.RadioButton quoteyes;
        private System.Windows.Forms.Label label3;
    }
}