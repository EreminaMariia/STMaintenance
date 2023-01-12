namespace WinFormsView
{
    partial class AddArtInfoForm
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
            this.artTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.supComboBox = new System.Windows.Forms.ComboBox();
            this.addSupButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(52, 72);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 35);
            this.label1.TabIndex = 3;
            this.label1.Text = "Артикул";
            // 
            // artTextBox
            // 
            this.artTextBox.Location = new System.Drawing.Point(324, 66);
            this.artTextBox.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.artTextBox.Name = "artTextBox";
            this.artTextBox.Size = new System.Drawing.Size(870, 41);
            this.artTextBox.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(52, 191);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(144, 35);
            this.label2.TabIndex = 6;
            this.label2.Text = "Поставщик";
            // 
            // supComboBox
            // 
            this.supComboBox.FormattingEnabled = true;
            this.supComboBox.Location = new System.Drawing.Point(324, 188);
            this.supComboBox.Name = "supComboBox";
            this.supComboBox.Size = new System.Drawing.Size(870, 43);
            this.supComboBox.TabIndex = 7;
            // 
            // addSupButton
            // 
            this.addSupButton.Location = new System.Drawing.Point(1214, 188);
            this.addSupButton.Name = "addSupButton";
            this.addSupButton.Size = new System.Drawing.Size(44, 43);
            this.addSupButton.TabIndex = 8;
            this.addSupButton.Text = "+";
            this.addSupButton.UseVisualStyleBackColor = true;
            this.addSupButton.Click += new System.EventHandler(this.addSupButton_Click);
            // 
            // AddArtInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 35F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1316, 401);
            this.Controls.Add(this.addSupButton);
            this.Controls.Add(this.supComboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.artTextBox);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(9, 9, 9, 9);
            this.Name = "AddArtInfoForm";
            this.Text = "AddArtInfoForm";
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.artTextBox, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.supComboBox, 0);
            this.Controls.SetChildIndex(this.addSupButton, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private TextBox artTextBox;
        private Label label2;
        private ComboBox supComboBox;
        private Button addSupButton;
    }
}