using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TTVTL_Nuppudega
{
    public partial class TTVT : Form
    {
        private void Next_Control(bool next, bool verticality)
        {
            var panel = verticality ? VPanel : HPanel;
            var focus = verticality ? VFocus : HFocus;
            panel.SelectNextControl(focus, next, true, false, true);
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool KeyChecking(Keys x) { return x == keyData; }
            var UP = new Keys[] { Keys.Up, Keys.W };
            var DOWN = new Keys[] { Keys.Down, Keys.S };
            var LEFT = new Keys[] { Keys.Left, Keys.A };
            var RIGHT = new Keys[] { Keys.Right, Keys.D };

            if (LEFT.Any(KeyChecking) || RIGHT.Any(KeyChecking))
            {
                Next_Control(RIGHT.Any(KeyChecking), false); return true;
            }
            else if (UP.Any(KeyChecking) || DOWN.Any(KeyChecking))
            {
                Next_Control(DOWN.Any(KeyChecking), true); return true;
            }
            else { return base.ProcessCmdKey(ref msg, keyData); }

        }
        private void MouseWheelScroll(object sender, MouseEventArgs e)
        {
            if (e.Delta != 0) { Next_Control(e.Delta < 0, true); }
        }
        private void VButtonFocusEnter(object sender, EventArgs e)
        {
            var button = sender as Button;
            button.FlatAppearance.BorderColor = Program.BActiveColor;

            Valikud valik = button.Tag as Valikud;
            if (VFocus != button)
            {
                try
                {
                    Valikud VFValik = VFocus.Tag as Valikud;
                    VFValik.ToggleVisibility();
                    VFocus.FlatAppearance.BorderColor = Program.BUnActiveColor;
                }
                catch (NullReferenceException) { }
                valik.ToggleVisibility();
            }

            valik.subList[valik.FCI].control.Focus();
            VFocus = button;
        }
        private void HButtonFocusEnter(object sender, EventArgs e)
        {
            var button = sender as Button;

            button.BackColor = Program.BActiveColor;
            button.FlatAppearance.BorderColor = Program.BActiveColor;

            HFocus = button;
        }
        private void HButtonFocusLeave(object sender, EventArgs e)
        {
            var button = sender as Button;

            button.BackColor = Program.BUnActiveColor;
            button.FlatAppearance.BorderColor = Program.BUnActiveColor;
        }
        private void HButtonKeyDown(object sender, KeyEventArgs e)
        {
            bool KeyChecking(Keys x) { return x == e.KeyCode; }
            var BACK = new Keys[] { Keys.Escape, Keys.Back };
            var vals = (sender as Button).Tag as Valikud;

            if (BACK.Any(KeyChecking))
            {
                Valikud gpa; Valikud gpasgpa; int iogpp; // Granparent, Granparents grandparent, index of grandparents parent
                try { gpa = vals.parent.parent; gpasgpa = gpa.parent.parent; }
                catch (NullReferenceException) { return; }

                gpasgpa.ToggleVisibility();
                iogpp = gpasgpa.subList.IndexOf(gpa.parent);
                gpasgpa.subList[iogpp].control.Focus();

                gpa.ToggleVisibility();
                e.Handled = true;
            }
        }
        /* 
         * Todo: Make enter button have the same effect as space and moushover event
         * Wasn't able to find an easy way to do it so thinking about trying to change
         * button colour to active colour while enter down and then undo it when up
        */
        private void HButtonClick(object sender, EventArgs e)
        {
            var vals = (sender as Button).Tag as Valikud;

            if (vals.ToggleVisibility())
            {
                vals.subList[vals.FCI].control.Focus();
                vals.parent.parent.ToggleVisibility();
            }
        }
    }
}
