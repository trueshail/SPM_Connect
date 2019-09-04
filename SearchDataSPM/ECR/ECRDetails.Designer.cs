namespace SearchDataSPM
{
    partial class ECRDetails
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ECRDetails));
            this.notestxt = new System.Windows.Forms.TextBox();
            this.invoicelbl = new System.Windows.Forms.Label();
            this.ecrnotxtbox = new System.Windows.Forms.TextBox();
            this.departmentcomboBox = new System.Windows.Forms.ComboBox();
            this.projectmanagercombobox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.TreeViewToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.savbttn = new System.Windows.Forms.Button();
            this.editbttn = new System.Windows.Forms.Button();
            this.descriptiontxtbox = new System.Windows.Forms.TextBox();
            this.requestedbycombobox = new System.Windows.Forms.ComboBox();
            this.jobtxt = new System.Windows.Forms.TextBox();
            this.satxt = new System.Windows.Forms.TextBox();
            this.partnotxt = new System.Windows.Forms.TextBox();
            this.SPM = new System.Windows.Forms.Label();
            this.commentslbl = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.iteminfogroupBox = new System.Windows.Forms.GroupBox();
            this.subassylbl = new System.Windows.Forms.Label();
            this.jobnamelbl = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.statusStrip2 = new System.Windows.Forms.StatusStrip();
            this.Createdon = new System.Windows.Forms.ToolStripStatusLabel();
            this.CreatedBy = new System.Windows.Forms.ToolStripStatusLabel();
            this.lastsavedon = new System.Windows.Forms.ToolStripStatusLabel();
            this.lastsavedby = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.submissiongroupBox = new System.Windows.Forms.GroupBox();
            this.ecrhandlercheckBox = new System.Windows.Forms.CheckBox();
            this.managercheckBox = new System.Windows.Forms.CheckBox();
            this.supcheckBox = new System.Windows.Forms.CheckBox();
            this.submitecrhandlercheckBox = new System.Windows.Forms.CheckBox();
            this.iteminfogroupBox.SuspendLayout();
            this.statusStrip2.SuspendLayout();
            this.submissiongroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // notestxt
            // 
            this.notestxt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.notestxt.BackColor = System.Drawing.Color.White;
            this.notestxt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.notestxt.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.notestxt.Location = new System.Drawing.Point(9, 531);
            this.notestxt.MinimumSize = new System.Drawing.Size(180, 20);
            this.notestxt.Multiline = true;
            this.notestxt.Name = "notestxt";
            this.notestxt.ReadOnly = true;
            this.notestxt.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.notestxt.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.notestxt.Size = new System.Drawing.Size(660, 119);
            this.notestxt.TabIndex = 12;
            this.TreeViewToolTip.SetToolTip(this.notestxt, "Notes or comments for invoice");
            // 
            // invoicelbl
            // 
            this.invoicelbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.invoicelbl.AutoSize = true;
            this.invoicelbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.invoicelbl.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.invoicelbl.Location = new System.Drawing.Point(6, 49);
            this.invoicelbl.Name = "invoicelbl";
            this.invoicelbl.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.invoicelbl.Size = new System.Drawing.Size(60, 13);
            this.invoicelbl.TabIndex = 91;
            this.invoicelbl.Text = "ECR No :";
            // 
            // ecrnotxtbox
            // 
            this.ecrnotxtbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ecrnotxtbox.BackColor = System.Drawing.Color.White;
            this.ecrnotxtbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ecrnotxtbox.Location = new System.Drawing.Point(9, 65);
            this.ecrnotxtbox.MaximumSize = new System.Drawing.Size(200, 30);
            this.ecrnotxtbox.Name = "ecrnotxtbox";
            this.ecrnotxtbox.ReadOnly = true;
            this.ecrnotxtbox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ecrnotxtbox.Size = new System.Drawing.Size(134, 22);
            this.ecrnotxtbox.TabIndex = 0;
            this.TreeViewToolTip.SetToolTip(this.ecrnotxtbox, "Invoice Number");
            // 
            // departmentcomboBox
            // 
            this.departmentcomboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.departmentcomboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.departmentcomboBox.BackColor = System.Drawing.SystemColors.Window;
            this.departmentcomboBox.DropDownWidth = 150;
            this.departmentcomboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.departmentcomboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.departmentcomboBox.FormattingEnabled = true;
            this.departmentcomboBox.ItemHeight = 13;
            this.departmentcomboBox.Location = new System.Drawing.Point(769, 54);
            this.departmentcomboBox.Name = "departmentcomboBox";
            this.departmentcomboBox.Size = new System.Drawing.Size(202, 21);
            this.departmentcomboBox.TabIndex = 7;
            this.TreeViewToolTip.SetToolTip(this.departmentcomboBox, "Requested by");
            // 
            // projectmanagercombobox
            // 
            this.projectmanagercombobox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.projectmanagercombobox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.projectmanagercombobox.BackColor = System.Drawing.SystemColors.Window;
            this.projectmanagercombobox.DropDownWidth = 150;
            this.projectmanagercombobox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.projectmanagercombobox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.projectmanagercombobox.FormattingEnabled = true;
            this.projectmanagercombobox.ItemHeight = 13;
            this.projectmanagercombobox.Location = new System.Drawing.Point(769, 22);
            this.projectmanagercombobox.Name = "projectmanagercombobox";
            this.projectmanagercombobox.Size = new System.Drawing.Size(202, 21);
            this.projectmanagercombobox.TabIndex = 6;
            this.TreeViewToolTip.SetToolTip(this.projectmanagercombobox, "Sales Person");
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label3.Location = new System.Drawing.Point(643, 56);
            this.label3.Name = "label3";
            this.label3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label3.Size = new System.Drawing.Size(90, 15);
            this.label3.TabIndex = 99;
            this.label3.Text = "Department :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label5.Location = new System.Drawing.Point(643, 24);
            this.label5.Name = "label5";
            this.label5.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label5.Size = new System.Drawing.Size(121, 15);
            this.label5.TabIndex = 98;
            this.label5.Text = "Project Manager :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TreeViewToolTip
            // 
            this.TreeViewToolTip.AutoPopDelay = 4000;
            this.TreeViewToolTip.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.TreeViewToolTip.InitialDelay = 500;
            this.TreeViewToolTip.ReshowDelay = 100;
            // 
            // savbttn
            // 
            this.savbttn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.savbttn.BackColor = System.Drawing.Color.Transparent;
            this.savbttn.Enabled = false;
            this.savbttn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.savbttn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.savbttn.ForeColor = System.Drawing.Color.White;
            this.savbttn.Image = ((System.Drawing.Image)(resources.GetObject("savbttn.Image")));
            this.savbttn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.savbttn.Location = new System.Drawing.Point(491, 656);
            this.savbttn.Name = "savbttn";
            this.savbttn.Size = new System.Drawing.Size(78, 27);
            this.savbttn.TabIndex = 14;
            this.savbttn.Text = "Save";
            this.savbttn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.savbttn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.TreeViewToolTip.SetToolTip(this.savbttn, "Save Invoice Details");
            this.savbttn.UseVisualStyleBackColor = false;
            this.savbttn.Visible = false;
            this.savbttn.Click += new System.EventHandler(this.savbttn_Click);
            // 
            // editbttn
            // 
            this.editbttn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.editbttn.BackColor = System.Drawing.Color.Transparent;
            this.editbttn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.editbttn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editbttn.ForeColor = System.Drawing.Color.White;
            this.editbttn.Image = ((System.Drawing.Image)(resources.GetObject("editbttn.Image")));
            this.editbttn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.editbttn.Location = new System.Drawing.Point(388, 656);
            this.editbttn.Name = "editbttn";
            this.editbttn.Size = new System.Drawing.Size(78, 27);
            this.editbttn.TabIndex = 13;
            this.editbttn.Text = "Edit";
            this.editbttn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.TreeViewToolTip.SetToolTip(this.editbttn, "Edit Invoice Details");
            this.editbttn.UseVisualStyleBackColor = false;
            this.editbttn.Click += new System.EventHandler(this.editbttn_Click);
            // 
            // descriptiontxtbox
            // 
            this.descriptiontxtbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.descriptiontxtbox.BackColor = System.Drawing.Color.White;
            this.descriptiontxtbox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.descriptiontxtbox.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.descriptiontxtbox.Location = new System.Drawing.Point(9, 243);
            this.descriptiontxtbox.MinimumSize = new System.Drawing.Size(180, 20);
            this.descriptiontxtbox.Multiline = true;
            this.descriptiontxtbox.Name = "descriptiontxtbox";
            this.descriptiontxtbox.ReadOnly = true;
            this.descriptiontxtbox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.descriptiontxtbox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.descriptiontxtbox.Size = new System.Drawing.Size(988, 266);
            this.descriptiontxtbox.TabIndex = 101;
            this.TreeViewToolTip.SetToolTip(this.descriptiontxtbox, "Notes or comments for invoice");
            // 
            // requestedbycombobox
            // 
            this.requestedbycombobox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.requestedbycombobox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.requestedbycombobox.BackColor = System.Drawing.SystemColors.Window;
            this.requestedbycombobox.DropDownWidth = 150;
            this.requestedbycombobox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.requestedbycombobox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.requestedbycombobox.FormattingEnabled = true;
            this.requestedbycombobox.ItemHeight = 13;
            this.requestedbycombobox.Location = new System.Drawing.Point(769, 87);
            this.requestedbycombobox.Name = "requestedbycombobox";
            this.requestedbycombobox.Size = new System.Drawing.Size(202, 21);
            this.requestedbycombobox.TabIndex = 100;
            this.TreeViewToolTip.SetToolTip(this.requestedbycombobox, "Requested by");
            // 
            // jobtxt
            // 
            this.jobtxt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.jobtxt.BackColor = System.Drawing.Color.White;
            this.jobtxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.jobtxt.Location = new System.Drawing.Point(84, 20);
            this.jobtxt.MaximumSize = new System.Drawing.Size(200, 30);
            this.jobtxt.MaxLength = 5;
            this.jobtxt.Name = "jobtxt";
            this.jobtxt.ReadOnly = true;
            this.jobtxt.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.jobtxt.Size = new System.Drawing.Size(108, 22);
            this.jobtxt.TabIndex = 102;
            this.TreeViewToolTip.SetToolTip(this.jobtxt, "Invoice Number");
            this.jobtxt.TextChanged += new System.EventHandler(this.jobtxt_TextChanged);
            this.jobtxt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.jobtxt_KeyPress);
            // 
            // satxt
            // 
            this.satxt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.satxt.BackColor = System.Drawing.Color.White;
            this.satxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.satxt.Location = new System.Drawing.Point(84, 57);
            this.satxt.MaximumSize = new System.Drawing.Size(200, 30);
            this.satxt.MaxLength = 6;
            this.satxt.Name = "satxt";
            this.satxt.ReadOnly = true;
            this.satxt.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.satxt.Size = new System.Drawing.Size(108, 22);
            this.satxt.TabIndex = 103;
            this.TreeViewToolTip.SetToolTip(this.satxt, "Invoice Number");
            this.satxt.TextChanged += new System.EventHandler(this.satxt_TextChanged);
            this.satxt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.satxt_KeyPress);
            // 
            // partnotxt
            // 
            this.partnotxt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.partnotxt.BackColor = System.Drawing.Color.White;
            this.partnotxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.partnotxt.Location = new System.Drawing.Point(84, 93);
            this.partnotxt.MaximumSize = new System.Drawing.Size(300, 30);
            this.partnotxt.Name = "partnotxt";
            this.partnotxt.ReadOnly = true;
            this.partnotxt.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.partnotxt.Size = new System.Drawing.Size(300, 22);
            this.partnotxt.TabIndex = 104;
            this.TreeViewToolTip.SetToolTip(this.partnotxt, "Invoice Number");
            // 
            // SPM
            // 
            this.SPM.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.SPM.BackColor = System.Drawing.Color.Transparent;
            this.SPM.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SPM.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.SPM.Image = ((System.Drawing.Image)(resources.GetObject("SPM.Image")));
            this.SPM.Location = new System.Drawing.Point(409, 2);
            this.SPM.MaximumSize = new System.Drawing.Size(288, 100);
            this.SPM.MinimumSize = new System.Drawing.Size(100, 100);
            this.SPM.Name = "SPM";
            this.SPM.Size = new System.Drawing.Size(211, 100);
            this.SPM.TabIndex = 90;
            this.SPM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // commentslbl
            // 
            this.commentslbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.commentslbl.AutoSize = true;
            this.commentslbl.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.commentslbl.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.commentslbl.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.commentslbl.Location = new System.Drawing.Point(12, 512);
            this.commentslbl.Name = "commentslbl";
            this.commentslbl.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.commentslbl.Size = new System.Drawing.Size(105, 16);
            this.commentslbl.TabIndex = 100;
            this.commentslbl.Text = "Comments/Notes :";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(12, 223);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label1.Size = new System.Drawing.Size(76, 16);
            this.label1.TabIndex = 102;
            this.label1.Text = "Description :";
            // 
            // iteminfogroupBox
            // 
            this.iteminfogroupBox.Controls.Add(this.subassylbl);
            this.iteminfogroupBox.Controls.Add(this.jobnamelbl);
            this.iteminfogroupBox.Controls.Add(this.label7);
            this.iteminfogroupBox.Controls.Add(this.label6);
            this.iteminfogroupBox.Controls.Add(this.label4);
            this.iteminfogroupBox.Controls.Add(this.partnotxt);
            this.iteminfogroupBox.Controls.Add(this.satxt);
            this.iteminfogroupBox.Controls.Add(this.jobtxt);
            this.iteminfogroupBox.Controls.Add(this.requestedbycombobox);
            this.iteminfogroupBox.Controls.Add(this.label2);
            this.iteminfogroupBox.Controls.Add(this.departmentcomboBox);
            this.iteminfogroupBox.Controls.Add(this.projectmanagercombobox);
            this.iteminfogroupBox.Controls.Add(this.label5);
            this.iteminfogroupBox.Controls.Add(this.label3);
            this.iteminfogroupBox.Enabled = false;
            this.iteminfogroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iteminfogroupBox.ForeColor = System.Drawing.Color.White;
            this.iteminfogroupBox.Location = new System.Drawing.Point(9, 93);
            this.iteminfogroupBox.Name = "iteminfogroupBox";
            this.iteminfogroupBox.Size = new System.Drawing.Size(988, 125);
            this.iteminfogroupBox.TabIndex = 103;
            this.iteminfogroupBox.TabStop = false;
            this.iteminfogroupBox.Text = "ECR Info";
            // 
            // subassylbl
            // 
            this.subassylbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.subassylbl.AutoSize = true;
            this.subassylbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.subassylbl.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.subassylbl.Location = new System.Drawing.Point(196, 60);
            this.subassylbl.Name = "subassylbl";
            this.subassylbl.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.subassylbl.Size = new System.Drawing.Size(114, 15);
            this.subassylbl.TabIndex = 109;
            this.subassylbl.Text = "Sub Assy Name :";
            this.subassylbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // jobnamelbl
            // 
            this.jobnamelbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.jobnamelbl.AutoSize = true;
            this.jobnamelbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.jobnamelbl.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.jobnamelbl.Location = new System.Drawing.Point(196, 23);
            this.jobnamelbl.Name = "jobnamelbl";
            this.jobnamelbl.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.jobnamelbl.Size = new System.Drawing.Size(80, 15);
            this.jobnamelbl.TabIndex = 108;
            this.jobnamelbl.Text = "Job Name :";
            this.jobnamelbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label7.Location = new System.Drawing.Point(5, 93);
            this.label7.Name = "label7";
            this.label7.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label7.Size = new System.Drawing.Size(63, 15);
            this.label7.TabIndex = 107;
            this.label7.Text = "Part No :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label6.Location = new System.Drawing.Point(6, 60);
            this.label6.Name = "label6";
            this.label6.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label6.Size = new System.Drawing.Size(72, 15);
            this.label6.TabIndex = 106;
            this.label6.Text = "Sub Assy :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label4.Location = new System.Drawing.Point(6, 24);
            this.label4.Name = "label4";
            this.label4.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label4.Size = new System.Drawing.Size(60, 15);
            this.label4.TabIndex = 105;
            this.label4.Text = "Job No :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(643, 89);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label2.Size = new System.Drawing.Size(103, 15);
            this.label2.TabIndex = 101;
            this.label2.Text = "Requested By :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // statusStrip2
            // 
            this.statusStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Createdon,
            this.CreatedBy,
            this.lastsavedon,
            this.lastsavedby,
            this.toolStripSplitButton1});
            this.statusStrip2.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.statusStrip2.Location = new System.Drawing.Point(0, 689);
            this.statusStrip2.Name = "statusStrip2";
            this.statusStrip2.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip2.Size = new System.Drawing.Size(1009, 22);
            this.statusStrip2.TabIndex = 104;
            this.statusStrip2.Text = "statusStrip2";
            // 
            // Createdon
            // 
            this.Createdon.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Createdon.Name = "Createdon";
            this.Createdon.Size = new System.Drawing.Size(66, 13);
            this.Createdon.Text = "Created On";
            this.Createdon.ToolTipText = "Created On";
            // 
            // CreatedBy
            // 
            this.CreatedBy.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CreatedBy.Name = "CreatedBy";
            this.CreatedBy.Size = new System.Drawing.Size(61, 13);
            this.CreatedBy.Text = "Created By";
            this.CreatedBy.ToolTipText = "Created By";
            // 
            // lastsavedon
            // 
            this.lastsavedon.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lastsavedon.Name = "lastsavedon";
            this.lastsavedon.Size = new System.Drawing.Size(79, 13);
            this.lastsavedon.Text = "Last Saved On";
            this.lastsavedon.ToolTipText = "Last Saved On";
            // 
            // lastsavedby
            // 
            this.lastsavedby.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lastsavedby.Name = "lastsavedby";
            this.lastsavedby.Size = new System.Drawing.Size(74, 13);
            this.lastsavedby.Text = "Last Saved By";
            this.lastsavedby.ToolTipText = "Last Saved By";
            // 
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButton1.Image")));
            this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(64, 20);
            this.toolStripSplitButton1.Text = "Print";
            this.toolStripSplitButton1.ToolTipText = "Print Invoice";
            // 
            // submissiongroupBox
            // 
            this.submissiongroupBox.Controls.Add(this.submitecrhandlercheckBox);
            this.submissiongroupBox.Controls.Add(this.ecrhandlercheckBox);
            this.submissiongroupBox.Controls.Add(this.managercheckBox);
            this.submissiongroupBox.Controls.Add(this.supcheckBox);
            this.submissiongroupBox.Enabled = false;
            this.submissiongroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.submissiongroupBox.ForeColor = System.Drawing.Color.White;
            this.submissiongroupBox.Location = new System.Drawing.Point(675, 515);
            this.submissiongroupBox.Name = "submissiongroupBox";
            this.submissiongroupBox.Size = new System.Drawing.Size(322, 135);
            this.submissiongroupBox.TabIndex = 105;
            this.submissiongroupBox.TabStop = false;
            this.submissiongroupBox.Text = "Submission Logs";
            // 
            // ecrhandlercheckBox
            // 
            this.ecrhandlercheckBox.AutoSize = true;
            this.ecrhandlercheckBox.Location = new System.Drawing.Point(10, 104);
            this.ecrhandlercheckBox.Name = "ecrhandlercheckBox";
            this.ecrhandlercheckBox.Size = new System.Drawing.Size(170, 20);
            this.ecrhandlercheckBox.TabIndex = 2;
            this.ecrhandlercheckBox.Text = "ECR Request Complete";
            this.ecrhandlercheckBox.UseVisualStyleBackColor = true;
            this.ecrhandlercheckBox.Click += new System.EventHandler(this.ecrhandlercheckBox_Click);
            // 
            // managercheckBox
            // 
            this.managercheckBox.AutoSize = true;
            this.managercheckBox.Location = new System.Drawing.Point(10, 52);
            this.managercheckBox.Name = "managercheckBox";
            this.managercheckBox.Size = new System.Drawing.Size(170, 20);
            this.managercheckBox.TabIndex = 1;
            this.managercheckBox.Text = "Submit to ECR Manager";
            this.managercheckBox.UseVisualStyleBackColor = true;
            this.managercheckBox.Click += new System.EventHandler(this.managercheckBox_Click);
            // 
            // supcheckBox
            // 
            this.supcheckBox.AutoSize = true;
            this.supcheckBox.Location = new System.Drawing.Point(10, 26);
            this.supcheckBox.Name = "supcheckBox";
            this.supcheckBox.Size = new System.Drawing.Size(150, 20);
            this.supcheckBox.TabIndex = 0;
            this.supcheckBox.Text = "Submit to Supervisor";
            this.supcheckBox.UseVisualStyleBackColor = true;
            this.supcheckBox.Click += new System.EventHandler(this.supcheckBox_Click);
            // 
            // submitecrhandlercheckBox
            // 
            this.submitecrhandlercheckBox.AutoSize = true;
            this.submitecrhandlercheckBox.Location = new System.Drawing.Point(10, 78);
            this.submitecrhandlercheckBox.Name = "submitecrhandlercheckBox";
            this.submitecrhandlercheckBox.Size = new System.Drawing.Size(164, 20);
            this.submitecrhandlercheckBox.TabIndex = 3;
            this.submitecrhandlercheckBox.Text = "Submit to ECR Handler";
            this.submitecrhandlercheckBox.UseVisualStyleBackColor = true;
            this.submitecrhandlercheckBox.Click += new System.EventHandler(this.submitecrhandlercheckBox_Click);
            // 
            // ECRDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(69)))), ((int)(((byte)(76)))));
            this.ClientSize = new System.Drawing.Size(1009, 711);
            this.Controls.Add(this.submissiongroupBox);
            this.Controls.Add(this.statusStrip2);
            this.Controls.Add(this.ecrnotxtbox);
            this.Controls.Add(this.invoicelbl);
            this.Controls.Add(this.iteminfogroupBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.descriptiontxtbox);
            this.Controls.Add(this.savbttn);
            this.Controls.Add(this.editbttn);
            this.Controls.Add(this.SPM);
            this.Controls.Add(this.notestxt);
            this.Controls.Add(this.commentslbl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(1025, 2000);
            this.MinimumSize = new System.Drawing.Size(1025, 750);
            this.Name = "ECRDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ECR Details";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.QuoteDetails_FormClosing);
            this.Load += new System.EventHandler(this.ECRDetails_Load);
            this.iteminfogroupBox.ResumeLayout(false);
            this.iteminfogroupBox.PerformLayout();
            this.statusStrip2.ResumeLayout(false);
            this.statusStrip2.PerformLayout();
            this.submissiongroupBox.ResumeLayout(false);
            this.submissiongroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox notestxt;
        private System.Windows.Forms.Label SPM;
        private System.Windows.Forms.Label invoicelbl;
        private System.Windows.Forms.TextBox ecrnotxtbox;
        private System.Windows.Forms.ComboBox projectmanagercombobox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button savbttn;
        private System.Windows.Forms.Button editbttn;
        private System.Windows.Forms.ToolTip TreeViewToolTip;
        private System.Windows.Forms.ComboBox departmentcomboBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox descriptiontxtbox;
        private System.Windows.Forms.Label commentslbl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox iteminfogroupBox;
        private System.Windows.Forms.StatusStrip statusStrip2;
        private System.Windows.Forms.ToolStripStatusLabel Createdon;
        private System.Windows.Forms.ToolStripStatusLabel CreatedBy;
        private System.Windows.Forms.ToolStripStatusLabel lastsavedon;
        private System.Windows.Forms.ToolStripStatusLabel lastsavedby;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton1;
        private System.Windows.Forms.ComboBox requestedbycombobox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox partnotxt;
        private System.Windows.Forms.TextBox satxt;
        private System.Windows.Forms.TextBox jobtxt;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label subassylbl;
        private System.Windows.Forms.Label jobnamelbl;
        private System.Windows.Forms.GroupBox submissiongroupBox;
        private System.Windows.Forms.CheckBox ecrhandlercheckBox;
        private System.Windows.Forms.CheckBox managercheckBox;
        private System.Windows.Forms.CheckBox supcheckBox;
        private System.Windows.Forms.CheckBox submitecrhandlercheckBox;
    }
}