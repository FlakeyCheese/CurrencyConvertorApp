namespace CurrencyConvertorApp
{
    partial class Form1
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
            lblTitle = new Label();
            lblAmount = new Label();
            txtAmount = new TextBox();
            lblFrom = new Label();
            cmbFrom = new ComboBox();
            lblTo = new Label();
            cmbTo = new ComboBox();
            btnConvert = new Button();
            lblResult = new Label();
            lblLastUpdated = new Label();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            lblTitle.Location = new Point(143, 9);
            lblTitle.Margin = new Padding(4, 0, 4, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(268, 25);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Live Currency Convertor";
            // 
            // lblAmount
            // 
            lblAmount.AutoSize = true;
            lblAmount.Location = new Point(27, 104);
            lblAmount.Margin = new Padding(4, 0, 4, 0);
            lblAmount.Name = "lblAmount";
            lblAmount.Size = new Size(51, 15);
            lblAmount.TabIndex = 1;
            lblAmount.Text = "Amount";
            // 
            // txtAmount
            // 
            txtAmount.Location = new Point(31, 140);
            txtAmount.Margin = new Padding(4, 3, 4, 3);
            txtAmount.Name = "txtAmount";
            txtAmount.Size = new Size(116, 23);
            txtAmount.TabIndex = 2;
            // 
            // lblFrom
            // 
            lblFrom.AutoSize = true;
            lblFrom.Location = new Point(217, 103);
            lblFrom.Margin = new Padding(4, 0, 4, 0);
            lblFrom.Name = "lblFrom";
            lblFrom.Size = new Size(38, 15);
            lblFrom.TabIndex = 3;
            lblFrom.Text = "From:";
            // 
            // cmbFrom
            // 
            cmbFrom.FormattingEnabled = true;
            cmbFrom.Location = new Point(221, 140);
            cmbFrom.Margin = new Padding(4, 3, 4, 3);
            cmbFrom.Name = "cmbFrom";
            cmbFrom.Size = new Size(140, 23);
            cmbFrom.TabIndex = 4;
            // 
            // lblTo
            // 
            lblTo.AutoSize = true;
            lblTo.Location = new Point(417, 103);
            lblTo.Margin = new Padding(4, 0, 4, 0);
            lblTo.Name = "lblTo";
            lblTo.Size = new Size(23, 15);
            lblTo.TabIndex = 3;
            lblTo.Text = "To:";
            // 
            // cmbTo
            // 
            cmbTo.FormattingEnabled = true;
            cmbTo.Location = new Point(420, 140);
            cmbTo.Margin = new Padding(4, 3, 4, 3);
            cmbTo.Name = "cmbTo";
            cmbTo.Size = new Size(140, 23);
            cmbTo.TabIndex = 4;
            // 
            // btnConvert
            // 
            btnConvert.Location = new Point(221, 194);
            btnConvert.Margin = new Padding(4, 3, 4, 3);
            btnConvert.Name = "btnConvert";
            btnConvert.Size = new Size(88, 27);
            btnConvert.TabIndex = 5;
            btnConvert.Text = "Convert";
            btnConvert.UseVisualStyleBackColor = true;
            btnConvert.Click += btnConvert_Click;
            // 
            // lblResult
            // 
            lblResult.AutoSize = true;
            lblResult.Location = new Point(221, 268);
            lblResult.Margin = new Padding(4, 0, 4, 0);
            lblResult.Name = "lblResult";
            lblResult.Size = new Size(42, 15);
            lblResult.TabIndex = 6;
            lblResult.Text = "Result:";
            // 
            // lblLastUpdated
            // 
            lblLastUpdated.AutoSize = true;
            lblLastUpdated.Location = new Point(221, 318);
            lblLastUpdated.Margin = new Padding(4, 0, 4, 0);
            lblLastUpdated.Name = "lblLastUpdated";
            lblLastUpdated.Size = new Size(0, 15);
            lblLastUpdated.TabIndex = 7;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(586, 351);
            Controls.Add(lblLastUpdated);
            Controls.Add(lblResult);
            Controls.Add(btnConvert);
            Controls.Add(cmbTo);
            Controls.Add(cmbFrom);
            Controls.Add(lblTo);
            Controls.Add(lblFrom);
            Controls.Add(txtAmount);
            Controls.Add(lblAmount);
            Controls.Add(lblTitle);
            Margin = new Padding(4, 3, 4, 3);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.ComboBox cmbFrom;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.ComboBox cmbTo;
        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Label lblLastUpdated;
    }


}

