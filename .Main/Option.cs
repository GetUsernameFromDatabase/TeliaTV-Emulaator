using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

namespace TTVTL_Nuppudega
{
    public class Option
    {
        public readonly TTVT mainForm;
        public readonly Control control; // Can be null

        public Option activeSubOption { get; set; }
        public readonly Option Parent;

        private readonly Option[] subOptions;
        private readonly XmlNode currentNode;

        public Option(Option Parent, XmlNode currentNode, TTVT mainForm)
        {
            this.Parent = Parent;
            this.mainForm = mainForm;
            this.currentNode = currentNode;

            this.control = MakeControl();
            this.subOptions = MakeSubOptions();

            this.activeSubOption = this.subOptions?.Where(
                    o => o.control != null).FirstOrDefault();
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
            var query = this.subOptions?.Where(o => o.control != null)
                ?? new Option[] { };
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
                this.Option.mainForm.Vertical =
                    this.Option.Parent.activeSubOption = this.Option;
                this.Option.ToggleSubOptionsVisibility();
                this.Option.activeSubOption.control.Focus();
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

            this.Click += ButtonClick;

            this.GotFocus += FocusReceived;
            this.LostFocus += FocusLost;
        }

        /* 
         * Todo: Make enter button have the same effect as space and click event
         * 
         * Haven't found an easy way to do it so thinking about trying to change
         * button colour to active colour while enter down and then undo it when up
        */
        private void ButtonClick(object sender, EventArgs e)
        {
            if (this.Option.activeSubOption != null)
            {
                // Toggles current vertical panel
                this.Option.Parent.Parent.ToggleSubOptionsVisibility();
                // Sets up new vertical panel
                this.Option.ToggleSubOptionsVisibility();
                // Sets up new horizontal panel
                this.Option.activeSubOption.control.Focus();
            }
        }

        private void FocusLost(object sender, EventArgs e)
        {
            var button = sender as Button;

            button.BackColor = this.UnActiveColour;
            button.FlatAppearance.BorderColor = this.UnActiveColour;
        }
        private void FocusReceived(object sender, EventArgs e)
        {
            this.Option.mainForm.Horizontal = 
                this.Option.Parent.activeSubOption = this.Option;

            var button = sender as Button;
            button.BackColor = this.ActiveColour;
            button.FlatAppearance.BorderColor = this.ActiveColour;
        }
    }
}
