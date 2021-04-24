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
        // Main Colours
        public static readonly Color BackColour = Color.Indigo;
        public static readonly Color MenuBackColour = Color.DarkMagenta;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]

        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var currentDirectory = new DirectoryInfo(".");
            foreach (FileInfo xml in currentDirectory.GetFiles("*.xml"))
                xml_files.Add(xml.FullName);

            if (xml_files.Count == 0)
            {
                MessageBox.Show("\"TeliaTVStruktuur.xml\" on puudu", "XML Fail Puudu");
                Environment.Exit(1);
            }
            else if (xml_files.Count > 1)
                Application.Run(new XMLChooser());

            SplashForm.ShowSplashScreen();
            Application.Run(new TTVT());
        }
    }
}
