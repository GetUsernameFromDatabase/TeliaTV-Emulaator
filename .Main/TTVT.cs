using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace TTVTL_Nuppudega
{
    public partial class TTVT : Form
    {
        public XmlDocument XML = new XmlDocument();
        public Option Vertical = null;
        public Option Horizontal = null;

        public TTVT()
        {
            InitializeComponent();
            TeavitusMenu.BackColor = Program.MenuBackColour;
            ContentPanel.BackColor = Program.BackColour;

            Text = Program.name;
            BackColor = Program.BackColour;

            MouseWheel += new MouseEventHandler(MouseWheelScroll);
        }


        private void TTVT_Load(object sender, EventArgs e)
        {
            XML.Load(Program.xml_files[0]);
            var root = new Option(null,
                                  XML.DocumentElement.SelectSingleNode("/*"),
                                  this);
            root.ToggleSubOptionsVisibility();
            EndLoadingSCreen();
            root.activeSubOption.control.Focus();
        }
        private void EndLoadingSCreen()
        {
            SplashForm.CloseForm();
            WindowState = FormWindowState.Minimized; // Doesn't come on top otherwise
            WindowState = FormWindowState.Normal;
            this.Show(); GC.Collect();
        }
        private void MouseWheelScroll(object sender, MouseEventArgs e)
        {
            if (e.Delta != 0) { Next_Control(e.Delta < 0, true); }
        }
    }
}
