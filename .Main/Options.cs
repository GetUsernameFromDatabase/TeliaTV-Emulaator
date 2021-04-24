using System;
using System.Collections.Generic;
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

            subList = SubNodeSubscription(Ttvt.XML.SelectNodes(xpath + "/*"));
            if (subList.Count > 0) { StartSizeChange(); }
        }

        private void StartSizeChange()
        {
            var widths = new List<int>();
            var heights = new List<int>();

            for (var i = 0; i < subList.Count; i++)
            {
                var c = subList[i].control;
                if (c == null)
                {
                    FCI = i + 1;
                    widths.Add(0);
                    heights.Add(0);
                    continue;
                }
                widths.Add(c.Width);
                heights.Add(c.Height);
            }
            width = widths.Max();
            height = heights.Max();
        }
        private List<Valikud> SubNodeSubscription(XmlNodeList nodes) // Slow, low priority
        {
            var SubNodes = new List<Valikud>();
            foreach (XmlNode node in nodes)
            {
                bool vert = control == null ? verticality : !verticality;
                SubNodes.Add(new Valikud(Ttvt, vert, GetXPathToNode(node), this));
            }
            return SubNodes;
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
        public static string GetXPathToNode(XmlNode node)
        { // https://stackoverflow.com/questions/241238/how-to-get-xpath-from-an-xmlnode-instance
            if (node.NodeType == XmlNodeType.Attribute)
            {
                // attributes have an OwnerElement, not a ParentNode; also they have             
                // to be matched by name, not found by position             
                return String.Format("{0}/@{1}", GetXPathToNode(((XmlAttribute)node).OwnerElement), node.Name);
            }
            if (node.ParentNode == null)
            {
                // the only node with no parent is the root node, which has no path
                return "";
            }

            // Get the Index
            int indexInParent = 1;
            XmlNode siblingNode = node.PreviousSibling;
            // Loop thru all Siblings
            while (siblingNode != null)
            {
                // Increase the Index if the Sibling has the same Name
                if (siblingNode.Name == node.Name)
                {
                    indexInParent++;
                }
                siblingNode = siblingNode.PreviousSibling;
            }

            // the path to a node is the path to its parent, plus "/node()[n]", where n is its position among its siblings.         
            return String.Format("{0}/{1}[{2}]", GetXPathToNode(node.ParentNode), node.Name, indexInParent);
        }
    }

    public class Options
    {
        public readonly TTVT mainForm;
        public readonly string name; // Can be null
        public readonly XmlNode currentNode;
        public readonly Control control;
        public readonly Options[] subOptions;

        public XmlNode activeSubNode { get; set; }

        public Options(TTVT mainForm, XmlNode currentNode)
        {
            this.mainForm = mainForm;
            this.currentNode = currentNode;

            this.name = currentNode.Attributes?["name"]?.InnerText;
            this.control = MakeControl();

            this.activeSubNode = null;
            this.subOptions = MakeSubOptions();
        }

        private Control MakeControl()
        {
            var type = this.currentNode.Name;
            Control output = null;

            if ((type == "vvalik" || type == "hvalik") && this.name != null)
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
                    this.activeSubNode = node;
            }
            return subOptionsList.Count == 0 ? null :
                subOptionsList.ToArray();
        }

        public void ToggleSubOptionsVisibility()
        {
            var query = this.subOptions.AsParallel().Where(o => o.control != null);
            foreach (var option in query)
                option.control.Visible = !option.control.Visible;
        }
    }


    public class OptionButton : Button
    {
        public OptionButton(Options callerOption, string name)
        {
            this.AutoSize = true;
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

            this.Tag = callerOption;
            this.Visible = false;
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
