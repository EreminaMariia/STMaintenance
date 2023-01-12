namespace WinFormsView
{
    partial class AddPassportForm
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
            this.nameLabel = new System.Windows.Forms.Label();
            this.serialLabel = new System.Windows.Forms.Label();
            this.inventoryLabel = new System.Windows.Forms.Label();
            this.supplierLabel = new System.Windows.Forms.Label();
            this.operatingLabel = new System.Windows.Forms.Label();
            this.typeLabel = new System.Windows.Forms.Label();
            this.dapertmentLabel = new System.Windows.Forms.Label();
            this.maintananceLabel = new System.Windows.Forms.Label();
            this.errorLabel = new System.Windows.Forms.Label();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.serialTextBox = new System.Windows.Forms.TextBox();
            this.inventoryTextBox = new System.Windows.Forms.TextBox();
            this.supplierComboBox = new System.Windows.Forms.ComboBox();
            this.operatingTextBox = new System.Windows.Forms.TextBox();
            this.typeComboBox = new System.Windows.Forms.ComboBox();
            this.departmentComboBox = new System.Windows.Forms.ComboBox();
            this.mainLabel = new System.Windows.Forms.Label();
            this.maintananceDataGridView = new System.Windows.Forms.DataGridView();
            this.IdColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.errorDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.supplierButton = new System.Windows.Forms.Button();
            this.typeButton = new System.Windows.Forms.Button();
            this.maintananceButton = new System.Windows.Forms.Button();
            this.errorButton = new System.Windows.Forms.Button();
            this.commissioningDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.releaseDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.decommissioningDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.maintananceDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(37, 72);
            this.nameLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(125, 35);
            this.nameLabel.TabIndex = 0;
            this.nameLabel.Text = "Название";
            // 
            // serialLabel
            // 
            this.serialLabel.AutoSize = true;
            this.serialLabel.Location = new System.Drawing.Point(37, 123);
            this.serialLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.serialLabel.Name = "serialLabel";
            this.serialLabel.Size = new System.Drawing.Size(217, 35);
            this.serialLabel.TabIndex = 1;
            this.serialLabel.Text = "Серийный номер";
            // 
            // inventoryLabel
            // 
            this.inventoryLabel.AutoSize = true;
            this.inventoryLabel.Location = new System.Drawing.Point(713, 123);
            this.inventoryLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.inventoryLabel.Name = "inventoryLabel";
            this.inventoryLabel.Size = new System.Drawing.Size(255, 35);
            this.inventoryLabel.TabIndex = 2;
            this.inventoryLabel.Text = "Инвентарный номер";
            // 
            // supplierLabel
            // 
            this.supplierLabel.AutoSize = true;
            this.supplierLabel.Location = new System.Drawing.Point(713, 387);
            this.supplierLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.supplierLabel.Name = "supplierLabel";
            this.supplierLabel.Size = new System.Drawing.Size(144, 35);
            this.supplierLabel.TabIndex = 3;
            this.supplierLabel.Text = "Поставщик";
            // 
            // operatingLabel
            // 
            this.operatingLabel.AutoSize = true;
            this.operatingLabel.Location = new System.Drawing.Point(37, 275);
            this.operatingLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.operatingLabel.Name = "operatingLabel";
            this.operatingLabel.Size = new System.Drawing.Size(167, 35);
            this.operatingLabel.TabIndex = 4;
            this.operatingLabel.Text = "Часы работы";
            // 
            // typeLabel
            // 
            this.typeLabel.AutoSize = true;
            this.typeLabel.Location = new System.Drawing.Point(37, 326);
            this.typeLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.typeLabel.Name = "typeLabel";
            this.typeLabel.Size = new System.Drawing.Size(232, 35);
            this.typeLabel.TabIndex = 5;
            this.typeLabel.Text = "Тип оборудования";
            // 
            // dapertmentLabel
            // 
            this.dapertmentLabel.AutoSize = true;
            this.dapertmentLabel.Location = new System.Drawing.Point(37, 379);
            this.dapertmentLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.dapertmentLabel.Name = "dapertmentLabel";
            this.dapertmentLabel.Size = new System.Drawing.Size(58, 35);
            this.dapertmentLabel.TabIndex = 6;
            this.dapertmentLabel.Text = "Цех";
            // 
            // maintananceLabel
            // 
            this.maintananceLabel.AutoSize = true;
            this.maintananceLabel.Location = new System.Drawing.Point(632, 439);
            this.maintananceLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.maintananceLabel.Name = "maintananceLabel";
            this.maintananceLabel.Size = new System.Drawing.Size(187, 35);
            this.maintananceLabel.TabIndex = 9;
            this.maintananceLabel.Text = "Обслуживание";
            // 
            // errorLabel
            // 
            this.errorLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.errorLabel.AutoSize = true;
            this.errorLabel.Location = new System.Drawing.Point(667, 691);
            this.errorLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size(110, 35);
            this.errorLabel.TabIndex = 10;
            this.errorLabel.Text = "Ошибки";
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(260, 69);
            this.nameTextBox.Margin = new System.Windows.Forms.Padding(5);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(764, 41);
            this.nameTextBox.TabIndex = 13;
            // 
            // serialTextBox
            // 
            this.serialTextBox.Location = new System.Drawing.Point(260, 120);
            this.serialTextBox.Margin = new System.Windows.Forms.Padding(5);
            this.serialTextBox.Name = "serialTextBox";
            this.serialTextBox.Size = new System.Drawing.Size(326, 41);
            this.serialTextBox.TabIndex = 14;
            // 
            // inventoryTextBox
            // 
            this.inventoryTextBox.Location = new System.Drawing.Point(978, 120);
            this.inventoryTextBox.Margin = new System.Windows.Forms.Padding(5);
            this.inventoryTextBox.Name = "inventoryTextBox";
            this.inventoryTextBox.Size = new System.Drawing.Size(326, 41);
            this.inventoryTextBox.TabIndex = 15;
            // 
            // supplierComboBox
            // 
            this.supplierComboBox.FormattingEnabled = true;
            this.supplierComboBox.Location = new System.Drawing.Point(869, 379);
            this.supplierComboBox.Margin = new System.Windows.Forms.Padding(5);
            this.supplierComboBox.Name = "supplierComboBox";
            this.supplierComboBox.Size = new System.Drawing.Size(435, 43);
            this.supplierComboBox.TabIndex = 16;
            // 
            // operatingTextBox
            // 
            this.operatingTextBox.Location = new System.Drawing.Point(260, 272);
            this.operatingTextBox.Margin = new System.Windows.Forms.Padding(5);
            this.operatingTextBox.Name = "operatingTextBox";
            this.operatingTextBox.Size = new System.Drawing.Size(764, 41);
            this.operatingTextBox.TabIndex = 17;
            // 
            // typeComboBox
            // 
            this.typeComboBox.FormattingEnabled = true;
            this.typeComboBox.Location = new System.Drawing.Point(260, 326);
            this.typeComboBox.Margin = new System.Windows.Forms.Padding(5);
            this.typeComboBox.Name = "typeComboBox";
            this.typeComboBox.Size = new System.Drawing.Size(764, 43);
            this.typeComboBox.TabIndex = 18;
            // 
            // departmentComboBox
            // 
            this.departmentComboBox.FormattingEnabled = true;
            this.departmentComboBox.Location = new System.Drawing.Point(260, 379);
            this.departmentComboBox.Margin = new System.Windows.Forms.Padding(5);
            this.departmentComboBox.Name = "departmentComboBox";
            this.departmentComboBox.Size = new System.Drawing.Size(435, 43);
            this.departmentComboBox.TabIndex = 19;
            // 
            // mainLabel
            // 
            this.mainLabel.AutoSize = true;
            this.mainLabel.Location = new System.Drawing.Point(632, 21);
            this.mainLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.mainLabel.Name = "mainLabel";
            this.mainLabel.Size = new System.Drawing.Size(145, 35);
            this.mainLabel.TabIndex = 22;
            this.mainLabel.Text = "Техпаспорт";
            // 
            // maintananceDataGridView
            // 
            this.maintananceDataGridView.AllowUserToAddRows = false;
            this.maintananceDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.maintananceDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.maintananceDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.maintananceDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IdColumn,
            this.NameColumn});
            this.maintananceDataGridView.Location = new System.Drawing.Point(335, 479);
            this.maintananceDataGridView.Margin = new System.Windows.Forms.Padding(5);
            this.maintananceDataGridView.Name = "maintananceDataGridView";
            this.maintananceDataGridView.ReadOnly = true;
            this.maintananceDataGridView.RowHeadersVisible = false;
            this.maintananceDataGridView.RowHeadersWidth = 51;
            this.maintananceDataGridView.RowTemplate.Height = 29;
            this.maintananceDataGridView.Size = new System.Drawing.Size(766, 207);
            this.maintananceDataGridView.TabIndex = 23;
            this.maintananceDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.maintananceDataGridView_CellContentClick);
            // 
            // IdColumn
            // 
            this.IdColumn.HeaderText = "Id";
            this.IdColumn.MinimumWidth = 6;
            this.IdColumn.Name = "IdColumn";
            this.IdColumn.ReadOnly = true;
            this.IdColumn.Visible = false;
            // 
            // NameColumn
            // 
            this.NameColumn.HeaderText = "Название";
            this.NameColumn.MinimumWidth = 6;
            this.NameColumn.Name = "NameColumn";
            this.NameColumn.ReadOnly = true;
            // 
            // errorDataGridView
            // 
            this.errorDataGridView.AllowUserToAddRows = false;
            this.errorDataGridView.AllowUserToDeleteRows = false;
            this.errorDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.errorDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.errorDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.errorDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.DateColumn});
            this.errorDataGridView.Location = new System.Drawing.Point(335, 731);
            this.errorDataGridView.Margin = new System.Windows.Forms.Padding(5);
            this.errorDataGridView.Name = "errorDataGridView";
            this.errorDataGridView.RowHeadersVisible = false;
            this.errorDataGridView.RowHeadersWidth = 51;
            this.errorDataGridView.RowTemplate.Height = 29;
            this.errorDataGridView.Size = new System.Drawing.Size(766, 201);
            this.errorDataGridView.TabIndex = 24;
            this.errorDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.errorDataGridView_CellContentClick);
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Id";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Visible = false;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Название";
            this.dataGridViewTextBoxColumn3.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // DateColumn
            // 
            this.DateColumn.HeaderText = "Исправлена";
            this.DateColumn.MinimumWidth = 6;
            this.DateColumn.Name = "DateColumn";
            // 
            // supplierButton
            // 
            this.supplierButton.Location = new System.Drawing.Point(1314, 379);
            this.supplierButton.Margin = new System.Windows.Forms.Padding(5);
            this.supplierButton.Name = "supplierButton";
            this.supplierButton.Size = new System.Drawing.Size(46, 43);
            this.supplierButton.TabIndex = 25;
            this.supplierButton.Text = "+";
            this.supplierButton.UseVisualStyleBackColor = true;
            this.supplierButton.Click += new System.EventHandler(this.supplierButton_Click);
            // 
            // typeButton
            // 
            this.typeButton.Location = new System.Drawing.Point(1034, 326);
            this.typeButton.Margin = new System.Windows.Forms.Padding(5);
            this.typeButton.Name = "typeButton";
            this.typeButton.Size = new System.Drawing.Size(45, 43);
            this.typeButton.TabIndex = 26;
            this.typeButton.Text = "+";
            this.typeButton.UseVisualStyleBackColor = true;
            this.typeButton.Click += new System.EventHandler(this.typeButton_Click);
            // 
            // maintananceButton
            // 
            this.maintananceButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.maintananceButton.Location = new System.Drawing.Point(1111, 479);
            this.maintananceButton.Margin = new System.Windows.Forms.Padding(5);
            this.maintananceButton.Name = "maintananceButton";
            this.maintananceButton.Size = new System.Drawing.Size(45, 45);
            this.maintananceButton.TabIndex = 30;
            this.maintananceButton.Text = "+";
            this.maintananceButton.UseVisualStyleBackColor = true;
            this.maintananceButton.Click += new System.EventHandler(this.maintananceButton_Click);
            // 
            // errorButton
            // 
            this.errorButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.errorButton.Location = new System.Drawing.Point(1111, 731);
            this.errorButton.Margin = new System.Windows.Forms.Padding(5);
            this.errorButton.Name = "errorButton";
            this.errorButton.Size = new System.Drawing.Size(54, 51);
            this.errorButton.TabIndex = 31;
            this.errorButton.Text = "+";
            this.errorButton.UseVisualStyleBackColor = true;
            this.errorButton.Click += new System.EventHandler(this.errorButton_Click);
            // 
            // commissioningDateTimePicker
            // 
            this.commissioningDateTimePicker.Location = new System.Drawing.Point(374, 170);
            this.commissioningDateTimePicker.Name = "commissioningDateTimePicker";
            this.commissioningDateTimePicker.Size = new System.Drawing.Size(250, 41);
            this.commissioningDateTimePicker.TabIndex = 32;
            // 
            // releaseDateTimePicker
            // 
            this.releaseDateTimePicker.Location = new System.Drawing.Point(260, 223);
            this.releaseDateTimePicker.Name = "releaseDateTimePicker";
            this.releaseDateTimePicker.Size = new System.Drawing.Size(250, 41);
            this.releaseDateTimePicker.TabIndex = 33;
            // 
            // decommissioningDateTimePicker
            // 
            this.decommissioningDateTimePicker.Location = new System.Drawing.Point(1054, 170);
            this.decommissioningDateTimePicker.Name = "decommissioningDateTimePicker";
            this.decommissioningDateTimePicker.Size = new System.Drawing.Size(250, 41);
            this.decommissioningDateTimePicker.TabIndex = 34;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 175);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(331, 35);
            this.label1.TabIndex = 35;
            this.label1.Text = "Дата ввода в эксплуатацию";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(37, 228);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(169, 35);
            this.label2.TabIndex = 36;
            this.label2.Text = "Дата выпуска";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(691, 175);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(357, 35);
            this.label3.TabIndex = 37;
            this.label3.Text = "Дата вывода из эксплуатации";
            // 
            // AddPassportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 35F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1400, 1055);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.decommissioningDateTimePicker);
            this.Controls.Add(this.releaseDateTimePicker);
            this.Controls.Add(this.commissioningDateTimePicker);
            this.Controls.Add(this.errorButton);
            this.Controls.Add(this.maintananceButton);
            this.Controls.Add(this.typeButton);
            this.Controls.Add(this.supplierButton);
            this.Controls.Add(this.errorDataGridView);
            this.Controls.Add(this.maintananceDataGridView);
            this.Controls.Add(this.mainLabel);
            this.Controls.Add(this.departmentComboBox);
            this.Controls.Add(this.typeComboBox);
            this.Controls.Add(this.operatingTextBox);
            this.Controls.Add(this.supplierComboBox);
            this.Controls.Add(this.inventoryTextBox);
            this.Controls.Add(this.serialTextBox);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.errorLabel);
            this.Controls.Add(this.maintananceLabel);
            this.Controls.Add(this.dapertmentLabel);
            this.Controls.Add(this.typeLabel);
            this.Controls.Add(this.operatingLabel);
            this.Controls.Add(this.supplierLabel);
            this.Controls.Add(this.inventoryLabel);
            this.Controls.Add(this.serialLabel);
            this.Controls.Add(this.nameLabel);
            this.Margin = new System.Windows.Forms.Padding(9);
            this.Name = "AddPassportForm";
            this.Text = "Редактирование техпаспорта";
            this.Controls.SetChildIndex(this.nameLabel, 0);
            this.Controls.SetChildIndex(this.serialLabel, 0);
            this.Controls.SetChildIndex(this.inventoryLabel, 0);
            this.Controls.SetChildIndex(this.supplierLabel, 0);
            this.Controls.SetChildIndex(this.operatingLabel, 0);
            this.Controls.SetChildIndex(this.typeLabel, 0);
            this.Controls.SetChildIndex(this.dapertmentLabel, 0);
            this.Controls.SetChildIndex(this.maintananceLabel, 0);
            this.Controls.SetChildIndex(this.errorLabel, 0);
            this.Controls.SetChildIndex(this.nameTextBox, 0);
            this.Controls.SetChildIndex(this.serialTextBox, 0);
            this.Controls.SetChildIndex(this.inventoryTextBox, 0);
            this.Controls.SetChildIndex(this.supplierComboBox, 0);
            this.Controls.SetChildIndex(this.operatingTextBox, 0);
            this.Controls.SetChildIndex(this.typeComboBox, 0);
            this.Controls.SetChildIndex(this.departmentComboBox, 0);
            this.Controls.SetChildIndex(this.mainLabel, 0);
            this.Controls.SetChildIndex(this.maintananceDataGridView, 0);
            this.Controls.SetChildIndex(this.errorDataGridView, 0);
            this.Controls.SetChildIndex(this.supplierButton, 0);
            this.Controls.SetChildIndex(this.typeButton, 0);
            this.Controls.SetChildIndex(this.maintananceButton, 0);
            this.Controls.SetChildIndex(this.errorButton, 0);
            this.Controls.SetChildIndex(this.commissioningDateTimePicker, 0);
            this.Controls.SetChildIndex(this.releaseDateTimePicker, 0);
            this.Controls.SetChildIndex(this.decommissioningDateTimePicker, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            ((System.ComponentModel.ISupportInitialize)(this.maintananceDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label nameLabel;
        private Label serialLabel;
        private Label inventoryLabel;
        private Label supplierLabel;
        private Label operatingLabel;
        private Label typeLabel;
        private Label dapertmentLabel;
        //private Label operatorLabel;
        private Label maintananceLabel;
        private Label errorLabel;
        //private Button saveButton;
        //private Button cancelButton;
        private TextBox nameTextBox;
        private TextBox serialTextBox;
        private TextBox inventoryTextBox;
        private ComboBox supplierComboBox;
        private TextBox operatingTextBox;
        private ComboBox typeComboBox;
        private ComboBox departmentComboBox;
        //private ComboBox operatorComboBox;
        private Label mainLabel;
        private DataGridView maintananceDataGridView;
        private DataGridView errorDataGridView;
        private Button supplierButton;
        private Button typeButton;
        //private Button dapertmentButton;
        //private Button operatorButton;
        private Button maintananceButton;
        private Button errorButton;
        private DataGridViewTextBoxColumn IdColumn;
        private DataGridViewTextBoxColumn NameColumn;

        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn DateColumn;
        private DateTimePicker commissioningDateTimePicker;
        private DateTimePicker releaseDateTimePicker;
        private DateTimePicker decommissioningDateTimePicker;
        private Label label1;
        private Label label2;
        private Label label3;
    }
}