namespace HwPush.CheckVersion
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.版本扫描器ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.版本1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.版本2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.版本3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.版本4ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.toolStripContainer2 = new System.Windows.Forms.ToolStripContainer();
            this.menuStrip1.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.toolStripContainer2.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.版本扫描器ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(150, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 版本扫描器ToolStripMenuItem
            // 
            this.版本扫描器ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.版本1ToolStripMenuItem,
            this.版本2ToolStripMenuItem,
            this.版本3ToolStripMenuItem,
            this.版本4ToolStripMenuItem});
            this.版本扫描器ToolStripMenuItem.Name = "版本扫描器ToolStripMenuItem";
            this.版本扫描器ToolStripMenuItem.Size = new System.Drawing.Size(80, 21);
            this.版本扫描器ToolStripMenuItem.Text = "版本扫描器";
            // 
            // 版本1ToolStripMenuItem
            // 
            this.版本1ToolStripMenuItem.Name = "版本1ToolStripMenuItem";
            this.版本1ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.版本1ToolStripMenuItem.Text = "版本1";
            this.版本1ToolStripMenuItem.Click += new System.EventHandler(this.版本1ToolStripMenuItem_Click);
            // 
            // 版本2ToolStripMenuItem
            // 
            this.版本2ToolStripMenuItem.Name = "版本2ToolStripMenuItem";
            this.版本2ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.版本2ToolStripMenuItem.Text = "版本2";
            this.版本2ToolStripMenuItem.Click += new System.EventHandler(this.版本2ToolStripMenuItem_Click);
            // 
            // 版本3ToolStripMenuItem
            // 
            this.版本3ToolStripMenuItem.Name = "版本3ToolStripMenuItem";
            this.版本3ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.版本3ToolStripMenuItem.Text = "版本3";
            this.版本3ToolStripMenuItem.Click += new System.EventHandler(this.版本3ToolStripMenuItem_Click);
            // 
            // 版本4ToolStripMenuItem
            // 
            this.版本4ToolStripMenuItem.Name = "版本4ToolStripMenuItem";
            this.版本4ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.版本4ToolStripMenuItem.Text = "版本4";
            this.版本4ToolStripMenuItem.Click += new System.EventHandler(this.版本4ToolStripMenuItem_Click);
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(150, 150);
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(150, 175);
            this.toolStripContainer1.TabIndex = 2;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer2
            // 
            // 
            // toolStripContainer2.ContentPanel
            // 
            this.toolStripContainer2.ContentPanel.Size = new System.Drawing.Size(150, 150);
            this.toolStripContainer2.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer2.Name = "toolStripContainer2";
            this.toolStripContainer2.Size = new System.Drawing.Size(150, 175);
            this.toolStripContainer2.TabIndex = 3;
            this.toolStripContainer2.Text = "toolStripContainer2";
            // 
            // toolStripContainer2.TopToolStripPanel
            // 
            this.toolStripContainer2.TopToolStripPanel.Controls.Add(this.menuStrip1);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(105, 104);
            this.Controls.Add(this.toolStripContainer2);
            this.Controls.Add(this.toolStripContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Main_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.toolStripContainer2.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer2.TopToolStripPanel.PerformLayout();
            this.toolStripContainer2.ResumeLayout(false);
            this.toolStripContainer2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 版本扫描器ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 版本1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 版本2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 版本3ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 版本4ToolStripMenuItem;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStripContainer toolStripContainer2;
    }
}