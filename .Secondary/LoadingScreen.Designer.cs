namespace TTVTL_Nuppudega
{
    partial class LoadingScreen
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
            this.TEKST = new System.Windows.Forms.Label();
            this.LAADIMINE = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.LAADIMINE)).BeginInit();
            this.SuspendLayout();
            // 
            // TEKST
            // 
            this.TEKST.Dock = System.Windows.Forms.DockStyle.Top;
            this.TEKST.Font = new System.Drawing.Font("Gadugi", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TEKST.ForeColor = System.Drawing.Color.LightGray;
            this.TEKST.Location = new System.Drawing.Point(0, 0);
            this.TEKST.Name = "TEKST";
            this.TEKST.Size = new System.Drawing.Size(410, 111);
            this.TEKST.TabIndex = 0;
            this.TEKST.Text = "Programm laeb\r\nVõib võtta minut\r\n";
            this.TEKST.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.TEKST.UseWaitCursor = true;
            // 
            // LAADIMINE
            // 
            this.LAADIMINE.Dock = System.Windows.Forms.DockStyle.Top;
            this.LAADIMINE.Image = global::TTVTL_Nuppudega.Properties.Resources.Loading_Icon;
            this.LAADIMINE.InitialImage = global::TTVTL_Nuppudega.Properties.Resources.Loading_Icon;
            this.LAADIMINE.Location = new System.Drawing.Point(0, 111);
            this.LAADIMINE.Name = "LAADIMINE";
            this.LAADIMINE.Size = new System.Drawing.Size(410, 74);
            this.LAADIMINE.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.LAADIMINE.TabIndex = 1;
            this.LAADIMINE.TabStop = false;
            this.LAADIMINE.UseWaitCursor = true;
            // 
            // LoadingScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.ClientSize = new System.Drawing.Size(410, 218);
            this.Controls.Add(this.LAADIMINE);
            this.Controls.Add(this.TEKST);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "LoadingScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Changes during runtime";
            this.UseWaitCursor = true;
            ((System.ComponentModel.ISupportInitialize)(this.LAADIMINE)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label TEKST;
        private System.Windows.Forms.PictureBox LAADIMINE;
    }
}