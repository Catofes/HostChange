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
        MainCore maincore;
        public HostChange()
        {
            maincore = new MainCore();
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Update_Click(object sender, EventArgs e)
        {
            label.Text = "Waite.";
            maincore.GetHostfromSmarthost();
            label.Text = "Update OK.";
        }

        private void Beijing_Click(object sender, EventArgs e)
        {
            maincore.SaveLocalHost(maincore.data.SmartHost_Beijing);
            label.Text = "Set OK.";
        }

        private void Reset_Click(object sender, EventArgs e)
        {
            List<string> resetdata= new List<string>();
            resetdata.Add("127.0.0.1    localhost");
            maincore.SaveLocalHost(resetdata);
            label.Text = "Set OK.";
        }

        private void US_Click(object sender, EventArgs e)
        {
            maincore.SaveLocalHost(maincore.data.SmartHost_US);
            label.Text = "Set OK.";
        }
    }
    [Serializable]
    class Data
    {
        public List<string> SmartHost_Beijing;//SmartHost which Service located in beijing
        public List<string> SmartHost_US;//SmartHost which Service located in Others
        public List<string> Other;//Didn't used now
        public Data()
        {
            SmartHost_Beijing = new List<string>();
            SmartHost_US = new List<string>();
            Other = new List<string>();
        }
    }
    class MainCore
    {
        public Data data;
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
            datafile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\HostChange\\HostChange.data";
            LoadLocalData();
        }
        public void LoadLocalHost()//Load the local host file
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
        public void GetHostfromSmarthost()
        {
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
            }
            catch (WebException)
            {
                MessageBox.Show("Download Error");
            }
            SaveLocalData();
        }//Download Host Data from SmartHost
        public void LoadLocalData()
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
