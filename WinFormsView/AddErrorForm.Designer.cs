namespace WinFormsView
{
    partial class AddErrorForm
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
            this.codeComboBox = new System.Windows.Forms.ComboBox();
            this.ErrorDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.machineComboBox = new System.Windows.Forms.ComboBox();
            this.isWorkingCheckBox = new System.Windows.Forms.CheckBox();
            this.nameLabel = new System.Windows.Forms.Label();
            this.errorDateLabel = new System.Windows.Forms.Label();
            this.codeLabel = new System.Windows.Forms.Label();
            this.machineLabel = new System.Windows.Forms.Label();
            this.methodTextBox = new System.Windows.Forms.TextBox();
            this.methodLabel = new System.Windows.Forms.Label();
            this.isFixedCheckBox = new System.Windows.Forms.CheckBox();
            this.fixedDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.fixedDatelabel = new System.Windows.Forms.Label();
            this.repairingsLabel = new System.Windows.Forms.Label();
            this.repairingsDataGridView = new System.Windows.Forms.DataGridView();
            this.addRepairingButton = new System.Windows.Forms.Button();
            this.codeButton = new System.Windows.Forms.Button();
            this.hoursTextBox = new System.Windows.Forms.TextBox();
            this.hoursLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.repairingsDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // codeComboBox
            // 
            this.codeComboBox.FormattingEnabled = true;
            this.codeComboBox.Location = new System.Drawing.Point(429, 37);
            this.codeComboBox.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.codeComboBox.Name = "codeComboBox";
            this.codeComboBox.Size = new System.Drawing.Size(879, 43);
            this.codeComboBox.TabIndex = 0;
            // 
            // ErrorDateTimePicker
            // 
            this.ErrorDateTimePicker.Location = new System.Drawing.Point(429, 210);
            this.ErrorDateTimePicker.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.ErrorDateTimePicker.Name = "ErrorDateTimePicker";
            this.ErrorDateTimePicker.Size = new System.Drawing.Size(434, 41);
            this.ErrorDateTimePicker.TabIndex = 1;
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(429, 122);
            this.nameTextBox.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(877, 41);
            this.nameTextBox.TabIndex = 2;
            // 
            // machineComboBox
            // 
            this.machineComboBox.FormattingEnabled = true;
            this.machineComboBox.Location = new System.Drawing.Point(430, 310);
            this.machineComboBox.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.machineComboBox.Name = "machineComboBox";
            this.machineComboBox.Size = new System.Drawing.Size(879, 43);
            this.machineComboBox.TabIndex = 3;
            // 
            // isWorkingCheckBox
            // 
            this.isWorkingCheckBox.AutoSize = true;
            this.isWorkingCheckBox.Location = new System.Drawing.Point(430, 397);
            this.isWorkingCheckBox.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.isWorkingCheckBox.Name = "isWorkingCheckBox";
            this.isWorkingCheckBox.Size = new System.Drawing.Size(319, 39);
            this.isWorkingCheckBox.TabIndex = 4;
            this.isWorkingCheckBox.Text = "Оборудование работает";
            this.isWorkingCheckBox.UseVisualStyleBackColor = true;
            this.isWorkingCheckBox.CheckedChanged += new System.EventHandler(this.isWorkingCheckBox_CheckedChanged);
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(80, 128);
            this.nameLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(125, 35);
            this.nameLabel.TabIndex = 5;
            this.nameLabel.Text = "Название";
            // 
            // errorDateLabel
            // 
            this.errorDateLabel.AutoSize = true;
            this.errorDateLabel.Location = new System.Drawing.Point(80, 219);
            this.errorDateLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.errorDateLabel.Name = "errorDateLabel";
            this.errorDateLabel.Size = new System.Drawing.Size(177, 35);
            this.errorDateLabel.TabIndex = 6;
            this.errorDateLabel.Text = "Дата поломки";
            // 
            // codeLabel
            // 
            this.codeLabel.AutoSize = true;
            this.codeLabel.Location = new System.Drawing.Point(80, 42);
            this.codeLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.codeLabel.Name = "codeLabel";
            this.codeLabel.Size = new System.Drawing.Size(157, 35);
            this.codeLabel.TabIndex = 7;
            this.codeLabel.Text = "Код ошибки";
            // 
            // machineLabel
            // 
            this.machineLabel.AutoSize = true;
            this.machineLabel.Location = new System.Drawing.Point(80, 310);
            this.machineLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.machineLabel.Name = "machineLabel";
            this.machineLabel.Size = new System.Drawing.Size(320, 35);
            this.machineLabel.TabIndex = 8;
            this.machineLabel.Text = "Сломанное оборудование";
            // 
            // methodTextBox
            // 
            this.methodTextBox.Location = new System.Drawing.Point(430, 466);
            this.methodTextBox.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.methodTextBox.Name = "methodTextBox";
            this.methodTextBox.Size = new System.Drawing.Size(877, 41);
            this.methodTextBox.TabIndex = 9;
            // 
            // methodLabel
            // 
            this.methodLabel.AutoSize = true;
            this.methodLabel.Location = new System.Drawing.Point(80, 466);
            this.methodLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.methodLabel.Name = "methodLabel";
            this.methodLabel.Size = new System.Drawing.Size(195, 35);
            this.methodLabel.TabIndex = 10;
            this.methodLabel.Text = "Метод починки";
            // 
            // isFixedCheckBox
            // 
            this.isFixedCheckBox.AutoSize = true;
            this.isFixedCheckBox.Location = new System.Drawing.Point(430, 551);
            this.isFixedCheckBox.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.isFixedCheckBox.Name = "isFixedCheckBox";
            this.isFixedCheckBox.Size = new System.Drawing.Size(253, 39);
            this.isFixedCheckBox.TabIndex = 11;
            this.isFixedCheckBox.Text = "Ошибка устранена";
            this.isFixedCheckBox.UseVisualStyleBackColor = true;
            this.isFixedCheckBox.CheckedChanged += new System.EventHandler(this.isFixedCheckBox_CheckedChanged);
            // 
            // fixedDateTimePicker
            // 
            this.fixedDateTimePicker.Location = new System.Drawing.Point(430, 621);
            this.fixedDateTimePicker.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.fixedDateTimePicker.Name = "fixedDateTimePicker";
            this.fixedDateTimePicker.Size = new System.Drawing.Size(434, 41);
            this.fixedDateTimePicker.TabIndex = 12;
            // 
            // fixedDatelabel
            // 
            this.fixedDatelabel.AutoSize = true;
            this.fixedDatelabel.Location = new System.Drawing.Point(80, 621);
            this.fixedDatelabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.fixedDatelabel.Name = "fixedDatelabel";
            this.fixedDatelabel.Size = new System.Drawing.Size(206, 35);
            this.fixedDatelabel.TabIndex = 13;
            this.fixedDatelabel.Text = "Дата устранения";
            // 
            // repairingsLabel
            // 
            this.repairingsLabel.AutoSize = true;
            this.repairingsLabel.Location = new System.Drawing.Point(536, 700);
            this.repairingsLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.repairingsLabel.Name = "repairingsLabel";
            this.repairingsLabel.Size = new System.Drawing.Size(313, 35);
            this.repairingsLabel.TabIndex = 14;
            this.repairingsLabel.Text = "Проведённые процедуры";
            // 
            // repairingsDataGridView
            // 
            this.repairingsDataGridView.AllowUserToAddRows = false;
            this.repairingsDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.repairingsDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.repairingsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.repairingsDataGridView.Location = new System.Drawing.Point(80, 740);
            this.repairingsDataGridView.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.repairingsDataGridView.Name = "repairingsDataGridView";
            this.repairingsDataGridView.RowHeadersVisible = false;
            this.repairingsDataGridView.RowHeadersWidth = 51;
            this.repairingsDataGridView.RowTemplate.Height = 29;
            this.repairingsDataGridView.Size = new System.Drawing.Size(1232, 229);
            this.repairingsDataGridView.TabIndex = 15;
            this.repairingsDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.repairingsDataGridView_CellContentClick);
            // 
            // addRepairingButton
            // 
            this.addRepairingButton.Location = new System.Drawing.Point(1325, 740);
            this.addRepairingButton.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.addRepairingButton.Name = "addRepairingButton";
            this.addRepairingButton.Size = new System.Drawing.Size(54, 51);
            this.addRepairingButton.TabIndex = 18;
            this.addRepairingButton.Text = "+";
            this.addRepairingButton.UseVisualStyleBackColor = true;
            this.addRepairingButton.Click += new System.EventHandler(this.addRepairingButton_Click);
            // 
            // codeButton
            // 
            this.codeButton.Location = new System.Drawing.Point(1325, 35);
            this.codeButton.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.codeButton.Name = "codeButton";
            this.codeButton.Size = new System.Drawing.Size(54, 51);
            this.codeButton.TabIndex = 19;
            this.codeButton.Text = "+";
            this.codeButton.UseVisualStyleBackColor = true;
            this.codeButton.Click += new System.EventHandler(this.codeButton_Click);
            // 
            // hoursTextBox
            // 
            this.hoursTextBox.Location = new System.Drawing.Point(1094, 392);
            this.hoursTextBox.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.hoursTextBox.Name = "hoursTextBox";
            this.hoursTextBox.Size = new System.Drawing.Size(216, 41);
            this.hoursTextBox.TabIndex = 20;
            // 
            // hoursLabel
            // 
            this.hoursLabel.AutoSize = true;
            this.hoursLabel.Location = new System.Drawing.Point(898, 399);
            this.hoursLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.hoursLabel.Name = "hoursLabel";
            this.hoursLabel.Size = new System.Drawing.Size(176, 35);
            this.hoursLabel.TabIndex = 21;
            this.hoursLabel.Text = "Часы простоя";
            // 
            // AddErrorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 35F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1400, 1055);
            this.Controls.Add(this.hoursLabel);
            this.Controls.Add(this.hoursTextBox);
            this.Controls.Add(this.codeButton);
            this.Controls.Add(this.addRepairingButton);
            this.Controls.Add(this.repairingsDataGridView);
            this.Controls.Add(this.repairingsLabel);
            this.Controls.Add(this.fixedDatelabel);
            this.Controls.Add(this.fixedDateTimePicker);
            this.Controls.Add(this.isFixedCheckBox);
            this.Controls.Add(this.methodLabel);
            this.Controls.Add(this.methodTextBox);
            this.Controls.Add(this.machineLabel);
            this.Controls.Add(this.codeLabel);
            this.Controls.Add(this.errorDateLabel);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.isWorkingCheckBox);
            this.Controls.Add(this.machineComboBox);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.ErrorDateTimePicker);
            this.Controls.Add(this.codeComboBox);
            this.Margin = new System.Windows.Forms.Padding(9, 9, 9, 9);
            this.Name = "AddErrorForm";
            this.Text = "AddErrorForm";
            this.Controls.SetChildIndex(this.codeComboBox, 0);
            this.Controls.SetChildIndex(this.ErrorDateTimePicker, 0);
            this.Controls.SetChildIndex(this.nameTextBox, 0);
            this.Controls.SetChildIndex(this.machineComboBox, 0);
            this.Controls.SetChildIndex(this.isWorkingCheckBox, 0);
            this.Controls.SetChildIndex(this.nameLabel, 0);
            this.Controls.SetChildIndex(this.errorDateLabel, 0);
            this.Controls.SetChildIndex(this.codeLabel, 0);
            this.Controls.SetChildIndex(this.machineLabel, 0);
            this.Controls.SetChildIndex(this.methodTextBox, 0);
            this.Controls.SetChildIndex(this.methodLabel, 0);
            this.Controls.SetChildIndex(this.isFixedCheckBox, 0);
            this.Controls.SetChildIndex(this.fixedDateTimePicker, 0);
            this.Controls.SetChildIndex(this.fixedDatelabel, 0);
            this.Controls.SetChildIndex(this.repairingsLabel, 0);
            this.Controls.SetChildIndex(this.repairingsDataGridView, 0);
            this.Controls.SetChildIndex(this.addRepairingButton, 0);
            this.Controls.SetChildIndex(this.codeButton, 0);
            this.Controls.SetChildIndex(this.hoursTextBox, 0);
            this.Controls.SetChildIndex(this.hoursLabel, 0);
            ((System.ComponentModel.ISupportInitialize)(this.repairingsDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComboBox codeComboBox;
        private DateTimePicker ErrorDateTimePicker;
        private TextBox nameTextBox;
        private ComboBox machineComboBox;
        private CheckBox isWorkingCheckBox;
        private Label nameLabel;
        private Label errorDateLabel;
        private Label codeLabel;
        private Label machineLabel;
        private TextBox methodTextBox;
        private Label methodLabel;
        private CheckBox isFixedCheckBox;
        private DateTimePicker fixedDateTimePicker;
        private Label fixedDatelabel;
        private Label repairingsLabel;
        private DataGridView repairingsDataGridView;
        private Button addRepairingButton;
        private Button codeButton;
        private TextBox hoursTextBox;
        private Label hoursLabel;
    }
}