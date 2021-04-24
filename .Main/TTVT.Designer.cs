namespace TTVTL_Nuppudega
{
    partial class TTVT
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TTVT));
            this.TeavitusMenu = new System.Windows.Forms.MenuStrip();
            this.Info = new System.Windows.Forms.ToolStripMenuItem();
            this.ABI = new System.Windows.Forms.ToolStripMenuItem();
            this.Navigatsioon = new System.Windows.Forms.ToolStripMenuItem();
            this.Sisenemine = new System.Windows.Forms.ToolStripMenuItem();
            this.väljumineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.VERSIOON = new System.Windows.Forms.ToolStripMenuItem();
            this.Edition = new System.Windows.Forms.ToolStripMenuItem();
            this.TEGIJA = new System.Windows.Forms.ToolStripMenuItem();
            this.VPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.HPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.ContentPanel = new System.Windows.Forms.TableLayoutPanel();
            this.TeavitusMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // TeavitusMenu
            // 
            this.TeavitusMenu.BackColor = System.Drawing.Color.DarkMagenta;
            this.TeavitusMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.TeavitusMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Info});
            this.TeavitusMenu.Location = new System.Drawing.Point(0, 0);
            this.TeavitusMenu.Name = "TeavitusMenu";
            this.TeavitusMenu.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.TeavitusMenu.Size = new System.Drawing.Size(1008, 24);
            this.TeavitusMenu.TabIndex = 99;
            this.TeavitusMenu.Text = "menuStrip1";
            // 
            // Info
            // 
            this.Info.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ABI,
            this.toolStripSeparator1,
            this.VERSIOON,
            this.TEGIJA});
            this.Info.Name = "Info";
            this.Info.Size = new System.Drawing.Size(40, 20);
            this.Info.Text = "Info";
            // 
            // ABI
            // 
            this.ABI.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Navigatsioon,
            this.Sisenemine,
            this.väljumineToolStripMenuItem});
            this.ABI.Image = ((System.Drawing.Image)(resources.GetObject("ABI.Image")));
            this.ABI.Name = "ABI";
            this.ABI.ShowShortcutKeys = false;
            this.ABI.Size = new System.Drawing.Size(174, 22);
            this.ABI.Text = "ABI";
            // 
            // Navigatsioon
            // 
            this.Navigatsioon.Image = ((System.Drawing.Image)(resources.GetObject("Navigatsioon.Image")));
            this.Navigatsioon.Name = "Navigatsioon";
            this.Navigatsioon.ShowShortcutKeys = false;
            this.Navigatsioon.Size = new System.Drawing.Size(201, 34);
            this.Navigatsioon.Text = "Navigatsioon\r\n←↑↓→  | WASD";
            // 
            // Sisenemine
            // 
            this.Sisenemine.Image = ((System.Drawing.Image)(resources.GetObject("Sisenemine.Image")));
            this.Sisenemine.Name = "Sisenemine";
            this.Sisenemine.Size = new System.Drawing.Size(201, 34);
            this.Sisenemine.Text = "Sisenemine\r\nENTER | Hiire Klõps";
            // 
            // väljumineToolStripMenuItem
            // 
            this.väljumineToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("väljumineToolStripMenuItem.Image")));
            this.väljumineToolStripMenuItem.Name = "väljumineToolStripMenuItem";
            this.väljumineToolStripMenuItem.Size = new System.Drawing.Size(201, 34);
            this.väljumineToolStripMenuItem.Text = "Väljumine\r\nEsc | ⇐ Tagasilükkeklahv";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(171, 6);
            // 
            // VERSIOON
            // 
            this.VERSIOON.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Edition});
            this.VERSIOON.Name = "VERSIOON";
            this.VERSIOON.ShowShortcutKeys = false;
            this.VERSIOON.Size = new System.Drawing.Size(174, 22);
            this.VERSIOON.Text = "Versioon: 0.8";
            // 
            // Edition
            // 
            this.Edition.Name = "Edition";
            this.Edition.Size = new System.Drawing.Size(150, 22);
            this.Edition.Text = "Button Edition";
            // 
            // TEGIJA
            // 
            this.TEGIJA.Name = "TEGIJA";
            this.TEGIJA.ShowShortcutKeys = false;
            this.TEGIJA.Size = new System.Drawing.Size(174, 22);
            this.TEGIJA.Text = "Tegija: Ryan Kruberg";
            // 
            // VPanel
            // 
            this.VPanel.AutoScroll = true;
            this.VPanel.AutoSize = true;
            this.VPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.VPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.VPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.VPanel.Location = new System.Drawing.Point(0, 24);
            this.VPanel.MinimumSize = new System.Drawing.Size(20, 0);
            this.VPanel.Name = "VPanel";
            this.VPanel.Padding = new System.Windows.Forms.Padding(5, 0, 20, 0);
            this.VPanel.Size = new System.Drawing.Size(25, 513);
            this.VPanel.TabIndex = 5;
            this.VPanel.TabStop = true;
            this.VPanel.WrapContents = false;
            // 
            // HPanel
            // 
            this.HPanel.AutoScroll = true;
            this.HPanel.AutoSize = true;
            this.HPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.HPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.HPanel.Location = new System.Drawing.Point(25, 512);
            this.HPanel.MinimumSize = new System.Drawing.Size(0, 20);
            this.HPanel.Name = "HPanel";
            this.HPanel.Padding = new System.Windows.Forms.Padding(0, 5, 0, 20);
            this.HPanel.Size = new System.Drawing.Size(983, 25);
            this.HPanel.TabIndex = 6;
            this.HPanel.TabStop = true;
            this.HPanel.WrapContents = false;
            // 
            // ContentPanel
            // 
            this.ContentPanel.ColumnCount = 2;
            this.ContentPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.ContentPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.ContentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ContentPanel.Location = new System.Drawing.Point(25, 24);
            this.ContentPanel.Name = "ContentPanel";
            this.ContentPanel.RowCount = 2;
            this.ContentPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.ContentPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.ContentPanel.Size = new System.Drawing.Size(983, 488);
            this.ContentPanel.TabIndex = 100;
            // 
            // TTVT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.Indigo;
            this.ClientSize = new System.Drawing.Size(1008, 537);
            this.Controls.Add(this.ContentPanel);
            this.Controls.Add(this.HPanel);
            this.Controls.Add(this.VPanel);
            this.Controls.Add(this.TeavitusMenu);
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "TTVT";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Changes during runtime";
            this.Load += new System.EventHandler(this.TTVT_Load);
            this.TeavitusMenu.ResumeLayout(false);
            this.TeavitusMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip TeavitusMenu;
        public System.Windows.Forms.FlowLayoutPanel VPanel;
        public System.Windows.Forms.FlowLayoutPanel HPanel;
        private System.Windows.Forms.TableLayoutPanel ContentPanel;
        private System.Windows.Forms.ToolStripMenuItem Info;
        private System.Windows.Forms.ToolStripMenuItem VERSIOON;
        private System.Windows.Forms.ToolStripMenuItem Edition;
        private System.Windows.Forms.ToolStripMenuItem TEGIJA;
        private System.Windows.Forms.ToolStripMenuItem ABI;
        private System.Windows.Forms.ToolStripMenuItem Navigatsioon;
        private System.Windows.Forms.ToolStripMenuItem Sisenemine;
        private System.Windows.Forms.ToolStripMenuItem väljumineToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}

