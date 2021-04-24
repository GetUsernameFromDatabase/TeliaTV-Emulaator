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
        public Options Vertical = null;
        public Options Horizontal = null;

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
            Vertical = new Options(this,
                XML.DocumentElement.SelectSingleNode("/*")
                );
            Vertical.ToggleSubOptionsVisibility();

            EndLoadingSCreen();
        }
        private void EndLoadingSCreen()
        {
            SplashForm.CloseForm();
            WindowState = FormWindowState.Minimized; // Doesn't come on top otherwise
            WindowState = FormWindowState.Normal;
            Show(); GC.Collect();
        }
    }
}
