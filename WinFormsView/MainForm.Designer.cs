namespace WinFormsView
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.MachinesPage = new System.Windows.Forms.TabPage();
            this.addTechButton = new System.Windows.Forms.Button();
            this.techDataGridView = new System.Windows.Forms.DataGridView();
            this.MaintanancePage = new System.Windows.Forms.TabPage();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.maintenanceDataGridView = new System.Windows.Forms.DataGridView();
            this.SupplaersPage = new System.Windows.Forms.TabPage();
            this.addButton = new System.Windows.Forms.Button();
            this.supDataGridView = new System.Windows.Forms.DataGridView();
            this.ErrorPage = new System.Windows.Forms.TabPage();
            this.addErrorButton = new System.Windows.Forms.Button();
            this.errorGridView = new System.Windows.Forms.DataGridView();
            this.MaterialPage = new System.Windows.Forms.TabPage();
            this.materialDataGridView = new System.Windows.Forms.DataGridView();
            this.mainTabControl.SuspendLayout();
            this.MachinesPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.techDataGridView)).BeginInit();
            this.MaintanancePage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maintenanceDataGridView)).BeginInit();
            this.SupplaersPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.supDataGridView)).BeginInit();
            this.ErrorPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorGridView)).BeginInit();
            this.MaterialPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.materialDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // mainTabControl
            // 
            this.mainTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainTabControl.Controls.Add(this.MachinesPage);
            this.mainTabControl.Controls.Add(this.MaintanancePage);
            this.mainTabControl.Controls.Add(this.SupplaersPage);
            this.mainTabControl.Controls.Add(this.ErrorPage);
            this.mainTabControl.Controls.Add(this.MaterialPage);
            this.mainTabControl.Location = new System.Drawing.Point(12, 12);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(1093, 426);
            this.mainTabControl.TabIndex = 0;
            // 
            // MachinesPage
            // 
            this.MachinesPage.Controls.Add(this.addTechButton);
            this.MachinesPage.Controls.Add(this.techDataGridView);
            this.MachinesPage.Location = new System.Drawing.Point(4, 29);
            this.MachinesPage.Name = "MachinesPage";
            this.MachinesPage.Padding = new System.Windows.Forms.Padding(3);
            this.MachinesPage.Size = new System.Drawing.Size(1085, 393);
            this.MachinesPage.TabIndex = 0;
            this.MachinesPage.Text = "Оборудование";
            this.MachinesPage.UseVisualStyleBackColor = true;
            // 
            // addTechButton
            // 
            this.addTechButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.addTechButton.Location = new System.Drawing.Point(516, 354);
            this.addTechButton.Name = "addTechButton";
            this.addTechButton.Size = new System.Drawing.Size(94, 29);
            this.addTechButton.TabIndex = 1;
            this.addTechButton.Text = "Добавить";
            this.addTechButton.UseVisualStyleBackColor = true;
            this.addTechButton.Click += new System.EventHandler(this.addTechButton_Click);
            // 
            // techDataGridView
            // 
            this.techDataGridView.AllowUserToAddRows = false;
            this.techDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.techDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.techDataGridView.Dock = System.Windows.Forms.DockStyle.Top;
            this.techDataGridView.Location = new System.Drawing.Point(3, 3);
            this.techDataGridView.Name = "techDataGridView";
            this.techDataGridView.ReadOnly = true;
            this.techDataGridView.RowHeadersVisible = false;
            this.techDataGridView.RowHeadersWidth = 51;
            this.techDataGridView.RowTemplate.Height = 29;
            this.techDataGridView.Size = new System.Drawing.Size(1079, 348);
            this.techDataGridView.TabIndex = 0;
            this.techDataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.techDataGridView_CellDoubleClick);
            // 
            // MaintanancePage
            // 
            this.MaintanancePage.Controls.Add(this.dateTimePicker1);
            this.MaintanancePage.Controls.Add(this.maintenanceDataGridView);
            this.MaintanancePage.Location = new System.Drawing.Point(4, 29);
            this.MaintanancePage.Name = "MaintanancePage";
            this.MaintanancePage.Padding = new System.Windows.Forms.Padding(3);
            this.MaintanancePage.Size = new System.Drawing.Size(1085, 393);
            this.MaintanancePage.TabIndex = 1;
            this.MaintanancePage.Text = "Обслуживание";
            this.MaintanancePage.UseVisualStyleBackColor = true;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dateTimePicker1.Location = new System.Drawing.Point(435, 360);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(250, 27);
            this.dateTimePicker1.TabIndex = 2;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // maintenanceDataGridView
            // 
            this.maintenanceDataGridView.AllowUserToAddRows = false;
            this.maintenanceDataGridView.AllowUserToDeleteRows = false;
            this.maintenanceDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.maintenanceDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.maintenanceDataGridView.Dock = System.Windows.Forms.DockStyle.Top;
            this.maintenanceDataGridView.Location = new System.Drawing.Point(3, 3);
            this.maintenanceDataGridView.Name = "maintenanceDataGridView";
            this.maintenanceDataGridView.ReadOnly = true;
            this.maintenanceDataGridView.RowHeadersVisible = false;
            this.maintenanceDataGridView.RowHeadersWidth = 51;
            this.maintenanceDataGridView.RowTemplate.Height = 29;
            this.maintenanceDataGridView.Size = new System.Drawing.Size(1079, 351);
            this.maintenanceDataGridView.TabIndex = 0;
            this.maintenanceDataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.maintenanceDataGridView_CellDoubleClick);
            // 
            // SupplaersPage
            // 
            this.SupplaersPage.Controls.Add(this.addButton);
            this.SupplaersPage.Controls.Add(this.supDataGridView);
            this.SupplaersPage.Location = new System.Drawing.Point(4, 29);
            this.SupplaersPage.Name = "SupplaersPage";
            this.SupplaersPage.Size = new System.Drawing.Size(1085, 393);
            this.SupplaersPage.TabIndex = 3;
            this.SupplaersPage.Text = "Поставщики";
            this.SupplaersPage.UseVisualStyleBackColor = true;
            // 
            // addButton
            // 
            this.addButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.addButton.Location = new System.Drawing.Point(512, 361);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(94, 29);
            this.addButton.TabIndex = 1;
            this.addButton.Text = "Добавить";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // supDataGridView
            // 
            this.supDataGridView.AllowUserToAddRows = false;
            this.supDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.supDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.supDataGridView.Dock = System.Windows.Forms.DockStyle.Top;
            this.supDataGridView.Location = new System.Drawing.Point(0, 0);
            this.supDataGridView.Name = "supDataGridView";
            this.supDataGridView.ReadOnly = true;
            this.supDataGridView.RowHeadersVisible = false;
            this.supDataGridView.RowHeadersWidth = 51;
            this.supDataGridView.RowTemplate.Height = 29;
            this.supDataGridView.Size = new System.Drawing.Size(1085, 355);
            this.supDataGridView.TabIndex = 0;
            this.supDataGridView.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.supDataGridView_CellMouseDoubleClick);
            // 
            // ErrorPage
            // 
            this.ErrorPage.Controls.Add(this.addErrorButton);
            this.ErrorPage.Controls.Add(this.errorGridView);
            this.ErrorPage.Location = new System.Drawing.Point(4, 29);
            this.ErrorPage.Name = "ErrorPage";
            this.ErrorPage.Size = new System.Drawing.Size(1085, 393);
            this.ErrorPage.TabIndex = 2;
            this.ErrorPage.Text = "Ошибки";
            this.ErrorPage.UseVisualStyleBackColor = true;
            // 
            // addErrorButton
            // 
            this.addErrorButton.Location = new System.Drawing.Point(499, 361);
            this.addErrorButton.Name = "addErrorButton";
            this.addErrorButton.Size = new System.Drawing.Size(94, 29);
            this.addErrorButton.TabIndex = 1;
            this.addErrorButton.Text = "Добавить";
            this.addErrorButton.UseVisualStyleBackColor = true;
            this.addErrorButton.Click += new System.EventHandler(this.addErrorButton_Click);
            // 
            // errorGridView
            // 
            this.errorGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.errorGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.errorGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.errorGridView.Location = new System.Drawing.Point(3, 3);
            this.errorGridView.Name = "errorGridView";
            this.errorGridView.RowHeadersVisible = false;
            this.errorGridView.RowHeadersWidth = 51;
            this.errorGridView.RowTemplate.Height = 29;
            this.errorGridView.Size = new System.Drawing.Size(1079, 356);
            this.errorGridView.TabIndex = 0;
            this.errorGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.errorGridView_CellDoubleClick);
            // 
            // MaterialPage
            // 
            this.MaterialPage.Controls.Add(this.materialDataGridView);
            this.MaterialPage.Location = new System.Drawing.Point(4, 29);
            this.MaterialPage.Name = "MaterialPage";
            this.MaterialPage.Padding = new System.Windows.Forms.Padding(3);
            this.MaterialPage.Size = new System.Drawing.Size(1085, 393);
            this.MaterialPage.TabIndex = 4;
            this.MaterialPage.Text = "Материалы";
            this.MaterialPage.UseVisualStyleBackColor = true;
            // 
            // materialDataGridView
            // 
            this.materialDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.materialDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.materialDataGridView.Location = new System.Drawing.Point(0, 0);
            this.materialDataGridView.Name = "materialDataGridView";
            this.materialDataGridView.RowHeadersVisible = false;
            this.materialDataGridView.RowHeadersWidth = 51;
            this.materialDataGridView.RowTemplate.Height = 29;
            this.materialDataGridView.Size = new System.Drawing.Size(1085, 393);
            this.materialDataGridView.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1117, 450);
            this.Controls.Add(this.mainTabControl);
            this.Name = "MainForm";
            this.Text = "ОбслуживаниеПриложение";
            this.mainTabControl.ResumeLayout(false);
            this.MachinesPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.techDataGridView)).EndInit();
            this.MaintanancePage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.maintenanceDataGridView)).EndInit();
            this.SupplaersPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.supDataGridView)).EndInit();
            this.ErrorPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorGridView)).EndInit();
            this.MaterialPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.materialDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private TabControl mainTabControl;
        private TabPage MachinesPage;
        private DataGridView techDataGridView;
        private TabPage MaintanancePage;
        private Button addTechButton;
        private TabPage ErrorPage;
        private TabPage SupplaersPage;
        private Button addButton;
        private DataGridView supDataGridView;
        private DateTimePicker dateTimePicker1;
        private DataGridView maintenanceDataGridView;
        private DataGridView errorGridView;
        private Button addErrorButton;
        private TabPage MaterialPage;
        private DataGridView materialDataGridView;
    }
}