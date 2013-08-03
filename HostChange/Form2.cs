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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            HostChange h = new HostChange(); 
            showlable.Text = " Version : 1.1.0 \n Data File Version : 1" + "\n Beijing Update Time: " + h.maincore.data.SmartHost_Beijing[0].Substring(8) + "\n US Update Time: " + h.maincore.data.SmartHost_US[0].Substring(8) + "\n Imouto Update Time: " + h.maincore.data.imouto_host[5].Substring(18);
        }
    }
}
