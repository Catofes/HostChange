using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;


namespace HostChange
{
    public partial class HostChange : Form
    {
        public MainCore maincore;
        public HostChange()
        {
            maincore = new MainCore();
            InitializeComponent();
            if (maincore.data.SmartHost_Beijing.Count() != 0)
            {
                Version.Text = maincore.data.SmartHost_Beijing[0];
            }
            else
            {
                Version.Text = "No Data , Please Update";
            }
            if (!maincore.data.custom_enable)
            {
                Custom.Enabled = false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Update_Click(object sender, EventArgs e)
        {
            label.Text = "Waite.";
            maincore.GetHost();
            label.Text = "Update OK.";
            Version.Text = "Beijing Update Time: " + maincore.data.SmartHost_Beijing[0].Substring(8);
        }

        private void Beijing_Click(object sender, EventArgs e)
        {

            maincore.SaveLocalHost(maincore.data.SmartHost_Beijing);
            label.Text = "Beijing Set OK.";
            Version.Text = "Beijing Update Time: " + maincore.data.SmartHost_Beijing[0].Substring(8);
        }

        private void Reset_Click(object sender, EventArgs e)
        {
            List<string> resetdata= new List<string>();
            resetdata.Add("127.0.0.1    localhost");
            maincore.SaveLocalHost(resetdata);
            label.Text = "Reset OK.";
            Version.Text = "Beijing Update Time: " + maincore.data.SmartHost_Beijing[0].Substring(8);
        }

        private void US_Click(object sender, EventArgs e)
        {
            maincore.SaveLocalHost(maincore.data.SmartHost_US);
            label.Text = "US Set OK.";
            Version.Text = "US Update Time: " + maincore.data.SmartHost_US[0].Substring(8);
        }

        private void Imouto_Click(object sender, EventArgs e)
        {
            maincore.SaveLocalHost(maincore.data.imouto_host);
            label.Text = "Imouto Set OK.";
            Version.Text = "Imouto Update Time: " + maincore.data.imouto_host[5].Substring(18);
        }

        private void settings_Click(object sender, EventArgs e)
        {
            settings set = new settings();
            set.SaveChange += set_SaveChange;
            set.Show();
        }

        void set_SaveChange(bool if_custom_enable, string custom_URL_in)
        {
            maincore.data.custom_enable = if_custom_enable;
            maincore.data.custom_URL = custom_URL_in;
            Custom.Enabled = if_custom_enable;
            maincore.SaveLocalData();
            //throw new NotImplementedException();
        }

        private void ninehost_Click(object sender, EventArgs e)
        {
            maincore.SaveLocalHost(maincore.data.ninehost);
            label.Text = "9host Set OK.";
            Version.Text = "9host Update Time: " + maincore.data.ninehost[0].Substring(8);
        }

        private void Custom_Click(object sender, EventArgs e)
        {
            maincore.SaveLocalHost(maincore.data.custom);
            label.Text = "Custom Set OK.";
            Version.Text = "Custom Update Time: " + maincore.data.custom_updatedate;
        }
    }
    [Serializable]
    public class Data
    {
        public string SmartHost_Beijing_URL;
        public string SmartHost_US_URL;
        public string imouto_host_URL;
        public string ninehost_URL;
        public string custom_URL;
        public string hostchange_version_URL;
        public bool SmartHost_Beijing_OK;
        public bool SmartHost_US_OK;
        public bool imouto_host_OK;
        public bool ninehost_OK;
        public bool custom_OK;
        public bool custom_enable;
        public string custom_updatedate;
        public List<string> SmartHost_Beijing;//SmartHost which Service located in beijing
        public List<string> SmartHost_US;//SmartHost which Service located in Others
        public List<string> imouto_host;//Imouto.host
        public List<string> ninehost;//9host
        public List<string> custom;//custom
        public List<string> backup_2;//no use now
        public int data_version =3;
        public Data()//初始化
        {
            Reset();
        }
        public bool Reset()//重置
        {
            SmartHost_Beijing = new List<string>();
            SmartHost_US = new List<string>();
            imouto_host = new List<string>();
            ninehost = new List<string>();
            custom = new List<string>();
            data_version = 3;
            custom_URL = "";
            custom_enable = false;
            custom_updatedate = "";
            SmartHost_Beijing_URL = "https://smarthosts.googlecode.com/svn/trunk/hosts";
            SmartHost_US_URL = "https://smarthosts.googlecode.com/svn/trunk/hosts_us";
            imouto_host_URL = "https://imoutohost.googlecode.com/git/imouto.host.txt";
            ninehost_URL = "http://moe9.tk/Xction/9Hosts/Static/Win";
            hostchange_version_URL = "https://raw.github.com/Catofes/HostChange/master/version";
            SmartHost_Beijing_OK = false;
            SmartHost_US_OK = false;
            imouto_host_OK = false;
            ninehost_OK = false;
            custom_OK = false;
            return true;
        }
    }
    public class MainCore//核心~
    {
        public Data data;
        private int data_version;
        private List<string> Host;
        private string datafile;
        public MainCore()
        {
            Core();
        }
        public void Core()
        {
            data = new Data();
            Host = new List<string>();
            data_version = 3;
            datafile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\HostChange\\HostChange.data";
            LoadLocalData();
        }//初始化
        public void LoadLocalHost()//Load the local host file, no use now
        {
            Host.Clear();//Clean the Host List 
            try
            {
                StreamReader loadfile = new StreamReader("C:\\Windows\\System32\\Drivers\\etc\\hosts");//Open the local host file using StreamReader
                string sLine = "";
                try
                {
                    while (sLine != null)
                    {
                        sLine = loadfile.ReadLine();
                        if (sLine != null)
                        {
                            Host.Add(sLine);//Add file content into List Host
                        }
                    }
                }
                catch (OutOfMemoryException)
                {
                    MessageBox.Show("Out of Memory.");
                }
                catch (IOException)
                {
                    MessageBox.Show("I/O Error");
                }
                loadfile.Close();//Close the file
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Host file didn't exit.Please check your system file or create one manually.");
            }
        }
        public void SaveLocalHost(List<string> Data)//Write Host Data into local host file;
        {
            try
            {
                StreamWriter writefile = new StreamWriter("C:\\Windows\\System32\\Drivers\\etc\\hosts",false);//Open the local host file using StreamWriter to write the host data.
                try
                {
                    for (int i = 0; i < Data.Count(); i++)
                    {
                        writefile.WriteLine(Data[i]);
                    }
                    writefile.Flush();
                }
                catch (IOException)
                {
                    MessageBox.Show("I/O Error");
                }
                writefile.Close();
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("File Access is Denied. Check if you are runing program by Administrator");
            }
        }
        public bool GHT_SB()//Get the Smarthost_Beijing host using thread
        {
            try
            {
                WebClient GetHost = new WebClient();
                Byte[] pageData = GetHost.DownloadData(data.SmartHost_Beijing_URL);
                string pageHtml = Encoding.Default.GetString(pageData);
                data.SmartHost_Beijing.Clear();
                data.SmartHost_Beijing = new List<string>(pageHtml.Split('\n'));
            }
            catch (WebException) { return false; }
            return true;
        }
        public bool GHT_SU()//Get the Smarthost_US host using thread
        {
            try
            {
                WebClient GetHost = new WebClient();
                Byte[] pageData = GetHost.DownloadData(data.SmartHost_US_URL);
                string pageHtml = Encoding.Default.GetString(pageData);
                data.SmartHost_US.Clear();
                data.SmartHost_US = new List<string>(pageHtml.Split('\n'));
            }
            catch (WebException) { return false; }
            return true;
        }
        public bool GHT_I()//Get the imouto host using thread
        {
            try
            {
                WebClient GetHost = new WebClient();
                Byte[] pageData = GetHost.DownloadData(data.imouto_host_URL);
                string pageHtml = Encoding.Default.GetString(pageData);
                data.imouto_host.Clear();
                data.imouto_host = new List<string>(pageHtml.Split('\n'));
            }
            catch (WebException) { return false; }
            return true;
        }
        public bool GHT_9()//Get the 9 host using thread
        {
            try
            {
                WebClient GetHost = new WebClient();
                Byte[] pageData = GetHost.DownloadData(data.ninehost_URL);
                string pageHtml = Encoding.Default.GetString(pageData);
                data.ninehost.Clear();
                data.ninehost = new List<string>(pageHtml.Split('\n'));
            }
            catch (WebException) { return false; }
            return true;
        }
        public bool GHT_C()//Get the custom host using thread
        {
            if (!data.custom_enable) {return true; }
            try
            {
                WebClient GetHost = new WebClient();
                Byte[] pageData = GetHost.DownloadData(data.custom_URL);
                string pageHtml = Encoding.Default.GetString(pageData);
                data.custom.Clear();
                data.custom = new List<string>(pageHtml.Split('\n'));
                data.custom_updatedate = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
            }
            catch (WebException) { return false; }
            return true;
        }
        public delegate bool TakesAWhileDelegate();
        public delegate string gethostchangeDelegate();
        public void GetHost()
        {
            if (data.data_version != data_version)data.Reset();
            TakesAWhileDelegate dl_SB = GHT_SB;
            IAsyncResult ar_SB = dl_SB.BeginInvoke(null,null);
            TakesAWhileDelegate dl_SU = GHT_SU;
            IAsyncResult ar_SU = dl_SU.BeginInvoke(null, null);
            TakesAWhileDelegate dl_I = GHT_I;
            IAsyncResult ar_I = dl_I.BeginInvoke(null, null);
            TakesAWhileDelegate dl_9 = GHT_9;
            IAsyncResult ar_9 = dl_9.BeginInvoke(null, null);
            TakesAWhileDelegate dl_C = GHT_C;
            IAsyncResult ar_C = dl_C.BeginInvoke(null, null);
            while (!(ar_SB.IsCompleted && ar_SU.IsCompleted && ar_I.IsCompleted && ar_9.IsCompleted && ar_C.IsCompleted))
                Thread.Sleep(100);
            data.SmartHost_Beijing_OK = dl_SB.EndInvoke(ar_SB);
            data.SmartHost_US_OK = dl_SU.EndInvoke(ar_SU);
            data.imouto_host_OK = dl_I.EndInvoke(ar_I);
            data.ninehost_OK = dl_9.EndInvoke(ar_9);
            data.custom_OK = dl_C.EndInvoke(ar_C);
            /* * update using thread.
            try
            {
                WebClient GetHost = new WebClient();
                GetHost.Credentials = CredentialCache.DefaultCredentials;
                Byte[] pageData = GetHost.DownloadData("https://smarthosts.googlecode.com/svn/trunk/hosts");
                string pageHtml = Encoding.Default.GetString(pageData);
                data.SmartHost_Beijing.Clear();
                data.SmartHost_Beijing = new List<string>(pageHtml.Split('\n'));
                Byte[] PageData2 = GetHost.DownloadData("https://smarthosts.googlecode.com/svn/trunk/hosts_us");
                string pageHtml2 = Encoding.Default.GetString(PageData2);
                data.SmartHost_US.Clear();
                data.SmartHost_US = new List<string>(pageHtml2.Split('\n'));
                Byte[] PageData3 = GetHost.DownloadData("https://imoutohost.googlecode.com/git/imouto.host.txt");
                string pageHtml3 = Encoding.Default.GetString(PageData3);
                data.imouto_host.Clear();
                data.imouto_host = new List<string>(pageHtml3.Split('\n'));
                Byte[] PageData4 = GetHost.DownloadData("http://moe9.tk/Xction/9Hosts/Static/Win");
                string pageHtml4 = Encoding.Default.GetString(PageData4);
                data.ninehost.Clear();
                data.ninehost = new List<string>(pageHtml4.Split('\n'));
            }
            catch (WebException)
            {
                MessageBox.Show("Download Error,Check your network or Update the Software.");
            }
                 * */
            string downloaderror = "";
            if (!data.SmartHost_Beijing_OK)
            {
                downloaderror += "Smarthost_Beijing,";
            }
            if (!data.SmartHost_US_OK)
            {
                downloaderror += "Smarthost_US,";
            }
            if (!data.imouto_host_OK)
            {
                downloaderror += "Imouto host,";
            }
            if (!data.ninehost_OK)
            {
                downloaderror += "9host,";
            }
            if (!data.custom_OK)
            {
                downloaderror += "Custom host,";
            }
            if (!downloaderror.Equals("")) MessageBox.Show(downloaderror + "Download Error. Some Host didn't Update. Check your Network or Update the Software.");
            SaveLocalData();
        }//Download Host Data from SmartHost
        public void LoadLocalData()//Load data from local file 
        {
            try
            {
                FileStream fileread = new FileStream(datafile, FileMode.Open, FileAccess.Read);
                BinaryFormatter bf = new BinaryFormatter();
                data = (Data)bf.Deserialize(fileread);
                fileread.Close();
            }
            catch (FileNotFoundException)
            {
            }
            catch (DirectoryNotFoundException)
            {
            }
            catch (SerializationException)
            {
                MessageBox.Show("Please delete all file in %AppData%\\HostChange\\ to reset the local data");
            }
            if (data.data_version != data_version)
            {
                MessageBox.Show("date_file version error. Please Update the host file");
            }

        }//Load
        public void SaveLocalData()
        {
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\HostChange"))
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\HostChange");
            }
            try
            {
                FileStream filewrite = new FileStream(datafile, FileMode.Create, FileAccess.Write);
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(filewrite, data);
                filewrite.Flush();
                filewrite.Close();
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("File Access is Denied. Check if you are runing program by Administrator");
            }
            catch (IOException)
            {
                MessageBox.Show("Can't Save file");
            }
        } //Save
    }    
}
