namespace WinFormsView
{
    partial class AddRepairingForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.hourTextBox = new System.Windows.Forms.TextBox();
            this.commentRichTextBox = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.workerButton = new System.Windows.Forms.Button();
            this.workerListBox = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(68, 79);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 35);
            this.label1.TabIndex = 4;
            this.label1.Text = "Выполнил";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(77, 269);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(257, 35);
            this.label2.TabIndex = 5;
            this.label2.Text = "Трудоёмкость (часы)";
            // 
            // hourTextBox
            // 
            this.hourTextBox.Location = new System.Drawing.Point(408, 269);
            this.hourTextBox.Margin = new System.Windows.Forms.Padding(5);
            this.hourTextBox.Name = "hourTextBox";
            this.hourTextBox.Size = new System.Drawing.Size(832, 41);
            this.hourTextBox.TabIndex = 6;
            // 
            // commentRichTextBox
            // 
            this.commentRichTextBox.Location = new System.Drawing.Point(408, 338);
            this.commentRichTextBox.Margin = new System.Windows.Forms.Padding(5);
            this.commentRichTextBox.Name = "commentRichTextBox";
            this.commentRichTextBox.Size = new System.Drawing.Size(832, 322);
            this.commentRichTextBox.TabIndex = 7;
            this.commentRichTextBox.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(68, 338);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(130, 35);
            this.label3.TabIndex = 8;
            this.label3.Text = "Описание";
            // 
            // workerButton
            // 
            this.workerButton.Location = new System.Drawing.Point(1272, 71);
            this.workerButton.Margin = new System.Windows.Forms.Padding(5);
            this.workerButton.Name = "workerButton";
            this.workerButton.Size = new System.Drawing.Size(52, 51);
            this.workerButton.TabIndex = 9;
            this.workerButton.Text = "+";
            this.workerButton.UseVisualStyleBackColor = true;
            this.workerButton.Click += new System.EventHandler(this.workerButton_Click);
            // 
            // workerListBox
            // 
            this.workerListBox.FormattingEnabled = true;
            this.workerListBox.Location = new System.Drawing.Point(408, 72);
            this.workerListBox.Name = "workerListBox";
            this.workerListBox.Size = new System.Drawing.Size(832, 184);
            this.workerListBox.TabIndex = 10;
            // 
            // AddRepairingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 35F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1400, 788);
            this.Controls.Add(this.workerListBox);
            this.Controls.Add(this.workerButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.commentRichTextBox);
            this.Controls.Add(this.hourTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(9);
            this.Name = "AddRepairingForm";
            this.Text = "AddRepairingForm";
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.hourTextBox, 0);
            this.Controls.SetChildIndex(this.commentRichTextBox, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.workerButton, 0);
            this.Controls.SetChildIndex(this.workerListBox, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Label label1;
        private Label label2;
        private TextBox hourTextBox;
        private RichTextBox commentRichTextBox;
        private Label label3;
        private Button workerButton;
        private CheckedListBox workerListBox;
    }
}