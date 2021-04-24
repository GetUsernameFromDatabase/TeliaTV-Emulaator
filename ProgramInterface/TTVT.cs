using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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

        private void Next_Control(bool next, bool verticality)
        {
            var panel = verticality ? VPanel : HPanel;
            var focus = verticality ? Vertical.control : Horizontal.control;
            panel.SelectNextControl(focus, next, true, false, true);
        }
        private void MouseWheelScroll(object sender, MouseEventArgs e)
        {
            if (e.Delta != 0) { Next_Control(e.Delta < 0, true); }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool KeyChecking(Keys x) { return x == keyData; }
            var BACK = new Keys[] { Keys.Escape, Keys.Back };

            var UP = new Keys[] { Keys.Up, Keys.W };
            var DOWN = new Keys[] { Keys.Down, Keys.S };

            var LEFT = new Keys[] { Keys.Left, Keys.A };
            var RIGHT = new Keys[] { Keys.Right, Keys.D };

            if (LEFT.Any(KeyChecking) || RIGHT.Any(KeyChecking))
            {
                Next_Control(RIGHT.Any(KeyChecking), false);
                return true;
            }
            else if (UP.Any(KeyChecking) || DOWN.Any(KeyChecking))
            {
                Next_Control(DOWN.Any(KeyChecking), true); 
                return true;
            }
            else if (BACK.Any(KeyChecking) && this.Vertical.Parent.Parent != null)
            {
                var prevVertOpt = this.Vertical.Parent.Parent.Parent;
                prevVertOpt.ToggleSubOptionsVisibility();

                this.Vertical.Parent.ToggleSubOptionsVisibility();
                prevVertOpt.activeSubOption.control.Focus();
                return true;
            }
            else { return base.ProcessCmdKey(ref msg, keyData); }
        }
    }
}
