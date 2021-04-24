using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

namespace TTVTL_Nuppudega
{
    public class Valikud
    {
        private int width = 0;
        public int height = 0;
        public int FCI = 0; // First Control Index

        public TTVT Ttvt { get; }
        public string xpath { get; }
        public Valikud parent { get; }
        public bool verticality { get; }
        public List<Valikud> subList { get; set; }
        public Control control { get; set; }

        public Valikud(TTVT Ttvt, bool verticality, string xpath,
            Valikud parent = null)
        {
            this.Ttvt = Ttvt;
            this.verticality = verticality;
            this.xpath = xpath;
            this.parent = parent;

        }
        public bool ToggleVisibility() // Slow, high priority since it's used more
        {
            if (width == 0)
            {
                return false;
            }
            var action = subList[FCI].control.Visible ?
                (Action<Control>)((Control c) => { c.Visible = false; }) :
                ((Control c) => { c.Visible = true; });

            for (int i = FCI; i < subList.Count; i++) { action(subList[i].control); };
            return true;
        }
    }

    public class Options
    {
        public Control activeSubControl { get; set; } // Can be null

        public readonly TTVT mainForm;
        public readonly XmlNode currentNode;

        public readonly string name; // Can be null
        public readonly Control control;
        public readonly Options[] subOptions;

        public Options(TTVT mainForm, XmlNode currentNode)
        {
            this.mainForm = mainForm;
            this.currentNode = currentNode;

            this.name = currentNode.Attributes?["name"]?.InnerText;
            this.control = MakeControl();

            this.activeSubControl = null;
            this.subOptions = MakeSubOptions();
        }

        private Control MakeControl()
        {
            var type = this.currentNode.Name;
            Control output = null;

            if (this.name != null) //  && (type == "vvalik" || type == "hvalik")
            {
                Control btn; FlowLayoutPanel panel;

                if (type[0] == 'v')
                {
                    btn = new ButtonVertical(this, this.name);
                    panel = this.mainForm.VPanel;
                }
                else
                {
                    btn = new ButtonHorizontal(this, this.name);
                    panel = this.mainForm.HPanel;
                }
                panel.Controls.Add(btn);
                output = btn;
            }
            // I've planned to make the TV's center section as well

            return output;
        }

        private Options[] MakeSubOptions()
        {
            var subOptionsList = new List<Options>();
            foreach (XmlNode node in currentNode.ChildNodes)
            {
                subOptionsList.Add(new Options(mainForm, node));
                if (node.Attributes?["default"]?.InnerText == "1")
                    this.activeSubControl = subOptionsList.Last().control;
            }
            return subOptionsList.Count == 0 ? null :
                subOptionsList.ToArray();
        }

        public void ToggleSubOptionsVisibility()
        {
            var query = this.subOptions.Where(o => o.control != null);
            foreach (Options option in query)
                option.control.Visible = !option.control.Visible;
        }
    }


    public class OptionButton : Button
    {
        protected readonly Options Option;

        public OptionButton(Options callerOption, string name)
        {
            this.Option = callerOption;
            this.AutoSize = true;
            this.Visible = false;

            this.Text = name;
            this.Name = name;

            this.Font = new Font("Arial", 10, FontStyle.Bold);
            this.TextAlign = ContentAlignment.MiddleLeft;
            this.FlatStyle = FlatStyle.Flat;

            this.ForeColor = Color.White;
            this.BackColor = Program.BUnActiveColor;

            this.FlatAppearance.BorderSize = 3;
            this.FlatAppearance.BorderColor = Program.BUnActiveColor;

            this.FlatAppearance.MouseOverBackColor = Program.BActiveColor;
            this.FlatAppearance.MouseDownBackColor = Program.BSelectColor;
        }
    }
    public class ButtonVertical : OptionButton
    {
        public ButtonVertical(Options callerOption, string name)
            : base(callerOption, name)
        {
            // Makes it strecth from Left to Right
            this.Anchor = (AnchorStyles.Left | AnchorStyles.Right);

            // Gets a darker background when hovering over button with mouse
            // Used to give more feedback concerning the clickability of the button
            var mobc = this.FlatAppearance.MouseOverBackColor;
            var bc = this.BackColor;
            var I = 12;
            this.FlatAppearance.MouseOverBackColor = Color.FromArgb(
                Math.Abs(mobc.R - bc.R) / I,
                Math.Abs(mobc.G - bc.G) / I,
                Math.Abs(mobc.B - bc.G) / I);

            GotFocus += FocusReceived;
        }
        private void FocusReceived(object sender, EventArgs e)
        {
            var button = sender as Button;
            button.FlatAppearance.BorderColor = Program.BActiveColor;

            this.Option.ToggleSubOptionsVisibility();
            //Console.WriteLine(this.Option.);

            // previous Program.BUnActiveColor
        }
    }
    public class ButtonHorizontal : OptionButton
    {
        public ButtonHorizontal(Options callerOption, string name)
            : base(callerOption, name)
        {
            // Makes it strecth from Bottom to Top
            this.Anchor = (AnchorStyles.Bottom | AnchorStyles.Top);

            GotFocus += FocusReceived;
            LostFocus += FocusLost;
        }

        private void FocusLost(object sender, EventArgs e)
        {
            var button = sender as Button;

            button.BackColor = Program.BUnActiveColor;
            button.FlatAppearance.BorderColor = Program.BUnActiveColor;
        }

        private void FocusReceived(object sender, EventArgs e)
        {
            var button = sender as Button;

            button.BackColor = Program.BActiveColor;
            button.FlatAppearance.BorderColor = Program.BActiveColor;
        }
    }

}
