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

    public class Option
    {
        public readonly TTVT mainForm;
        public readonly Control control; // Can be null
        public Option activeSubOption { get; set; }

        private readonly Option Parent;
        private readonly Option[] subOptions;
        private readonly XmlNode currentNode;

        public Option(Option Parent, XmlNode currentNode, TTVT mainForm)
        {
            this.Parent = Parent;
            this.mainForm = mainForm;
            this.currentNode = currentNode;

            this.control = MakeControl();
            this.subOptions = MakeSubOptions();

            if (this.currentNode.Name == "vvalik")
                this.activeSubOption = this.subOptions.Where(
                    o => o.control != null).First();
        }


        private Control MakeControl()
        {
            var type = this.currentNode.Name;
            Control output = null;

            var name = currentNode.Attributes?["name"]?.InnerText;
            if (name != null)
            {
                Control btn; FlowLayoutPanel panel;
                if (type[0] == 'v')
                {
                    btn = new ButtonVertical(this, name);
                    panel = this.mainForm.VPanel;
                }
                else
                {
                    btn = new ButtonHorizontal(this, name);
                    panel = this.mainForm.HPanel;
                }
                panel.Controls.Add(btn);
                output = btn;
            }
            // I've planned to make the TV's center section as well

            return output;
        }
        private Option[] MakeSubOptions()
        {
            var subOptionsList = new List<Option>();
            foreach (XmlNode node in currentNode.ChildNodes)
            {
                subOptionsList.Add(new Option(this, node, mainForm));
                if (node.Attributes?["default"]?.InnerText == "1")
                    this.activeSubOption = subOptionsList.Last();
            }
            return subOptionsList.Count == 0 ? null :
                subOptionsList.ToArray();
        }
        public void ToggleSubOptionsVisibility()
        {
            var query = this.subOptions.Where(o => o.control != null);
            foreach (Option option in query)
                option.control.Visible = !option.control.Visible;
        }
    }


    public class OptionButton : Button
    {
        protected readonly Option Option;
        protected readonly Color TextColour = Color.White;
        protected readonly Color HoverColour = Color.Black;
        protected readonly Color ActiveColour = Color.LimeGreen;
        protected readonly Color UnActiveColour = Color.Black;


        public OptionButton(Option callerOption, string name)
        {
            this.Option = callerOption;
            this.AutoSize = true;
            this.Visible = false;

            this.Text = name;
            this.Name = name;

            this.Font = new Font("Arial", 10, FontStyle.Bold);
            this.TextAlign = ContentAlignment.MiddleLeft;
            this.FlatStyle = FlatStyle.Flat;

            this.ForeColor = this.TextColour;
            this.BackColor = this.UnActiveColour;

            this.FlatAppearance.BorderSize = 3;
            this.FlatAppearance.BorderColor = this.UnActiveColour;

            this.FlatAppearance.MouseOverBackColor = this.ActiveColour;
            this.FlatAppearance.MouseDownBackColor = this.HoverColour;
        }
    }
    public class ButtonVertical : OptionButton
    {
        public ButtonVertical(Option callerOption, string name)
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

            this.GotFocus += FocusReceived;
        }

        private void FocusReceived(object sender, EventArgs e)
        {
            void newFocus()
            {
                this.Option.ToggleSubOptionsVisibility();
                this.Option.activeSubOption.control.Focus();
                this.Option.mainForm.Vertical = this.Option;
            }

            var button = sender as Button;
            button.FlatAppearance.BorderColor = this.ActiveColour;

            var previous = this.Option.mainForm.Vertical;
            if (previous == null) newFocus();
            else if (previous != this.Option)
            {
                newFocus();
                (previous.control as Button)
                    .FlatAppearance.BorderColor = this.UnActiveColour;
                previous.ToggleSubOptionsVisibility();
            }
        }
    }
    public class ButtonHorizontal : OptionButton
    {
        public ButtonHorizontal(Option callerOption, string name)
            : base(callerOption, name)
        {
            // Makes it strecth from Bottom to Top
            this.Anchor = (AnchorStyles.Bottom | AnchorStyles.Top);

            this.GotFocus += FocusReceived;
            this.LostFocus += FocusLost;
        }

        private void FocusLost(object sender, EventArgs e)
        {
            var button = sender as Button;

            button.BackColor = this.UnActiveColour;
            button.FlatAppearance.BorderColor = this.UnActiveColour;
        }
        private void FocusReceived(object sender, EventArgs e)
        {
            this.Option.mainForm.Horizontal = this.Option;
            var button = sender as Button;

            button.BackColor = this.ActiveColour;
            button.FlatAppearance.BorderColor = this.ActiveColour;
        }
    }

}
