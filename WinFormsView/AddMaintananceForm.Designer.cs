namespace WinFormsView
{
    partial class AddMaintananceForm
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
            this.typeLabel = new System.Windows.Forms.Label();
            this.intervalLabel = new System.Windows.Forms.Label();
            this.hoursLabel = new System.Windows.Forms.Label();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.typeTextBox = new System.Windows.Forms.TextBox();
            this.intervalTextBox = new System.Windows.Forms.TextBox();
            this.hoursTextBox = new System.Windows.Forms.TextBox();
            this.episodeDataGridView = new System.Windows.Forms.DataGridView();
            this.episodeLabel = new System.Windows.Forms.Label();
            this.materialDataGridView = new System.Windows.Forms.DataGridView();
            this.materialLabel = new System.Windows.Forms.Label();
            this.changedDateLabel = new System.Windows.Forms.Label();
            this.changeDateButton = new System.Windows.Forms.Button();
            this.futureDateLabel = new System.Windows.Forms.Label();
            this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.changedDateValueLabel = new System.Windows.Forms.Label();
            this.instractionDataGridView = new System.Windows.Forms.DataGridView();
            this.IdColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.instractionLabel = new System.Windows.Forms.Label();
            this.instructionButton = new System.Windows.Forms.Button();
            this.addMaterialButton = new System.Windows.Forms.Button();
            this.hoursRadioButton = new System.Windows.Forms.RadioButton();
            this.daysRadioButton = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.episodeDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.materialDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.instractionDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(21, 9);
            this.nameLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(125, 35);
            this.nameLabel.TabIndex = 0;
            this.nameLabel.Text = "Название";
            // 
            // typeLabel
            // 
            this.typeLabel.AutoSize = true;
            this.typeLabel.Location = new System.Drawing.Point(21, 56);
            this.typeLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.typeLabel.Name = "typeLabel";
            this.typeLabel.Size = new System.Drawing.Size(57, 35);
            this.typeLabel.TabIndex = 1;
            this.typeLabel.Text = "Тип";
            // 
            // intervalLabel
            // 
            this.intervalLabel.AutoSize = true;
            this.intervalLabel.Location = new System.Drawing.Point(21, 109);
            this.intervalLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.intervalLabel.Name = "intervalLabel";
            this.intervalLabel.Size = new System.Drawing.Size(125, 35);
            this.intervalLabel.TabIndex = 4;
            this.intervalLabel.Text = "Интервал";
            // 
            // hoursLabel
            // 
            this.hoursLabel.AutoSize = true;
            this.hoursLabel.Location = new System.Drawing.Point(14, 188);
            this.hoursLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.hoursLabel.Name = "hoursLabel";
            this.hoursLabel.Size = new System.Drawing.Size(257, 35);
            this.hoursLabel.TabIndex = 5;
            this.hoursLabel.Text = "Трудоёмкость (часы)";
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(348, 9);
            this.nameTextBox.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(646, 41);
            this.nameTextBox.TabIndex = 9;
            // 
            // typeTextBox
            // 
            this.typeTextBox.Location = new System.Drawing.Point(348, 60);
            this.typeTextBox.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.typeTextBox.Name = "typeTextBox";
            this.typeTextBox.Size = new System.Drawing.Size(646, 41);
            this.typeTextBox.TabIndex = 10;
            // 
            // intervalTextBox
            // 
            this.intervalTextBox.Location = new System.Drawing.Point(348, 109);
            this.intervalTextBox.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.intervalTextBox.Name = "intervalTextBox";
            this.intervalTextBox.Size = new System.Drawing.Size(646, 41);
            this.intervalTextBox.TabIndex = 11;
            // 
            // hoursTextBox
            // 
            this.hoursTextBox.Location = new System.Drawing.Point(348, 182);
            this.hoursTextBox.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.hoursTextBox.Name = "hoursTextBox";
            this.hoursTextBox.Size = new System.Drawing.Size(646, 41);
            this.hoursTextBox.TabIndex = 12;
            // 
            // episodeDataGridView
            // 
            this.episodeDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.episodeDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.episodeDataGridView.Location = new System.Drawing.Point(284, 509);
            this.episodeDataGridView.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.episodeDataGridView.Name = "episodeDataGridView";
            this.episodeDataGridView.RowHeadersVisible = false;
            this.episodeDataGridView.RowHeadersWidth = 51;
            this.episodeDataGridView.RowTemplate.Height = 29;
            this.episodeDataGridView.Size = new System.Drawing.Size(826, 230);
            this.episodeDataGridView.TabIndex = 13;
            // 
            // episodeLabel
            // 
            this.episodeLabel.AutoSize = true;
            this.episodeLabel.Location = new System.Drawing.Point(517, 469);
            this.episodeLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.episodeLabel.Name = "episodeLabel";
            this.episodeLabel.Size = new System.Drawing.Size(347, 35);
            this.episodeLabel.TabIndex = 14;
            this.episodeLabel.Text = "Проведенное обслуживание";
            // 
            // materialDataGridView
            // 
            this.materialDataGridView.AllowUserToAddRows = false;
            this.materialDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.materialDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.materialDataGridView.Location = new System.Drawing.Point(284, 268);
            this.materialDataGridView.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.materialDataGridView.Name = "materialDataGridView";
            this.materialDataGridView.RowHeadersVisible = false;
            this.materialDataGridView.RowHeadersWidth = 51;
            this.materialDataGridView.RowTemplate.Height = 29;
            this.materialDataGridView.Size = new System.Drawing.Size(826, 196);
            this.materialDataGridView.TabIndex = 15;
            this.materialDataGridView.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.materialDataGridView_UserDeletedRow);
            // 
            // materialLabel
            // 
            this.materialLabel.AutoSize = true;
            this.materialLabel.Location = new System.Drawing.Point(530, 228);
            this.materialLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.materialLabel.Name = "materialLabel";
            this.materialLabel.Size = new System.Drawing.Size(314, 35);
            this.materialLabel.TabIndex = 16;
            this.materialLabel.Text = "Необходимые материалы";
            // 
            // changedDateLabel
            // 
            this.changedDateLabel.AutoSize = true;
            this.changedDateLabel.Location = new System.Drawing.Point(21, 808);
            this.changedDateLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.changedDateLabel.Name = "changedDateLabel";
            this.changedDateLabel.Size = new System.Drawing.Size(242, 35);
            this.changedDateLabel.TabIndex = 17;
            this.changedDateLabel.Text = "Перенесённая дата:";
            // 
            // changeDateButton
            // 
            this.changeDateButton.Location = new System.Drawing.Point(21, 858);
            this.changeDateButton.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.changeDateButton.Name = "changeDateButton";
            this.changeDateButton.Size = new System.Drawing.Size(364, 51);
            this.changeDateButton.TabIndex = 18;
            this.changeDateButton.Text = "Запланировать вручную";
            this.changeDateButton.UseVisualStyleBackColor = true;
            this.changeDateButton.Click += new System.EventHandler(this.changeDateButton_Click);
            // 
            // futureDateLabel
            // 
            this.futureDateLabel.AutoSize = true;
            this.futureDateLabel.Location = new System.Drawing.Point(21, 762);
            this.futureDateLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.futureDateLabel.Name = "futureDateLabel";
            this.futureDateLabel.Size = new System.Drawing.Size(205, 35);
            this.futureDateLabel.TabIndex = 19;
            this.futureDateLabel.Text = "Рассчётная дата:";
            // 
            // dateTimePicker
            // 
            this.dateTimePicker.Location = new System.Drawing.Point(430, 858);
            this.dateTimePicker.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.Size = new System.Drawing.Size(434, 41);
            this.dateTimePicker.TabIndex = 20;
            this.dateTimePicker.Visible = false;
            this.dateTimePicker.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // changedDateValueLabel
            // 
            this.changedDateValueLabel.AutoSize = true;
            this.changedDateValueLabel.Location = new System.Drawing.Point(298, 940);
            this.changedDateValueLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.changedDateValueLabel.Name = "changedDateValueLabel";
            this.changedDateValueLabel.Size = new System.Drawing.Size(0, 35);
            this.changedDateValueLabel.TabIndex = 21;
            this.changedDateValueLabel.Visible = false;
            // 
            // instractionDataGridView
            // 
            this.instractionDataGridView.AllowUserToAddRows = false;
            this.instractionDataGridView.AllowUserToDeleteRows = false;
            this.instractionDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.instractionDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.instractionDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IdColumn,
            this.NameColumn});
            this.instractionDataGridView.Location = new System.Drawing.Point(284, 1101);
            this.instractionDataGridView.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.instractionDataGridView.Name = "instractionDataGridView";
            this.instractionDataGridView.RowHeadersVisible = false;
            this.instractionDataGridView.RowHeadersWidth = 51;
            this.instractionDataGridView.RowTemplate.Height = 29;
            this.instractionDataGridView.Size = new System.Drawing.Size(826, 186);
            this.instractionDataGridView.TabIndex = 21;
            this.instractionDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.instractionDataGridView_CellContentClick);
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
            // 
            // instractionLabel
            // 
            this.instractionLabel.AutoSize = true;
            this.instractionLabel.Location = new System.Drawing.Point(604, 1060);
            this.instractionLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.instractionLabel.Name = "instractionLabel";
            this.instractionLabel.Size = new System.Drawing.Size(154, 35);
            this.instractionLabel.TabIndex = 8;
            this.instractionLabel.Text = "Инструкции";
            // 
            // instructionButton
            // 
            this.instructionButton.Location = new System.Drawing.Point(1120, 1101);
            this.instructionButton.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.instructionButton.Name = "instructionButton";
            this.instructionButton.Size = new System.Drawing.Size(54, 51);
            this.instructionButton.TabIndex = 32;
            this.instructionButton.Text = "+";
            this.instructionButton.UseVisualStyleBackColor = true;
            this.instructionButton.Click += new System.EventHandler(this.instructionButton_Click);
            // 
            // addMaterialButton
            // 
            this.addMaterialButton.Location = new System.Drawing.Point(1120, 268);
            this.addMaterialButton.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.addMaterialButton.Name = "addMaterialButton";
            this.addMaterialButton.Size = new System.Drawing.Size(54, 51);
            this.addMaterialButton.TabIndex = 33;
            this.addMaterialButton.Text = "+";
            this.addMaterialButton.UseVisualStyleBackColor = true;
            this.addMaterialButton.Click += new System.EventHandler(this.addMaterialButton_Click);
            // 
            // hoursRadioButton
            // 
            this.hoursRadioButton.AutoSize = true;
            this.hoursRadioButton.Location = new System.Drawing.Point(1019, 96);
            this.hoursRadioButton.Name = "hoursRadioButton";
            this.hoursRadioButton.Size = new System.Drawing.Size(93, 39);
            this.hoursRadioButton.TabIndex = 34;
            this.hoursRadioButton.TabStop = true;
            this.hoursRadioButton.Text = "часы";
            this.hoursRadioButton.UseVisualStyleBackColor = true;
            // 
            // daysRadioButton
            // 
            this.daysRadioButton.AutoSize = true;
            this.daysRadioButton.Location = new System.Drawing.Point(1019, 129);
            this.daysRadioButton.Name = "daysRadioButton";
            this.daysRadioButton.Size = new System.Drawing.Size(238, 39);
            this.daysRadioButton.TabIndex = 35;
            this.daysRadioButton.TabStop = true;
            this.daysRadioButton.Text = "календарные дни";
            this.daysRadioButton.UseVisualStyleBackColor = true;
            // 
            // AddMaintananceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 35F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1400, 1055);
            this.Controls.Add(this.daysRadioButton);
            this.Controls.Add(this.hoursRadioButton);
            this.Controls.Add(this.addMaterialButton);
            this.Controls.Add(this.instractionLabel);
            this.Controls.Add(this.instractionDataGridView);
            this.Controls.Add(this.instructionButton);
            this.Controls.Add(this.changedDateValueLabel);
            this.Controls.Add(this.dateTimePicker);
            this.Controls.Add(this.futureDateLabel);
            this.Controls.Add(this.changeDateButton);
            this.Controls.Add(this.changedDateLabel);
            this.Controls.Add(this.materialLabel);
            this.Controls.Add(this.materialDataGridView);
            this.Controls.Add(this.episodeLabel);
            this.Controls.Add(this.episodeDataGridView);
            this.Controls.Add(this.hoursTextBox);
            this.Controls.Add(this.intervalTextBox);
            this.Controls.Add(this.typeTextBox);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.hoursLabel);
            this.Controls.Add(this.intervalLabel);
            this.Controls.Add(this.typeLabel);
            this.Controls.Add(this.nameLabel);
            this.Margin = new System.Windows.Forms.Padding(9, 9, 9, 9);
            this.Name = "AddMaintananceForm";
            this.Text = "Редактировать процесс обслуживания";
            this.Controls.SetChildIndex(this.nameLabel, 0);
            this.Controls.SetChildIndex(this.typeLabel, 0);
            this.Controls.SetChildIndex(this.intervalLabel, 0);
            this.Controls.SetChildIndex(this.hoursLabel, 0);
            this.Controls.SetChildIndex(this.nameTextBox, 0);
            this.Controls.SetChildIndex(this.typeTextBox, 0);
            this.Controls.SetChildIndex(this.intervalTextBox, 0);
            this.Controls.SetChildIndex(this.hoursTextBox, 0);
            this.Controls.SetChildIndex(this.episodeDataGridView, 0);
            this.Controls.SetChildIndex(this.episodeLabel, 0);
            this.Controls.SetChildIndex(this.materialDataGridView, 0);
            this.Controls.SetChildIndex(this.materialLabel, 0);
            this.Controls.SetChildIndex(this.changedDateLabel, 0);
            this.Controls.SetChildIndex(this.changeDateButton, 0);
            this.Controls.SetChildIndex(this.futureDateLabel, 0);
            this.Controls.SetChildIndex(this.dateTimePicker, 0);
            this.Controls.SetChildIndex(this.changedDateValueLabel, 0);
            this.Controls.SetChildIndex(this.instructionButton, 0);
            this.Controls.SetChildIndex(this.instractionDataGridView, 0);
            this.Controls.SetChildIndex(this.instractionLabel, 0);
            this.Controls.SetChildIndex(this.addMaterialButton, 0);
            this.Controls.SetChildIndex(this.hoursRadioButton, 0);
            this.Controls.SetChildIndex(this.daysRadioButton, 0);
            ((System.ComponentModel.ISupportInitialize)(this.episodeDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.materialDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.instractionDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label nameLabel;
        private Label typeLabel;
        private Label intervalLabel;
        private Label hoursLabel;
        private TextBox nameTextBox;
        private TextBox typeTextBox;
        private TextBox intervalTextBox;
        private TextBox hoursTextBox;
        private DataGridView episodeDataGridView;
        private Label episodeLabel;
        private DataGridView materialDataGridView;
        private Label materialLabel;
        private Label changedDateLabel;
        private Button changeDateButton;
        private Label futureDateLabel;
        private DateTimePicker dateTimePicker;
        private Label changedDateValueLabel;
        private DataGridView instractionDataGridView;
        private Label instractionLabel;
        private Button instructionButton;
        private DataGridViewTextBoxColumn IdColumn;
        private DataGridViewTextBoxColumn NameColumn;
        private Button addMaterialButton;
        private RadioButton hoursRadioButton;
        private RadioButton daysRadioButton;
    }
}