namespace TTVTL_Nuppudega
{
    partial class XMLChooser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XMLChooser));
            this.Seletus = new System.Windows.Forms.RichTextBox();
            this.ENTER = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Seletus
            // 
            this.Seletus.AcceptsTab = true;
            this.Seletus.BackColor = System.Drawing.Color.DarkMagenta;
            this.Seletus.BulletIndent = 10;
            this.Seletus.DetectUrls = false;
            this.Seletus.ForeColor = System.Drawing.Color.Black;
            this.Seletus.Location = new System.Drawing.Point(16, 16);
            this.Seletus.Margin = new System.Windows.Forms.Padding(16);
            this.Seletus.MaximumSize = new System.Drawing.Size(150, 4);
            this.Seletus.MaxLength = 200;
            this.Seletus.MinimumSize = new System.Drawing.Size(150, 91);
            this.Seletus.Name = "Seletus";
            this.Seletus.ReadOnly = true;
            this.Seletus.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.Seletus.ShortcutsEnabled = false;
            this.Seletus.Size = new System.Drawing.Size(150, 91);
            this.Seletus.TabIndex = 0;
            this.Seletus.TabStop = false;
            this.Seletus.Text = "Vali sobilik XML fail. XML faile saab ka siia tirida.\n\nSelle järgi moodustatakse " +
    "TV toetuse süsteem.";
            // 
            // ENTER
            // 
            this.ENTER.BackColor = System.Drawing.Color.DarkMagenta;
            this.ENTER.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ENTER.Location = new System.Drawing.Point(16, 124);
            this.ENTER.Margin = new System.Windows.Forms.Padding(3, 3, 3, 16);
            this.ENTER.Name = "ENTER";
            this.ENTER.Size = new System.Drawing.Size(150, 23);
            this.ENTER.TabIndex = 0;
            this.ENTER.Text = "Valisin sobiva";
            this.ENTER.UseVisualStyleBackColor = false;
            this.ENTER.Click += new System.EventHandler(this.EnterClick);
            this.ENTER.KeyUp += new System.Windows.Forms.KeyEventHandler(this.EnterButtonUp);
            // 
            // XMLChooser
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Indigo;
            this.ClientSize = new System.Drawing.Size(184, 161);
            this.ControlBox = false;
            this.Controls.Add(this.ENTER);
            this.Controls.Add(this.Seletus);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "XMLChooser";
            this.Text = "Soovitud XML Faili Valik";
            this.TopMost = true;
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form2_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form2_DragEnter);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox Seletus;
        private System.Windows.Forms.Button ENTER;
    }
}