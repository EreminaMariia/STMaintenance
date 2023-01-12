namespace WinFormsView
{
    partial class AddMaintananceEpisodeForm
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
            this.maintananceLabel = new System.Windows.Forms.Label();
            this.workerLabel = new System.Windows.Forms.Label();
            this.maintananceComboBox = new System.Windows.Forms.ComboBox();
            this.maintananceButton = new System.Windows.Forms.Button();
            this.workerButton = new System.Windows.Forms.Button();
            this.dateLabel = new System.Windows.Forms.Label();
            this.hoursLabel = new System.Windows.Forms.Label();
            this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.hoursTextBox = new System.Windows.Forms.TextBox();
            this.workerListBox = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // maintananceLabel
            // 
            this.maintananceLabel.AutoSize = true;
            this.maintananceLabel.Location = new System.Drawing.Point(21, 72);
            this.maintananceLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.maintananceLabel.Name = "maintananceLabel";
            this.maintananceLabel.Size = new System.Drawing.Size(187, 35);
            this.maintananceLabel.TabIndex = 3;
            this.maintananceLabel.Text = "Обслуживание";
            // 
            // workerLabel
            // 
            this.workerLabel.AutoSize = true;
            this.workerLabel.Location = new System.Drawing.Point(21, 158);
            this.workerLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.workerLabel.Name = "workerLabel";
            this.workerLabel.Size = new System.Drawing.Size(131, 35);
            this.workerLabel.TabIndex = 4;
            this.workerLabel.Text = "Выполнил";
            // 
            // maintananceComboBox
            // 
            this.maintananceComboBox.FormattingEnabled = true;
            this.maintananceComboBox.Location = new System.Drawing.Point(301, 74);
            this.maintananceComboBox.Margin = new System.Windows.Forms.Padding(5);
            this.maintananceComboBox.Name = "maintananceComboBox";
            this.maintananceComboBox.Size = new System.Drawing.Size(854, 43);
            this.maintananceComboBox.TabIndex = 5;
            // 
            // maintananceButton
            // 
            this.maintananceButton.Location = new System.Drawing.Point(1202, 72);
            this.maintananceButton.Margin = new System.Windows.Forms.Padding(5);
            this.maintananceButton.Name = "maintananceButton";
            this.maintananceButton.Size = new System.Drawing.Size(54, 51);
            this.maintananceButton.TabIndex = 7;
            this.maintananceButton.Text = "+";
            this.maintananceButton.UseVisualStyleBackColor = true;
            this.maintananceButton.Click += new System.EventHandler(this.maintananceButton_Click);
            // 
            // workerButton
            // 
            this.workerButton.Location = new System.Drawing.Point(1202, 158);
            this.workerButton.Margin = new System.Windows.Forms.Padding(5);
            this.workerButton.Name = "workerButton";
            this.workerButton.Size = new System.Drawing.Size(54, 51);
            this.workerButton.TabIndex = 8;
            this.workerButton.Text = "+";
            this.workerButton.UseVisualStyleBackColor = true;
            this.workerButton.Click += new System.EventHandler(this.workerButton_Click);
            // 
            // dateLabel
            // 
            this.dateLabel.AutoSize = true;
            this.dateLabel.Location = new System.Drawing.Point(21, 318);
            this.dateLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.dateLabel.Name = "dateLabel";
            this.dateLabel.Size = new System.Drawing.Size(68, 35);
            this.dateLabel.TabIndex = 9;
            this.dateLabel.Text = "Дата";
            // 
            // hoursLabel
            // 
            this.hoursLabel.AutoSize = true;
            this.hoursLabel.Location = new System.Drawing.Point(21, 379);
            this.hoursLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.hoursLabel.Name = "hoursLabel";
            this.hoursLabel.Size = new System.Drawing.Size(257, 35);
            this.hoursLabel.TabIndex = 10;
            this.hoursLabel.Text = "Трудоёмкость (часы)";
            // 
            // dateTimePicker
            // 
            this.dateTimePicker.Location = new System.Drawing.Point(301, 318);
            this.dateTimePicker.Margin = new System.Windows.Forms.Padding(5);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.Size = new System.Drawing.Size(434, 41);
            this.dateTimePicker.TabIndex = 11;
            // 
            // hoursTextBox
            // 
            this.hoursTextBox.Location = new System.Drawing.Point(301, 376);
            this.hoursTextBox.Margin = new System.Windows.Forms.Padding(5);
            this.hoursTextBox.Name = "hoursTextBox";
            this.hoursTextBox.Size = new System.Drawing.Size(854, 41);
            this.hoursTextBox.TabIndex = 12;
            // 
            // workerListBox
            // 
            this.workerListBox.FormattingEnabled = true;
            this.workerListBox.Location = new System.Drawing.Point(301, 158);
            this.workerListBox.Name = "workerListBox";
            this.workerListBox.Size = new System.Drawing.Size(854, 148);
            this.workerListBox.TabIndex = 13;
            // 
            // AddMaintananceEpisodeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 35F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1353, 520);
            this.Controls.Add(this.workerListBox);
            this.Controls.Add(this.hoursTextBox);
            this.Controls.Add(this.dateTimePicker);
            this.Controls.Add(this.hoursLabel);
            this.Controls.Add(this.dateLabel);
            this.Controls.Add(this.workerButton);
            this.Controls.Add(this.maintananceButton);
            this.Controls.Add(this.maintananceComboBox);
            this.Controls.Add(this.workerLabel);
            this.Controls.Add(this.maintananceLabel);
            this.Margin = new System.Windows.Forms.Padding(9);
            this.Name = "AddMaintananceEpisodeForm";
            this.Text = "AddMaintananceEpisodeForm";
            this.Controls.SetChildIndex(this.maintananceLabel, 0);
            this.Controls.SetChildIndex(this.workerLabel, 0);
            this.Controls.SetChildIndex(this.maintananceComboBox, 0);
            this.Controls.SetChildIndex(this.maintananceButton, 0);
            this.Controls.SetChildIndex(this.workerButton, 0);
            this.Controls.SetChildIndex(this.dateLabel, 0);
            this.Controls.SetChildIndex(this.hoursLabel, 0);
            this.Controls.SetChildIndex(this.dateTimePicker, 0);
            this.Controls.SetChildIndex(this.hoursTextBox, 0);
            this.Controls.SetChildIndex(this.workerListBox, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label maintananceLabel;
        private Label workerLabel;
        private ComboBox maintananceComboBox;
        private Button maintananceButton;
        private Button workerButton;
        private Label dateLabel;
        private Label hoursLabel;
        private DateTimePicker dateTimePicker;
        private TextBox hoursTextBox;
        private CheckedListBox workerListBox;
    }
}