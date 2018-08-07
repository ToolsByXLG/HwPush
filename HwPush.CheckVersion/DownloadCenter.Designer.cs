namespace HwPush.CheckVersion
{
    partial class DownloadCenter
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DownloadCenter));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.skinProgressBar1 = new CCWin.SkinControl.SkinProgressBar();
            this.skinListBox1 = new CCWin.SkinControl.SkinListBox();
            this.skinButton1 = new CCWin.SkinControl.SkinButton();
            this.skinTextBox1 = new CCWin.SkinControl.SkinTextBox();
            this.skinTextBox2 = new CCWin.SkinControl.SkinTextBox();
            this.skinLabel1 = new CCWin.SkinControl.SkinLabel();
            this.skinLabel2 = new CCWin.SkinControl.SkinLabel();
            this.skinTextBox5 = new CCWin.SkinControl.SkinTextBox();
            this.skinLabel6 = new CCWin.SkinControl.SkinLabel();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // skinProgressBar1
            // 
            this.skinProgressBar1.Back = null;
            this.skinProgressBar1.BackColor = System.Drawing.Color.Transparent;
            this.skinProgressBar1.BarBack = null;
            this.skinProgressBar1.BarRadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.skinProgressBar1.ForeColor = System.Drawing.Color.Red;
            this.skinProgressBar1.FormatString = "{0:0.000000%}";
            this.skinProgressBar1.Location = new System.Drawing.Point(7, 663);
            this.skinProgressBar1.Name = "skinProgressBar1";
            this.skinProgressBar1.RadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.skinProgressBar1.Size = new System.Drawing.Size(501, 23);
            this.skinProgressBar1.TabIndex = 9;
            // 
            // skinListBox1
            // 
            this.skinListBox1.Back = null;
            this.skinListBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.skinListBox1.FormattingEnabled = true;
            this.skinListBox1.Location = new System.Drawing.Point(4, 28);
            this.skinListBox1.Name = "skinListBox1";
            this.skinListBox1.Size = new System.Drawing.Size(370, 628);
            this.skinListBox1.TabIndex = 10;
            this.skinListBox1.SelectedIndexChanged += new System.EventHandler(this.skinListBox1_SelectedIndexChanged);
            // 
            // skinButton1
            // 
            this.skinButton1.BackColor = System.Drawing.Color.Transparent;
            this.skinButton1.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinButton1.DownBack = null;
            this.skinButton1.Location = new System.Drawing.Point(378, 634);
            this.skinButton1.MouseBack = null;
            this.skinButton1.Name = "skinButton1";
            this.skinButton1.NormlBack = null;
            this.skinButton1.Size = new System.Drawing.Size(130, 23);
            this.skinButton1.TabIndex = 11;
            this.skinButton1.Text = "开始获取";
            this.skinButton1.UseVisualStyleBackColor = false;
            this.skinButton1.Click += new System.EventHandler(this.skinButton1_Click);
            // 
            // skinTextBox1
            // 
            this.skinTextBox1.BackColor = System.Drawing.Color.Transparent;
            this.skinTextBox1.DownBack = null;
            this.skinTextBox1.Icon = null;
            this.skinTextBox1.IconIsButton = false;
            this.skinTextBox1.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.skinTextBox1.IsPasswordChat = '\0';
            this.skinTextBox1.IsSystemPasswordChar = false;
            this.skinTextBox1.Lines = new string[] {
        "90000"};
            this.skinTextBox1.Location = new System.Drawing.Point(434, 28);
            this.skinTextBox1.Margin = new System.Windows.Forms.Padding(0);
            this.skinTextBox1.MaxLength = 32767;
            this.skinTextBox1.MinimumSize = new System.Drawing.Size(28, 28);
            this.skinTextBox1.MouseBack = null;
            this.skinTextBox1.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.skinTextBox1.Multiline = false;
            this.skinTextBox1.Name = "skinTextBox1";
            this.skinTextBox1.NormlBack = null;
            this.skinTextBox1.Padding = new System.Windows.Forms.Padding(5);
            this.skinTextBox1.ReadOnly = false;
            this.skinTextBox1.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.skinTextBox1.Size = new System.Drawing.Size(74, 28);
            // 
            // 
            // 
            this.skinTextBox1.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.skinTextBox1.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skinTextBox1.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.skinTextBox1.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.skinTextBox1.SkinTxt.Name = "BaseText";
            this.skinTextBox1.SkinTxt.Size = new System.Drawing.Size(64, 18);
            this.skinTextBox1.SkinTxt.TabIndex = 0;
            this.skinTextBox1.SkinTxt.Text = "90000";
            this.skinTextBox1.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.skinTextBox1.SkinTxt.WaterText = "";
            this.skinTextBox1.TabIndex = 13;
            this.skinTextBox1.Text = "90000";
            this.skinTextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.skinTextBox1.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.skinTextBox1.WaterText = "";
            this.skinTextBox1.WordWrap = true;
            // 
            // skinTextBox2
            // 
            this.skinTextBox2.BackColor = System.Drawing.Color.Transparent;
            this.skinTextBox2.DownBack = null;
            this.skinTextBox2.Icon = null;
            this.skinTextBox2.IconIsButton = false;
            this.skinTextBox2.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.skinTextBox2.IsPasswordChat = '\0';
            this.skinTextBox2.IsSystemPasswordChar = false;
            this.skinTextBox2.Lines = new string[] {
        "99000"};
            this.skinTextBox2.Location = new System.Drawing.Point(434, 60);
            this.skinTextBox2.Margin = new System.Windows.Forms.Padding(0);
            this.skinTextBox2.MaxLength = 32767;
            this.skinTextBox2.MinimumSize = new System.Drawing.Size(28, 28);
            this.skinTextBox2.MouseBack = null;
            this.skinTextBox2.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.skinTextBox2.Multiline = false;
            this.skinTextBox2.Name = "skinTextBox2";
            this.skinTextBox2.NormlBack = null;
            this.skinTextBox2.Padding = new System.Windows.Forms.Padding(5);
            this.skinTextBox2.ReadOnly = false;
            this.skinTextBox2.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.skinTextBox2.Size = new System.Drawing.Size(74, 28);
            // 
            // 
            // 
            this.skinTextBox2.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.skinTextBox2.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skinTextBox2.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.skinTextBox2.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.skinTextBox2.SkinTxt.Name = "BaseText";
            this.skinTextBox2.SkinTxt.Size = new System.Drawing.Size(64, 18);
            this.skinTextBox2.SkinTxt.TabIndex = 0;
            this.skinTextBox2.SkinTxt.Text = "99000";
            this.skinTextBox2.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.skinTextBox2.SkinTxt.WaterText = "";
            this.skinTextBox2.TabIndex = 13;
            this.skinTextBox2.Text = "99000";
            this.skinTextBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.skinTextBox2.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.skinTextBox2.WaterText = "";
            this.skinTextBox2.WordWrap = true;
            // 
            // skinLabel1
            // 
            this.skinLabel1.AutoSize = true;
            this.skinLabel1.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel1.BorderColor = System.Drawing.Color.White;
            this.skinLabel1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel1.Location = new System.Drawing.Point(377, 33);
            this.skinLabel1.Name = "skinLabel1";
            this.skinLabel1.Size = new System.Drawing.Size(43, 17);
            this.skinLabel1.TabIndex = 14;
            this.skinLabel1.Text = "id起始";
            // 
            // skinLabel2
            // 
            this.skinLabel2.AutoSize = true;
            this.skinLabel2.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel2.BorderColor = System.Drawing.Color.White;
            this.skinLabel2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel2.Location = new System.Drawing.Point(377, 65);
            this.skinLabel2.Name = "skinLabel2";
            this.skinLabel2.Size = new System.Drawing.Size(43, 17);
            this.skinLabel2.TabIndex = 14;
            this.skinLabel2.Text = "id结束";
            // 
            // skinTextBox5
            // 
            this.skinTextBox5.BackColor = System.Drawing.Color.Transparent;
            this.skinTextBox5.DownBack = null;
            this.skinTextBox5.Icon = null;
            this.skinTextBox5.IconIsButton = false;
            this.skinTextBox5.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.skinTextBox5.IsPasswordChat = '\0';
            this.skinTextBox5.IsSystemPasswordChar = false;
            this.skinTextBox5.Lines = new string[] {
        "500"};
            this.skinTextBox5.Location = new System.Drawing.Point(439, 603);
            this.skinTextBox5.Margin = new System.Windows.Forms.Padding(0);
            this.skinTextBox5.MaxLength = 32767;
            this.skinTextBox5.MinimumSize = new System.Drawing.Size(28, 28);
            this.skinTextBox5.MouseBack = null;
            this.skinTextBox5.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.skinTextBox5.Multiline = false;
            this.skinTextBox5.Name = "skinTextBox5";
            this.skinTextBox5.NormlBack = null;
            this.skinTextBox5.Padding = new System.Windows.Forms.Padding(5);
            this.skinTextBox5.ReadOnly = false;
            this.skinTextBox5.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.skinTextBox5.Size = new System.Drawing.Size(74, 28);
            // 
            // 
            // 
            this.skinTextBox5.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.skinTextBox5.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skinTextBox5.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.skinTextBox5.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.skinTextBox5.SkinTxt.Name = "BaseText";
            this.skinTextBox5.SkinTxt.Size = new System.Drawing.Size(64, 18);
            this.skinTextBox5.SkinTxt.TabIndex = 0;
            this.skinTextBox5.SkinTxt.Text = "500";
            this.skinTextBox5.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.skinTextBox5.SkinTxt.WaterText = "";
            this.skinTextBox5.TabIndex = 13;
            this.skinTextBox5.Text = "500";
            this.skinTextBox5.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.skinTextBox5.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.skinTextBox5.WaterText = "";
            this.skinTextBox5.WordWrap = true;
            // 
            // skinLabel6
            // 
            this.skinLabel6.AutoSize = true;
            this.skinLabel6.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel6.BorderColor = System.Drawing.Color.White;
            this.skinLabel6.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel6.Location = new System.Drawing.Point(382, 614);
            this.skinLabel6.Name = "skinLabel6";
            this.skinLabel6.Size = new System.Drawing.Size(44, 17);
            this.skinLabel6.TabIndex = 14;
            this.skinLabel6.Text = "线程数";
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // DownloadCenter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(517, 691);
            this.Controls.Add(this.skinLabel2);
            this.Controls.Add(this.skinLabel6);
            this.Controls.Add(this.skinLabel1);
            this.Controls.Add(this.skinTextBox2);
            this.Controls.Add(this.skinTextBox5);
            this.Controls.Add(this.skinTextBox1);
            this.Controls.Add(this.skinButton1);
            this.Controls.Add(this.skinListBox1);
            this.Controls.Add(this.skinProgressBar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "DownloadCenter";
            this.Text = "获取升级版本";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LookupVersionALL_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolTip toolTip1;
        private CCWin.SkinControl.SkinProgressBar skinProgressBar1;
        private CCWin.SkinControl.SkinListBox skinListBox1;
        private CCWin.SkinControl.SkinButton skinButton1;
        private CCWin.SkinControl.SkinTextBox skinTextBox1;
        private CCWin.SkinControl.SkinTextBox skinTextBox2;
        private CCWin.SkinControl.SkinLabel skinLabel1;
        private CCWin.SkinControl.SkinLabel skinLabel2;
        private CCWin.SkinControl.SkinTextBox skinTextBox5;
        private CCWin.SkinControl.SkinLabel skinLabel6;
        private System.Windows.Forms.ImageList imageList1;
    }
}

