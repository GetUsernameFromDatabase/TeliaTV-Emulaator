using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TTVTL_Nuppudega
{
    public partial class XMLChooser : Form
    {
        private class RadioButtonPanel: TableLayoutPanel
        {
            public RadioButtonPanel(XMLChooser parent, int rBtnHeight)
            {
                this.AutoSize = true;
                this.GrowStyle = TableLayoutPanelGrowStyle.AddColumns;

                var refObj = parent.Seletus;
                this.Location = new Point(refObj.Right + refObj.Margin.Right, refObj.Top);

                this.Height = parent.Height - refObj.Top;
                this.RowCount = this.Height / rBtnHeight;
            }
            public void MakeFirstButtonActive()
            {
                var btn = this.Controls[0] as XMLRadioButton;
                btn.Checked = true;
                btn.PerformClick();
            }
        }

        private class XMLRadioButton : RadioButton
        {
            public XMLChooser Caller { get; }

            public XMLRadioButton(XMLChooser parent, string xmlLocation)
            {
                this.Caller = parent;

                this.AutoSize = true;
                this.Name = xmlLocation;
                this.Text = xmlLocation.Split('\\').Last();
                this.ForeColor = Color.WhiteSmoke;

                Click += new EventHandler(this.ClickEvent);
                KeyPress += new KeyPressEventHandler(this.KeyPressEvent);
            }

            private void ClickEvent(object sender, EventArgs e)
            {
                Caller.ENTER.Tag = (sender as RadioButton).Name;
            }
            private void KeyPressEvent(object sender, KeyPressEventArgs e)
            {
                if (e.KeyChar == (char)13) 
                    Caller.EndForm((sender as RadioButton).Name);
            }
        }

        public XMLChooser()
        {
            InitializeComponent();
            this.BackColor = Program.BackColour;
            Seletus.BackColor = Program.MenuBackColour;

            var radioButtons = new RadioButtonPanel(this,
                new XMLRadioButton(this, "").Height);
            this.Controls.Add(radioButtons);

            foreach (var file in Program.xml_files)
                radioButtons.Controls.Add(new XMLRadioButton(this, file));
            radioButtons.MakeFirstButtonActive();

            Show();
        }
        
        private void EnterClick(object sender, EventArgs e)
        {
            EndForm(ENTER.Tag as string);
        }
        private void EnterButtonUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13) { EndForm(ENTER.Tag as string); }
        }
        private void Form2_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else e.Effect = DragDropEffects.None;
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
        private void EndForm(string xml_loc)
        {
            Program.xml_files = new List<string>() { xml_loc };
            Close();
        }
    }
}