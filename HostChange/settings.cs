using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;

namespace HostChange
{
    public delegate void SaveChangeDelegate(bool if_custom_enable,string custom_URL_in);
    public partial class settings : Form
    {
        public settings()
        {
            InitializeComponent();
        }

        private void settings_Load(object sender, EventArgs e)
        {
            HostChange h = new HostChange();
            custom_enable.Checked = h.maincore.data.custom_enable;
            custom_URL_input.Text = h.maincore.data.custom_URL;
            SmartHost_Beijing_URL_input.Text = h.maincore.data.SmartHost_Beijing_URL;
            SmartHost_US_URL_input.Text = h.maincore.data.SmartHost_US_URL;
            imoutohost_URL_input.Text = h.maincore.data.imouto_host_URL;
            ninehost_URL_input.Text = h.maincore.data.ninehost_URL;
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void About_Click(object sender, EventArgs e)
        {
            about a = new about();
            a.Show();
        }
        public event SaveChangeDelegate SaveChange;
        private void OK_Click(object sender, EventArgs e)
        {
            SaveChange(custom_enable.Checked, custom_URL_input.Text);
            this.Close();
        }

        private void CheckUpdate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                HostChange h = new HostChange();
                WebClient GetHost = new WebClient();
                Byte[] pageData = GetHost.DownloadData(h.maincore.data.hostchange_version_URL);
                string a = Encoding.Default.GetString(pageData);
                string b = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
                if (!(a.Equals(b) || a.Equals("")))
                {
                    Update up = new Update(a);
                    up.Show();
                }
                else
                {
                    CheckUpdate.Text = "Up To Date! ლ(╹◡→ლ)﻿";
                }
            }
            catch (WebException) { MessageBox.Show("Check Update Failed.Please Check your Network Or go to https://github.com/Catofes/HostChange for help"); }
        }
    }
}
