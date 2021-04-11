using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TTVTL_Nuppudega
{
    public partial class XMLChooser : Form
    {
        public string xml_file;
        public bool ticked = true;

        public XMLChooser()
        {
            InitializeComponent();
            Show();
            this.BackColor = Program.BackColour;
            Seletus.BackColor = Program.MenuBackColour;

            // Starting arguments for making RadioButtons
            var defaultRBLoc = new List<int>(2)
            {
                Seletus.Width + Seletus.Margin.Left + Seletus.Margin.Right,
                Seletus.Location.Y + 3
            };
            var currentRBLoc = new int[] { 0, 0 };
            defaultRBLoc.CopyTo(currentRBLoc);
            // So that controls in one column would be nice and orderly (Largest RadialButton width)
            var LRBW = 0;
            // Serves only to make the first button as the "default"
            var s = 5; // Spacing
            var HEIGHT = defaultRBLoc[1] + Seletus.Height + ENTER.Height;

            foreach (var file in Program.xml_files)
            {   // Dynamically creating RadioButtons
                var location = new Point(currentRBLoc[0], currentRBLoc[1]);
                var size = RButtonCreator(file.Split('\\').Last(), file, location);

                if (ticked)
                {
                    (Controls[Controls.Count - 1] as RadioButton).PerformClick();
                    ticked = false;
                }
                LRBW = Math.Max(size.Width, LRBW);
                currentRBLoc[1] = currentRBLoc[1] + size.Height + s;

                if (currentRBLoc[1] >= HEIGHT + s)
                {
                    currentRBLoc[0] += LRBW + s;
                    currentRBLoc[1] = defaultRBLoc[1];
                    LRBW = 0;
                }
            }
            //Controls.
        }
        private Size RButtonCreator(string text, string xml_location,
            Point location)
        {
            RadioButton drButton = new RadioButton
            {
                Location = location,
                Name = xml_location,
                Text = text,
                AutoSize = true
            };
            drButton.KeyPress += new KeyPressEventHandler(RButtonKeyPress);
            drButton.Click += new EventHandler(RButtonClick);
            drButton.Checked = ticked;

            Controls.Add(drButton);
            return drButton.Size;
        }
        private void RButtonClick(object sender, EventArgs e)
        {
            ENTER.Tag = (sender as RadioButton).Name;
        }
        private void EnterClick(object sender, EventArgs e)
        {
            EndForm(ENTER.Tag as string);
        }
        // KeyPress EVENTS
        private void RButtonKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13) { EndForm((sender as RadioButton).Name); }
        }
        private void EnterButtonUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13) { EndForm(ENTER.Tag as string); }
        }
        // KeyPress EVENTS END
        // DRAGGING Stuff into form
        private void Form2_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else { e.Effect = DragDropEffects.None; }
        }
        private void Form2_DragDrop(object sender, DragEventArgs e)
        {
            string[] fileList = null;
            try { fileList = (string[])e.Data.GetData(DataFormats.FileDrop, false); }

            catch (InvalidCastException exception)
            {
                if (exception.Source != null)
                    Console.WriteLine("InvalidCastException source: {0}",
                        exception.Source);
            }

            if (fileList[0].Contains(".xml")) { EndForm(fileList[0]); }
            else
            {
                MessageBox.Show("See pole xml fail.", "Teavitus",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        // DRAGGING Stuff into form END
        private void EndForm(string xml_loc)
        {
            Program.xml_files = new List<string>() { xml_loc };
            Close();
        }
    }
}