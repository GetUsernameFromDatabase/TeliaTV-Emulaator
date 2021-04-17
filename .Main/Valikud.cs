using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
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

            this.control = ControlMaker(xpath, this);
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
            UnifySize();
        }
        private Control ControlMaker(string path, Valikud valik)
        {
            Func<string, Valikud, Control> VerticalButton = (string x, Valikud y) =>
                { return Ttvt.VButtonCreator(x, y); };
            Func<string, Valikud, Control> HorizontalButton = (string x, Valikud y) =>
                { return Ttvt.HButtonCreator(x, y); };

            var Maker = verticality ? VerticalButton : HorizontalButton;
            try { var name = Ttvt.XML.SelectSingleNode(path).Attributes["name"].InnerText; return Maker(name, valik); }
            catch (NullReferenceException) { return null; }

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
        private void UnifySize()
        {
            if (width == 0)
            {
                return;
            }
            Action<Control> WIDTH = (Control c) => { c.Width = width; };
            Action<Control> HEIGHT = (Control c) => { c.Height = height; };

            var action = subList[0].verticality ? WIDTH : HEIGHT;
            for (int i = FCI; i < subList.Count; i++)
            {
                action(subList[i].control);
            }
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
}
