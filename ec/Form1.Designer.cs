namespace ec
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
            label1 = new Label();
            label2 = new Label();
            cbPeriod = new ComboBox();
            gbOrigin = new GroupBox();
            cbOrigin = new ComboBox();
            tbOrigin = new TextBox();
            lblOrigin = new Label();
            button1 = new Button();
            gbTarget = new GroupBox();
            cbTarget = new ComboBox();
            tbTarget = new TextBox();
            lblTarget = new Label();
            gbOrigin.SuspendLayout();
            gbTarget.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(252, 49);
            label1.Name = "label1";
            label1.Size = new Size(256, 37);
            label1.TabIndex = 0;
            label1.Text = "Currency Converter";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(122, 120);
            label2.Name = "label2";
            label2.Size = new Size(41, 15);
            label2.TabIndex = 1;
            label2.Text = "Period";
            // 
            // cbPeriod
            // 
            cbPeriod.FormattingEnabled = true;
            cbPeriod.Location = new Point(166, 117);
            cbPeriod.Name = "cbPeriod";
            cbPeriod.Size = new Size(492, 23);
            cbPeriod.TabIndex = 2;
            cbPeriod.SelectedIndexChanged += cbPeriod_SelectedIndexChanged;
            // 
            // gbOrigin
            // 
            gbOrigin.Controls.Add(cbOrigin);
            gbOrigin.Controls.Add(tbOrigin);
            gbOrigin.Controls.Add(lblOrigin);
            gbOrigin.Location = new Point(140, 185);
            gbOrigin.Name = "gbOrigin";
            gbOrigin.Size = new Size(200, 100);
            gbOrigin.TabIndex = 3;
            gbOrigin.TabStop = false;
            gbOrigin.Text = "Origin Amount";
            // 
            // cbOrigin
            // 
            cbOrigin.FormattingEnabled = true;
            cbOrigin.Location = new Point(112, 65);
            cbOrigin.Name = "cbOrigin";
            cbOrigin.Size = new Size(82, 23);
            cbOrigin.TabIndex = 2;
            cbOrigin.SelectedIndexChanged += cbOrigin_SelectedIndexChanged;
            // 
            // tbOrigin
            // 
            tbOrigin.Location = new Point(6, 65);
            tbOrigin.Name = "tbOrigin";
            tbOrigin.Size = new Size(100, 23);
            tbOrigin.TabIndex = 1;
            tbOrigin.TextChanged += tbOrigin_TextChanged;
            tbOrigin.KeyPress += tbOrigin_KeyPress;
            // 
            // lblOrigin
            // 
            lblOrigin.AutoSize = true;
            lblOrigin.Location = new Point(6, 31);
            lblOrigin.Name = "lblOrigin";
            lblOrigin.Size = new Size(76, 15);
            lblOrigin.TabIndex = 0;
            lblOrigin.Text = "Japanese Yen";
            // 
            // button1
            // 
            button1.BackgroundImage = Properties.Resources.exchange;
            button1.BackgroundImageLayout = ImageLayout.Zoom;
            button1.Location = new Point(355, 216);
            button1.Name = "button1";
            button1.Size = new Size(75, 48);
            button1.TabIndex = 4;
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // gbTarget
            // 
            gbTarget.Controls.Add(cbTarget);
            gbTarget.Controls.Add(tbTarget);
            gbTarget.Controls.Add(lblTarget);
            gbTarget.Location = new Point(449, 185);
            gbTarget.Name = "gbTarget";
            gbTarget.Size = new Size(200, 100);
            gbTarget.TabIndex = 4;
            gbTarget.TabStop = false;
            gbTarget.Text = "Converted To";
            // 
            // cbTarget
            // 
            cbTarget.FormattingEnabled = true;
            cbTarget.Location = new Point(112, 65);
            cbTarget.Name = "cbTarget";
            cbTarget.Size = new Size(82, 23);
            cbTarget.TabIndex = 2;
            cbTarget.SelectedIndexChanged += cbTarget_SelectedIndexChanged;
            // 
            // tbTarget
            // 
            tbTarget.Location = new Point(6, 65);
            tbTarget.Name = "tbTarget";
            tbTarget.ReadOnly = true;
            tbTarget.Size = new Size(100, 23);
            tbTarget.TabIndex = 1;
            // 
            // lblTarget
            // 
            lblTarget.AutoSize = true;
            lblTarget.Location = new Point(6, 31);
            lblTarget.Name = "lblTarget";
            lblTarget.Size = new Size(105, 15);
            lblTarget.TabIndex = 0;
            lblTarget.Text = "Indonesian Rupiah";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(gbTarget);
            Controls.Add(button1);
            Controls.Add(gbOrigin);
            Controls.Add(cbPeriod);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Form1";
            gbOrigin.ResumeLayout(false);
            gbOrigin.PerformLayout();
            gbTarget.ResumeLayout(false);
            gbTarget.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private ComboBox cbPeriod;
        private GroupBox gbOrigin;
        private ComboBox cbOrigin;
        private TextBox tbOrigin;
        private Label lblOrigin;
        private Button button1;
        private GroupBox gbTarget;
        private ComboBox cbTarget;
        private TextBox tbTarget;
        private Label lblTarget;
    }
}
