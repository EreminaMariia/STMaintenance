namespace WinFormsView
{
    partial class AddMaterialInfoForm
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
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.innerTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.originalTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.commentRichTextBox = new System.Windows.Forms.RichTextBox();
            this.artsDataGridView = new System.Windows.Forms.DataGridView();
            this.addButton = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.supComboBox = new System.Windows.Forms.ComboBox();
            this.addSupButton = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.unitComboBox = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.artsDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 38);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 35);
            this.label1.TabIndex = 3;
            this.label1.Text = "Название";
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(346, 38);
            this.nameTextBox.Margin = new System.Windows.Forms.Padding(5);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(944, 41);
            this.nameTextBox.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 146);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(249, 35);
            this.label2.TabIndex = 5;
            this.label2.Text = "Внутрениий артикул";
            // 
            // innerTextBox
            // 
            this.innerTextBox.Location = new System.Drawing.Point(346, 143);
            this.innerTextBox.Margin = new System.Windows.Forms.Padding(5);
            this.innerTextBox.Name = "innerTextBox";
            this.innerTextBox.Size = new System.Drawing.Size(944, 41);
            this.innerTextBox.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 206);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(286, 35);
            this.label3.TabIndex = 7;
            this.label3.Text = "Оригинальный артикул";
            // 
            // originalTextBox
            // 
            this.originalTextBox.Location = new System.Drawing.Point(346, 206);
            this.originalTextBox.Margin = new System.Windows.Forms.Padding(5);
            this.originalTextBox.Name = "originalTextBox";
            this.originalTextBox.Size = new System.Drawing.Size(944, 41);
            this.originalTextBox.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 332);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(223, 35);
            this.label4.TabIndex = 9;
            this.label4.Text = "Артикулы-замены";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(21, 679);
            this.label5.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(176, 35);
            this.label5.TabIndex = 10;
            this.label5.Text = "Комментарий";
            // 
            // commentRichTextBox
            // 
            this.commentRichTextBox.Location = new System.Drawing.Point(346, 674);
            this.commentRichTextBox.Margin = new System.Windows.Forms.Padding(5);
            this.commentRichTextBox.Name = "commentRichTextBox";
            this.commentRichTextBox.Size = new System.Drawing.Size(944, 238);
            this.commentRichTextBox.TabIndex = 11;
            this.commentRichTextBox.Text = "";
            // 
            // artsDataGridView
            // 
            this.artsDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.artsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.artsDataGridView.Location = new System.Drawing.Point(346, 332);
            this.artsDataGridView.Margin = new System.Windows.Forms.Padding(5);
            this.artsDataGridView.Name = "artsDataGridView";
            this.artsDataGridView.RowHeadersWidth = 51;
            this.artsDataGridView.RowTemplate.Height = 29;
            this.artsDataGridView.Size = new System.Drawing.Size(947, 307);
            this.artsDataGridView.TabIndex = 12;
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(1303, 332);
            this.addButton.Margin = new System.Windows.Forms.Padding(5);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(49, 51);
            this.addButton.TabIndex = 13;
            this.addButton.Text = "+";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 272);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(144, 35);
            this.label6.TabIndex = 14;
            this.label6.Text = "Поставщик";
            // 
            // supComboBox
            // 
            this.supComboBox.FormattingEnabled = true;
            this.supComboBox.Location = new System.Drawing.Point(346, 269);
            this.supComboBox.Name = "supComboBox";
            this.supComboBox.Size = new System.Drawing.Size(947, 43);
            this.supComboBox.TabIndex = 15;
            // 
            // addSupButton
            // 
            this.addSupButton.Location = new System.Drawing.Point(1303, 269);
            this.addSupButton.Name = "addSupButton";
            this.addSupButton.Size = new System.Drawing.Size(49, 51);
            this.addSupButton.TabIndex = 16;
            this.addSupButton.Text = "+";
            this.addSupButton.UseVisualStyleBackColor = true;
            this.addSupButton.Click += new System.EventHandler(this.addSupButton_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(21, 92);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(253, 35);
            this.label7.TabIndex = 17;
            this.label7.Text = "Единицы измерения";
            // 
            // unitComboBox
            // 
            this.unitComboBox.FormattingEnabled = true;
            this.unitComboBox.Location = new System.Drawing.Point(346, 89);
            this.unitComboBox.Name = "unitComboBox";
            this.unitComboBox.Size = new System.Drawing.Size(944, 43);
            this.unitComboBox.TabIndex = 18;
            // 
            // AddMaterialInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 35F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1400, 1055);
            this.Controls.Add(this.unitComboBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.addSupButton);
            this.Controls.Add(this.supComboBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.artsDataGridView);
            this.Controls.Add(this.commentRichTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.originalTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.innerTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(9);
            this.Name = "AddMaterialInfoForm";
            this.Text = "AddMaterialInfoForm";
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.nameTextBox, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.innerTextBox, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.originalTextBox, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.commentRichTextBox, 0);
            this.Controls.SetChildIndex(this.artsDataGridView, 0);
            this.Controls.SetChildIndex(this.addButton, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.supComboBox, 0);
            this.Controls.SetChildIndex(this.addSupButton, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.unitComboBox, 0);
            ((System.ComponentModel.ISupportInitialize)(this.artsDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private TextBox nameTextBox;
        private Label label2;
        private TextBox innerTextBox;
        private Label label3;
        private TextBox originalTextBox;
        private Label label4;
        private Label label5;
        private RichTextBox commentRichTextBox;
        private DataGridView artsDataGridView;
        private Button addButton;
        private Label label6;
        private ComboBox supComboBox;
        private Button addSupButton;
        private Label label7;
        private ComboBox unitComboBox;
    }
}