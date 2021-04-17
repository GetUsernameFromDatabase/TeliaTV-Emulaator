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
        public Button VFocus = null;
        public Button HFocus = null;

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
            var AlgMenüü = new Valikud(this, true, "/root");

            foreach (Control panel in new List<Control> { VPanel, HPanel })
            {
                foreach (Button button in panel.Controls) { button.Hide(); }
            }
            AlgMenüü.ToggleVisibility();
            HPanel.Padding = new Padding(0, 15, 0, AlgMenüü.height / 4 + 15);
            EndLoadingSCreen();
        }
        private void EndLoadingSCreen()
        {
            SplashForm.CloseForm();
            WindowState = FormWindowState.Minimized;
            WindowState = FormWindowState.Normal;
            Show(); GC.Collect();
        }
        // Button Creator START
        private Button GeneralButtonCreator(string name)
        {
            var dButton = new Button
            {
                Text = name,
                Name = name,
                Font = new Font("Arial", 10, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft,
                BackColor = Program.BUnActiveColor,
                ForeColor = Color.White,
                AutoSize = true,

                FlatStyle = FlatStyle.Flat
            };
            dButton.FlatAppearance.BorderSize = 3;
            dButton.FlatAppearance.BorderColor = Program.BUnActiveColor;
            dButton.FlatAppearance.MouseOverBackColor = Program.BActiveColor;
            dButton.FlatAppearance.MouseDownBackColor = Program.BSelectColor;

            return dButton;
        }
        public Button VButtonCreator(string name, Valikud valik)
        {
            var dButton = GeneralButtonCreator(name);
            dButton.GotFocus += new EventHandler(VButtonFocusEnter);
            dButton.Tag = valik;

            var mobc = dButton.FlatAppearance.MouseOverBackColor;
            var bc = dButton.BackColor;
            var I = 12;
            dButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(
                Math.Abs(mobc.R - bc.R) / I,
                Math.Abs(mobc.G - bc.G) / I,
                Math.Abs(mobc.B - bc.G) / I);

            VPanel.Controls.Add(dButton); return dButton;
        }
        public Button HButtonCreator(string name, Valikud valik)
        {
            var dButton = GeneralButtonCreator(name);
            dButton.Click += new EventHandler(HButtonClick);
            dButton.GotFocus += new EventHandler(HButtonFocusEnter);
            dButton.LostFocus += new EventHandler(HButtonFocusLeave);
            dButton.KeyDown += new KeyEventHandler(HButtonKeyDown);
            dButton.Tag = valik;

            HPanel.Controls.Add(dButton); return dButton;
        }
        // Button Creator END
    }
}
