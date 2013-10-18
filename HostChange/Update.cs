using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HostChange
{
    public partial class Update : Form
    {
        public Update()
        {
            InitializeComponent();
        }
        public Update(string version)
        {
            InitializeComponent();
            UpdateLable.Text = "New Version Detected. \nVersion: " + version;
            linkLabel1.Text = "Click Here To Download New Version.";
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/Catofes/HostChange/raw/master/HostChange/bin/Release/HostChange.exe");
        }
    }
}
