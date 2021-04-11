using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace TTVTL_Nuppudega
{
    static class Program
    {
        public static List<string> xml_files = new List<string>();
        public static readonly string name = "Telia TV Toetuse Lihtsustaja";
        // Larger stuff colours
        public static readonly Color BackColour = Color.Indigo;
        public static readonly Color MenuBackColour = Color.DarkMagenta;
        // Button Colours
        public static readonly Color BSelectColor = Color.ForestGreen;
        public static readonly Color BActiveColor = Color.LimeGreen;
        public static readonly Color BUnActiveColor = Color.Black;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]


        static void Main()
        {
            var FilesThere = new DirectoryInfo(".");

            foreach (FileInfo ini in FilesThere.GetFiles("*.xml"))
            {
                xml_files.Add(ini.FullName);
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (xml_files.Count == 0)
            {
                MessageBox.Show("\"TeliaTVStruktuur.xml\" on puudu", "XML Fail Puudu");
                Application.Exit();
            }
            else if (xml_files.Count > 1)
            {
                Application.Run(new XMLChooser());
            }
            SplashForm.ShowSplashScreen();
            Application.Run(new TTVT());
        }
    }
}
