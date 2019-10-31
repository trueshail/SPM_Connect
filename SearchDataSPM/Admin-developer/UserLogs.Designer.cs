namespace SearchDataSPM.Admin_developer
{
    partial class UserLogs
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserLogs));
            this.advancedDataGridView1 = new ADGV.AdvancedDataGridView();
            this.dateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.loggerFrmNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctrlNameThreadDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.eventLevelDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.messageValueDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.exceptionUserDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.unionLogsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.sPM_DatabaseDataSet1 = new SearchDataSPM.SPM_DatabaseDataSet1();
            this.unionLogsTableAdapter = new SearchDataSPM.SPM_DatabaseDataSet1TableAdapters.UnionLogsTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.advancedDataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.unionLogsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sPM_DatabaseDataSet1)).BeginInit();
            this.SuspendLayout();
            // 
            // advancedDataGridView1
            // 
            this.advancedDataGridView1.AllowUserToAddRows = false;
            this.advancedDataGridView1.AllowUserToDeleteRows = false;
            this.advancedDataGridView1.AutoGenerateColumns = false;
            this.advancedDataGridView1.AutoGenerateContextFilters = true;
            this.advancedDataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.advancedDataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dateDataGridViewTextBoxColumn,
            this.loggerFrmNameDataGridViewTextBoxColumn,
            this.ctrlNameThreadDataGridViewTextBoxColumn,
            this.eventLevelDataGridViewTextBoxColumn,
            this.messageValueDataGridViewTextBoxColumn,
            this.exceptionUserDataGridViewTextBoxColumn});
            this.advancedDataGridView1.DataSource = this.unionLogsBindingSource;
            this.advancedDataGridView1.DateWithTime = false;
            this.advancedDataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.advancedDataGridView1.Location = new System.Drawing.Point(0, 0);
            this.advancedDataGridView1.Name = "advancedDataGridView1";
            this.advancedDataGridView1.ReadOnly = true;
            this.advancedDataGridView1.RowHeadersVisible = false;
            this.advancedDataGridView1.Size = new System.Drawing.Size(801, 450);
            this.advancedDataGridView1.TabIndex = 0;
            this.advancedDataGridView1.TimeFilter = false;
            this.advancedDataGridView1.SortStringChanged += new System.EventHandler(this.advancedDataGridView_SortStringChanged);
            this.advancedDataGridView1.FilterStringChanged += new System.EventHandler(this.advancedDataGridView_FilterStringChanged);
            // 
            // dateDataGridViewTextBoxColumn
            // 
            this.dateDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dateDataGridViewTextBoxColumn.DataPropertyName = "Date";
            this.dateDataGridViewTextBoxColumn.HeaderText = "Date";
            this.dateDataGridViewTextBoxColumn.MinimumWidth = 22;
            this.dateDataGridViewTextBoxColumn.Name = "dateDataGridViewTextBoxColumn";
            this.dateDataGridViewTextBoxColumn.ReadOnly = true;
            this.dateDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // loggerFrmNameDataGridViewTextBoxColumn
            // 
            this.loggerFrmNameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.loggerFrmNameDataGridViewTextBoxColumn.DataPropertyName = "loggerFrmName";
            this.loggerFrmNameDataGridViewTextBoxColumn.HeaderText = "loggerFrmName";
            this.loggerFrmNameDataGridViewTextBoxColumn.MinimumWidth = 22;
            this.loggerFrmNameDataGridViewTextBoxColumn.Name = "loggerFrmNameDataGridViewTextBoxColumn";
            this.loggerFrmNameDataGridViewTextBoxColumn.ReadOnly = true;
            this.loggerFrmNameDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // ctrlNameThreadDataGridViewTextBoxColumn
            // 
            this.ctrlNameThreadDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ctrlNameThreadDataGridViewTextBoxColumn.DataPropertyName = "ctrlNameThread";
            this.ctrlNameThreadDataGridViewTextBoxColumn.HeaderText = "ctrlNameThread";
            this.ctrlNameThreadDataGridViewTextBoxColumn.MinimumWidth = 22;
            this.ctrlNameThreadDataGridViewTextBoxColumn.Name = "ctrlNameThreadDataGridViewTextBoxColumn";
            this.ctrlNameThreadDataGridViewTextBoxColumn.ReadOnly = true;
            this.ctrlNameThreadDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // eventLevelDataGridViewTextBoxColumn
            // 
            this.eventLevelDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.eventLevelDataGridViewTextBoxColumn.DataPropertyName = "EventLevel";
            this.eventLevelDataGridViewTextBoxColumn.HeaderText = "EventLevel";
            this.eventLevelDataGridViewTextBoxColumn.MinimumWidth = 22;
            this.eventLevelDataGridViewTextBoxColumn.Name = "eventLevelDataGridViewTextBoxColumn";
            this.eventLevelDataGridViewTextBoxColumn.ReadOnly = true;
            this.eventLevelDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // messageValueDataGridViewTextBoxColumn
            // 
            this.messageValueDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.messageValueDataGridViewTextBoxColumn.DataPropertyName = "MessageValue";
            this.messageValueDataGridViewTextBoxColumn.HeaderText = "MessageValue";
            this.messageValueDataGridViewTextBoxColumn.MinimumWidth = 22;
            this.messageValueDataGridViewTextBoxColumn.Name = "messageValueDataGridViewTextBoxColumn";
            this.messageValueDataGridViewTextBoxColumn.ReadOnly = true;
            this.messageValueDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // exceptionUserDataGridViewTextBoxColumn
            // 
            this.exceptionUserDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.exceptionUserDataGridViewTextBoxColumn.DataPropertyName = "ExceptionUser";
            this.exceptionUserDataGridViewTextBoxColumn.HeaderText = "ExceptionUser";
            this.exceptionUserDataGridViewTextBoxColumn.MinimumWidth = 22;
            this.exceptionUserDataGridViewTextBoxColumn.Name = "exceptionUserDataGridViewTextBoxColumn";
            this.exceptionUserDataGridViewTextBoxColumn.ReadOnly = true;
            this.exceptionUserDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // unionLogsBindingSource
            // 
            this.unionLogsBindingSource.DataMember = "UnionLogs";
            this.unionLogsBindingSource.DataSource = this.sPM_DatabaseDataSet1;
            // 
            // sPM_DatabaseDataSet1
            // 
            this.sPM_DatabaseDataSet1.DataSetName = "SPM_DatabaseDataSet1";
            this.sPM_DatabaseDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // unionLogsTableAdapter
            // 
            this.unionLogsTableAdapter.ClearBeforeFill = true;
            // 
            // UserLogs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(801, 450);
            this.Controls.Add(this.advancedDataGridView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "UserLogs";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SPM Connect User Actions Log";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.UserLogs_FormClosed);
            this.Load += new System.EventHandler(this.UserLogs_Load);
            ((System.ComponentModel.ISupportInitialize)(this.advancedDataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.unionLogsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sPM_DatabaseDataSet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ADGV.AdvancedDataGridView advancedDataGridView1;
        private SPM_DatabaseDataSet1 sPM_DatabaseDataSet1;
        private System.Windows.Forms.BindingSource unionLogsBindingSource;
        private SPM_DatabaseDataSet1TableAdapters.UnionLogsTableAdapter unionLogsTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn loggerFrmNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ctrlNameThreadDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn eventLevelDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn messageValueDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn exceptionUserDataGridViewTextBoxColumn;
    }
}