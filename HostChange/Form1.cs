using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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

        private void showversion_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }

        private void ninehost_Click(object sender, EventArgs e)
        {
            maincore.SaveLocalHost(maincore.data.ninehost);
            label.Text = "9host Set OK.";
            Version.Text = "9host Update Time: " + maincore.data.ninehost[0].Substring(8);
        }
    }
    [Serializable]
    public class Data
    {
        public List<string> SmartHost_Beijing;//SmartHost which Service located in beijing
        public List<string> SmartHost_US;//SmartHost which Service located in Others
        public List<string> imouto_host;//Imouto.host
        public List<string> ninehost;//9host
        public List<string> backup_1;//no use now
        public List<string> backup_2;//no use now
        public int data_version;
        public Data()//初始化
        {
            SmartHost_Beijing = new List<string>();
            SmartHost_US = new List<string>();
            imouto_host = new List<string>();
            ninehost = new List<string>();
            data_version = 2;
        }
        public bool Reset()//重置
        {
            SmartHost_Beijing = new List<string>();
            SmartHost_US = new List<string>();
            imouto_host = new List<string>();
            ninehost = new List<string>();
            data_version = 2;
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
            data_version = 2;
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
        public void GetHost()
        {
            if (data.data_version != data_version)
            {
                data.Reset();
            }
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
        private void SaveLocalData()
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
