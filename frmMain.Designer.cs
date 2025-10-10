namespace OXCFontConverter
{
    partial class frmMain
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lbTargetPngFileName = new System.Windows.Forms.Label();
            this.cmbFont = new System.Windows.Forms.ComboBox();
            this.grpboxXY = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.numOffsetY = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numOffsetX = new System.Windows.Forms.NumericUpDown();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtWords = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnConvert = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.grpboxXY.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numOffsetY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numOffsetX)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lbTargetPngFileName);
            this.splitContainer1.Panel1.Controls.Add(this.cmbFont);
            this.splitContainer1.Panel1.Controls.Add(this.grpboxXY);
            this.splitContainer1.Panel1.Controls.Add(this.cmbType);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.txtWords);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.btnConvert);
            this.splitContainer1.Size = new System.Drawing.Size(855, 450);
            this.splitContainer1.SplitterDistance = 210;
            this.splitContainer1.TabIndex = 0;
            // 
            // lbTargetPngFileName
            // 
            this.lbTargetPngFileName.AutoSize = true;
            this.lbTargetPngFileName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbTargetPngFileName.Font = new System.Drawing.Font("Calibri", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTargetPngFileName.Location = new System.Drawing.Point(557, 39);
            this.lbTargetPngFileName.Name = "lbTargetPngFileName";
            this.lbTargetPngFileName.Size = new System.Drawing.Size(152, 24);
            this.lbTargetPngFileName.TabIndex = 10;
            this.lbTargetPngFileName.Text = "FontBig_zh-TW.png";
            // 
            // cmbFont
            // 
            this.cmbFont.Font = new System.Drawing.Font("Calibri", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbFont.FormattingEnabled = true;
            this.cmbFont.Location = new System.Drawing.Point(55, 53);
            this.cmbFont.Name = "cmbFont";
            this.cmbFont.Size = new System.Drawing.Size(257, 30);
            this.cmbFont.TabIndex = 2;
            this.cmbFont.SelectedIndexChanged += new System.EventHandler(this.cmbFont_SelectedIndexChanged);
            // 
            // grpboxXY
            // 
            this.grpboxXY.Controls.Add(this.numOffsetX);
            this.grpboxXY.Controls.Add(this.label2);
            this.grpboxXY.Controls.Add(this.label4);
            this.grpboxXY.Controls.Add(this.numOffsetY);
            this.grpboxXY.Location = new System.Drawing.Point(318, 3);
            this.grpboxXY.Name = "grpboxXY";
            this.grpboxXY.Size = new System.Drawing.Size(141, 86);
            this.grpboxXY.TabIndex = 9;
            this.grpboxXY.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 10.8F);
            this.label4.Location = new System.Drawing.Point(6, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 22);
            this.label4.TabIndex = 3;
            this.label4.Text = "OffsetY";
            // 
            // numOffsetY
            // 
            this.numOffsetY.Font = new System.Drawing.Font("Calibri", 10.8F);
            this.numOffsetY.Location = new System.Drawing.Point(76, 52);
            this.numOffsetY.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numOffsetY.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            -2147483648});
            this.numOffsetY.Name = "numOffsetY";
            this.numOffsetY.Size = new System.Drawing.Size(50, 29);
            this.numOffsetY.TabIndex = 2;
            this.numOffsetY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numOffsetY.ValueChanged += new System.EventHandler(this.numOffset_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 10.8F);
            this.label2.Location = new System.Drawing.Point(6, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 22);
            this.label2.TabIndex = 1;
            this.label2.Text = "OffsetX";
            // 
            // numOffsetX
            // 
            this.numOffsetX.Font = new System.Drawing.Font("Calibri", 10.8F);
            this.numOffsetX.Location = new System.Drawing.Point(76, 19);
            this.numOffsetX.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numOffsetX.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            -2147483648});
            this.numOffsetX.Name = "numOffsetX";
            this.numOffsetX.Size = new System.Drawing.Size(50, 29);
            this.numOffsetX.TabIndex = 0;
            this.numOffsetX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numOffsetX.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numOffsetX.ValueChanged += new System.EventHandler(this.numOffset_ValueChanged);
            // 
            // cmbType
            // 
            this.cmbType.Font = new System.Drawing.Font("Calibri", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Items.AddRange(new object[] {
            "5:9",
            "5:7",
            "8:9",
            "16:16"});
            this.cmbType.Location = new System.Drawing.Point(55, 12);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(157, 30);
            this.cmbType.TabIndex = 7;
            this.cmbType.SelectedIndexChanged += new System.EventHandler(this.cmbType_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(7, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 22);
            this.label3.TabIndex = 6;
            this.label3.Text = "Type:";
            // 
            // txtWords
            // 
            this.txtWords.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtWords.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtWords.Font = new System.Drawing.Font("新細明體", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtWords.Location = new System.Drawing.Point(5, 101);
            this.txtWords.Multiline = true;
            this.txtWords.Name = "txtWords";
            this.txtWords.Size = new System.Drawing.Size(846, 106);
            this.txtWords.TabIndex = 3;
            this.txtWords.Text = "一丁七三下丈上丑丐不丙世丕且丘丞丟並丫中串丸凡丹主乃久么之尹乍乏乎乒乓乖乘乙九也乞乩乳乾亂了予事二于云井互五亙些亞";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 22);
            this.label1.TabIndex = 1;
            this.label1.Text = "Font:";
            // 
            // btnConvert
            // 
            this.btnConvert.Font = new System.Drawing.Font("Calibri", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConvert.Location = new System.Drawing.Point(465, 34);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(86, 34);
            this.btnConvert.TabIndex = 0;
            this.btnConvert.Text = "Convert";
            this.btnConvert.UseVisualStyleBackColor = true;
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(855, 450);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.Text = "OXC Font Converter";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.grpboxXY.ResumeLayout(false);
            this.grpboxXY.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numOffsetY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numOffsetX)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.ComboBox cmbFont;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtWords;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.GroupBox grpboxXY;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numOffsetX;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numOffsetY;
        private System.Windows.Forms.Label lbTargetPngFileName;
    }
}

